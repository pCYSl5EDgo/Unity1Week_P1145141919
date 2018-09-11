using Unity.Entities;
using System;

namespace Unity1Week
{
    public struct ChipTemperature : IComponentData, IEquatable<ChipTemperature>
    {
        public float Value;

        public bool Equals(ChipTemperature other) => Value == other.Value;
    }
}