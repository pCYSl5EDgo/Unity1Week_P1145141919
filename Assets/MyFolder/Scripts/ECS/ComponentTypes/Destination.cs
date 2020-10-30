using System;
using Unity.Mathematics;

namespace Unity1Week
{
    public struct Destination : IEquatable<Destination>
    {
        public Destination(float2 value)
        {
            Value = value;
        }

        public float2 Value;

        public bool Equals(Destination other)
        {
            return Value.x == other.Value.x && Value.y == other.Value.y;
        }

        public static bool operator ==(Destination left, Destination right)
        {
            return left.Value.x == right.Value.x && left.Value.y == right.Value.y;
        }

        public static bool operator !=(Destination left, Destination right)
        {
            return left.Value.x != right.Value.x || left.Value.y != right.Value.y;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return obj != null && ((Destination) obj).Value.Equals(Value);
        }
    }
}