using Unity.Entities;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Rendering;
using Unity.Mathematics;
using Unity.Transforms;

using System;

namespace Unity1Week
{
    [UpdateAfter(typeof(MoveEnemySystem))]
    sealed class DecidePositionHashCodeSystem : ComponentSystem
    {
        public readonly struct Tuple : IEquatable<Tuple>
        {
            public Tuple(Entity entity, float x, float y)
            {
                Entity = entity;
                Position.x = x;
                Position.y = y;
            }
            public Tuple(Entity entity, float2 position)
            {
                Entity = entity;
                Position = position;
            }
            public readonly Entity Entity;
            public readonly float2 Position;

            public bool Equals(Tuple other) => Entity.Index == other.Entity.Index && Position.x == other.Position.x && Position.y == other.Position.y;
            public static bool operator ==(Tuple left, Tuple right) => left.Entity.Index == right.Entity.Index && left.Position.x == right.Position.x && left.Position.y == right.Position.y;
            public static bool operator !=(Tuple left, Tuple right) => left.Entity.Index != right.Entity.Index || left.Position.x != right.Position.x || left.Position.y != right.Position.y;
            public override bool Equals(object obj) => obj != null && Equals((Tuple)obj);
            public override int GetHashCode() => Entity.Index ^ Entity.Version ^ math.asint(Position.x) ^ math.asint(Position.y);
        }
        public DecidePositionHashCodeSystem(uint2 range)
        {
            EnemyHashCodes = new NativeMultiHashMap<int, Tuple>((int)(range.x * range.y), Allocator.Persistent);
        }

        public readonly NativeMultiHashMap<int, Tuple> EnemyHashCodes;
        private ComponentGroup g;

        protected override void OnCreateManager(int capacity)
        {
            g = GetComponentGroup(ComponentType.ReadOnly<Position>(), ComponentType.ReadOnly<Enemy>());
        }
        protected override void OnDestroyManager()
        {
            EnemyHashCodes.Dispose();
        }

        protected override void OnUpdate()
        {
            EnemyHashCodes.Clear();
            var positions = g.GetComponentDataArray<Position>();
            var entities = g.GetEntityArray();
            for (int consumed = 0, length = positions.Length; consumed < length;)
            {
                var positionChunk = positions.GetChunkArray(consumed, length - consumed);
                var entityChunk = entities.GetChunkArray(consumed, positionChunk.Length);
                for (int i = 0; i < positionChunk.Length; i++)
                    EnemyHashCodes.Add((((int)positionChunk[i].Value.x) << 16) | ((int)positionChunk[i].Value.z), new Tuple(entityChunk[i], positionChunk[i].Value.x, positionChunk[i].Value.z));
                consumed += positionChunk.Length;
            }
        }
    }
}