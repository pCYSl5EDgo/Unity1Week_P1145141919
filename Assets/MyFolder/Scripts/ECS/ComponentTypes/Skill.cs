using Unity.Entities;
using Unity.Collections;
using System;

namespace Unity1Week
{
    [InternalBufferCapacity(1)]
    public struct SkillElement : IBufferElementData, IEquatable<SkillElement>
    {
        public SkillElement(uint value, float coolTime)
        {
            Value = value;
            CoolTime = coolTime;
            SinceLastTime = float.MaxValue / 10;
        }
        public readonly uint Value;
        public readonly float CoolTime;
        public float SinceLastTime;

        public bool IsActivateble => SinceLastTime >= CoolTime;

        public bool Equals(SkillElement other) => Value == other.Value;
        public static bool operator ==(SkillElement left, SkillElement other) => left.Value == other.Value;
        public static bool operator !=(SkillElement left, SkillElement other) => left.Value != other.Value;
        public override bool Equals(object obj) => obj != null && Equals((SkillElement)obj);
        public override int GetHashCode() => (int)Value;
    }
}