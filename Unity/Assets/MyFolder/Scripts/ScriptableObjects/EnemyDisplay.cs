using System;
using UnityEngine;

namespace Unity1Week.ScriptableObjects
{
    public class EnemyDisplay : ScriptableObject
    {
        [SerializeField] public Mesh EnemyMesh;
        [SerializeField] public Material EnemyMaterial;
        [SerializeField] public Texture2D NormalTexture;
        [SerializeField] public Texture2D LeaderTexture;
        [SerializeField] public Texture2D BossTexture;
        
        [field:NonSerialized] public MaterialPropertyBlock NormalBlock { get; private set; }
        [field:NonSerialized] public MaterialPropertyBlock LeaderBlock { get; private set; }
        [field:NonSerialized] public MaterialPropertyBlock BossBlock { get; private set; }
        
        private static readonly int mainTex = Shader.PropertyToID("_MainTex");

        public void Initialize()
        {
            NormalBlock ??= new MaterialPropertyBlock();
            NormalBlock.SetTexture(mainTex, NormalTexture);
            LeaderBlock ??= new MaterialPropertyBlock();
            LeaderBlock.SetTexture(mainTex, LeaderTexture);
            BossBlock ??= new MaterialPropertyBlock();
            BossBlock.SetTexture(mainTex, BossTexture);
        }
    }
}
