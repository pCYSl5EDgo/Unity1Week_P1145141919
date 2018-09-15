using UnityEngine;
using UnityEngine.Serialization;

namespace Unity1Week.ScriptableObjects
{
    [CreateAssetMenu]
    public sealed class SkillSetting : ScriptableObject
    {
        public string Name;
        public float CoolTime;
        [FormerlySerializedAs("DamageRatio")]
        public float UtilityNumber;
        public bool IsSingleMaterial;
        public Sprite[] Sprites;
        public Material Material;
        public Material[] Materials;
    }
}