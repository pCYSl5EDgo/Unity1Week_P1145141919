using System;
using System.Runtime.CompilerServices;
using MyAttribute;
using Unity.Collections;
using Unity.Mathematics;
using Unity.Burst.Intrinsics;
using ComponentTypes;

namespace System.Runtime.CompilerServices
{
    [System.AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Constructor | System.AttributeTargets.Event | System.AttributeTargets.Interface | System.AttributeTargets.Method | System.AttributeTargets.Module | System.AttributeTargets.Property | System.AttributeTargets.Struct, Inherited=false)]
    public sealed class SkipLocalsInitAttribute : Attribute
    {
    }
}

namespace Unity1Week.Hit
{
    [SingleLoopType(
        new[] { typeof(Position2D), typeof(AliveState) }, new[] { true, false },
        new[] { typeof(float), typeof(float) }, new[] { true, true }
    )]
    public static partial class BulletArea
    {
        [SingleLoopMethod(IntrinsicsKind.Ordinal, 3), SkipLocalsInit]
        public static void Exe(
            [LoopParameter(0, nameof(Position2D.X))] ref float4 bulletX,
            [LoopParameter(0, nameof(Position2D.Y))] ref float4 bulletY,
            [LoopParameter(1, nameof(AliveState.Value))] ref int4 bulletAliveState,
            [LoopParameter(0)] ref float4 maxX,
            [LoopParameter(1)] ref float4 maxY
        )
        {
            bulletAliveState = math.select(bulletAliveState, -1, bulletX >= maxX);
            bulletAliveState = math.select(bulletAliveState, -1, bulletY >= maxY);
        }
    }

    [CollisionType(
        new[] { typeof(Position2D), typeof(AliveState) }, new[] { true, false },
        new[] { typeof(Position2D), typeof(AliveState) }, new[] { true, false },
        new[] { typeof(float), typeof(float), typeof(int) }, new[] { true, false, false },
        new[] { typeof(Position2D.Eight), typeof(FireStartTime.Eight) }, new[] { false, false }
    )]
    public unsafe static partial class BulletEnemyHit
    {
        [CollisionMethod(IntrinsicsKind.Ordinal, 3, 3, 3), SkipLocalsInit]
        public static void Exe(
            [LoopParameter(0, nameof(Position2D.X))] ref float4 bulletX,
            [LoopParameter(0, nameof(Position2D.Y))] ref float4 bulletY,
            [LoopParameter(1, nameof(AliveState.Value))] ref int4 bulletAliveState,
            [LoopParameter(0, nameof(Position2D.X))] ref float4 enemyX,
            [LoopParameter(0, nameof(Position2D.Y))] ref float4 enemyY,
            [LoopParameter(1, nameof(AliveState.Value))] ref int4 enemyAliveState,
            [LoopParameter(0)] ref float4 collisionRadiusSquare,
            [LoopParameter(1)] ref float currentTime,
            [LoopParameter(2)] ref int fireCount,
            [LoopParameter(0)] ref NativeArray<Position2D.Eight> firePositionArray,
            [LoopParameter(1)] ref NativeArray<FireStartTime.Eight> fireStartTimeArray
        )
        {
            var diffX = bulletX - enemyX;
            var diffY = bulletY - enemyY;
            var hit = (diffX * diffX + diffY * diffY < collisionRadiusSquare) & (bulletAliveState == default) & (enemyAliveState == default);
            bulletAliveState = math.select(bulletAliveState, -1, hit);
            enemyAliveState = math.select(enemyAliveState, -1, hit);
            for (var i = 0; i < 4; ++i)
            {
                if (!hit[i])
                {
                    continue;
                }

                var fireGreaterIndex = fireCount >> 3;
                var fireLesserIndex = fireCount & 0b111;
                var firePosition = firePositionArray[fireGreaterIndex];
                var fireStartTime = fireStartTimeArray[fireGreaterIndex];
                ((float*)&firePosition.X)[fireLesserIndex] = bulletX[i];
                ((float*)&firePosition.Y)[fireLesserIndex] = bulletY[i];
                firePositionArray[fireGreaterIndex] = firePosition;
                ((float*)&fireStartTime.Value)[fireLesserIndex] = currentTime;
                fireStartTimeArray[fireGreaterIndex] = fireStartTime;
                ++fireCount;
            }
        }

