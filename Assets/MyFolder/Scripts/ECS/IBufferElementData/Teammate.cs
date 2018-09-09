using System;
using Unity.Entities;

namespace Unity1Week
{
    [InternalBufferCapacity(4)]
    struct Teammate : IBufferElementData, IEquatable<Teammate>
    {
        public Teammate(Entity value) => Value = value;
        public Entity Value;

        public bool Equals(Teammate other) => Value.Index == other.Value.Index && Value.Version == other.Value.Version;
        public override bool Equals(object obj) => obj != null && Value.Equals(((Teammate)obj).Value);
        public override int GetHashCode() => Value.Index;
        public static bool operator ==(Teammate left, Teammate right) => left.Value.Index == right.Value.Index && left.Value.Version == right.Value.Version;
        public static bool operator !=(Teammate left, Teammate right) => left.Value.Index != right.Value.Index || left.Value.Version != right.Value.Version;
        public static implicit operator Entity(Teammate value) => value.Value;
        public static implicit operator Teammate(Entity value) => new Teammate(value);
    }
}