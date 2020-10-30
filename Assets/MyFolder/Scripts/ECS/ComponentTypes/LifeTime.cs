using System;
using Unity.Mathematics;

namespace Unity1Week
{
    public struct LifeTime
        : IComparable<LifeTime>,
            IEquatable<LifeTime>
    {
        public float Value;

        public int CompareTo(LifeTime other)
        {
            return Value.CompareTo(other.Value);
        }

        public bool Equals(LifeTime other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            return obj != null && Value == ((LifeTime) obj).Value;
        }

        public override int GetHashCode()
        {
            return math.asint(Value);
        }

        public static bool operator ==(LifeTime left, LifeTime right)
        {
            return left.Value == right.Value;
        }

        public static bool operator !=(LifeTime left, LifeTime right)
        {
            return left.Value != right.Value;
        }
    }
}