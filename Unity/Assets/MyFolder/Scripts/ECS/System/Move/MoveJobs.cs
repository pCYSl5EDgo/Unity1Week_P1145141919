using System;
using ComponentTypes;
using MyAttribute;
using Unity.Burst;
using Unity.Burst.Intrinsics;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using Random = Unity.Mathematics.Random;

namespace Unity1Week
{
    [SingleLoopType(
        new[] { typeof(Position2D), typeof(Speed2D) }, new[] { false, true }, "",
        new[] { typeof(float) }, new[] { true }, new[] { "DeltaTime" }
    )]
    public static partial class MultiMove
    {
        [MethodIntrinsicsKind(IntrinsicsKind.Ordinal)]
        private static void Exe(
            ref float4 x,
            ref float4 y,
            ref float4 speedX,
            ref float4 speedY,
            ref float4 deltaTime
        )
        {
            x += speedX * deltaTime;
            y += speedY * deltaTime;
        }

        [MethodIntrinsicsKind(IntrinsicsKind.Fma)]
        private static void Exe(
            ref v256 x,
            ref v256 y,
            ref v256 speedX,
            ref v256 speedY,
            ref v256 deltaTime
        )
        {
            if (!X86.Fma.IsFmaSupported) return;

            x = X86.Fma.mm256_fmadd_ps(speedX, deltaTime, x);
            y = X86.Fma.mm256_fmadd_ps(speedX, deltaTime, y);
        }
    }

    [SingleLoopType(
        new[] { typeof(Position2D), typeof(Speed2D) }, new[] { true, false }, "",
        new[] { typeof(float), typeof(int), typeof(int), typeof(int) }, new[] { true, true, true, true }, new[] { "RcpCellSize", "CellWidthCount", "CellCountAdjustment", "MaxCellCountInclusive" },
        new[] { typeof(float), typeof(int) }, new[] { true, true }, new[] { "SpeedSetting", "ChipKind" }
    )]
    public static unsafe partial class CalculateMoveSpeedJob
    {
        [MethodIntrinsicsKind(IntrinsicsKind.Ordinal)]
        private static void Exe(
            ref float4 positionX,
            ref float4 positionY,
            ref float4 speedX,
            ref float4 speedY,
            ref float4 rcpCellSize,
            ref int4 cellWidthCount,
            ref int4 cellCountAdjustment,
            ref int4 maxCellCountInclusive,
            void* speedSettings,
            int speedSettingsLength,
            void* chipKinds,
            int chipKindsLength
        )
        {
            var lengthSquared = speedX * speedX + speedY * speedY;
            var rSqrt = math.rsqrt(lengthSquared);
            var indexX = (int4)(positionX * rcpCellSize) + cellCountAdjustment;
            var indexY = (int4)(positionY * rcpCellSize) + cellCountAdjustment;
            var cellIndex = math.clamp(indexY * cellWidthCount + indexX, int4.zero, maxCellCountInclusive);
            var speedNew = rSqrt * new float4(
                ((float*)speedSettings)[((int*)chipKinds)[cellIndex.x]],
                ((float*)speedSettings)[((int*)chipKinds)[cellIndex.y]],
                ((float*)speedSettings)[((int*)chipKinds)[cellIndex.z]],
                ((float*)speedSettings)[((int*)chipKinds)[cellIndex.w]]
            );
            speedX *= speedNew;
            speedY *= speedNew;
        }

        [MethodIntrinsicsKind(IntrinsicsKind.Fma)]
        private static void Exe2(
            ref v256 positionX,
            ref v256 positionY,
            ref v256 speedX,
            ref v256 speedY,
            ref v256 rcpCellSize,
            ref v256 cellWidthCount,
            ref v256 cellCountAdjustment,
            ref v256 maxCellCountInclusive,
            void* speedSettings,
            int speedSettingsLength,
            void* chipKinds,
            int chipKindsLength
        )
        {
            if (!X86.Fma.IsFmaSupported) return;
            
            var lengthSquared = X86.Fma.mm256_fmadd_ps(speedY, speedY, X86.Avx.mm256_mul_ps(speedX, speedX));
            var rSqrt = X86.Avx.mm256_rsqrt_ps(lengthSquared);
            var indexX = X86.Avx2.mm256_add_epi32(X86.Avx.mm256_cvtps_epi32(X86.Avx.mm256_mul_ps(positionX, rcpCellSize)), cellCountAdjustment);
            var indexY = X86.Avx2.mm256_add_epi32(X86.Avx.mm256_cvtps_epi32(X86.Avx.mm256_mul_ps(positionY, rcpCellSize)), cellCountAdjustment);
            var cellIndex = X86.Avx2.mm256_min_epi32(X86.Avx2.mm256_max_epi32(X86.Avx2.mm256_add_epi32(indexX, X86.Avx2.mm256_mul_epi32(indexY, cellWidthCount)), default), maxCellCountInclusive);
            var newSpeed = X86.Avx.mm256_mul_ps(rSqrt, new v256(
                ((float*)speedSettings)[((int*)chipKinds)[cellIndex.SInt0]],
                ((float*)speedSettings)[((int*)chipKinds)[cellIndex.SInt1]],
                ((float*)speedSettings)[((int*)chipKinds)[cellIndex.SInt2]],
                ((float*)speedSettings)[((int*)chipKinds)[cellIndex.SInt3]],
                ((float*)speedSettings)[((int*)chipKinds)[cellIndex.SInt4]],
                ((float*)speedSettings)[((int*)chipKinds)[cellIndex.SInt5]],
                ((float*)speedSettings)[((int*)chipKinds)[cellIndex.SInt6]],
                ((float*)speedSettings)[((int*)chipKinds)[cellIndex.SInt7]]
            ));
            speedX = X86.Avx.mm256_mul_ps(speedX, newSpeed);
            speedY = X86.Avx.mm256_mul_ps(speedY, newSpeed);
        }
    }
    