        [CollisionMethod(IntrinsicsKind.Fma, 3, 3, 3), SkipLocalsInit]
        public static void Exe2(
            [LoopParameter(0, nameof(Position2D.X))] ref v256 bulletX,
            [LoopParameter(0, nameof(Position2D.Y))] ref v256 bulletY,
            [LoopParameter(1, nameof(AliveState.Value))] ref v256 bulletAliveState,
            [LoopParameter(0, nameof(Position2D.X))] ref v256 enemyX,
            [LoopParameter(0, nameof(Position2D.Y))] ref v256 enemyY,
            [LoopParameter(1, nameof(AliveState.Value))] ref v256 enemyAliveState,
            [LoopParameter(0)] ref v256 collisionRadiusSquare,
            [LoopParameter(1)] ref float currentTime,
            [LoopParameter(2)] ref int fireCount,
            [LoopParameter(0)] ref NativeArray<Position2D.Eight> firePositionArray,
            [LoopParameter(1)] ref NativeArray<FireStartTime.Eight> fireStartTimeArray
        )
        {
            if (!X86.Fma.IsFmaSupported) return;

            var diffX = X86.Avx.mm256_sub_ps(bulletX, enemyX);
            var s = X86.Avx.mm256_mul_ps(diffX, diffX);
            var diffY = X86.Avx.mm256_sub_ps(bulletY, enemyY);
            var sq = X86.Fma.mm256_fmadd_ps(diffY, diffY, s);
            var cmp = X86.Avx.mm256_cmp_ps(sq, collisionRadiusSquare, (int)X86.Avx.CMP.LT_OQ);
            var hit = X86.Avx.mm256_andnot_ps(X86.Avx.mm256_or_ps(bulletAliveState, enemyAliveState), cmp);
            bulletAliveState = X86.Avx.mm256_or_ps(bulletAliveState, hit);
            enemyAliveState = X86.Avx.mm256_or_ps(enemyAliveState, hit);

            var hitbits = X86.Avx.mm256_movemask_ps(hit);
            if (hitbits == 0) return;

            if ((hitbits & 1) == 1)
            {
                var fireGreaterIndex = fireCount >> 3;
                var fireLesserIndex = fireCount & 0b111;
                var firePosition = firePositionArray[fireGreaterIndex];
                var fireStartTime = fireStartTimeArray[fireGreaterIndex];
                ((float*)&firePosition.X)[fireLesserIndex] = bulletX.Float0;
                ((float*)&firePosition.Y)[fireLesserIndex] = bulletY.Float0;
                firePositionArray[fireGreaterIndex] = firePosition;
                ((float*)&fireStartTime.Value)[fireLesserIndex] = currentTime;
                fireStartTimeArray[fireGreaterIndex] = fireStartTime;
                ++fireCount;
            }

            if ((hitbits & 0b10) == 0b10)
            {
                var fireGreaterIndex = fireCount >> 3;
                var fireLesserIndex = fireCount & 0b111;
                var firePosition = firePositionArray[fireGreaterIndex];
                var fireStartTime = fireStartTimeArray[fireGreaterIndex];
                ((float*)&firePosition.X)[fireLesserIndex] = bulletX.Float1;
                ((float*)&firePosition.Y)[fireLesserIndex] = bulletY.Float1;
                firePositionArray[fireGreaterIndex] = firePosition;
                ((float*)&fireStartTime.Value)[fireLesserIndex] = currentTime;
                fireStartTimeArray[fireGreaterIndex] = fireStartTime;
                ++fireCount;
            }

            if ((hitbits & 0b100) == 0b100)
            {
                var fireGreaterIndex = fireCount >> 3;
                var fireLesserIndex = fireCount & 0b111;
                var firePosition = firePositionArray[fireGreaterIndex];
                var fireStartTime = fireStartTimeArray[fireGreaterIndex];
                ((float*)&firePosition.X)[fireLesserIndex] = bulletX.Float2;
                ((float*)&firePosition.Y)[fireLesserIndex] = bulletY.Float2;
                firePositionArray[fireGreaterIndex] = firePosition;
                ((float*)&fireStartTime.Value)[fireLesserIndex] = currentTime;
                fireStartTimeArray[fireGreaterIndex] = fireStartTime;
                ++fireCount;
            }

            if ((hitbits & 0b1000) == 0b1000)
            {
                var fireGreaterIndex = fireCount >> 3;
                var fireLesserIndex = fireCount & 0b111;
                var firePosition = firePositionArray[fireGreaterIndex];
                var fireStartTime = fireStartTimeArray[fireGreaterIndex];
                ((float*)&firePosition.X)[fireLesserIndex] = bulletX.Float3;
                ((float*)&firePosition.Y)[fireLesserIndex] = bulletY.Float3;
                firePositionArray[fireGreaterIndex] = firePosition;
                ((float*)&fireStartTime.Value)[fireLesserIndex] = currentTime;
                fireStartTimeArray[fireGreaterIndex] = fireStartTime;
                ++fireCount;
            }

            if ((hitbits & 0b10000) == 0b10000)
            {
                var fireGreaterIndex = fireCount >> 3;
                var fireLesserIndex = fireCount & 0b111;
                var firePosition = firePositionArray[fireGreaterIndex];
                var fireStartTime = fireStartTimeArray[fireGreaterIndex];
                ((float*)&firePosition.X)[fireLesserIndex] = bulletX.Float4;
                ((float*)&firePosition.Y)[fireLesserIndex] = bulletY.Float4;
                firePositionArray[fireGreaterIndex] = firePosition;
                ((float*)&fireStartTime.Value)[fireLesserIndex] = currentTime;
                fireStartTimeArray[fireGreaterIndex] = fireStartTime;
                ++fireCount;
            }

            if ((hitbits & 0b100000) == 0b100000)
            {
                var fireGreaterIndex = fireCount >> 3;
                var fireLesserIndex = fireCount & 0b111;
                var firePosition = firePositionArray[fireGreaterIndex];
                var fireStartTime = fireStartTimeArray[fireGreaterIndex];
                ((float*)&firePosition.X)[fireLesserIndex] = bulletX.Float5;
                ((float*)&firePosition.Y)[fireLesserIndex] = bulletY.Float5;
                firePositionArray[fireGreaterIndex] = firePosition;
                ((float*)&fireStartTime.Value)[fireLesserIndex] = currentTime;
                fireStartTimeArray[fireGreaterIndex] = fireStartTime;
                ++fireCount;
            }

            if ((hitbits & 0b1000000) == 0b1000000)
            {
                var fireGreaterIndex = fireCount >> 3;
                var fireLesserIndex = fireCount & 0b111;
                var firePosition = firePositionArray[fireGreaterIndex];
                var fireStartTime = fireStartTimeArray[fireGreaterIndex];
                ((float*)&firePosition.X)[fireLesserIndex] = bulletX.Float6;
                ((float*)&firePosition.Y)[fireLesserIndex] = bulletY.Float6;
                firePositionArray[fireGreaterIndex] = firePosition;
                ((float*)&fireStartTime.Value)[fireLesserIndex] = currentTime;
                fireStartTimeArray[fireGreaterIndex] = fireStartTime;
                ++fireCount;
            }

            if ((hitbits & 0b10000000) == 0b10000000)
            {
                var fireGreaterIndex = fireCount >> 3;
                var fireLesserIndex = fireCount & 0b111;
                var firePosition = firePositionArray[fireGreaterIndex];
                var fireStartTime = fireStartTimeArray[fireGreaterIndex];
                ((float*)&firePosition.X)[fireLesserIndex] = bulletX.Float7;
                ((float*)&firePosition.Y)[fireLesserIndex] = bulletY.Float7;
                firePositionArray[fireGreaterIndex] = firePosition;
                ((float*)&fireStartTime.Value)[fireLesserIndex] = currentTime;
                fireStartTimeArray[fireGreaterIndex] = fireStartTime;
                ++fireCount;
            }
        }
    }

