using System;
using UnityEngine;

namespace Unity1Week.ScriptableObjects
{
    public class EnemyDisplay : ScriptableObject
    {
        [SerializeField] public Mesh EnemyMesh;
        [SerializeField] public Material EnemyNormalMaterial;
        [SerializeField] public Material EnemyBossMaterial;
        [SerializeField] public Material EnemyLeaderMaterial;
    }
}
