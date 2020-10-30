using System;
using Unity.Mathematics;

namespace Unity1Week
{
    public struct MoveSpeed
        : IEquatable<MoveSpeed>,
            IComparable<MoveSpeed>
    {
        public MoveSpeed(float value)
        {
            Value = value;
        }

        public float Value;

        public int CompareTo(MoveSpeed other)
        {
            return Value.CompareTo(other.Value);
        }

        public bool Equals(MoveSpeed other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            return obj != null && Value == ((MoveSpeed) obj).Value;
        }

        public override int GetHashCode()
        {
            return math.asint(Value);
        }

        public static bool operator ==(MoveSpeed left, MoveSpeed right)
        {
            return left.Value == right.Value;
        }

        public static bool operator !=(MoveSpeed left, MoveSpeed right)
        {
            return left.Value != right.Value;
        }
    }
}