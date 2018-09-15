using UnityEngine;
using UnityEngine.Serialization;

namespace Unity1Week.ScriptableObjects
{
    public sealed class SkillSetting : ScriptableObject
    {
        public string Name;
        public float CoolTime;
        public float DamageRatio;
        public bool IsSingleMaterial;
        public Sprite[] Sprites;
        public Material Material;
        public Material[] Materials;
    }
}