    [CollisionType(
        new[] { typeof(Position2D), typeof(AliveState) }, new[] { true, false },
        new[] { typeof(Position2D) }, new[] { true },
        new[] { typeof(float), typeof(int) }, new[] { true, false },
        new[] { typeof(AliveState.Eight) }, new[] { false }
    )]
    public static partial class CollisionHolder
    {
        [CollisionMethod(IntrinsicsKind.Fma, 3, 2)]
        private static void Exe2(
            [LoopParameter(0, nameof(Position2D.X))] ref v256 enemyX,
            [LoopParameter(0, nameof(Position2D.Y))] ref v256 enemyY,
            [LoopParameter(1, nameof(AliveState.Value))] ref v256 enemyAliveState,
            [LoopParameter(0, nameof(Position2D.X))] ref v256 playerX,
            [LoopParameter(0, nameof(Position2D.Y))] ref v256 playerY,
            [LoopParameter(0)] ref v256 radius,
            [LoopParameter(1)] ref int count
        )
        {
            if (!X86.Fma.IsFmaSupported)
            {
                return;
            }

            var diffX = X86.Avx.mm256_sub_ps(enemyX, playerX);
            var square = X86.Avx.mm256_mul_ps(diffX, diffX);
            var diffY = X86.Avx.mm256_sub_ps(enemyY, playerY);
            var len = X86.Fma.mm256_fmadd_ps(diffY, diffY, square);
            var cmp = X86.Avx.mm256_cmp_ps(len, radius, (int)X86.Avx.CMP.LT_OQ);
            var newState = X86.Avx.mm256_or_ps(enemyAliveState, cmp);
            count += math.countbits(X86.Avx.mm256_movemask_ps(X86.Avx.mm256_xor_ps(enemyAliveState, newState)));
            enemyAliveState = newState;
        }

