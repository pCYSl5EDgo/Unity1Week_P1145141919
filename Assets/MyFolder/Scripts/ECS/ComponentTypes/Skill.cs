using System;

namespace Unity1Week
{
    public struct SkillElement : IEquatable<SkillElement>
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

        public bool Equals(SkillElement other)
        {
            return Value == other.Value;
        }

        public static bool operator ==(SkillElement left, SkillElement other)
        {
            return left.Value == other.Value;
        }

        public static bool operator !=(SkillElement left, SkillElement other)
        {
            return left.Value != other.Value;
        }

        public override bool Equals(object obj)
        {
            return obj != null && Equals((SkillElement) obj);
        }

        public override int GetHashCode()
        {
            return (int) Value;
        }
    }
}