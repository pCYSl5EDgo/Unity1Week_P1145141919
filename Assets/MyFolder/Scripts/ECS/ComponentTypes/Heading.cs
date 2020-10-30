using System;
using Unity.Mathematics;

namespace Unity1Week
{
    public struct Heading2D : IEquatable<Heading2D>
    {
        public Heading2D(float2 value)
        {
            Value = value;
        }

        public float2 Value;

        public bool Equals(Heading2D other)
        {
            return Value.x == other.Value.x && Value.y == other.Value.y;
        }

        public static bool operator ==(Heading2D left, Heading2D right)
        {
            return left.Value.x == right.Value.x && left.Value.y == right.Value.y;
        }

        public static bool operator !=(Heading2D left, Heading2D right)
        {
            return left.Value.x != right.Value.x || left.Value.y != right.Value.y;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return obj != null && ((Heading2D) obj).Value.Equals(Value);
        }
    }

    public struct Heading3D : IEquatable<Heading3D>
    {
        public Heading3D(float3 value)
        {
            Value = value;
        }

        public float3 Value;

        public bool Equals(Heading3D other)
        {
            return Value.x == other.Value.x && Value.y == other.Value.y && Value.z == other.Value.z;
        }

        public static bool operator ==(Heading3D left, Heading3D right)
        {
            return left.Value.x == right.Value.x && left.Value.y == right.Value.y && left.Value.z == right.Value.z;
        }

        public static bool operator !=(Heading3D left, Heading3D right)
        {
            return left.Value.x != right.Value.x || left.Value.y != right.Value.y || left.Value.z != right.Value.z;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return obj != null && ((Heading3D) obj).Value.Equals(Value);
        }
    }
}