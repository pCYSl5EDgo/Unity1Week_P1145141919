using System;
using Unity.Mathematics;
using Unity.Entities;

namespace Unity1Week
{
    struct MoveSpeed : IComponentData, IEquatable<MoveSpeed>, IComparable<MoveSpeed>
    {
        public MoveSpeed(float value) => Value = value;
        public float Value;
        public int CompareTo(MoveSpeed other) => Value.CompareTo(other.Value);
        public bool Equals(MoveSpeed other) => Value == other.Value;
        public override bool Equals(object obj) => obj != null && Value == ((MoveSpeed)obj).Value;
        public override int GetHashCode() => math.asint(Value);
        public static bool operator ==(MoveSpeed left, MoveSpeed right) => left.Value == right.Value;
        public static bool operator !=(MoveSpeed left, MoveSpeed right) => left.Value != right.Value;
    }
}