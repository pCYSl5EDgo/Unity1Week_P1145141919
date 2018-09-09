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
    [AlwaysUpdateSystem]
    sealed class EnemyPlayerCollisionSystem : ComponentSystem
    {
        private readonly NativeMultiHashMap<int, DecidePositionHashCodeSystem.Tuple> enemyHashCodes;
        private readonly Entity player;
        private readonly float radiusSquared;

        public EnemyPlayerCollisionSystem(Entity player, NativeMultiHashMap<int, DecidePositionHashCodeSystem.Tuple> enemyHashCodes, float radius)
        {
            this.enemyHashCodes = enemyHashCodes;
            this.player = player;
            this.radiusSquared = radius * radius;
        }

        protected override void OnUpdate()
        {
            var pos = EntityManager.GetComponentData<Position>(player).Value;
            var key = (((int)pos.x) << 16) | ((int)pos.z);
            if (!enemyHashCodes.TryGetFirstValue(key, out var item, out var it)) return;
            var deltaTime = Time.deltaTime;
            var manager = EntityManager;
            var settings = manager.GetComponentData<PlayerSettings>(player);
            var diffX = item.Position.x - pos.x;
            var diffY = item.Position.y - pos.z;
            if (diffX * diffX + diffY * diffY <= radiusSquared)
            {
                settings.Life -= deltaTime * 10;
                settings.Temperature += deltaTime * 10;
            }
            while (enemyHashCodes.TryGetNextValue(out item, ref it))
            {
                diffX = item.Position.x - pos.x;
                diffY = item.Position.y - pos.z;
                if (diffX * diffX + diffY * diffY <= radiusSquared)
                {
                    settings.Life -= deltaTime * 10;
                    settings.Temperature += deltaTime * 10;
                }
            }
            manager.SetComponentData(player, settings);
        }
    }
}