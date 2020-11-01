using System;

namespace Unity1Week
{
    public struct ChipTemperature : IEquatable<ChipTemperature>
    {
        public float Value;

        public bool Equals(ChipTemperature other)
        {
            return Value == other.Value;
        }
    }
}