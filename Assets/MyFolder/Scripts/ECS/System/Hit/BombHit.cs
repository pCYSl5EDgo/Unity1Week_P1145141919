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
    sealed class BombHitCheckSystem : ComponentSystem
    {
        private readonly float rangeSquared;
        private readonly NativeMultiHashMap<int, DecidePositionHashCodeSystem.Tuple> enemyHashCodes;
        private readonly Entity player;
        private readonly EntityArchetypeQuery q = new EntityArchetypeQuery
        {
            None = Array.Empty<ComponentType>(),
            Any = Array.Empty<ComponentType>(),
            All = new[] { ComponentType.Create<Position2D>(), ComponentType.Create<BombEffect>() }
        };
        private readonly NativeList<EntityArchetype> f = new NativeList<EntityArchetype>(Allocator.Persistent);
        private EntityArchetype deadMan;
        private readonly (int, int)[] diff;
        private readonly HashSet<Entity> toDestruct = new HashSet<Entity>();

        public BombHitCheckSystem(Entity player, float radius, NativeMultiHashMap<int, DecidePositionHashCodeSystem.Tuple> enemyHashCodes)
        {
            this.player = player;
            this.rangeSquared = radius * radius;
            this.enemyHashCodes = enemyHashCodes;
            var ls = new List<(int, int)>((int)rangeSquared);
            for (int i = 0, r = (int)radius, r2 = r * r; i <= r; i++)
            {
                for (int j = 0, end = (int)Math.Sqrt(r2 - i * i); j <= end; j++)
                {
                    ls.Add((i, j));
                    if (i != 0)
                        ls.Add((-i, j));
                    if (j != 0)
                        ls.Add((i, -j));
                    if (i != 0 && j != 0)
                        ls.Add((-i, -j));
                }
            }
            this.diff = ls.ToArray();
        }
        [Inject] EndFrameBarrier barrier;

        protected override void OnCreateManager(int capacity)
        {
            deadMan = EntityManager.CreateArchetype(ComponentType.Create<DeadMan>());
        }

        protected override void OnDestroyManager() => f.Dispose();

        protected override void OnUpdate()
        {
            var manager = EntityManager;
            manager.AddMatchingArchetypes(q, f);
            var deltaTime = Time.deltaTime;
            var playerPos = manager.GetComponentData<Position>(player).Value;
            var playerPos2D = playerPos.xz;
            var Position2DTypeRO = manager.GetArchetypeChunkComponentType<Position2D>(true);
            var setting = manager.GetComponentData<PlayerSettings>(player);
            var buf = barrier.CreateCommandBuffer();
            toDestruct.Clear();
            using (var chunks = manager.CreateArchetypeChunkArray(f, Allocator.Temp))
            {
                for (int i = 0; i < chunks.Length; i++)
                {
                    var positions = chunks[i].GetNativeArray(Position2DTypeRO);
                    for (int j = 0; j < positions.Length; j++)
                    {
                        var plDistanceSquared = math.lengthSquared(playerPos2D - positions[j].Value);
                        if (plDistanceSquared <= rangeSquared)
                            setting.Temperature += 300f / plDistanceSquared * deltaTime;
                        var x = (int)positions[j].Value.x;
                        var y = (int)positions[j].Value.y;
                        for (int k = 0; k < diff.Length; k++)
                        {
                            if (!enemyHashCodes.TryGetFirstValue(((diff[k].Item1 + x) << 16) | (diff[k].Item2 + y), out var item, out var it))
                                continue;
                            toDestruct.Add(item.Entity);
                            while (enemyHashCodes.TryGetNextValue(out item, ref it))
                                toDestruct.Add(item.Entity);
                            buf.CreateEntity(deadMan);
                            break;
                        }
                    }
                }
            }
            foreach (var entity in toDestruct)
                buf.DestroyEntity(entity);
            manager.SetComponentData(player, setting);
        }
    }
}