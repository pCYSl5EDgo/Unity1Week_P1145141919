using Unity.Entities;
using Unity.Mathematics;
using System;

namespace Unity1Week
{
    struct PlayerSettings : IComponentData
    {
        public float Life;
        public int MaxLife;
        public bool IsAlive => Life > 0 && Temperature < ThermalDeathPoint;
        public float Temperature;
        public float ThermalDeathPoint;
        public bool IsOverHeat => Temperature > ThermalDeathPoint;
    }
}