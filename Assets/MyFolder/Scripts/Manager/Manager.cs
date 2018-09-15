using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Unity.Entities;
using Unity.Transforms;
using Unity.Rendering;
using System;

namespace Unity1Week
{
    [RequireComponent(typeof(Camera))]
    sealed partial class Manager : MonoBehaviour
    {
        [SerializeField] ScriptableObjects.Map mapTable;
        [SerializeField] Material unlit;
        [SerializeField] ScriptableObjects.EnemyDisplay enemyDisplay;
        [SerializeField] Material bombMaterial;
        [SerializeField] Sprite[] bombSprites;
        [SerializeField] ScriptableObjects.Speed playerSpeeds;
        [SerializeField] ScriptableObjects.Speed enemySpeeds;
        [SerializeField] AudioSource BgmSource;
        [SerializeField] AudioClip[] BgmClips;
        [SerializeField] float heatDamageRatio;
        [SerializeField] float coolRatio;
        [SerializeField] float rainCoolPower;
        [SerializeField] float rainCoolTimeSpan;
        [SerializeField] float rainCoolFrequency;
        [SerializeField] ScriptableObjects.SkillSetting snowSkillSetting;
        [SerializeField] AudioClip takenokoBulletShoot;
        [SerializeField] AudioClip takenokoBulletBurst;
        [SerializeField] AudioClip snowBurst;
        [SerializeField] AudioSource[] sources;
        [SerializeField] Sprite playerSprite;
        [SerializeField] Material playerMaterial;
        [SerializeField] Sprite kinokoHammer;
        [SerializeField] Material kinokoMaterial;
        [SerializeField] GameObject 武器欄;
        [SerializeField] ScriptableObjects.Speed stage4EnemySpeed;
        [SerializeField] GameObject respawnDisplay;
        [SerializeField] AudioSource BGMSource;
        [SerializeField] ScriptableObjects.TitleSettings titleSettings;
        [SerializeField] ScriptableObjects.Result resultSettings;
        [SerializeField] ScriptableObjects.SkillSetting[] playerSkills;

        void Start()
        {
            mainCamera = GetComponent<Camera>();
            UICamera = GameObject.Find("UI Camera").GetComponent<Camera>();
            sourceInfos = new (float, float, AudioClip)[sources.Length];
            var position = this.transform.position;
            position.x = titleSettings.Width * 0.5f;
            position.z = titleSettings.Height * 0.5f;
            this.transform.position = position;
#if UNITY_EDITOR
            Validate();
#endif
            InitializeAudio();
            InitializeWorld();
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
                for (int i = 0; i < sources.Length; i++)
                    sources[i].enabled = false;
        }
#if UNITY_EDITOR
        private void Validate()
        {
            if (mapTable.map.Length != enemySpeeds.Speeds.Length || enemySpeeds.Speeds.Length != playerSpeeds.Speeds.Length) throw new ArgumentException();
        }
#endif

        const float ThermalDeathPoint = 842f;
        private const float InitialTemperature = 97.7f;

        private void InitializeWorld()
        {
            var enemyMesh = RotateSprite(enemyDisplay.enemySprite);
            var world = World.Active = new World("default");
            world.SetDefaultCapacity(1 << 18);
            manager = world.CreateManager<EntityManager>();
            var range = new Unity.Mathematics.uint2(titleSettings.Width, titleSettings.Height);
            var decidePositionHashCodeSystem = world.CreateManager<DecidePositionHashCodeSystem>(range);
            var enemyHashCodes = decidePositionHashCodeSystem.EnemyHashCodes;
            var snowHashCodes = decidePositionHashCodeSystem.SnowBulletCodes;
            var playerBulletHashCodes = decidePositionHashCodeSystem.PlayerBulletCodes;
            var playerBulletPositionHashSet = decidePositionHashCodeSystem.PlayerBulletPositionHashCodeSet;
            var snowBulletPositionHashSet = decidePositionHashCodeSystem.SnowBulletPositionHashCodeSet;
            var chips = InitializePlane(range.x, range.y);
            InitializePlayer(range, 100, InitialTemperature, ThermalDeathPoint);
            world.CreateManager(typeof(PlayerEnemyRenderSystem), mainCamera, playerSprite, playerMaterial, enemyMesh, new Material[] { enemyDisplay.bossMaterial, enemyDisplay.leaderMaterial, enemyDisplay.subordinateMaterial });
            world.CreateManager(typeof(MoveSystem));
            world.CreateManager(typeof(EnemyBulletRenderSystem), mainCamera, snowSkillSetting.Sprites[0], snowSkillSetting.Material);
            world.CreateManager(typeof(MoveEnemySystem), player);
            world.CreateManager(typeof(EnemySnowShootSystem), player, 4);
            world.CreateManager(typeof(ConfinePlayerPositionSystem), player, range, mainCamera.transform);
            world.CreateManager(typeof(KinokoRenderSystem), mainCamera, kinokoHammer, kinokoMaterial, 120 * Math.PI / 180, 1f);
            PlayerShootSystem = world.CreateManager<PlayerShootSystem>(player, mainCamera, new Action(TryToPlayTakenokoShoot));
            var SpawnEnemySystem = InitializeSpawnEnemy(player, enemyMesh, world, range, titleSettings.LeaderCount);
            deathCounter = SpawnEnemySystem.DeathCount;
            nearToRespawn = SpawnEnemySystem.NearToRespawn;
            world.CreateManager(typeof(TakenokoEnemyHitCheckSystem), 0.16f, enemyHashCodes, playerBulletHashCodes, playerBulletPositionHashSet, new Action(TryToPlayTakenokoBurst));
            world.CreateManager(typeof(PlayerMoveSystem), player, mainCamera.transform);
            world.CreateManager(typeof(BombRenderSystem), mainCamera, bombMaterial, bombSprites, (int)takenokoBulletBurst.length);
            world.CreateManager(typeof(DestroyEnemyOutOfBoundsSystem), range);
            world.CreateManager(typeof(DecideMoveSpeedSystem), range, chips, playerSpeeds.Speeds, enemySpeeds.Speeds);
            world.CreateManager(typeof(UpdateCoolTimeSystem));
            world.CreateManager(typeof(TakenokoRenderSystem), mainCamera, playerSkills[0].Sprites[0], playerSkills[0].Material);
            world.CreateManager(typeof(BombHitCheckSystem), player, 4, enemyHashCodes);
            world.CreateManager(typeof(ChipRenderSystem), mainCamera, range, chips, mapTable.chipTemperatures, mapTable.map, unlit);
            world.CreateManager(typeof(SnowPlayerHitCheckSystem), player, snowSkillSetting.DamageRatio, deathCounter, 0.5f, snowHashCodes, playerBulletHashCodes, snowBulletPositionHashSet, new Action(TryToPlaySnowBurst));
            (this.RainSystem = world.CreateManager<RainSystem>(range, rainCoolTimeSpan, rainCoolPower, rainCoolFrequency)).Enabled = false;
            (this.EnemyPlayerCollisionSystem = world.CreateManager<EnemyPlayerCollisionSystem>(player, enemyHashCodes, 0.16f, deathCounter)).Enabled = false;
            world.CreateManager(typeof(PlayerTemperatureSystem), player, range, chips, heatDamageRatio, coolRatio);
            world.CreateManager(typeof(UtusemiRenderSystem), mainCamera, playerSkills[1].Sprites[0], playerSkills[1].Material, 15);
            ScriptBehaviourUpdateOrder.UpdatePlayerLoop(world);
        }

