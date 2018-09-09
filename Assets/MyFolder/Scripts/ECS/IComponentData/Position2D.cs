using Unity.Mathematics;
using Unity.Entities;
using System;

namespace Unity1Week
{
    struct Position2D : IComponentData, IEquatable<Position2D>
    {
        public float2 Value;
        public bool Equals(Position2D other) => Value.x == other.Value.x && Value.y == other.Value.y;
        public static bool operator ==(Position2D left, Position2D right) => left.Value.x == right.Value.x && left.Value.y == right.Value.y;
        public static bool operator !=(Position2D left, Position2D right) => left.Value.x != right.Value.x || left.Value.y != right.Value.y;
        public override bool Equals(object obj) => obj != null && this.Equals((Position2D)obj);
        public override int GetHashCode() => math.asint(Value.x) ^ math.asint(Value.y);
    }
}