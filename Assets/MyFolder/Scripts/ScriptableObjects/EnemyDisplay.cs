using UnityEngine;

namespace Unity1Week.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/EnemyDisplay", fileName = "EnemyDisplayTable")]
    public class EnemyDisplay : ScriptableObject
    {
        public Sprite enemySprite;
        public Material bossMaterial, leaderMaterial, subordinateMaterial;
    }
}