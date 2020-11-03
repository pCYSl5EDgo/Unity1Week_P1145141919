using ComponentTypes;
using MyFolder.Scripts.ECS.Types;
using Unity.Burst;
using Unity.Burst.Intrinsics;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;

namespace Unity1Week.Hit
{
    [BurstCompile]
    public struct BulletEnemyHitJob : IJob
    {
        [ReadOnly] public NativeArray<Position2D.Eight> BulletPositionArray;
        public NativeArray<AliveState.Eight> BulletStateArray;
        [ReadOnly] public NativeArray<Position2D.Eight> EnemyPositionArray;
        [ReadOnly] public NativeArray<AliveState.Eight> EnemyAliveStateArray;

        public NativeArray<Position2D.Eight> FirePositionArray;
        public NativeArray<FireStartTime.Eight> FireStartTimeArray;
        public NativeArray<int> FireCount;

        public float CollisionRadiusSquare;
        public float CurrentTime;

        public void Execute()
        {
            var oldFireCount = FireCount[0];
            var fireIndex = oldFireCount;
            var fireColumn = fireIndex % 8;
            fireIndex -= fireColumn;
            fireIndex >>= 3;
            var fireRow = fireIndex % 4;
            fireColumn -= fireRow;
            fireColumn >>= 2;

            void AddFire(float x, float y, NativeArray<Position2D.Eight> firePositionArray)
            {
                if (fireIndex == firePositionArray.Length) return;
                
                var position = firePositionArray[fireIndex];
                position.X[fireColumn][fireRow] = x;
                position.Y[fireColumn][fireRow] = y;
                firePositionArray[fireIndex] = position;
                
                if (++fireRow != 4) return;

                fireRow = 0;
                if (++fireColumn != 2) return;

                fireColumn = 0;
                ++fireIndex;
            }
            
            for (var bulletIndex = 0; bulletIndex < BulletPositionArray.Length; bulletIndex++)
            {
                var bulletPosition = BulletPositionArray[bulletIndex];
                var bulletState = BulletStateArray[bulletIndex];
                var oldBulletState = bulletState;
                // ReSharper disable once ForCanBeConvertedToForeach
                for (var enemyIndex = 0; enemyIndex < EnemyPositionArray.Length; enemyIndex++)
                {
                    var enemyPosition4X2 = EnemyPositionArray[enemyIndex];
                    var enemyAliveState4X2 = EnemyAliveStateArray[enemyIndex].Value;
                    for (var i = 0; i < 8; i++)
                    {
                        if (i != 0)
                        {
                            RotateUtility.RotateHeadToTail<float4x2, float>(ref enemyPosition4X2.X);
                            RotateUtility.RotateHeadToTail<float4x2, float>(ref enemyPosition4X2.Y);
                            RotateUtility.RotateHeadToTail<int4x2, int>(ref enemyAliveState4X2);
                        }

                        var lengthSquared = Position2DHelper.CalculateDistanceSquared(enemyPosition4X2, bulletPosition);
                        var collision = lengthSquared <= CollisionRadiusSquare;
                        for (var columnIndex = 0; columnIndex < 2; columnIndex++)
                        for (var rowIndex = 0; rowIndex < 4; rowIndex++)
                            if (collision[columnIndex][rowIndex] && enemyAliveState4X2[columnIndex][rowIndex] == (int)AliveState.State.Alive && (int)AliveState.State.Alive == bulletState.Value[columnIndex][rowIndex])
                                bulletState.Value[columnIndex][rowIndex] = (int)AliveState.State.Dead;
                    }
                }

                if (bulletState.Value.c0.x != oldBulletState.Value.c0.x)
                {
                    AddFire(bulletPosition.X.c0.x, bulletPosition.Y.c0.x, FirePositionArray);
                }
                    
                if (bulletState.Value.c0.y != oldBulletState.Value.c0.y)
                {
                    AddFire(bulletPosition.X.c0.y, bulletPosition.Y.c0.y, FirePositionArray);
                }
                    
                if (bulletState.Value.c0.z != oldBulletState.Value.c0.z)
                {
                    AddFire(bulletPosition.X.c0.z, bulletPosition.Y.c0.z, FirePositionArray);
                }
                    
                if (bulletState.Value.c0.w != oldBulletState.Value.c0.w)
                {
                    AddFire(bulletPosition.X.c0.w, bulletPosition.Y.c0.w, FirePositionArray);
                }
                    
                if (bulletState.Value.c1.x != oldBulletState.Value.c1.x)
                {
                    AddFire(bulletPosition.X.c1.x, bulletPosition.Y.c1.x, FirePositionArray);
                }
                    
                if (bulletState.Value.c1.y != oldBulletState.Value.c1.y)
                {
                    AddFire(bulletPosition.X.c1.y, bulletPosition.Y.c1.y, FirePositionArray);
                }
                    
                if (bulletState.Value.c1.z != oldBulletState.Value.c1.z)
                {
                    AddFire(bulletPosition.X.c1.z, bulletPosition.Y.c1.z, FirePositionArray);
                }
                    
                if (bulletState.Value.c1.w != oldBulletState.Value.c1.w)
                {
                    AddFire(bulletPosition.X.c1.w, bulletPosition.Y.c1.w, FirePositionArray);
                }
                
                BulletStateArray[bulletIndex] = bulletState;
            }

            var newFireCount = (fireIndex << 3) | (fireColumn << 2) | fireRow;
            if (newFireCount != oldFireCount)
            {
                var currentTime = CurrentTime;
                unsafe
                {
                    UnsafeUtility.MemCpyReplicate((float*)FireStartTimeArray.GetUnsafePtr() + oldFireCount, &currentTime, sizeof(float), newFireCount - oldFireCount);
                }

                FireCount[0] = newFireCount;
            }
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