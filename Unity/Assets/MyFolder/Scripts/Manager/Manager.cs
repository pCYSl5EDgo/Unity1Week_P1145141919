using System;
using System.Collections.Generic;
using Unity1Week.ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Unity1Week
{
    [RequireComponent(typeof(Camera))]
    internal sealed partial class Manager : MonoBehaviour
    {
        [SerializeField] private Map mapTable;
        [SerializeField] private Material unlit;
        [SerializeField] private EnemyDisplay enemyDisplay;
        [SerializeField] private Speed playerSpeeds;
        [SerializeField] private Speed enemySpeeds;
        [SerializeField] private AudioSource BgmSource;
        [SerializeField] private AudioClip[] BgmClips;
        [SerializeField] private float heatDamageRatio;
        [SerializeField] private float coolRatio;
        [SerializeField] private float rainCoolPower;
        [SerializeField] private float rainCoolTimeSpan;
        [SerializeField] private float rainCoolFrequency;
        [SerializeField] private SkillSetting snowSkillSetting;
        [SerializeField] private AudioClip takenokoBulletShoot;
        [SerializeField] private AudioClip takenokoBulletBurst;
        [SerializeField] private AudioClip snowBurst;
        [SerializeField] private AudioSource[] sources;
        [SerializeField] private Sprite playerSprite;
        [SerializeField] private Material playerMaterial;
        [SerializeField] private Sprite kinokoHammer;
        [SerializeField] private Material kinokoMaterial;
        [SerializeField] private GameObject 武器欄;
        [SerializeField] private Speed stage4EnemySpeed;
        [SerializeField] private GameObject respawnDisplay;
        [SerializeField] private AudioSource BGMSource;
        [SerializeField] private TitleSettings titleSettings;
        [SerializeField] private Result resultSettings;
        [SerializeField] private SkillSetting[] playerSkills;
        [SerializeField] private SkillSetting bombSkillEffect;

        private void Start()
        {
            mainCamera = GetComponent<Camera>();
            UICamera = GameObject.Find("UI Camera").GetComponent<Camera>();
            sourceInfos = new (float, float, AudioClip)[sources.Length];
            var position = transform.position;
            position.x = titleSettings.Width * 0.5f;
            position.z = titleSettings.Height * 0.5f;
            transform.position = position;
            #if UNITY_EDITOR
            Validate();
            #endif
            InitializeAudio();
            InitializeUGUI();
            InitializeBGM();
            InitializeUniRx();
            InitializeStageWatch();
            InitializeGameOverUI();
        }

        private void InitializeAudio()
        {
            if (!titleSettings.IsBgmOn)
                BgmSource.enabled = false;
            if (!titleSettings.IsSEOn)
                for (var i = 0; i < sources.Length; i++)
                    sources[i].enabled = false;
        }
        #if UNITY_EDITOR
        private void Validate()
        {
            if (mapTable.ChipTemperatures.Length != enemySpeeds.Speeds.Length || enemySpeeds.Speeds.Length != playerSpeeds.Speeds.Length) throw new ArgumentException();
        }
        #endif

        private const float ThermalDeathPoint = 842f;
        private const float InitialTemperature = 97.7f;

        /*private ScriptBehaviourManager InitializeAnimationRenderSystem(World world, params float[] times)
        {
            var pos2d = ComponentType.Create<Position2D>();
            var lifeTime = ComponentType.Create<LifeTime>();
            var array = new (EntityArchetypeQuery, Sprite[], Material, float)[1];
            array[0] = (
                    new EntityArchetypeQuery
                    {
                        None = Array.Empty<ComponentType>(),
                        Any = Array.Empty<ComponentType>(),
                        All = new[] { pos2d, lifeTime, ComponentType.Create<BombEffect>() }
                    }, bombSkillEffect.Sprites, bombSkillEffect.Material, times[0]
                );
            return world.CreateManager(typeof(AnimationSkillRenderSystem), mainCamera, array);
        }*/

        /*private SpawnEnemySystem InitializeSpawnEnemy(Entity player, Mesh enemyMesh, World world, Unity.Mathematics.uint2 range, uint count)
        => world.CreateManager<SpawnEnemySystem>(player, titleSettings.ClearKillScore, count, range, snowSkillSetting.CoolTime,
            new MeshInstanceRenderer
            {
                mesh = enemyMesh,
                material = enemyDisplay.bossMaterial,
                castShadows = ShadowCastingMode.Off,
                receiveShadows = false,
                subMesh = 0,
            },
            new MeshInstanceRenderer
            {
                mesh = enemyMesh,
                material = enemyDisplay.leaderMaterial,
                castShadows = ShadowCastingMode.Off,
                receiveShadows = false,
                subMesh = 0,
            },
            new MeshInstanceRenderer
            {
                mesh = enemyMesh,
                material = enemyDisplay.subordinateMaterial,
                castShadows = ShadowCastingMode.Off,
                receiveShadows = false,
                subMesh = 0,
            });*/

        private Camera UICamera;
        /*private EntityManager manager;
         private EnemyPlayerCollisionSystem EnemyPlayerCollisionSystem;
        private RainSystem RainSystem;
        private PlayerShootSystem PlayerShootSystem;*/

        private Mesh CreateQuad()
        {
            var answer = new Mesh();
            answer.SetVertices(new List<Vector3>(4)
            {
                new Vector3(0, 0, 0),
                new Vector3(0, 0, 1),
                new Vector3(1, 0, 0),
                new Vector3(1, 0, 1)
            });
            answer.SetTriangles(new[]
            {
                0, 1, 2,
                3, 2, 1
            }, 0);
            answer.SetUVs(0, new List<Vector2>(4)
            {
                new Vector2(0, 0),
                new Vector2(0, 1),
                new Vector2(1, 0),
                new Vector2(1, 1)
            });
            answer.RecalculateNormals();
            return answer;
        }

        private Mesh RotateSprite(Sprite enemy)
        {
            var mesh = new Mesh();
            var v = enemy.vertices;
            var verts = new List<Vector3>(v.Length);
            for (var i = 0; i < v.Length; i++)
                verts.Add(new Vector3(v[i].x, 0.01f, v[i].y));
            mesh.SetVertices(verts);
            var ts = enemy.triangles;
            var triangles = new int[ts.Length];
            for (var i = 0; i < ts.Length; i++)
                triangles[i] = ts[i];
            mesh.SetTriangles(triangles, 0);
            mesh.SetUVs(0, new List<Vector2>(enemy.uv));
            mesh.RecalculateNormals();
            return mesh;
        }
    }
}
