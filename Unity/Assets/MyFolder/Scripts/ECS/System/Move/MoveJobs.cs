using ComponentTypes;
using MyAttribute;
using Unity.Burst.Intrinsics;
using Unity.Mathematics;

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
        private static void Exe2(
            ref v256 x,
            ref v256 y,
            ref v256 speedX,
            ref v256 speedY,
            ref v256 deltaTime
        )
        {
            if (!X86.Fma.IsFmaSupported) return;

            x = X86.Fma.mm256_fmadd_ps(speedX, deltaTime, x);
            y = X86.Fma.mm256_fmadd_ps(speedY, deltaTime, y);
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
        new[] { typeof(Destination2D) }, new[] { false }, "",
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
        new[] { typeof(Destination2D) }, new[] { false }, "",
        new[] { typeof(Random), typeof(float), typeof(float) }, new[] { false, true, true }, new[] { "RandomArray", "TwoMinInclusiveMinusMaxExclusive", "MaxExclusiveMinusMinInclusive" }
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

    [SingleLoopType(
        new[] { typeof(Position2D), typeof(AliveState) }, new[] { true, false }, "",
        new[] { typeof(float), typeof(float) }, new[] { true, true }, new[] { "MinInclusive", "MaxExclusive" }
    )]
    public static partial class KillOutOfBounds
    {
        [MethodIntrinsicsKind(IntrinsicsKind.Ordinal)]
        private static void Exe(
            ref float4 x,
            ref float4 y,
            ref int4 alive,
            ref float4 minInclusive,
            ref float4 maxExclusive
        )
        {
            var outOfRange = math.min(x, y) < minInclusive | math.max(x, y) >= maxExclusive;
            alive = math.select(alive, -1, outOfRange);
        }

        [MethodIntrinsicsKind(IntrinsicsKind.Fma)]
        private static void Exe2(
            ref v256 x,
            ref v256 y,
            ref v256 alive,
            ref v256 minInclusive,
            ref v256 maxExclusive
        )
        {
            if (!X86.Fma.IsFmaSupported) return;
            var outOfRange = X86.Avx.mm256_or_ps(
                X86.Avx.mm256_or_ps(
                    X86.Avx.mm256_cmp_ps(minInclusive, x, (int)X86.Avx.CMP.GT_OQ), 
                    X86.Avx.mm256_cmp_ps(minInclusive, y, (int)X86.Avx.CMP.GT_OQ)),
                X86.Avx.mm256_or_ps(
                    X86.Avx.mm256_cmp_ps(x, maxExclusive, (int)X86.Avx.CMP.GE_OQ), 
                    X86.Avx.mm256_cmp_ps(y, maxExclusive, (int)X86.Avx.CMP.GE_OQ)));
            alive = X86.Avx.mm256_or_ps(alive, outOfRange);
        }
    }
    
    [SingleLoopType(
        new[] { typeof(Position2D), typeof(Speed2D), typeof(AliveState) }, new[] { false, false, false }, "",
        new[] { typeof(float), typeof(float), typeof(float) }, new[] { true, true, true }, new[] { "TwoMinInclusive_Minus_MaxExclusive", "MaxExclusive_Minus_MinInclusive", "MaxSpeed" },
        new[] { typeof(v256) }, new[] { false }, new[] { "Random" }
    )]
    public static partial class RandomSpawn
    {
        [MethodIntrinsicsKind(IntrinsicsKind.Ordinal)]
        private static unsafe void Exe(
            ref float4 positionX,
            ref float4 positionY,
            ref float4 speedX,
            ref float4 speedY,
            ref int4 alive,
            ref float4 twoMinInclusive_Minus_MaxExclusive,
            ref float4 maxExclusive_Minus_MinInclusive,
            ref float4 speedMax,
            void* randomPointer,
            int randomPointerLength
        )
        {
            var state = *(uint4*)randomPointer;
            var constant = (uint4)0x3f800000U;
            var constant2 = speedMax * 2;
            var constant3 = speedMax * -3;
            
            state ^= state << 13;
            state ^= state >> 17;
            state ^= state << 5;
            positionX = math.mad(math.asfloat((state >> 9) | constant), maxExclusive_Minus_MinInclusive, twoMinInclusive_Minus_MaxExclusive);
            
            state ^= state << 13;
            state ^= state >> 17;
            state ^= state << 5;
            positionY = math.mad(math.asfloat((state >> 9) | constant), maxExclusive_Minus_MinInclusive, twoMinInclusive_Minus_MaxExclusive);
            
            state ^= state << 13;
            state ^= state >> 17;
            state ^= state << 5;
            speedX = math.mad(math.asfloat((state >> 9) | constant), constant2, constant3);
                                                                           
            state ^= state << 13;                                          
            state ^= state >> 17;                                          
            state ^= state << 5;                                           
            speedY = math.mad(math.asfloat((state >> 9) | constant), constant2, constant3);

            *(uint4*)randomPointer = state;
            
            alive = 0;
        }
        
        [MethodIntrinsicsKind(IntrinsicsKind.Fma)]
        private static unsafe void Exe2(
            ref v256 positionX,
            ref v256 positionY,
            ref v256 speedX,
            ref v256 speedY,
            ref v256 alive,
            ref v256 twoMinInclusive_Minus_MaxExclusive,
            ref v256 maxExclusive_Minus_MinInclusive,
            ref v256 speedMax,
            void* randomPointer,
            int randomPointerLength
        )
        {
            if (!X86.Fma.IsFmaSupported) return;
            
            var state = *(v256*)randomPointer;
            var constant = new v256(0x3f800000U, 0x3f800000U, 0x3f800000U, 0x3f800000U, 0x3f800000U, 0x3f800000U, 0x3f800000U, 0x3f800000U);
            var constant2 = X86.Avx.mm256_mul_ps(speedMax, new v256(2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f));
            var constant3 = X86.Avx.mm256_mul_ps(speedMax, new v256(3f, 3f, 3f, 3f, 3f, 3f, 3f, 3f));
            
            state = X86.Avx2.mm256_xor_si256(state, X86.Avx2.mm256_slli_epi32(state, 13));
            state = X86.Avx2.mm256_xor_si256(state, X86.Avx2.mm256_srli_epi32(state, 17));
            state = X86.Avx2.mm256_xor_si256(state, X86.Avx2.mm256_slli_epi32(state, 5));
            positionX = X86.Fma.mm256_fmadd_ps(X86.Avx2.mm256_or_si256(X86.Avx2.mm256_srli_epi32(state, 9), constant), maxExclusive_Minus_MinInclusive, twoMinInclusive_Minus_MaxExclusive);
            
            state = X86.Avx2.mm256_xor_si256(state, X86.Avx2.mm256_slli_epi32(state, 13));
            state = X86.Avx2.mm256_xor_si256(state, X86.Avx2.mm256_srli_epi32(state, 17));
            state = X86.Avx2.mm256_xor_si256(state, X86.Avx2.mm256_slli_epi32(state, 5));
            positionY = X86.Fma.mm256_fmadd_ps(X86.Avx2.mm256_or_si256(X86.Avx2.mm256_srli_epi32(state, 9), constant), maxExclusive_Minus_MinInclusive, twoMinInclusive_Minus_MaxExclusive);
            
            state = X86.Avx2.mm256_xor_si256(state, X86.Avx2.mm256_slli_epi32(state, 13));
            state = X86.Avx2.mm256_xor_si256(state, X86.Avx2.mm256_srli_epi32(state, 17));
            state = X86.Avx2.mm256_xor_si256(state, X86.Avx2.mm256_slli_epi32(state, 5));
            speedX = X86.Fma.mm256_fmsub_ps(X86.Avx2.mm256_or_si256(X86.Avx2.mm256_srli_epi32(state, 9), constant), constant2, constant3);
            
            state = X86.Avx2.mm256_xor_si256(state, X86.Avx2.mm256_slli_epi32(state, 13));
            state = X86.Avx2.mm256_xor_si256(state, X86.Avx2.mm256_srli_epi32(state, 17));
            state = X86.Avx2.mm256_xor_si256(state, X86.Avx2.mm256_slli_epi32(state, 5));
            speedY = X86.Fma.mm256_fmsub_ps(X86.Avx2.mm256_or_si256(X86.Avx2.mm256_srli_epi32(state, 9), constant), constant2, constant3);

            *(v256*)randomPointer = state;
            
            alive = default;
        }
    }
}
