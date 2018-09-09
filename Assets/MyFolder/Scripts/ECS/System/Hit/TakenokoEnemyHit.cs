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
    sealed class TakenokoEnemyHitCheckSystem : ComponentSystem
    {
        public TakenokoEnemyHitCheckSystem(float range, NativeMultiHashMap<int, DecidePositionHashCodeSystem.Tuple> enemyHashCodes, Action playSoundEffect)
        {
            this.rangeSquared = range * range;
            this.enemyHashCodes = enemyHashCodes;
            this.playSoundEffect = playSoundEffect;
        }
        private readonly EntityArchetypeQuery qTakenoko = new EntityArchetypeQuery
        {
            None = Array.Empty<ComponentType>(),
            Any = Array.Empty<ComponentType>(),
            All = new[] { ComponentType.Create<Position>(), ComponentType.Create<PlayerShootSystem.TakenokoBullet>() },
        };
        private EntityArchetype archetype, temperatureArchetype;
        private readonly NativeList<EntityArchetype> fTakenoko = new NativeList<EntityArchetype>(Allocator.Persistent);
        private readonly float rangeSquared;
        private readonly NativeMultiHashMap<int, DecidePositionHashCodeSystem.Tuple> enemyHashCodes;
        private readonly Action playSoundEffect;

        protected override void OnCreateManager(int capacity)
        {
            archetype = EntityManager.CreateArchetype(ComponentType.Create<Position2D>(), ComponentType.Create<BombEffect>(), ComponentType.Create<LifeTime>());
            temperatureArchetype = EntityManager.CreateArchetype(ComponentType.Create<ChipRenderSystem.Tag>());
        }
        protected override void OnDestroyManager()
        {
            fTakenoko.Dispose();
        }
        protected override unsafe void OnUpdate()
        {
            var manager = EntityManager;
            var time = new LifeTime { Value = Time.time };
            manager.AddMatchingArchetypes(qTakenoko, fTakenoko);
            var PositionTypeRO = manager.GetArchetypeChunkComponentType<Position>(true);
            var EntityType = manager.GetArchetypeChunkEntityType();
            var buf = PostUpdateCommands;
            using (var takenokoChunks = manager.CreateArchetypeChunkArray(fTakenoko, Allocator.Temp))
            {
                for (int i = 0; i < takenokoChunks.Length; ++i)
                {
                    var positionArray = takenokoChunks[i].GetNativeArray(PositionTypeRO);
                    var posPtr = (Position*)positionArray.GetUnsafeReadOnlyPtr();
                    var entities = takenokoChunks[i].GetNativeArray(EntityType);
                    for (int j = 0; j < positionArray.Length; ++j, ++posPtr)
                    {
                        if (!enemyHashCodes.TryGetFirstValue(((ushort)posPtr->Value.x << 16) | (ushort)posPtr->Value.z, out var item, out var iterator))
                            continue;
                        var pos = new float2(item.Position.x, item.Position.y);
                        var x = pos.x - posPtr->Value.x;
                        var y = pos.y - posPtr->Value.z;
                        if (x * x + y * y <= rangeSquared)
                        {
                            BurstTakenoko(time, ref buf, ref entities, j, pos);
                            continue;
                        }
                        while (enemyHashCodes.TryGetNextValue(out item, ref iterator))
                        {
                            pos = new float2(item.Position.x, item.Position.y);
                            x = pos.x - posPtr->Value.x;
                            y = pos.y - posPtr->Value.z;
                            if (x * x + y * y <= rangeSquared)
                            {
                                BurstTakenoko(time, ref buf, ref entities, j, pos);
                                break;
                            }
                        }
                    }
                }
            }
        }
        private unsafe void BurstTakenoko(LifeTime time, ref EntityCommandBuffer buf, ref NativeArray<Entity> entities, int j, float2 item)
        {
            buf.DestroyEntity(entities[j]);
            buf.CreateEntity(archetype);
            buf.SetComponent(time);
            buf.SetComponent(new Position2D { Value = item });
            playSoundEffect();
            buf.CreateEntity(temperatureArchetype);
            buf.SetComponent(new ChipRenderSystem.Tag
            {
                X = (int)item.x,
                Y = (int)item.y,
                TemperatureDelta = 5000f,
            });
            buf.CreateEntity(temperatureArchetype);
            buf.SetComponent(new ChipRenderSystem.Tag
            {
                X = (int)item.x - 1,
                Y = (int)item.y,
                TemperatureDelta = 8000f,
            });
            buf.CreateEntity(temperatureArchetype);
            buf.SetComponent(new ChipRenderSystem.Tag
            {
                X = (int)item.x + 1,
                Y = (int)item.y,
                TemperatureDelta = 4500f,
            });
            buf.CreateEntity(temperatureArchetype);
            buf.SetComponent(new ChipRenderSystem.Tag
            {
                X = (int)item.x,
                Y = (int)item.y - 1,
                TemperatureDelta = 4500f,
            });
            buf.CreateEntity(temperatureArchetype);
            buf.SetComponent(new ChipRenderSystem.Tag
            {
                X = (int)item.x,
                Y = (int)item.y + 1,
                TemperatureDelta = 4500f,
            });
        }
    }
}