    [SingleLoopType(
        new[]{ typeof(Destination2D)}, new[] { false }, "",
        new[] { typeof(float), typeof(float) }, new[] { true, true }, new[] { "TargetX", "TargetY" }
    )]
    public static partial class ChangeDestination
    {
        [MethodIntrinsicsKind(IntrinsicsKind.Ordinal)]
        private static void Exe(
            ref float4 x,
            ref float4 y,
            ref float4 targetX,
            ref float4 targetY
        )
        {
            x = targetX;
            y = targetY;
        }
        
        [MethodIntrinsicsKind(IntrinsicsKind.Fma)]
        private static void Exe2(
            ref v256 x,
            ref v256 y,
            ref v256 targetX,
            ref v256 targetY
        )
        {
            x = targetX;
            y = targetY;
        }
    }
    
    [SingleLoopType(
        new[] { typeof(Destination2D)}, new[]{false}, "",
        new[]{typeof(Random), typeof(float), typeof(float)}, new[]{ false, true, true}, new[]{ "RandomArray", "TwoMinInclusiveMinusMaxExclusive", "MaxExclusiveMinusMinInclusive" }
    )]
    public static partial class RandomlyChangeDestination
    {
        private const int ConstMask = 0x3f800000;

        [MethodIntrinsicsKind(IntrinsicsKind.Ordinal)]
        private static void Exe(
            ref float4 destinationX,    
            ref float4 destinationY,
            ref Random random,
            ref float4 twoMinInclusiveMinusMaxExclusive,
            ref float4 maxExclusiveMinusMinInclusive
        )
        {
            var state = random.state;
            uint Next()
            {
                state ^= state << 13;
                state ^= state >> 17;
                state ^= state << 5;
                return state;
            }

            destinationX = math.asfloat((new uint4(Next(), Next(), Next(), Next()) >> 9) | ConstMask) * maxExclusiveMinusMinInclusive + twoMinInclusiveMinusMaxExclusive;
            destinationY = math.asfloat((new uint4(Next(), Next(), Next(), Next()) >> 9) | ConstMask) * maxExclusiveMinusMinInclusive + twoMinInclusiveMinusMaxExclusive;
            
            random.state = state;
        }
        
        [MethodIntrinsicsKind(IntrinsicsKind.Fma)]
        private static void Exe2(
            ref v256 destinationX,
            ref v256 destinationY,
            ref Random random,
            ref v256 twoMinInclusiveMinusMaxExclusive,
            ref v256 maxExclusiveMinusMinInclusive
        )
        {
            if (!X86.Fma.IsFmaSupported) return;

            var state = random.state;
            uint Next()
            {
                state ^= state << 13;
                state ^= state >> 17;
                state ^= state << 5;
                return state;
            }

            var constantMask = new v256(ConstMask, ConstMask, ConstMask, ConstMask, ConstMask, ConstMask, ConstMask, ConstMask);
            
            destinationX = new v256(Next(), Next(), Next(), Next(), Next(), Next(), Next(), Next());
            destinationY = new v256(Next(), Next(), Next(), Next(), Next(), Next(), Next(), Next());
            destinationX = X86.Avx2.mm256_srli_epi32(destinationX, 9);
            destinationX = X86.Avx.mm256_or_ps(destinationX, constantMask);
            destinationX = X86.Fma.mm256_fmadd_ps(destinationX, maxExclusiveMinusMinInclusive, twoMinInclusiveMinusMaxExclusive);
            
            destinationY = X86.Avx2.mm256_srli_epi32(destinationY, 9);
            destinationY = X86.Avx.mm256_or_ps(destinationY, constantMask);
            destinationY = X86.Fma.mm256_fmadd_ps(destinationY, maxExclusiveMinusMinInclusive, twoMinInclusiveMinusMaxExclusive);
            
            random.state = state;
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
