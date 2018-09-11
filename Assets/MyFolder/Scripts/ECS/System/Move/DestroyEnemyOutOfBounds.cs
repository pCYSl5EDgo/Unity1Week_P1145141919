using Unity.Entities;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Rendering;
using Unity.Mathematics;
using Unity.Transforms;

using System;

namespace Unity1Week
{
    [UpdateAfter(typeof(ConfinePlayerPositionSystem))]
    public sealed class DestroyEnemyOutOfBoundsSystem : ComponentSystem
    {
        private readonly uint2 range;
        private readonly EntityArchetypeQuery q = new EntityArchetypeQuery
        {
            None = new[] { ComponentType.Create<Controlable>() },
            Any = new[] { ComponentType.Create<Enemy>(), ComponentType.Create<PlayerShootSystem.TakenokoBullet>() },
            All = new[] { ComponentType.Create<Position>() },
        };
        private readonly NativeList<EntityArchetype> f = new NativeList<EntityArchetype>(1024, Allocator.Persistent);

        public DestroyEnemyOutOfBoundsSystem(uint2 range)
        {
            this.range = range;
        }

        protected override void OnDestroyManager()
        {
            f.Dispose();
        }
        protected override void OnUpdate()
        {
            var manager = EntityManager;
            var buf = PostUpdateCommands;
            manager.AddMatchingArchetypes(q, f);
            var positionTypeRO = manager.GetArchetypeChunkComponentType<Position>(true);
            var entityType = manager.GetArchetypeChunkEntityType();
            using (var chunks = manager.CreateArchetypeChunkArray(f, Allocator.Temp))
            {
                for (int i = 0; i < chunks.Length; i++)
                {
                    var positions = chunks[i].GetNativeArray(positionTypeRO);
                    var entities = chunks[i].GetNativeArray(entityType);
                    for (int j = 0; j < positions.Length; j++)
                    {
                        var pos = positions[j].Value;
                        if (pos.x < 0 || pos.z < 0 || pos.x >= range.x || pos.z >= range.y)
                            buf.DestroyEntity(entities[j]);
                    }
                }
            }
        }
    }
}