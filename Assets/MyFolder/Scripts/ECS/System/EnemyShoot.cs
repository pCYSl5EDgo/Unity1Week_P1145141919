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
    public sealed class ShootSystem : ComponentSystem
    {
        private readonly Entity player;
        private readonly float moveSpeed;

        public ShootSystem(Entity player, float moveSpeed)
        {
            this.player = player;
            this.moveSpeed = moveSpeed;
        }
        private readonly EntityArchetypeQuery q = new EntityArchetypeQuery
        {
            None = Array.Empty<ComponentType>(),
            Any = Array.Empty<ComponentType>(),
            All = new[] { ComponentType.Create<Boss>(), ComponentType.Create<Position>(), ComponentType.Create<SkillElement>() }
        };
        private readonly NativeList<EntityArchetype> f = new NativeList<EntityArchetype>(1024, Allocator.Persistent);

        private EntityArchetype archetype;

        protected override void OnCreateManager(int capacity)
        {
            archetype = EntityManager.CreateArchetype(ComponentType.Create<Position>(), ComponentType.Create<Heading2D>(), ComponentType.Create<MoveSpeed>(), ComponentType.Create<SnowBulletTag>(), ComponentType.Create<DestroyEnemyOutOfBoundsSystem.Tag>());
        }

        protected override void OnDestroyManager()
        {
            f.Dispose();
        }

        protected override unsafe void OnUpdate()
        {
            var manager = EntityManager;
            var buf = PostUpdateCommands;
            uint playerX, playerY;
            {
                var value = manager.GetComponentData<Position>(player).Value;
                playerX = (uint)value.x;
                playerY = (uint)value.z;
            }
            manager.AddMatchingArchetypes(q, f);
            var posTypeRO = manager.GetArchetypeChunkComponentType<Position>(true);
            var skillElementRW = manager.GetArchetypeChunkBufferType<SkillElement>(false);
            var component = new MoveSpeed(moveSpeed);
            using (var chunks = manager.CreateArchetypeChunkArray(f, Allocator.Temp))
            {
                for (int i = 0; i < chunks.Length; i++)
                {
                    var positions = chunks[i].GetNativeArray(posTypeRO);
                    var positionPtr = (Position*)positions.GetUnsafeReadOnlyPtr();
                    var skills = chunks[i].GetBufferAccessor(skillElementRW);
                    for (int j = 0; j < positions.Length; ++j, ++positionPtr)
                    {
                        var skill = skills[j];
                        var _ = skill[0];
                        if (!_.IsActivateble) continue;
                        _.SinceLastTime = 0;
                        skill[0] = _;
                        buf.CreateEntity(archetype);
                        buf.SetComponent(*positionPtr);
                        var diffX = playerX - positionPtr->Value.x;
                        var diffY = playerY - positionPtr->Value.z;
                        var length = 1 / Math.Sqrt(diffX * diffX + diffY * diffY);
                        buf.SetComponent(new Heading2D(new float2((float)(diffX * length), (float)(diffY * length))));
                        buf.SetComponent(component);
                    }
                }
            }
        }
    }
}