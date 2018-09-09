using System;
using Unity.Entities;
using Unity.Mathematics;

namespace Unity1Week
{
    struct Destination : IComponentData, IEquatable<Destination>
    {
		public Destination(float2 value) => Value = value;
        public float2 Value;

        public bool Equals(Destination other) => Value.x == other.Value.x && Value.y == other.Value.y;
        public static bool operator ==(Destination left, Destination right) => left.Value.x == right.Value.x && left.Value.y == right.Value.y;
        public static bool operator !=(Destination left, Destination right) => left.Value.x != right.Value.x || left.Value.y != right.Value.y;
        public override int GetHashCode() => Value.GetHashCode();
        public override bool Equals(object obj) => obj != null && ((Destination)obj).Value.Equals(Value);
    }
}