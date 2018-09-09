using Unity.Entities;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Rendering;
using Unity.Mathematics;
using Unity.Transforms;

using System;

namespace Unity1Week
{
    [AlwaysUpdateSystem]
    sealed class ShootSystem : ComponentSystem
    {
        private readonly Entity player;
        private readonly MeshInstanceRenderer bullet;

        public ShootSystem(Entity player, MeshInstanceRenderer bullet)
        {
            this.player = player;
            this.bullet = bullet;
        }

        private readonly EntityArchetypeQuery q = new EntityArchetypeQuery
        {
            None = Array.Empty<ComponentType>(),
            Any = Array.Empty<ComponentType>(),
            All = new[] { ComponentType.Create<Enemy>(), ComponentType.Create<Position>(), ComponentType.Create<SkillElement>() }
        };
        private readonly NativeList<EntityArchetype> f = new NativeList<EntityArchetype>(1024, Allocator.Persistent);

        private EntityArchetype archetype;
        private ComponentGroup srcGroup;

        protected override void OnCreateManager(int capacity)
        {
            archetype = EntityManager.CreateArchetype(ComponentType.Create<Position>(), ComponentType.Create<Heading2D>(), ComponentType.Create<MoveSpeed>(), ComponentType.Create<MeshInstanceRenderer>(), ComponentType.Create<LifeTime>(), ComponentType.Create<EnemyBulletTag>());
            srcGroup = GetComponentGroup(ComponentType.ReadOnly<Position>(), ComponentType.ReadOnly<MeshInstanceRenderer>(), ComponentType.ReadOnly<EnemyBulletTag>());
        }

        protected override void OnDestroyManager()
        {
            f.Dispose();
        }

        protected override unsafe void OnUpdate()
        {
            Entity src;
            {
                var entities = srcGroup.GetEntityArray();
                if (entities.Length != 0)
                    src = entities[0];
            }
            var manager = EntityManager;
            uint playerX, playerY;
            {
                var value = manager.GetComponentData<Position>(player).Value;
                playerX = (uint)value.x;
                playerY = (uint)value.z;
            }
            manager.AddMatchingArchetypes(q, f);
            var posTypeRO = manager.GetArchetypeChunkComponentType<Position>(true);
            var skillElementRW = manager.GetArchetypeChunkBufferType<SkillElement>(false);
            using (var chunks = manager.CreateArchetypeChunkArray(f, Allocator.Temp))
            {
                for (int i = 0; i < chunks.Length; i++)
                {
                    var positions = chunks[i].GetNativeArray(posTypeRO);
                    var positionPtr = (Position*)positions.GetUnsafeReadOnlyPtr();
                    var skills = chunks[i].GetBufferAccessor(skillElementRW);
                    for (int j = 0; j < positions.Length; ++j, ++positionPtr)
                    {
                        var _ = skills[j];
                        for (int k = 0; k < _.Length; k++)
                        {
                            if (_[k].Value != 2) continue;
                            if (!_[k].IsActivateble) break;

                        }
                    }
                }
            }
        }
    }
}