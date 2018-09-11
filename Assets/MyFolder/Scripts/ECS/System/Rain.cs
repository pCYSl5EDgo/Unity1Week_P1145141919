using Unity.Entities;
using Unity.Mathematics;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Transforms;
using Unity.Rendering;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;


namespace Unity1Week
{
    [AlwaysUpdateSystem]
    public sealed class RainSystem : ComponentSystem
    {
        private readonly uint2 range;
        private readonly float timeSpan;
        private readonly float rainCoolPower;
        private readonly float frequency;
        private EntityArchetype archetype;
        private EntityManager manager;
        private float sinceLast;

        public RainSystem(uint2 range, float timeSpan, float rainCoolPower, float frequency)
        {
            this.range = range;
            this.timeSpan = timeSpan;
            this.rainCoolPower = rainCoolPower;
            this.frequency = frequency;
        }

        protected override void OnCreateManager(int capacity)
        {
            archetype = (manager = EntityManager).CreateArchetype(ComponentType.Create<ChipRenderSystem.Tag>());
        }

        protected override void OnUpdate()
        {
            sinceLast += Time.deltaTime;
            if (sinceLast <= timeSpan) return;
            sinceLast = 0;
            for (int y = 0; y < range.y; y++)
            {
                for (int x = 0; x < range.x; x++)
                {
                    if (UnityEngine.Random.value > frequency) continue;
                    manager.SetComponentData(manager.CreateEntity(archetype), new ChipRenderSystem.Tag
                    {
                        X = x,
                        Y = y,
                        TemperatureDelta = rainCoolPower,
                    });
                }
            }
        }
    }
}