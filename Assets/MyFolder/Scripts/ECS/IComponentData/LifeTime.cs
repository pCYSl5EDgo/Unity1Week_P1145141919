using System;
using Unity.Entities;

namespace Unity1Week
{
    struct LifeTime : IComponentData, IComparable<LifeTime>, IEquatable<LifeTime>
    {
        public float Value;

        public int CompareTo(LifeTime other) => Value.CompareTo(other.Value);

        public bool Equals(LifeTime other) => Value == other.Value;
        public override bool Equals(object obj) => obj != null && Value == ((LifeTime)obj).Value;
        public override int GetHashCode() => Unity.Mathematics.math.asint(Value);
        public static bool operator ==(LifeTime left, LifeTime right) => left.Value == right.Value;
        public static bool operator !=(LifeTime left, LifeTime right) => left.Value != right.Value;
    }
}