using ComponentTypes;
using MyFolder.Scripts.ECS.Types;
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
        new[] { typeof(Position2D), typeof(AliveState) }, new[] { true, false },
        new[] { typeof(Position2D), typeof(AliveState) }, new[] { true, true },
        new[] { typeof(float), typeof(float), typeof(int) }, new[] { true, true, false },
        new[] { typeof(Position2D.Eight), typeof(FireStartTime.Eight) }, new[] { false, false }
    )]
    public static unsafe partial class BulletEnemyHit
    {
        [CollisionMethod(IntrinsicsKind.Fma, 3, 3, 3)]
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

        [CollisionMethod(IntrinsicsKind.Ordinal, 3, 3, 3)]
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

        [CollisionCloseMethod(IntrinsicsKind.Ordinal, CollisionFieldKind.Outer, 1, "Value")]
        public static int4 CloseAlive(int4 a, int4 b)
        {
            return a | b;
        }

        [CollisionCloseMethod(IntrinsicsKind.Fma, CollisionFieldKind.Outer, 1, "Value")]
        public static v256 CloseAlive2(v256 a, v256 b)
        {
            if (!X86.Fma.IsFmaSupported) return a;

            return X86.Avx.mm256_or_ps(a, b);
        }
    }

    [BurstCompile]
    public struct PlayerEnemyHitJob : IJob
    {
        [ReadOnly] public NativeArray<Position2D.Eight> EnemyPositionArray;
        public NativeArray<AliveState.Eight> EnemyAliveStateArray;
        public NativeArray<int> CollisionCount;
        public float PlayerPositionX;
        public float PlayerPositionY;
        public float CollisionRadiusSquare;

        public void Execute()
        {
            var playerPosition = new Position2D.Eight
            {
                X = PlayerPositionX,
                Y = PlayerPositionY
            };
            float4x2 collisionSquare = CollisionRadiusSquare;
            var hitCount = CollisionCount[0];

            for (var enemyIndex = 0; enemyIndex < EnemyAliveStateArray.Length; enemyIndex++)
            {
                var enemyState = EnemyAliveStateArray[enemyIndex];
                var enemyPosition = EnemyPositionArray[enemyIndex];
                var isHit = (enemyState.Value == new int4x2()) & (Position2DHelper.CalculateDistanceSquared(enemyPosition, playerPosition) <= collisionSquare);
                enemyState.Value.c0 = math.select(enemyState.Value.c0, new int4(-1), isHit.c0);
                enemyState.Value.c1 = math.select(enemyState.Value.c1, new int4(-1), isHit.c1);
                EnemyAliveStateArray[enemyIndex] = enemyState;
                for (var i = 0; i < 2; i++)
                for (var j = 0; j < 4; j++)
                    if (isHit[i][j])
                        hitCount++;
            }

            CollisionCount[0] = hitCount;
        }
    }

    [BurstCompile]
    public struct PlayerFireEnemyHitJob : IJob
    {
        [ReadOnly] public NativeArray<Position2D.Eight> FirePositionArray;
        [ReadOnly] public NativeArray<FireStartTime.Eight> FireStartTimeArray;
        [ReadOnly] public NativeArray<Position2D.Eight> EnemyPositionArray;
        public NativeArray<AliveState.Eight> EnemyAliveStateArray;
        public NativeArray<int> EnemyKillCount;
        public float CollisionRadiusSquare;
        public float FireDeadTime;

        public void Execute()
        {
            float4x2 collisionRadiusSquare = CollisionRadiusSquare;
            var enemyKillCount = EnemyKillCount[0];
            for (var fireIndex = 0; fireIndex < FirePositionArray.Length; fireIndex++)
            {
                var firePosition = FirePositionArray[fireIndex];
                var fireTime = FireStartTimeArray[fireIndex].Value;
                for (var enemyIndex = 0; enemyIndex < EnemyAliveStateArray.Length; enemyIndex++)
                {
                    var enemyState = EnemyAliveStateArray[enemyIndex];
                    var enemyPosition = EnemyPositionArray[enemyIndex];
                    for (var i = 0; i < 8; RotateUtility.RotateHeadToTail<float4x2, float>(ref enemyPosition.X), RotateUtility.RotateHeadToTail<float4x2, float>(ref enemyPosition.Y), RotateUtility.RotateHeadToTail<AliveState.Eight, int>(ref enemyState), i++)
                    {
                        var lengthSquared = Position2DHelper.CalculateDistanceSquared(enemyPosition, firePosition);
                        var collision = lengthSquared <= collisionRadiusSquare;

                        for (var column = 0; column < 2; column++)
                        for (var row = 0; row < 4; row++)
                        {
                            if (!collision[column][row] || enemyState.Value[column][row] != (int)AliveState.State.Alive || fireTime[column][row] < FireDeadTime) continue;

                            enemyState.Value[column][row] = (int)AliveState.State.Dead;
                            enemyKillCount++;
                        }
                    }

                    EnemyAliveStateArray[enemyIndex] = enemyState;
                }
            }

            EnemyKillCount[0] = enemyKillCount;
        }
    }

    [BurstCompile]
    public struct EnemyAttackPlayerHitJob : IJob
    {
        [ReadOnly] public NativeArray<Position2D.Eight> EnemyAttackPositionArray;
        public NativeArray<AliveState.Eight> EnemyAttackAliveStateArray;
        public NativeArray<int> CollisionCount;
        public float CollisionRadiusSquare;
        public float PlayerPositionX;
        public float PlayerPositionY;

        public void Execute()
        {
            var collisionCount = CollisionCount[0];
            var playerPosition = new Position2D.Eight
            {
                X = PlayerPositionX,
                Y = PlayerPositionY
            };

            float4x2 radiusSquare = CollisionRadiusSquare;
            for (var attackIndex = 0; attackIndex < EnemyAttackAliveStateArray.Length; attackIndex++)
            {
                var attackAliveState = EnemyAttackAliveStateArray[attackIndex];
                var attackPosition = EnemyAttackPositionArray[attackIndex];
                var lengthSquared = Position2DHelper.CalculateDistanceSquared(playerPosition, attackPosition);

                var isInRange = lengthSquared <= radiusSquare;
                for (var column = 0; column < 2; column++)
                for (var row = 0; row < 4; row++)
                {
                    if (!isInRange[column][row] || attackAliveState.Value[column][row] != (int)AliveState.State.Alive) continue;
                    
                    attackAliveState.Value[column][row] = (int)AliveState.State.Dead;
                    collisionCount++;
                }

                EnemyAttackAliveStateArray[attackIndex] = attackAliveState;
            }

            CollisionCount[0] = collisionCount;
        }
    }

    internal static class Position2DHelper
    {
        public static float4x2 CalculateDistanceSquared(in Position2D.Eight obj0, in Position2D.Eight obj1)
        {
            if (X86.Fma.IsFmaSupported)
            {
                unsafe
                {
                    fixed (void* ptr0 = &obj0)
                    fixed (void* ptr1 = &obj1)
                    {
                        var x0 = X86.Avx.mm256_load_ps(ptr0);
                        var x1 = X86.Avx.mm256_load_ps(ptr1);
                        var diffX = X86.Avx.mm256_sub_ps(x0, x1);
                        var lengthSquared = X86.Avx.mm256_mul_ps(diffX, diffX);

                        var y0 = X86.Avx.mm256_load_ps((byte*)ptr0 + sizeof(v256));
                        var y1 = X86.Avx.mm256_load_ps((byte*)ptr1 + sizeof(v256));
                        var diffY = X86.Avx.mm256_sub_ps(y0, y1);
                        lengthSquared = X86.Fma.mm256_fmadd_ps(diffY, diffY, lengthSquared);
                            
                        return *(float4x2*)&lengthSquared;
                    }
                }
            }
            
            {
                var diffX = obj0.X - obj1.X;
                var diffY = obj0.Y - obj1.Y;
                return diffX * diffX + diffY * diffY;
            }
        }
    }
}