using System;
using Unity.Mathematics;

namespace Unity1Week
{
    public readonly struct DeadMan : IEquatable<DeadMan>
    {
        public DeadMan(float2 value)
        {
            Value.x = value.x < 0 ? 0 : (uint) value.x;
            Value.y = value.y < 0 ? 0 : (uint) value.y;
        }

        public DeadMan(uint2 value)
        {
            Value = value;
        }

        public readonly uint2 Value;

        public bool Equals(DeadMan other)
        {
            return Value.x == other.Value.x && Value.y == other.Value.y;
        }

        public bool Equals(in DeadMan other)
        {
            return Value.x == other.Value.x && Value.y == other.Value.y;
        }

        public static bool operator ==(DeadMan left, DeadMan right)
        {
            return left.Value.x == right.Value.x && left.Value.y == right.Value.y;
        }

        public static bool operator !=(DeadMan left, DeadMan right)
        {
            return left.Value.x != right.Value.x || left.Value.y != right.Value.y;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            var other = (DeadMan) obj;
            return Value.x == other.Value.x && Value.y == other.Value.y;
        }

        public override int GetHashCode()
        {
            return (int) (Value.x ^ Value.y);
        }
    }
}