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
    public sealed class PlayerTemperatureSystem : ComponentSystem
    {
        private readonly Entity player;
        private readonly Chip[] chips;
        private readonly uint2 range;
        private readonly float heatDamageRatio;
        private readonly float coolRatio;

        public PlayerTemperatureSystem(Entity player, uint2 range, Chip[] chips, float heatDamageRatio, float coolRatio)
        {
            this.player = player;
            this.chips = chips;
            this.range = range;
            this.heatDamageRatio = heatDamageRatio;
            this.coolRatio = coolRatio;
        }

        protected override void OnUpdate()
        {
            var manager = EntityManager;
            var settings = manager.GetComponentData<PlayerSettings>(player);
            var deltaTime = Time.deltaTime;
            if (!settings.IsOverHeat) goto SEA;
            settings.Life -= deltaTime * heatDamageRatio * settings.Temperature / settings.ThermalDeathPoint;
            settings.Temperature -= deltaTime;
        SEA:
            var position = manager.GetComponentData<Position>(player).Value;
            if (chips[(int)position.x + range.x * (int)position.z].Value != 1) goto SET;
            settings.Temperature = Math.Max(0, settings.Temperature - deltaTime * coolRatio);
        SET:
            manager.SetComponentData(player, settings);
        }
    }
}