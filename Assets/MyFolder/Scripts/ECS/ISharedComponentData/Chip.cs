using Unity.Entities;
using System;

namespace Unity1Week
{
    struct Chip : ISharedComponentData, IEquatable<Chip>, IComparable<Chip>
    {
        public int Value;
        public float Temperature;
        public int CompareTo(Chip other) => Value.CompareTo(other.Value);
        public bool Equals(Chip other) => Value == other.Value && Temperature == other.Temperature;
        public override bool Equals(object obj) => obj != null && Value == ((Chip)obj).Value;
        public override int GetHashCode() => Value ^ Unity.Mathematics.math.asint(Temperature);
    }
}