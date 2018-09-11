using Unity.Entities;
using Unity.Mathematics;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Transforms;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unity1Week
{
    [UpdateAfter(typeof(DecidePositionHashCodeSystem))]
    [UpdateBefore(typeof(BombRenderSystem))]
    public sealed class TakenokoEnemyHitCheckSystem : ComponentSystem
    {
        public TakenokoEnemyHitCheckSystem(float radius, NativeMultiHashMap<int, DecidePositionHashCodeSystem.Tuple> enemyHashCodes, NativeMultiHashMap<int, DecidePositionHashCodeSystem.Tuple> playerBulletHashCodes, HashSet<int> allPositionHashCodes, Action playSoundEffect)
        {
            this.radiusSquared = radius * radius;
            this.enemyHashCodes = enemyHashCodes;
            this.playerBulletHashCodes = playerBulletHashCodes;
            this.playSoundEffect = playSoundEffect;
            this.allPositionHashCodes = allPositionHashCodes;
        }
        private EntityArchetype archetype, temperatureArchetype;
        private readonly float radiusSquared;
        private readonly NativeMultiHashMap<int, DecidePositionHashCodeSystem.Tuple> enemyHashCodes;
        private readonly NativeMultiHashMap<int, DecidePositionHashCodeSystem.Tuple> playerBulletHashCodes;
        private readonly Action playSoundEffect;
        private readonly HashSet<int> allPositionHashCodes;
        private readonly HashSet<DecidePositionHashCodeSystem.Tuple> toDestroyTuples = new HashSet<DecidePositionHashCodeSystem.Tuple>();

        protected override void OnCreateManager(int capacity)
        {
            archetype = EntityManager.CreateArchetype(ComponentType.Create<Position2D>(), ComponentType.Create<BombEffect>(), ComponentType.Create<LifeTime>());
            temperatureArchetype = EntityManager.CreateArchetype(ComponentType.Create<ChipRenderSystem.Tag>());
        }
        protected override unsafe void OnUpdate()
        {
            var manager = EntityManager;
            var time = new LifeTime { Value = Time.time };
            var buf = PostUpdateCommands;
            toDestroyTuples.Clear();
            foreach (var key in allPositionHashCodes)
            {
                if (!playerBulletHashCodes.TryGetFirstValue(key, out var playerBulletItem, out var playerBulletIt) || !enemyHashCodes.TryGetFirstValue(key, out var enemyItem, out var enemyIt))
                    continue;
                var diffX = enemyItem.Position.x - playerBulletItem.Position.x;
                var diffY = enemyItem.Position.y - playerBulletItem.Position.y;
                if (diffX * diffX + diffY * diffY <= radiusSquared)
                    toDestroyTuples.Add(playerBulletItem);
                while (enemyHashCodes.TryGetNextValue(out enemyItem, ref enemyIt))
                {
                    diffX = enemyItem.Position.x - playerBulletItem.Position.x;
                    diffY = enemyItem.Position.y - playerBulletItem.Position.y;
                    if (diffX * diffX + diffY * diffY <= radiusSquared)
                        toDestroyTuples.Add(playerBulletItem);
                }
                while (playerBulletHashCodes.TryGetNextValue(out playerBulletItem, ref playerBulletIt))
                {
                    diffX = enemyItem.Position.x - playerBulletItem.Position.x;
                    diffY = enemyItem.Position.y - playerBulletItem.Position.y;
                    if (diffX * diffX + diffY * diffY <= radiusSquared)
                        toDestroyTuples.Add(playerBulletItem);
                    while (enemyHashCodes.TryGetNextValue(out enemyItem, ref enemyIt))
                    {
                        diffX = enemyItem.Position.x - playerBulletItem.Position.x;
                        diffY = enemyItem.Position.y - playerBulletItem.Position.y;
                        if (diffX * diffX + diffY * diffY <= radiusSquared)
                            toDestroyTuples.Add(playerBulletItem);
                    }
                }
            }
            foreach (var toDestroy in toDestroyTuples)
                BurstTakenoko(time, ref buf, toDestroy);
        }
        private unsafe void BurstTakenoko(in LifeTime time, ref EntityCommandBuffer buf, in DecidePositionHashCodeSystem.Tuple tuple)
        {
            buf.DestroyEntity(tuple.Entity);
            buf.CreateEntity(archetype);
            buf.SetComponent(time);
            buf.SetComponent(new Position2D { Value = tuple.Position });
            playSoundEffect();
            buf.CreateEntity(temperatureArchetype);
            buf.SetComponent(new ChipRenderSystem.Tag
            {
                X = (int)tuple.Position.x,
                Y = (int)tuple.Position.y,
                TemperatureDelta = 5000f,
            });
            buf.CreateEntity(temperatureArchetype);
            buf.SetComponent(new ChipRenderSystem.Tag
            {
                X = (int)tuple.Position.x - 1,
                Y = (int)tuple.Position.y,
                TemperatureDelta = 4500f,
            });
            buf.CreateEntity(temperatureArchetype);
            buf.SetComponent(new ChipRenderSystem.Tag
            {
                X = (int)tuple.Position.x + 1,
                Y = (int)tuple.Position.y,
                TemperatureDelta = 4500f,
            });
            buf.CreateEntity(temperatureArchetype);
            buf.SetComponent(new ChipRenderSystem.Tag
            {
                X = (int)tuple.Position.x,
                Y = (int)tuple.Position.y - 1,
                TemperatureDelta = 4500f,
            });
            buf.CreateEntity(temperatureArchetype);
            buf.SetComponent(new ChipRenderSystem.Tag
            {
                X = (int)tuple.Position.x,
                Y = (int)tuple.Position.y + 1,
                TemperatureDelta = 4500f,
            });
        }
    }
}