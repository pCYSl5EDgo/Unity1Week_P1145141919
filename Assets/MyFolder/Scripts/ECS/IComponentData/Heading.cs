using System;
using Unity.Entities;
using Unity.Mathematics;

namespace Unity1Week
{
    struct Heading2D : IComponentData, IEquatable<Heading2D>
    {
        public Heading2D(float2 value) => Value = value;
        public float2 Value;

        public bool Equals(Heading2D other) => Value.x == other.Value.x && Value.y == other.Value.y;
        public static bool operator ==(Heading2D left, Heading2D right) => left.Value.x == right.Value.x && left.Value.y == right.Value.y;
        public static bool operator !=(Heading2D left, Heading2D right) => left.Value.x != right.Value.x || left.Value.y != right.Value.y;
        public override int GetHashCode() => Value.GetHashCode();
        public override bool Equals(object obj) => obj != null && ((Heading2D)obj).Value.Equals(Value);
    }

    struct Heading3D : IComponentData, IEquatable<Heading3D>
    {
        public Heading3D(float3 value) => Value = value;
        public float3 Value;

        public bool Equals(Heading3D other) => Value.x == other.Value.x && Value.y == other.Value.y && Value.z == other.Value.z;
        public static bool operator ==(Heading3D left, Heading3D right) => left.Value.x == right.Value.x && left.Value.y == right.Value.y && left.Value.z == right.Value.z;
        public static bool operator !=(Heading3D left, Heading3D right) => left.Value.x != right.Value.x || left.Value.y != right.Value.y || left.Value.z != right.Value.z;
        public override int GetHashCode() => Value.GetHashCode();
        public override bool Equals(object obj) => obj != null && ((Heading3D)obj).Value.Equals(Value);
    }
}