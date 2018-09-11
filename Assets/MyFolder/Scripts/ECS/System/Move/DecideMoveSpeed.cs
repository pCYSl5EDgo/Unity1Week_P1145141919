using System;

using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace Unity1Week
{
    [UpdateAfter(typeof(DestroyEnemyOutOfBoundsSystem))]
    public sealed class DecideMoveSpeedSystem : ComponentSystem
    {
        public DecideMoveSpeedSystem(uint2 range, Chip[] chips, float[] playerSpeed, float[] enemySpeed)
        {
            this.range = range;
            this.chips = chips;
            this.playerSpeed = playerSpeed;
            this.enemySpeed = enemySpeed;
        }
        readonly EntityArchetypeQuery qMover = new EntityArchetypeQuery
        {
            None = Array.Empty<ComponentType>(),
            Any = new[] { ComponentType.Create<Enemy>(), ComponentType.Create<Controlable>() },
            All = new[] { ComponentType.Create<Position>(), ComponentType.Create<MoveSpeed>() }
        };
        readonly NativeList<EntityArchetype> fMover = new NativeList<EntityArchetype>(4096, Allocator.Persistent);
        private readonly uint2 range;
        private Chip[] chips;
        private readonly float[] playerSpeed;
        private float[] enemySpeed;

        protected override void OnDestroyManager()
        {
            fMover.Dispose();
        }
        protected override unsafe void OnUpdate()
        {
            EntityManager.AddMatchingArchetypes(qMover, fMover);
            var MoveSpeedTypeRW = EntityManager.GetArchetypeChunkComponentType<MoveSpeed>(false);
            var PositionTypeRO = EntityManager.GetArchetypeChunkComponentType<Position>(true);
            var EntityRO = EntityManager.GetArchetypeChunkEntityType();
            using (var moverChunks = EntityManager.CreateArchetypeChunkArray(fMover, Allocator.Temp))
            {
                for (int i = 0; i < moverChunks.Length; i++)
                {
                    var entities = moverChunks[i].GetNativeArray(EntityRO);
                    if (entities.Length == 0) continue;
                    var speeds = moverChunks[i].GetNativeArray<MoveSpeed>(MoveSpeedTypeRW);
                    var ptr = (MoveSpeed*)NativeArrayUnsafeUtility.GetUnsafePtr(speeds);
                    var positions = moverChunks[i].GetNativeArray<Position>(PositionTypeRO);
                    if (EntityManager.HasComponent<Controlable>(entities[0]))
                        for (int j = 0; j < positions.Length; ++j, ++ptr)
                            ptr->Value = playerSpeed[chips[((uint)positions[j].Value.x) + range.x * (uint)positions[j].Value.z].Value - 1];
                    else for (int j = 0; j < positions.Length; ++j, ++ptr)
                            ptr->Value = enemySpeed[chips[((uint)positions[j].Value.x) + range.x * (uint)positions[j].Value.z].Value - 1];
                }
            }
        }
    }
}