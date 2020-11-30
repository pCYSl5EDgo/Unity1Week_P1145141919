using ComponentTypes;
using MyAttribute;
using Unity.Burst;
using Unity.Burst.Intrinsics;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;

namespace Unity1Week.Hit
{
    [CollisionType(
        new[] { typeof(Position2D), typeof(AliveState) }, new[] { true, false }, "Bullet",
        new[] { typeof(Position2D), typeof(AliveState) }, new[] { true, true }, "Enemy",
        new[] { typeof(float), typeof(float), typeof(int) }, new[] { true, true, false }, new[] { "CollisionRadiusSquare", "CurrentTime", "FireCount" },
        new[] { typeof(Position2D.Eight), typeof(FireStartTime.Eight) }, new[] { false, false }, new[] { "FirePositionArray", "FireStartTimeArray" }
    )]
    public static unsafe partial class BulletEnemyHit
    {
        [MethodIntrinsicsKind(IntrinsicsKind.Fma)]
        public static void Exe2(
            ref v256 bulletPositionX,
            ref v256 bulletPositionY,
            ref v256 bulletAliveState,
            ref v256 enemyPositionX,
            ref v256 enemyPositionY,
            ref v256 enemyAliveState,
            ref v256 collisionRadiusSquare,
            ref v256 currentTime,
            ref int fireCount,
            void* firePositionArray,
            int firePositionArrayLength,
            void* FireStartTimeArray,
            int FireStartTimeArrayLength
        )
        {
            if (!X86.Fma.IsFmaSupported) return;

            var currentTimeX = currentTime.Float0;
            var diffX = X86.Avx.mm256_sub_ps(bulletPositionX, enemyPositionX);
            var diffY = X86.Avx.mm256_sub_ps(bulletPositionY, enemyPositionY);
            var lenSq = X86.Fma.mm256_fmadd_ps(diffY, diffY, X86.Avx.mm256_mul_ps(diffX, diffX));
            var hit = X86.Avx.mm256_andnot_ps(X86.Avx.mm256_or_ps(bulletAliveState, enemyAliveState), X86.Avx.mm256_cmp_ps(lenSq, collisionRadiusSquare, (int)X86.Avx.CMP.LT_OQ));
            bulletAliveState = X86.Avx.mm256_or_ps(bulletAliveState, hit);
            var mask = X86.Avx.mm256_movemask_ps(hit);
            if ((mask & 1) == 1)
            {
                var fireBigIndex = fireCount >> 3;
                var fireColumn = (fireBigIndex & 4) >> 2;
                var fireRow = fireBigIndex & 3;
                ref var position = ref ((Position2D.Eight*)firePositionArray)[fireBigIndex];
                ref var time = ref ((FireStartTime.Eight*)FireStartTimeArray)[fireBigIndex];
                if (fireColumn == 0)
                {
                    position.X.c0[fireRow] = bulletPositionX.Float0;
                    position.Y.c0[fireRow] = bulletPositionY.Float0;
                    time.Value.c0[fireRow] = currentTimeX;
                }
                else
                {
                    position.X.c1[fireRow] = bulletPositionX.Float0;
                    position.Y.c1[fireRow] = bulletPositionY.Float0;
                    time.Value.c1[fireRow] = currentTimeX;
                }
                ++fireCount;
            }

            if (((mask >> 1) & 1) == 1)
            {
                var fireBigIndex = fireCount >> 3;
                var fireColumn = (fireBigIndex & 4) >> 2;
                var fireRow = fireBigIndex & 3;
                ref var position = ref ((Position2D.Eight*)firePositionArray)[fireBigIndex];
                ref var time = ref ((FireStartTime.Eight*)FireStartTimeArray)[fireBigIndex];
                if (fireColumn == 0)
                {
                    position.X.c0[fireRow] = bulletPositionX.Float1;
                    position.Y.c0[fireRow] = bulletPositionY.Float1;
                    time.Value.c0[fireRow] = currentTimeX;
                }
                else
                {
                    position.X.c1[fireRow] = bulletPositionX.Float1;
                    position.Y.c1[fireRow] = bulletPositionY.Float1;
                    time.Value.c1[fireRow] = currentTimeX;
                }
                ++fireCount;
            }

            if (((mask >> 2) & 1) == 1)
            {
                var fireBigIndex = fireCount >> 3;
                var fireColumn = (fireBigIndex & 4) >> 2;
                var fireRow = fireBigIndex & 3;
                ref var position = ref ((Position2D.Eight*)firePositionArray)[fireBigIndex];
                ref var time = ref ((FireStartTime.Eight*)FireStartTimeArray)[fireBigIndex];
                if (fireColumn == 0)
                {
                    position.X.c0[fireRow] = bulletPositionX.Float2;
                    position.Y.c0[fireRow] = bulletPositionY.Float2;
                    time.Value.c0[fireRow] = currentTimeX;
                }
                else
                {
                    position.X.c1[fireRow] = bulletPositionX.Float2;
                    position.Y.c1[fireRow] = bulletPositionY.Float2;
                    time.Value.c1[fireRow] = currentTimeX;
                }
                ++fireCount;
            }

            if (((mask >> 3) & 1) == 1)
            {
                var fireBigIndex = fireCount >> 3;
                var fireColumn = (fireBigIndex & 4) >> 2;
                var fireRow = fireBigIndex & 3;
                ref var position = ref ((Position2D.Eight*)firePositionArray)[fireBigIndex];
                ref var time = ref ((FireStartTime.Eight*)FireStartTimeArray)[fireBigIndex];
                if (fireColumn == 0)
                {
                    position.X.c0[fireRow] = bulletPositionX.Float3;
                    position.Y.c0[fireRow] = bulletPositionY.Float3;
                    time.Value.c0[fireRow] = currentTimeX;
                }
                else
                {
                    position.X.c1[fireRow] = bulletPositionX.Float3;
                    position.Y.c1[fireRow] = bulletPositionY.Float3;
                    time.Value.c1[fireRow] = currentTimeX;
                }
                ++fireCount;
            }

            if (((mask >> 4) & 1) == 1)
            {
                var fireBigIndex = fireCount >> 3;
                var fireColumn = (fireBigIndex & 4) >> 2;
                var fireRow = fireBigIndex & 3;
                ref var position = ref ((Position2D.Eight*)firePositionArray)[fireBigIndex];
                ref var time = ref ((FireStartTime.Eight*)FireStartTimeArray)[fireBigIndex];
                if (fireColumn == 0)
                {
                    position.X.c0[fireRow] = bulletPositionX.Float4;
                    position.Y.c0[fireRow] = bulletPositionY.Float4;
                    time.Value.c0[fireRow] = currentTimeX;
                }
                else
                {
                    position.X.c1[fireRow] = bulletPositionX.Float4;
                    position.Y.c1[fireRow] = bulletPositionY.Float4;
                    time.Value.c1[fireRow] = currentTimeX;
                }
                ++fireCount;
            }

            if (((mask >> 5) & 1) == 1)
            {
                var fireBigIndex = fireCount >> 3;
                var fireColumn = (fireBigIndex & 4) >> 2;
                var fireRow = fireBigIndex & 3;
                ref var position = ref ((Position2D.Eight*)firePositionArray)[fireBigIndex];
                ref var time = ref ((FireStartTime.Eight*)FireStartTimeArray)[fireBigIndex];
                if (fireColumn == 0)
                {
                    position.X.c0[fireRow] = bulletPositionX.Float5;
                    position.Y.c0[fireRow] = bulletPositionY.Float5;
                    time.Value.c0[fireRow] = currentTimeX;
                }
                else
                {
                    position.X.c1[fireRow] = bulletPositionX.Float5;
                    position.Y.c1[fireRow] = bulletPositionY.Float5;
                    time.Value.c1[fireRow] = currentTimeX;
                }
                ++fireCount;
            }

            if (((mask >> 6) & 1) == 1)
            {
                var fireBigIndex = fireCount >> 3;
                var fireColumn = (fireBigIndex & 4) >> 2;
                var fireRow = fireBigIndex & 3;
                ref var position = ref ((Position2D.Eight*)firePositionArray)[fireBigIndex];
                ref var time = ref ((FireStartTime.Eight*)FireStartTimeArray)[fireBigIndex];
                if (fireColumn == 0)
                {
                    position.X.c0[fireRow] = bulletPositionX.Float6;
                    position.Y.c0[fireRow] = bulletPositionY.Float6;
                    time.Value.c0[fireRow] = currentTimeX;
                }
                else
                {
                    position.X.c1[fireRow] = bulletPositionX.Float6;
                    position.Y.c1[fireRow] = bulletPositionY.Float6;
                    time.Value.c1[fireRow] = currentTimeX;
                }
                ++fireCount;
            }

            if (((mask >> 7) & 1) == 1)
            {
                var fireBigIndex = fireCount >> 3;
                var fireColumn = (fireBigIndex & 4) >> 2;
                var fireRow = fireBigIndex & 3;
                ref var position = ref ((Position2D.Eight*)firePositionArray)[fireBigIndex];
                ref var time = ref ((FireStartTime.Eight*)FireStartTimeArray)[fireBigIndex];
                if (fireColumn == 0)
                {
                    position.X.c0[fireRow] = bulletPositionX.Float7;
                    position.Y.c0[fireRow] = bulletPositionY.Float7;
                    time.Value.c0[fireRow] = currentTimeX;
                }
                else
                {
                    position.X.c1[fireRow] = bulletPositionX.Float7;
                    position.Y.c1[fireRow] = bulletPositionY.Float7;
                    time.Value.c1[fireRow] = currentTimeX;
                }
                ++fireCount;
            }
        }

