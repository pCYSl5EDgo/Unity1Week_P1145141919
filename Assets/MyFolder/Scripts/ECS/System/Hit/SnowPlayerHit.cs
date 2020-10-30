/*using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Unity1Week
{
    [UpdateAfter(typeof(DecidePositionHashCodeSystem))]
    [UpdateBefore(typeof(AnimationSkillRenderSystem))]
    public sealed class SnowPlayerHitCheckSystem : ComponentSystem
    {
        private readonly float rangeSquared;
        private readonly float damageRatio;
        private readonly ReactiveProperty<uint> killScore;
        private readonly Entity player;
        private readonly Action playSoundEffect;
        private readonly NativeMultiHashMap<int, DecidePositionHashCodeSystem.Tuple> snowHashCodes;
        private readonly NativeMultiHashMap<int, DecidePositionHashCodeSystem.Tuple> playerBulletHashCodes;
        private readonly HashSet<int> snowBulletPositionHashSet;
        private readonly HashSet<DecidePositionHashCodeSystem.Tuple> toDestroy = new HashSet<DecidePositionHashCodeSystem.Tuple>();

        public SnowPlayerHitCheckSystem(Entity player, float damageRatio, UniRx.ReactiveProperty<uint> killScore, float radius, NativeMultiHashMap<int, DecidePositionHashCodeSystem.Tuple> snowHashCodes, NativeMultiHashMap<int, DecidePositionHashCodeSystem.Tuple> playerBulletHashCodes, HashSet<int> snowBulletPositionHashSet, Action playSoundEffect)
        {
            this.rangeSquared = radius * radius;
            this.damageRatio = damageRatio;
            this.killScore = killScore;
            this.player = player;
            this.playSoundEffect = playSoundEffect;
            this.snowHashCodes = snowHashCodes;
            this.playerBulletHashCodes = playerBulletHashCodes;
            this.snowBulletPositionHashSet = snowBulletPositionHashSet;
        }

        protected override void OnUpdate()
        {
            toDestroy.Clear();
            var manager = EntityManager;
            var buf = PostUpdateCommands;
            var playerPosition = manager.GetComponentData<Position>(player).Value;
            var playerSettings = manager.GetComponentData<PlayerSettings>(player);
            var playerX = playerPosition.x;
            var playerY = playerPosition.z;
            var damage = damageRatio * Time.deltaTime * killScore.Value;
            var temperatureDelta = damageRatio * Time.deltaTime;
            DecidePositionHashCodeSystem.Tuple snowItem, playerBulletItem;
            NativeMultiHashMapIterator<int> snowIt, playerBulletIt;
            foreach (var key in snowBulletPositionHashSet)
            {
                if (!snowHashCodes.TryGetFirstValue(key, out snowItem, out snowIt)) continue;
                var diffX = playerX - snowItem.Position.x;
                var diffY = playerY - snowItem.Position.y;
                if (diffX * diffX + diffY * diffY <= rangeSquared)
                {
                    playerSettings.Life -= damage;
                    playerSettings.Temperature -= temperatureDelta;
                    toDestroy.Add(snowItem);
                    goto NEXT;
                }
                if (!playerBulletHashCodes.TryGetFirstValue(key, out playerBulletItem, out playerBulletIt)) continue;
                toDestroy.Add(snowItem);
                toDestroy.Add(playerBulletItem);
                while (playerBulletHashCodes.TryGetNextValue(out playerBulletItem, ref playerBulletIt))
                    toDestroy.Add(playerBulletItem);
                NEXT:
                while (snowHashCodes.TryGetNextValue(out snowItem, ref snowIt))
                {
                    diffX = playerX - snowItem.Position.x;
                    diffY = playerY - snowItem.Position.y;
                    if (diffX * diffX + diffY * diffY <= rangeSquared)
                    {
                        playerSettings.Life -= damage;
                        playerSettings.Temperature -= temperatureDelta;
                        toDestroy.Add(snowItem);
                        continue;
                    }
                    if (!playerBulletHashCodes.TryGetFirstValue(key, out playerBulletItem, out playerBulletIt)) continue;
                    toDestroy.Add(snowItem);
                    toDestroy.Add(playerBulletItem);
                    while (playerBulletHashCodes.TryGetNextValue(out playerBulletItem, ref playerBulletIt))
                        toDestroy.Add(playerBulletItem);
                }
            }
            foreach (var item in toDestroy)
                if (manager.Exists(item.Entity))
                    manager.DestroyEntity(item.Entity);
            manager.SetComponentData(player, playerSettings);
        }
    }
}*/

