using ComponentTypes;
using MyFolder.Scripts.ECS.Types;
using Unity.Burst;
using Unity.Burst.Intrinsics;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

namespace Unity1Week
{
    [BurstCompile]
    public struct MultiMoveJob : IJob
    {
        public NativeArray<Position2D.Eight> Position;
        [ReadOnly] public NativeArray<Speed2D.Eight> Speed;
        public float DeltaTime;

        public unsafe void Execute()
        {
            if (X86.Fma.IsFmaSupported)
            {
                var positionReinterpret = Position.Reinterpret<v256>(sizeof(v256));
                var speedReinterpret = Speed.Reinterpret<v256>(sizeof(v256));
                var deltaTime = new v256(DeltaTime, DeltaTime, DeltaTime, DeltaTime, DeltaTime, DeltaTime, DeltaTime, DeltaTime);
                for (var index = 0; index < positionReinterpret.Length; index++) positionReinterpret[index] = X86.Fma.mm256_fmadd_ps(deltaTime, speedReinterpret[index], positionReinterpret[index]);

                return;
            }

            for (var index = 0; index < Position.Length; index++)
            {
                var position = Position[index];
                position.X += DeltaTime * Speed[index].X;
                position.Y += DeltaTime * Speed[index].Y;
                Position[index] = position;
            }
        }
    }

    [BurstCompile]
    public struct CalculateMoveSpeedJob : IJob
    {
        [ReadOnly] public NativeArray<float> SpeedSettings;
        [ReadOnly] public NativeArray<int> ChipKind;
        [ReadOnly] public NativeArray<Position2D.Eight> Position;
        public NativeArray<Speed2D.Eight> Speed;
        private int4x2 cellCountAdjustment;
        private int4x2 cellWidthCount;
        private int4 maxCellCountInclusive;
        private float4x2 rcpCellSize;

        public void Execute()
        {
            for (var index = 0; index < Position.Length; index++)
            {
                var speed = Speed[index];
                {
                    // speed = Normalize(speed);
                    var speedSquare = speed.X * speed.X + speed.Y + speed.Y;
                    var speedOldLengthRcp = new float4x2(math.rcp(math.sqrt(speedSquare.c0)), math.rcp(math.sqrt(speedSquare.c1)));
                    speed.X *= speedOldLengthRcp;
                    speed.Y *= speedOldLengthRcp;
                }

                var position = Position[index];
                {
                    var xIndex = (int4x2)(position.X * rcpCellSize) + cellCountAdjustment;
                    var yIndex = (int4x2)(position.Y * rcpCellSize) + cellCountAdjustment;
                    var cellIndex = yIndex * cellWidthCount + xIndex;
                    var speedSetting = GetSpeed(cellIndex.c0);
                    speed.X.c0 *= speedSetting;
                    speed.Y.c0 *= speedSetting;
                    speedSetting = GetSpeed(cellIndex.c1);
                    speed.X.c1 *= speedSetting;
                    speed.Y.c1 *= speedSetting;
                }

                Speed[index] = speed;
            }
        }

        private float GetSpeed(int cellIndex)
        {
            return SpeedSettings[ChipKind[cellIndex]];
        }

        private float4 GetSpeed(int4 cellIndex)
        {
            cellIndex = math.clamp(cellIndex, int4.zero, maxCellCountInclusive);
            return new float4(
                GetSpeed(cellIndex.x),
                GetSpeed(cellIndex.y),
                GetSpeed(cellIndex.z),
                GetSpeed(cellIndex.w));
        }
    }

    [BurstCompile]
    public struct ChangeDestinationJob : IJob
    {
        public NativeArray<Destination2D.Eight> Destination;
        public float TargetX;
        public float TargetY;

        public unsafe void Execute()
        {
            if (X86.Avx.IsAvxSupported)
            {
                var targetX = new v256(TargetX, TargetX, TargetX, TargetX, TargetX, TargetX, TargetX, TargetX);
                var targetY = new v256(TargetY, TargetY, TargetY, TargetY, TargetY, TargetY, TargetY, TargetY);
                var destinationReinterpret = Destination.Reinterpret<v256>(sizeof(v256));
                for (var index = 0; index < Destination.Length; index++)
                {
                    destinationReinterpret[index << 1] = targetX;
                    destinationReinterpret[(index << 1) + 1] = targetY;
                }
                return;
            }

            for (var index = 0; index < Destination.Length; index++)
                Destination[index] = new Destination2D.Eight
                {
                    X = TargetX,
                    Y = TargetY
                };
        }
    }

