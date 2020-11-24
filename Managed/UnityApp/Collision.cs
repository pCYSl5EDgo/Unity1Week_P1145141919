using MyAttribute;
using Unity.Mathematics;
using Unity.Burst.Intrinsics;
using ComponentTypes;

namespace Workflow
{
    [CollisionType(
        new[] { typeof(Position2D), typeof(AliveState) }, new[] { true, false },
        new[] { typeof(Position2D) }, new[] { true }
    )]
    public static partial class CollisionHolder
    {
        [CollisionMethod(CollisionIntrinsicsKind.Fma)]
        private static void Exe2(
            [CollisionParameter(true, 0, nameof(Position2D.X))] ref v256 enemyX,
            [CollisionParameter(true, 0, nameof(Position2D.Y))] ref v256 enemyY,
            [CollisionParameter(true, 1, nameof(AliveState.Value))] ref v256 enemyAliveState,
            [CollisionParameter(false, 0, nameof(Position2D.X))] ref v256 playerX,
            [CollisionParameter(false, 0, nameof(Position2D.Y))] ref v256 playerY
        )
        {
            if (X86.Fma.IsFmaSupported)
            {
                var diffX = X86.Avx.mm256_sub_ps(enemyX, playerX);
                var square = X86.Avx.mm256_mul_ps(diffX, diffX);
                var diffY = X86.Avx.mm256_sub_ps(enemyY, playerY);
                var len = X86.Fma.mm256_fmadd_ps(diffY, diffY, square);
                var cmp = X86.Avx.mm256_cmp_ps(len, new v256(4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f), (int)X86.Avx.CMP.LT_OQ);
                enemyAliveState = X86.Avx.mm256_or_ps(enemyAliveState, cmp);
            }
        }

        [CollisionMethod(CollisionIntrinsicsKind.None)]
        private static void Exe(
            [CollisionParameter(true, 0, nameof(Position2D.X))] ref float4x2 enemyX,
            [CollisionParameter(true, 0, nameof(Position2D.Y))] ref float4x2 enemyY,
            [CollisionParameter(true, 1, nameof(AliveState.Value))] ref int4x2 enemyAliveState,
            [CollisionParameter(false, 0, nameof(Position2D.X))] ref float4x2 playerX,
            [CollisionParameter(false, 0, nameof(Position2D.Y))] ref float4x2 playerY
        )
        {
            var x0 = enemyX.c0 - playerX.c0;
            var y0 = enemyY.c0 - playerY.c0;
            var len0 = x0 * x0 + y0 * y0 < 4f;
            enemyAliveState.c0 = math.select(-1, enemyAliveState.c0, len0);
            var x1 = enemyX.c1 - playerX.c1;
            var y1 = enemyY.c1 - playerY.c1;
            var len1 = x1 * x1 + y1 * y1 < 4f;
            enemyAliveState.c1 = math.select(-1, enemyAliveState.c1, len1);
        }

        [CollisionCloseMethod(CollisionIntrinsicsKind.None, true, 1, nameof(AliveState.Value))]
        private static int4x2 Close(int4x2 a0, int4x2 a1, int4x2 a2, int4x2 a3, int4x2 a4, int4x2 a5, int4x2 a6, int4x2 a7)
        {
            return new(
                a0.c0 & a1.c0 & a2.c0 & a3.c0 & a4.c0 & a5.c0 & a6.c0 & a7.c0,
                a0.c1 & a1.c1 & a2.c1 & a3.c1 & a4.c1 & a5.c1 & a6.c1 & a7.c1
            );
        }

        [CollisionCloseMethod(CollisionIntrinsicsKind.Fma, true, 1, nameof(AliveState.Value))]
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
