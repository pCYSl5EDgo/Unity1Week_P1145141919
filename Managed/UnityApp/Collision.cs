using System;
using MyAttribute;
using Unity.Mathematics;
using Unity.Burst.Intrinsics;
using ComponentTypes;

namespace Workflow
{
    [CollisionType(
        new[] { typeof(Position2D), typeof(AliveState) }, new[] { true, false },
        new[] { typeof(Position2D) }, new[] { true },
        new[] { typeof(float), typeof(int) }, new[] { true, false },
        new[] { typeof(AliveState.Eight) }, new[] { false }
    )]
    public static partial class CollisionHolder
    {
        [CollisionMethod(CollisionIntrinsicsKind.Fma, 3, 2)]
        private static void Exe2(
            [CollisionParameter(0, nameof(Position2D.X))] ref v256 enemyX,
            [CollisionParameter(0, nameof(Position2D.Y))] ref v256 enemyY,
            [CollisionParameter(1, nameof(AliveState.Value))] ref v256 enemyAliveState,
            [CollisionParameter(0, nameof(Position2D.X))] ref v256 playerX,
            [CollisionParameter(0, nameof(Position2D.Y))] ref v256 playerY,
            [CollisionParameter(0)] ref v256 radius,
            [CollisionParameter(1)] ref int count
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

        [CollisionMethod(CollisionIntrinsicsKind.Ordinal, 3, 2)]
        private static void Exe(
            [CollisionParameter(0, nameof(Position2D.X))] ref float4 enemyX,
            [CollisionParameter(0, nameof(Position2D.Y))] ref float4 enemyY,
            [CollisionParameter(1, nameof(AliveState.Value))] ref int4 enemyAliveState,
            [CollisionParameter(0, nameof(Position2D.X))] ref float4 playerX,
            [CollisionParameter(0, nameof(Position2D.Y))] ref float4 playerY,
            [CollisionParameter(0)] ref float4 radius,
            [CollisionParameter(1)] ref int count
        )
        {
            var x0 = enemyX - playerX;
            var y0 = enemyY - playerY;
            var len0 = x0 * x0 + y0 * y0 < radius;
            var newState = math.select(-1, enemyAliveState, len0);
            var change = enemyAliveState != newState;
            for (var i = 0; i < 4; ++i)
            {
                if (change[i])
                {
                    ++count;
                }
            }
        }

        [CollisionCloseMethod(CollisionIntrinsicsKind.Ordinal, CollisionFieldKind.Outer, 1, nameof(AliveState.Value))]
        private static int4x2 Close(int4x2 a0, int4x2 a1, int4x2 a2, int4x2 a3)
        {
            return new int4x2(
                a0.c0 & a1.c0 & a2.c0 & a3.c0,
                a0.c1 & a1.c1 & a2.c1 & a3.c1
            );
        }

        [CollisionCloseMethod(CollisionIntrinsicsKind.Fma, CollisionFieldKind.Outer, 1, nameof(AliveState.Value))]
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
