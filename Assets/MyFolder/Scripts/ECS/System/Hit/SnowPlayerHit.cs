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
    sealed class SnowPlayerHitCheckSystem : ComponentSystem
    {
        private readonly float rangeSquared;
        private readonly Entity player;
        private readonly Action playSoundEffect;
        private readonly NativeMultiHashMap<int, DecidePositionHashCodeSystem.Tuple> snowHashCodes;
        private readonly NativeMultiHashMap<int, DecidePositionHashCodeSystem.Tuple> playerBulletHashCodes;

        public SnowPlayerHitCheckSystem(Entity player, float radius, NativeMultiHashMap<int, DecidePositionHashCodeSystem.Tuple> snowHashCodes, NativeMultiHashMap<int, DecidePositionHashCodeSystem.Tuple> playerBulletHashCodes, Action playSoundEffect)
        {
            this.rangeSquared = radius * radius;
            this.player = player;
            this.playSoundEffect = playSoundEffect;
            this.snowHashCodes = snowHashCodes;
			this.playerBulletHashCodes = playerBulletHashCodes;
        }

        protected override void OnUpdate()
        {
            throw new NotImplementedException();
        }
    }
}