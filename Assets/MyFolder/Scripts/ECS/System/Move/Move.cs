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
        public NativeArray<Position2D> Position;
        [ReadOnly] public NativeArray<Speed2D> Speed;
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
        [ReadOnly] public NativeArray<Position2D> Position;
        public NativeArray<Speed2D> Speed;
        public int4x2 CellCountAdjustment;
        public int4x2 CellWidthCount;
        public int4 MaxCellCountInclusive;
        public float4x2 RcpCellSize;

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
                    var xIndex = (int4x2)(position.X * RcpCellSize) + CellCountAdjustment;
                    var yIndex = (int4x2)(position.Y * RcpCellSize) + CellCountAdjustment;
                    var cellIndex = yIndex * CellWidthCount + xIndex;
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
            cellIndex = math.clamp(cellIndex, int4.zero, MaxCellCountInclusive);
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
        public NativeArray<Destination2D> Destination;
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
                Destination[index] = new Destination2D
                {
                    X = TargetX,
                    Y = TargetY
                };
        }
    }

    [BurstCompile]
    public struct RandomlyChangeDestinationJob : IJob
    {
        public NativeArray<Destination2D> Destination;
        public NativeArray<Random> Random;
        public float MinInclusive;
        public float MaxExclusive;

        public void Execute()
        {
            var random = Random[0];
            for (var index = 0; index < Destination.Length; index++)
                Destination[index] = new Destination2D
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
        [ReadOnly] public NativeArray<Position2D> Position;
        [ReadOnly] public NativeArray<IsAlive> IsAlive;
        public float MinInclusive;
        public float MaxExclusive;
        
        public void Execute()
        {
            for (var index = 0; index < Position.Length; index++)
            {
                var position = Position[index];
                var isAlive = IsAlive[index];
                isAlive.Value &= position.X >= MinInclusive & position.Y >= MinInclusive & position.X < MaxExclusive & position.Y < MaxExclusive;
                IsAlive[index] = isAlive;
            }
        }
    }
}
