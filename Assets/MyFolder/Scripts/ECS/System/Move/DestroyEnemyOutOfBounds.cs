using Unity.Entities;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Rendering;
using Unity.Mathematics;
using Unity.Transforms;

using System;
using System.Collections.Generic;

namespace Unity1Week
{
    [UpdateAfter(typeof(ConfinePlayerPositionSystem))]
    public sealed class DestroyEnemyOutOfBoundsSystem : ComponentSystem
    {
        public struct Tag : IComponentData { }
        private readonly uint2 range;
        private readonly HashSet<Entity> toDestroy = new HashSet<Entity>();
        private readonly EntityArchetypeQuery q = new EntityArchetypeQuery
        {
            None = Array.Empty<ComponentType>(),
            Any = Array.Empty<ComponentType>(),
            All = new[] { ComponentType.Create<Position>(), ComponentType.Create<Tag>() },
        };
        private readonly NativeList<EntityArchetype> f = new NativeList<EntityArchetype>(1024, Allocator.Persistent);

        public DestroyEnemyOutOfBoundsSystem(uint2 range) => this.range = range;

        protected override void OnDestroyManager() => f.Dispose();
        protected override void OnUpdate()
        {
            toDestroy.Clear();
            var manager = EntityManager;
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
                            toDestroy.Add(entities[j]);
                    }
                }
            }
            foreach (var item in toDestroy)
                if (manager.Exists(item))
                    manager.DestroyEntity(item);
        }
    }
}