        [MethodIntrinsicsKind(IntrinsicsKind.Ordinal)]
        public static void Exe(
            ref float4 bulletPositionX,
            ref float4 bulletPositionY,
            ref int4 bulletAliveState,
            ref float4 enemyPositionX,
            ref float4 enemyPositionY,
            ref int4 enemyAliveState,
            ref float4 collisionRadiusSquare,
            ref float4 currentTime,
            ref int fireCount,
            void* firePositionArray,
            int firePositionArrayLength,
            void* FireStartTimeArray,
            int FireStartTimeArrayLength
        )
        {
            var diffX = bulletPositionX - enemyPositionX;
            var diffY = bulletPositionY - enemyPositionY;
            var lenSq = diffX * diffX + diffY * diffY;
            var hit = lenSq < collisionRadiusSquare & enemyAliveState == 0 & bulletAliveState == 0;
            enemyAliveState = math.select(bulletAliveState, -1, hit);
            for (var i = 0; i < 4; ++i)
            {
                if (hit[i])
                {
                    var fireBigIndex = fireCount >> 3;
                    var fireColumn = (fireBigIndex & 4) >> 2;
                    var fireRow = fireBigIndex & 3;
                    ref var position = ref ((Position2D.Eight*)firePositionArray)[fireBigIndex];
                    ref var time = ref ((FireStartTime.Eight*)FireStartTimeArray)[fireBigIndex];
                    if (fireColumn == 0)
                    {
                        position.X.c0[fireRow] = bulletPositionX[i];
                        position.Y.c0[fireRow] = bulletPositionY[i];
                        time.Value.c0[fireRow] = currentTime.x;
                    }
                    else
                    {
                        position.X.c1[fireRow] = bulletPositionX[i];
                        position.Y.c1[fireRow] = bulletPositionY[i];
                        time.Value.c1[fireRow] = currentTime.x;
                    }

                    ++fireCount;
                }
            }
        }

