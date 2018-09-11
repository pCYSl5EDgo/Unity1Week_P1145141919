using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Rendering;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace Unity1Week
{
    [AlwaysUpdateSystem]
    [UpdateBefore(typeof(MoveSystem))]
    public sealed class SpawnEnemySystem : ComponentSystem
    {
        public SpawnEnemySystem(Entity player, uint spawnLimit, uint2 range, float skillCoolTime, MeshInstanceRenderer bossMesh, MeshInstanceRenderer leaderMesh, MeshInstanceRenderer subordinateMesh)
        {
            this.player = player;
            this.spawnLimit = spawnLimit;
            this.respwan = (uint)(spawnLimit * 0.2f);
            this.range = range;
            this.bossMesh = bossMesh;
            this.leaderMesh = leaderMesh;
            this.subordinateMesh = subordinateMesh;
            this.skillCoolTime = skillCoolTime;
        }
        protected override void OnCreateManager(int capacity)
        {
            archetypeBoss = EntityManager.CreateArchetype(ComponentType.Create<Boss>(), ComponentType.Create<Position>(), ComponentType.Create<MeshInstanceRenderer>(), ComponentType.Create<Enemy>(), ComponentType.Create<SkillElement>(), ComponentType.Create<DestroyEnemyOutOfBoundsSystem.Tag>());
            archetypeLeader = EntityManager.CreateArchetype(ComponentType.Create<Teammate>(), ComponentType.Create<Position>(), ComponentType.Create<MeshInstanceRenderer>(), ComponentType.Create<Enemy>(), ComponentType.Create<MoveSpeed>(), ComponentType.Create<Heading2D>(), ComponentType.Create<DestroyEnemyOutOfBoundsSystem.Tag>());
            archetypeSubordinate = EntityManager.CreateArchetype(ComponentType.Create<Position>(), ComponentType.Create<MeshInstanceRenderer>(), ComponentType.Create<Enemy>(), ComponentType.Create<MoveSpeed>(), ComponentType.Create<Heading2D>(), ComponentType.Create<DestroyEnemyOutOfBoundsSystem.Tag>());
            gDead = GetComponentGroup(ComponentType.ReadOnly<DeadMan>());
            gAlive = GetComponentGroup(ComponentType.ReadOnly<Position>(), ComponentType.ReadOnly<MeshInstanceRenderer>(), ComponentType.ReadOnly<Enemy>());
            gLeader = GetComponentGroup(ComponentType.ReadOnly<Teammate>());
        }

        private const float DISTANCE = 0.3f;
        private readonly uint respwan;
        private readonly Entity player;
        private readonly uint spawnLimit;
        private readonly uint2 range;
        private EntityArchetype archetypeBoss, archetypeLeader, archetypeSubordinate;
        private readonly MeshInstanceRenderer bossMesh;
        private readonly MeshInstanceRenderer leaderMesh;
        private Random random = new Random((uint)((ulong)System.DateTime.Now.Ticks >> 32) ^ (uint)((ulong)System.DateTime.Now.Ticks));
        private readonly MeshInstanceRenderer subordinateMesh;
        private readonly float skillCoolTime;
        ComponentGroup gDead, gAlive, gLeader;
        public UniRx.ReactiveProperty<uint> DeathCount { get; } = new UniRx.ReactiveProperty<uint>(0);
        public UniRx.BoolReactiveProperty NearToRespawn { get; } = new UniRx.BoolReactiveProperty(true);
        private uint aliveCount;
        private uint spawnTime = 1;

        protected override void OnUpdate()
        {
            DeathCount.Value += (uint)gDead.CalculateLength();
            var manager = EntityManager;
            manager.DestroyEntity(gDead);
            if (DeathCount.Value >= 114514u) return;
            SpawnBoss(manager, DeathCount.Value >> 10);
            SpawnLeaders(manager);
        }

        void SpawnBoss(EntityManager manager, uint spawnCount)
        {
            if (spawnCount < spawnTime) return;
            spawnTime = spawnCount + 1;
            var bosses = new NativeArray<Entity>((int)spawnCount << 1, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
            try
            {
                bosses[0] = manager.CreateEntity(archetypeBoss);
                manager.SetSharedComponentData(bosses[0], bossMesh);
                manager.GetBuffer<SkillElement>(bosses[0]).Add(new SkillElement(2, skillCoolTime));
                manager.Instantiate(bosses[0], bosses.SkipFirst());
                for (int i = 0; i < bosses.Length; i++)
                {
                    manager.SetComponentData(bosses[i], new Position { Value = random.NextFloat3(default, new float3(range.x, 0, range.y)) });
                    random = new Random(random.state + 1);
                }
            }
            finally
            {
                bosses.Dispose();
            }
        }

        unsafe void SpawnLeaders(EntityManager manager)
        {
            aliveCount = (uint)gAlive.CalculateLength();
            var diff0 = aliveCount - spawnLimit;
            var diff1 = gLeader.CalculateLength() - respwan;
            NearToRespawn.Value = diff1 < 0.1f * respwan || diff0 < 0.1 * respwan;
            if (diff0 >= 0 && diff1 >= 0) return;
            NearToRespawn.Value = false;
            var playerPosition = manager.GetComponentData<Position>(player).Value;
            var _range = new float3(range.x - 0.0001f, 0, range.y - 0.0001f);
            var leaders = new NativeArray<Entity>((int)spawnLimit, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
            var subordinates = new NativeArray<Entity>(4 * leaders.Length, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
            try
            {
                leaders[0] = manager.CreateEntity(archetypeLeader);
                manager.SetSharedComponentData(leaders[0], leaderMesh);
                manager.Instantiate(leaders[0], leaders.SkipFirst());
                for (int i = 0; i < leaders.Length; i++)
                {
                    float3 float31;
                    do
                    {
                        float31 = random.NextFloat3(default, new float3(range.x, 0, range.y));
                    }
                    while (System.Math.Abs(float31.x - playerPosition.x) <= DISTANCE && System.Math.Abs(float31.z - playerPosition.z) <= DISTANCE);
                    manager.SetComponentData(leaders[i], new Position { Value = float31 });
                    manager.SetComponentData(leaders[i], new Heading2D { Value = random.NextFloat2Direction() });
                    random = new Random(random.state + 1);
                }
                subordinates[0] = manager.CreateEntity(archetypeSubordinate);
                manager.SetSharedComponentData(subordinates[0], subordinateMesh);
                manager.Instantiate(subordinates[0], subordinates.SkipFirst());
                for (int i = 0; i < leaders.Length; i++)
                {
                    var buf = manager.GetBuffer<Teammate>(leaders[i]);
                    var pos = manager.GetComponentData<Position>(leaders[i]).Value;
                    var index = i << 2;
                    SetComponentData(in playerPosition, ref subordinates, _range, manager, index, pos);
                    buf.Add(subordinates[index++]);
                    SetComponentData(in playerPosition, ref subordinates, _range, manager, index, pos);
                    random = new Random(random.state + 1);
                    buf.Add(subordinates[index++]);
                    SetComponentData(in playerPosition, ref subordinates, _range, manager, index, pos);
                    buf.Add(subordinates[index++]);
                    SetComponentData(in playerPosition, ref subordinates, _range, manager, index, pos);
                    buf.Add(subordinates[index]);
                }
            }
            finally
            {
                leaders.Dispose();
                subordinates.Dispose();
            }
        }

        private unsafe void SetComponentData(in float3 playerPosition, ref NativeArray<Entity> subordinates, in float3 _range, EntityManager manager, int i, in float3 pos)
        {
            float3 float31;
            do
            {
                float31 = math.clamp(pos + random.NextFloat3(new float3(-5f, 0, -5f), new float3(5f, 0, 5f)), 0, _range);
            }
            while (System.Math.Abs(float31.x - playerPosition.x) <= DISTANCE && System.Math.Abs(float31.z - playerPosition.z) <= DISTANCE);
            manager.SetComponentData(subordinates[i], new Position
            {
                Value = float31
            });
            manager.SetComponentData(subordinates[i], new Heading2D { Value = random.NextFloat2Direction() });
            random = new Random(random.state + 1);
        }
    }
}