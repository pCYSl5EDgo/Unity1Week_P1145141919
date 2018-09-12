using UnityEngine;

namespace Unity1Week.ScriptableObjects
{
    public class EnemyDisplay : ScriptableObject
    {
        public Sprite enemySprite;
        public Material bossMaterial, leaderMaterial, subordinateMaterial;
    }
}