        [CollisionCloseMethod(IntrinsicsKind.Ordinal, 1, "Value")]
        public static int4 CloseAlive(int4 a, int4 b)
        {
            return a | b;
        }

        [CollisionCloseMethod(IntrinsicsKind.Fma, 1, "Value")]
        public static v256 CloseAlive2(v256 a, v256 b)
        {
            if (!X86.Fma.IsFmaSupported) return a;

            return X86.Avx.mm256_or_ps(a, b);
        }
    }

    [SingleLoopType(
        new[] { typeof(Position2D), typeof(AliveState) }, new[] { true, false }, "Enemy",
        new[] { typeof(float), typeof(float), typeof(float), typeof(int) }, new[] { true, true, true, false }, new[] { "PlayerPositionX", "PlayerPositionY", "CollisionRadiusSquare", "CollisionCount" }
    )]
    public static partial class PlayerEnemyHitJob
    {
        [MethodIntrinsicsKind(IntrinsicsKind.Ordinal)]
        public static void Exe(
            ref float4 enemyPositionX,
            ref float4 enemyPositionY,
            ref int4 enemyAliveState,
            ref float4 playerPositionX,
            ref float4 playerPositionY,
            ref float4 collisionRadiusSquare,
            ref int collisionCount
        )
        {
            var diffX = playerPositionX - enemyPositionX;
            var diffY = playerPositionY - enemyPositionY;
            var hit = enemyAliveState == 0 & (diffX * diffX + diffY * diffY < collisionRadiusSquare);
            enemyAliveState = math.select(enemyAliveState, -1, hit);
            for (var i = 0; i < 4; i++)
            {
                if (hit[i])
                {
                    collisionCount++;
                }
            }
        }