    [BurstCompile]
    public struct RandomlyChangeDestinationJob : IJob
    {
        public NativeArray<Destination2D.Eight> Destination;
        public NativeArray<Random> Random;
        public float MinInclusive;
        public float MaxExclusive;

        public void Execute()
        {
            var random = Random[0];
            for (var index = 0; index < Destination.Length; index++)
                Destination[index] = new Destination2D.Eight
                {
                    X = new float4x2(random.NextFloat4(MinInclusive, MaxExclusive), random.NextFloat4(MinInclusive, MaxExclusive)),
                    Y = new float4x2(random.NextFloat4(MinInclusive, MaxExclusive), random.NextFloat4(MinInclusive, MaxExclusive))
                };

            Random[0] = random;
        }
    }

    [BurstCompile]
    public struct KillOutOfBoundsJob : IJob
    {
        [ReadOnly] public NativeArray<Position2D.Eight> Position;
        [ReadOnly] public NativeArray<AliveState.Eight> IsAlive;
        private readonly float4x2 minInclusive;
        private readonly float4x2 maxExclusive;

        public KillOutOfBoundsJob(NativeArray<Position2D.Eight> position, NativeArray<AliveState.Eight> isAlive, float minInclusive, float maxExclusive)
        {
            Position = position;
            IsAlive = isAlive;
            this.minInclusive = minInclusive;
            this.maxExclusive = maxExclusive;
        }

        public unsafe void Execute()
        {
            if (X86.Avx.IsAvxSupported)
            {
                var positionReinterpret = Position.Reinterpret<v256>(sizeof(v256));
                var isAliveReinterpret = IsAlive.Reinterpret<v256>(sizeof(v256));
                var minInclusive256 = new v256(minInclusive.c0.x, minInclusive.c0.x, minInclusive.c0.x, minInclusive.c0.x, minInclusive.c0.x, minInclusive.c0.x, minInclusive.c0.x, minInclusive.c0.x);
                var maxExclusive256 = new v256(maxExclusive.c0.x, maxExclusive.c0.x, maxExclusive.c0.x, maxExclusive.c0.x, maxExclusive.c0.x, maxExclusive.c0.x, maxExclusive.c0.x, maxExclusive.c0.x);
                for (var index = 0; index < isAliveReinterpret.Length; index++)
                {
                    var isOutOfBounds = isAliveReinterpret[index];
                    
                    var position = positionReinterpret[index << 1];
                    isOutOfBounds = X86.Avx.mm256_or_ps(isOutOfBounds, X86.Avx.mm256_cmp_ps(position, minInclusive256, (int)X86.Avx.CMP.LE_OQ));
                    isOutOfBounds = X86.Avx.mm256_and_ps(X86.Avx.mm256_cmp_ps(position, maxExclusive256, (int)X86.Avx.CMP.NLT_UQ), isOutOfBounds);

                    position = positionReinterpret[(index << 1) + 1];
                    isOutOfBounds = X86.Avx.mm256_or_ps(isOutOfBounds, X86.Avx.mm256_cmp_ps(position, minInclusive256, (int)X86.Avx.CMP.LE_OQ));
                    isOutOfBounds = X86.Avx.mm256_and_ps(X86.Avx.mm256_cmp_ps(position, maxExclusive256, (int)X86.Avx.CMP.NLT_UQ), isOutOfBounds);
                    
                    isAliveReinterpret[index] = isOutOfBounds;
                }
                
                return;
            }
            
            var minus1 = new int4(-1, -1, -1, -1);
            for (var index = 0; index < Position.Length; index++)
            {
                var position = Position[index];
                var isAlive = IsAlive[index];
                var isInRange = position.X >= minInclusive & position.Y >= minInclusive & position.X < maxExclusive & position.Y < maxExclusive;
                isAlive.Value.c0 = math.@select(int4.zero, minus1, isInRange.c0);
                isAlive.Value.c1 = math.@select(int4.zero, minus1, isInRange.c1);
                IsAlive[index] = isAlive;
            }
        }
    }
}
