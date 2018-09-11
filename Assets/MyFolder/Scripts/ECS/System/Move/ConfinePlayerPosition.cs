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
    [AlwaysUpdateSystem]
    [UpdateAfter(typeof(MoveSystem))]
    [UpdateBefore(typeof(DecideMoveSpeedSystem))]
    public sealed class ConfinePlayerPositionSystem : ComponentSystem
    {
        private readonly Entity player;
        private readonly float2 range;
        private readonly Transform transform;

        public ConfinePlayerPositionSystem(Entity player, uint2 range, Transform transform)
        {
            this.player = player;
            this.range = new float2((float)range.x - 0.0001f, (float)range.y - 0.0001f);
            this.transform = transform;
        }

        protected override void OnUpdate()
        {
            var manager = EntityManager;
            var pos = manager.GetComponentData<Position>(player);
            var old = pos.Value;
            pos.Value.x = math.clamp(pos.Value.x, 0, range.x);
            pos.Value.z = math.clamp(pos.Value.z, 0, range.y);
            if (old.x != pos.Value.x || old.z != pos.Value.z)
            {
                manager.SetComponentData(player, pos);
                var tPos = transform.position;
                tPos.x = pos.Value.x;
                tPos.z = pos.Value.z;
                transform.position = tPos;
            }
        }
    }
}