        [MethodIntrinsicsKind(IntrinsicsKind.Fma)]
        public static void Exe2(
            ref v256 enemyPositionX,
            ref v256 enemyPositionY,
            ref v256 enemyAliveState,
            ref v256 playerPositionX,
            ref v256 playerPositionY,
            ref v256 collisionRadiusSquare,
            ref int collisionCount
        )
        {
            if (!X86.Fma.IsFmaSupported) return;

            var diffX = X86.Avx.mm256_sub_ps(playerPositionX, enemyPositionX);
            var diffY = X86.Avx.mm256_sub_ps(playerPositionY, enemyPositionY);
            var lengthSquared = X86.Fma.mm256_fmadd_ps(diffY, diffY, X86.Avx.mm256_mul_ps(diffX, diffX));
            var hit = X86.Avx.mm256_andnot_ps(enemyAliveState, X86.Avx.mm256_cmp_ps(lengthSquared, collisionRadiusSquare, (int)X86.Avx.CMP.LT_OQ));
            enemyAliveState = X86.Avx.mm256_or_ps(enemyAliveState, hit);
            var mask = X86.Avx.mm256_movemask_ps(hit);
            collisionCount += math.countbits(mask);
        }
    }

    [CollisionType(
        new[] { typeof(Position2D), typeof(FireStartTime) }, new[] { true, true }, "Fire",
        new[] { typeof(Position2D), typeof(AliveState) }, new[] { true, false }, "Enemy",
        new[] { typeof(float), typeof(float), typeof(int) }, new[] { true, true, false }, new[] { "CollisionRadiusSquare", "FireDeadTime", "EnemyKillCount" }
    )]
    public static partial class PlayerFireEnemyHitJob
    {
        [MethodIntrinsicsKind(IntrinsicsKind.Ordinal)]
        public static void Exe(
            ref float4 firePositionX,
            ref float4 firePositionY,
            ref float4 fireStartTime,
            ref float4 enemyPositionX,
            ref float4 enemyPositionY,
            ref int4 enemyAliveState,
            ref float4 collisionRadiusSquare,
            ref float4 fireDeadTime,
            ref int enemyKillCount
        )
        {
            var diffX = firePositionX - enemyPositionX;
            var diffY = firePositionY - enemyPositionY;
            var lengthSquared = diffX * diffX + diffY * diffY;
            var hit = lengthSquared < collisionRadiusSquare & enemyAliveState == 0 & fireStartTime > fireDeadTime;
            enemyAliveState = math.select(enemyAliveState, -1, hit);
            for (var i = 0; i < 4; ++i)
            {
                if (hit[i])
                {
                    enemyKillCount++;
                }
            }
        }

