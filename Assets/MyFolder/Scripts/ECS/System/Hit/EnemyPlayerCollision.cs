/*using System;
using UnityEngine;
using UniRx;

namespace Unity1Week
{
    [UpdateAfter(typeof(DecidePositionHashCodeSystem))]
    [AlwaysUpdateSystem]
    public sealed class EnemyPlayerCollisionSystem : ComponentSystem
    {
        private readonly NativeMultiHashMap<int, DecidePositionHashCodeSystem.Tuple> enemyHashCodes;
        private readonly Entity player;
        private readonly float radiusSquared;
        private readonly ReactiveProperty<uint> killScore;

        public EnemyPlayerCollisionSystem(Entity player, NativeMultiHashMap<int, DecidePositionHashCodeSystem.Tuple> enemyHashCodes, float radius, ReactiveProperty<uint> killScore)
        {
            this.enemyHashCodes = enemyHashCodes;
            this.player = player;
            this.radiusSquared = radius * radius;
            this.killScore = killScore;
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
            var damage = (float)Math.Sqrt(killScore.Value * deltaTime);
            if (diffX * diffX + diffY * diffY <= radiusSquared)
            {
                settings.Life -= damage;
                settings.Temperature += damage;
            }
            while (enemyHashCodes.TryGetNextValue(out item, ref it))
            {
                diffX = item.Position.x - pos.x;
                diffY = item.Position.y - pos.z;
                if (diffX * diffX + diffY * diffY <= radiusSquared)
                {
                    settings.Life -= damage;
                    settings.Temperature += damage;
                }
            }
            manager.SetComponentData(player, settings);
        }
    }
}*/