        [CollisionMethod(IntrinsicsKind.Ordinal, 3, 2)]
        private static void Exe(
            [LoopParameter(0, nameof(Position2D.X))] ref float4 enemyX,
            [LoopParameter(0, nameof(Position2D.Y))] ref float4 enemyY,
            [LoopParameter(1, nameof(AliveState.Value))] ref int4 enemyAliveState,
            [LoopParameter(0, nameof(Position2D.X))] ref float4 playerX,
            [LoopParameter(0, nameof(Position2D.Y))] ref float4 playerY,
            [LoopParameter(0)] ref float4 radius,
            [LoopParameter(1)] ref int count
        )
        {
            var x0 = enemyX - playerX;
            var y0 = enemyY - playerY;
            var len0 = x0 * x0 + y0 * y0 < radius;
            var newState = math.select(enemyAliveState, -1, len0);
            var change = enemyAliveState != newState;
            for (var i = 0; i < 4; ++i)
            {
                if (change[i])
                {
                    ++count;
                }
            }
        }

        [CollisionCloseMethod(IntrinsicsKind.Ordinal, CollisionFieldKind.Outer, 1, nameof(AliveState.Value))]
        private static int4 Close(int4 a0, int4 a1, int4 a2, int4 a3)
        {
            return a0 & a1 & a2 & a3;
        }

        [CollisionCloseMethod(IntrinsicsKind.Fma, CollisionFieldKind.Outer, 1, nameof(AliveState.Value))]
        private static v256 Close2(v256 a0, v256 a1, v256 a2, v256 a3, v256 a4, v256 a5, v256 a6, v256 a7)
        {
            if (!X86.Fma.IsFmaSupported)
            {
                return a0;
            }

            a0 = X86.Avx.mm256_or_ps(a0, a1);
            a0 = X86.Avx.mm256_or_ps(a0, a2);
            a0 = X86.Avx.mm256_or_ps(a0, a3);
            a0 = X86.Avx.mm256_or_ps(a0, a4);
            a0 = X86.Avx.mm256_or_ps(a0, a5);
            a0 = X86.Avx.mm256_or_ps(a0, a6);
            return X86.Avx.mm256_or_ps(a0, a7);
        }
    }
}
