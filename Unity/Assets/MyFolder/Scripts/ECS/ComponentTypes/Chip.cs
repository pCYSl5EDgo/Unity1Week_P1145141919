using System;
using Unity.Mathematics;

namespace Unity1Week
{
    public struct Chip
        : IEquatable<Chip>,
            IComparable<Chip>
    {
        public int Value;
        public float Temperature;

        public int CompareTo(Chip other)
        {
            return Value.CompareTo(other.Value);
        }

        public bool Equals(Chip other)
        {
            return Value == other.Value && Temperature == other.Temperature;
        }

        public override bool Equals(object obj)
        {
            return obj != null && Value == ((Chip) obj).Value;
        }

        public override int GetHashCode()
        {
            return Value ^ math.asint(Temperature);
        }
    }
}