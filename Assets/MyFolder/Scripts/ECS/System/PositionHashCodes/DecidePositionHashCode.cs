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
    [UpdateAfter(typeof(MoveEnemySystem))]
    public sealed class DecidePositionHashCodeSystem : ComponentSystem
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
            public override int GetHashCode() => Entity.Index;
        }
        public DecidePositionHashCodeSystem(uint2 range)
        {
            EnemyHashCodes = new NativeMultiHashMap<int, Tuple>((int)(range.x * range.y), Allocator.Persistent);
            PlayerBulletCodes = new NativeMultiHashMap<int, Tuple>((int)(range.x * range.y), Allocator.Persistent);
            SnowBulletCodes = new NativeMultiHashMap<int, Tuple>((int)(range.x * range.y), Allocator.Persistent);
        }

        public readonly NativeMultiHashMap<int, Tuple> EnemyHashCodes;
        public readonly NativeMultiHashMap<int, Tuple> PlayerBulletCodes;
        public readonly NativeMultiHashMap<int, Tuple> SnowBulletCodes;
        private readonly EntityArchetypeQuery qEnemy = new EntityArchetypeQuery
        {
            None = Array.Empty<ComponentType>(),
            Any = Array.Empty<ComponentType>(),
            All = new[] { ComponentType.Create<Enemy>() },
        };
        private readonly EntityArchetypeQuery qPlayerBullet = new EntityArchetypeQuery
        {
            None = Array.Empty<ComponentType>(),
            Any = Array.Empty<ComponentType>(),
            All = new[] { ComponentType.Create<PlayerShootSystem.TakenokoBullet>() },
        };
        private readonly EntityArchetypeQuery qSnowBullet = new EntityArchetypeQuery
        {
            None = Array.Empty<ComponentType>(),
            Any = Array.Empty<ComponentType>(),
            All = new[] { ComponentType.Create<SnowBulletTag>() },
        };
        private readonly NativeList<EntityArchetype> fEnemy = new NativeList<EntityArchetype>(128, Allocator.Persistent);
        private readonly NativeList<EntityArchetype> fPlayerBullet = new NativeList<EntityArchetype>(128, Allocator.Persistent);
        private readonly NativeList<EntityArchetype> fSnowBullet = new NativeList<EntityArchetype>(128, Allocator.Persistent);
        public HashSet<int> AllPositionHashCodeSet = new HashSet<int>();
        public HashSet<int> PlayerBulletPositionHashCodeSet = new HashSet<int>();
        public HashSet<int> SnowBulletPositionHashCodeSet = new HashSet<int>();
        protected override void OnDestroyManager()
        {
            EnemyHashCodes.Dispose();
            fEnemy.Dispose();
            PlayerBulletCodes.Dispose();
            fPlayerBullet.Dispose();
            SnowBulletCodes.Dispose();
            fSnowBullet.Dispose();
        }

        protected override void OnUpdate()
        {
            EnemyHashCodes.Clear();
            PlayerBulletCodes.Clear();
            SnowBulletCodes.Clear();
            PlayerBulletPositionHashCodeSet.Clear();
            SnowBulletPositionHashCodeSet.Clear();
            var manager = EntityManager;
            manager.AddMatchingArchetypes(qEnemy, fEnemy);
            manager.AddMatchingArchetypes(qPlayerBullet, fPlayerBullet);
            manager.AddMatchingArchetypes(qSnowBullet, fSnowBullet);
            var PositionTypeRO = manager.GetArchetypeChunkComponentType<Position>(true);
            var EntityType = manager.GetArchetypeChunkEntityType();
            using (var enemyChunks = manager.CreateArchetypeChunkArray(fEnemy, Allocator.Temp))
            {
                AddHashCode(PositionTypeRO, EntityType, enemyChunks, EnemyHashCodes);
            }
            using (var playerBulletChunks = manager.CreateArchetypeChunkArray(fPlayerBullet, Allocator.Temp))
            {
                AddHashCode(PositionTypeRO, EntityType, playerBulletChunks, PlayerBulletCodes, PlayerBulletPositionHashCodeSet);
            }
            using (var snowBulletChunks = manager.CreateArchetypeChunkArray(fSnowBullet, Allocator.Temp))
            {
                AddHashCode(PositionTypeRO, EntityType, snowBulletChunks, SnowBulletCodes, SnowBulletPositionHashCodeSet);
            }
        }

        private void AddHashCode(in ArchetypeChunkComponentType<Position> PositionTypeRO, in ArchetypeChunkEntityType EntityType, NativeArray<ArchetypeChunk> chunks, NativeMultiHashMap<int, Tuple> hashCodes)
        {
            for (int i = 0; i < chunks.Length; ++i)
            {
                var positions = chunks[i].GetNativeArray(PositionTypeRO);
                var entities = chunks[i].GetNativeArray(EntityType);
                for (int j = 0; j < positions.Length; ++j)
                {
                    var key = (int)positions[j].Value.x;
                    key <<= 16;
                    key |= (int)positions[j].Value.z;
                    hashCodes.Add(key, new Tuple(entities[j], positions[j].Value.x, positions[j].Value.z));
                }
            }
        }
        private void AddHashCode(in ArchetypeChunkComponentType<Position> PositionTypeRO, in ArchetypeChunkEntityType EntityType, NativeArray<ArchetypeChunk> chunks, NativeMultiHashMap<int, Tuple> hashCodes, HashSet<int> hashSet)
        {
            for (int i = 0; i < chunks.Length; ++i)
            {
                var positions = chunks[i].GetNativeArray(PositionTypeRO);
                var entities = chunks[i].GetNativeArray(EntityType);
                for (int j = 0; j < positions.Length; ++j)
                {
                    var key = (int)positions[j].Value.x;
                    key <<= 16;
                    key |= (int)positions[j].Value.z;
                    hashCodes.Add(key, new Tuple(entities[j], positions[j].Value.x, positions[j].Value.z));
                    hashSet.Add(key);
                }
            }
        }
    }
}