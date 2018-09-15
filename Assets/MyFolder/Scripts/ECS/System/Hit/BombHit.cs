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
    [UpdateBefore(typeof(AnimationSkillRenderSystem))]
    public sealed class BombHitCheckSystem : ComponentSystem
    {
        private readonly float rangeSquared;
        private readonly NativeMultiHashMap<int, DecidePositionHashCodeSystem.Tuple> enemyHashCodes;
        private readonly Entity player;
        private ComponentGroup g;
        private EntityArchetype deadMan;
        private readonly (int, int)[] diff;
        private readonly HashSet<Entity> toDestroy = new HashSet<Entity>();

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

        protected override void OnCreateManager(int capacity)
        {
            g = GetComponentGroup(ComponentType.ReadOnly<Position2D>(), ComponentType.ReadOnly<BombEffect>());
            deadMan = EntityManager.CreateArchetype(ComponentType.Create<DeadMan>());
        }

        protected override void OnUpdate()
        {
            var positions = g.GetComponentDataArray<Position2D>();
            var manager = EntityManager;
            var buf = PostUpdateCommands;
            var playerPos = manager.GetComponentData<Position>(player).Value;
            var deltaTime = Time.deltaTime;
            toDestroy.Clear();
            for (int consumed = 0, length = positions.Length; consumed < length;)
            {
                var posChunk = positions.GetChunkArray(consumed, length - consumed);
                for (int i = 0; i < posChunk.Length; i++)
                {
                    var x = (int)posChunk[i].Value.x;
                    var y = (int)posChunk[i].Value.y;
                    float plDistanceSquared;
                    {
                        var diffX = playerPos.x - posChunk[i].Value.x;
                        var diffY = playerPos.z - posChunk[i].Value.y;
                        plDistanceSquared = diffX * diffX + diffY * diffY;
                    }
                    if (plDistanceSquared <= rangeSquared)
                    {
                        var setting = manager.GetComponentData<PlayerSettings>(player);
                        setting.Temperature += 300f / plDistanceSquared * deltaTime;
                        manager.SetComponentData(player, setting);
                    }
                    for (int j = 0; j < diff.Length; j++)
                    {
                        if (!enemyHashCodes.TryGetFirstValue(((diff[j].Item1 + x) << 16) | (diff[j].Item2 + y), out var item, out var it))
                            continue;
                        toDestroy.Add(item.Entity);
                        while (enemyHashCodes.TryGetNextValue(out item, ref it))
                            toDestroy.Add(item.Entity);
                        manager.CreateEntity(deadMan);
                        break;
                    }
                }
                foreach (var item in toDestroy)
                    if (manager.Exists(item))
                        manager.DestroyEntity(item);
                consumed += posChunk.Length;
            }
        }
    }
}