        [MethodIntrinsicsKind(IntrinsicsKind.Fma)]
        public static void Exe2(
            ref v256 firePositionX,
            ref v256 firePositionY,
            ref v256 fireStartTime,
            ref v256 enemyPositionX,
            ref v256 enemyPositionY,
            ref v256 enemyAliveState,
            ref v256 collisionRadiusSquare,
            ref v256 fireDeadTime,
            ref int enemyKillCount
        )
        {
            if (!X86.Fma.IsFmaSupported) return;

            var diffX = X86.Avx.mm256_sub_ps(firePositionX, enemyPositionX);
            var diffY = X86.Avx.mm256_sub_ps(firePositionY, enemyPositionY);
            var lengthSquared = X86.Fma.mm256_fmadd_ps(diffY, diffY, X86.Avx.mm256_mul_ps(diffX, diffX));
            var hit = X86.Avx.mm256_andnot_ps(enemyAliveState, X86.Avx.mm256_cmp_ps(lengthSquared, collisionRadiusSquare, (int)X86.Avx.CMP.LT_OQ));
            hit = X86.Avx.mm256_and_ps(hit, X86.Avx.mm256_cmp_ps(fireDeadTime, fireStartTime, (int)X86.Avx.CMP.LT_OQ));
            enemyAliveState = X86.Avx.mm256_or_ps(enemyAliveState, hit);
            enemyKillCount += math.countbits(X86.Avx.mm256_movemask_ps(hit));
        }
    }

    [SingleLoopType(
        new[] { typeof(Position2D), typeof(AliveState) }, new[] { true, false }, "Enemy",
        new[] { typeof(float), typeof(float), typeof(float), typeof(int) }, new[] { true, true, true, false }, new[] { "PlayerPositionX", "PlayerPositionY", "CollisionRadiusSquare", "CollisionCount" }
    )]
    public static partial class EnemyAttackPlayerHitJob
    {
        [MethodIntrinsicsKind(IntrinsicsKind.Ordinal)]
        public static void Exe(
            ref float4 attackX,
            ref float4 attackY,
            ref int4 attackAliveState,
            ref float4 playerPositionX,
            ref float4 playerPositionY,
            ref float4 collisionRadiusSquare,
            ref int collisionCount
        )
        {
            var lengthSquared = math.distancesq(attackX - playerPositionX, attackY - playerPositionY);
            var hit = attackAliveState == 0 & lengthSquared < collisionRadiusSquare;
            attackAliveState = math.select(attackAliveState, -1, hit);
            for (var i = 0; i < 4; ++i)
            {
                if (hit[i])
                {
                    ++collisionCount;
                }
            }
        }

        [MethodIntrinsicsKind(IntrinsicsKind.Fma)]
        public static void Exe2(
            ref v256 attackX,
            ref v256 attackY,
            ref v256 attackAliveState,
            ref v256 playerPositionX,
            ref v256 playerPositionY,
            ref v256 collisionRadiusSquare,
            ref int collisionCount
        )
        {
            var diffX = X86.Avx.mm256_sub_ps(attackX, playerPositionX);
            var diffY = X86.Avx.mm256_sub_ps(attackY, playerPositionY);
            var lengthSquared = X86.Fma.mm256_fmadd_ps(diffY, diffY, X86.Avx.mm256_mul_ps(diffX, diffX));
            var hit = X86.Avx.mm256_andnot_ps(attackAliveState, X86.Avx.mm256_cmp_ps(lengthSquared, collisionRadiusSquare, (int)X86.Avx.CMP.LT_OQ));
            attackAliveState = X86.Avx.mm256_or_ps(attackAliveState, hit);
            collisionCount += math.countbits(X86.Avx.mm256_movemask_ps(hit));
        }
    }
}