        private SpawnEnemySystem InitializeSpawnEnemy(Entity player, Mesh enemyMesh, World world, Unity.Mathematics.uint2 range, uint count)
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
            });

        private EntityManager manager;
        private Camera UICamera;
        private EnemyPlayerCollisionSystem EnemyPlayerCollisionSystem;
        private RainSystem RainSystem;
        private PlayerShootSystem PlayerShootSystem;

        Mesh CreateQuad()
        {
            var answer = new Mesh();
            answer.SetVertices(new List<Vector3>(4)
            {
                new Vector3(0, 0, 0),
                new Vector3(0, 0, 1),
                new Vector3(1, 0, 0),
                new Vector3(1, 0, 1),
            });
            answer.SetTriangles(new int[]{
                0, 1, 2,
                3, 2, 1,
            }, 0);
            answer.SetUVs(0, new List<Vector2>(4){
                new Vector2(0, 0),
                new Vector2(0, 1),
                new Vector2(1, 0),
                new Vector2(1, 1),
            });
            answer.RecalculateNormals();
            return answer;
        }

        Mesh RotateSprite(Sprite enemy)
        {
            var mesh = new Mesh();
            var v = enemy.vertices;
            var verts = new List<Vector3>(v.Length);
            for (int i = 0; i < v.Length; i++)
                verts.Add(new Vector3(v[i].x, 0.01f, v[i].y));
            mesh.SetVertices(verts);
            var ts = enemy.triangles;
            var triangles = new int[ts.Length];
            for (int i = 0; i < ts.Length; i++)
                triangles[i] = ts[i];
            mesh.SetTriangles(triangles, 0);
            mesh.SetUVs(0, new List<Vector2>(enemy.uv));
            mesh.RecalculateNormals();
            return mesh;
        }

        // unsafe
        Chip[] InitializePlane(uint width, uint height)
        {
            var answer = new Chip[width * height];
            var random = UnityEngine.Random.value * 5;
            int index = 0;
            for (uint y = 0; y < height; y++)
            {
                for (uint x = 0; x < width; x++)
                {
                    var perlin = Mathf.PerlinNoise(x / (float)width * random, y / (float)height * random);
                    if (perlin > 0.8f)
                        answer[index++] = new Chip { Value = 5 };
                    else if (perlin > 0.6f)
                        answer[index++] = new Chip { Value = 4 };
                    else if (perlin > 0.4f)
                        answer[index++] = new Chip { Value = 3 };
                    else if (perlin > 0.2f)
                        answer[index++] = new Chip { Value = 2 };
                    else
                        answer[index++] = new Chip { Value = 1 };
                }
            }
            return answer;
        }

        private MeshInstanceRenderer[] InitializeMeshInstanceRendererArray()
        {
            var renderers = new MeshInstanceRenderer[mapTable.map.Length];
            for (int i = 0; i < mapTable.map.Length; i++)
            {
                renderers[i] = new MeshInstanceRenderer
                {
                    castShadows = ShadowCastingMode.Off,
                    mesh = CreateQuad(),
                    material = new Material(unlit)
                    {
                        mainTexture = mapTable.map[i],
                    },
                    receiveShadows = false,
                    subMesh = 0,
                };
            }

            return renderers;
        }
    }
}