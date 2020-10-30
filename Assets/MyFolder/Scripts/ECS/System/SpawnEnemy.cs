/*using Unity.Mathematics;
using Unity.Collections;

namespace Unity1Week
{
    public sealed class SpawnEnemySystem
    {
        const int DIVISION = 10;
        const int ONCE = 100;
        public SpawnEnemySystem(Entity player, uint clearCount, uint spawnLimit, uint2 range, float skillCoolTime, MeshInstanceRenderer bossMesh, MeshInstanceRenderer leaderMesh, MeshInstanceRenderer subordinateMesh)
        {
            this.player = player;
            this.clearCount = clearCount;
            this.spawnLimit = spawnLimit;
            if (clearCount >= ONCE * DIVISION)
            {
                this.repetitionCount = spawnLimit / DIVISION / ONCE;
                this.leaders = new NativeArray<Entity>(ONCE, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
                int length = ((int)(spawnLimit / DIVISION)) % ONCE;
                this.leadersRest = length == 0 ? default : new NativeArray<Entity>(length, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
                var restLength = (int)(spawnLimit - repetitionCount * this.leaders.Length + this.leadersRest.Length);
                this.leadersRestRest = restLength == 0 ? default : new NativeArray<Entity>(restLength, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
            }
            else
            {
                this.repetitionCount = 1u;
                this.leaders = new NativeArray<Entity>((int)clearCount, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
                this.leadersRest = this.leadersRestRest = default;
            }
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
            archetypeLeader = EntityManager.CreateArchetype(ComponentType.Create<Leader>(), ComponentType.Create<Position>(), ComponentType.Create<MeshInstanceRenderer>(), ComponentType.Create<Enemy>(), ComponentType.Create<MoveSpeed>(), ComponentType.Create<Heading2D>(), ComponentType.Create<DestroyEnemyOutOfBoundsSystem.Tag>());
            archetypeSubordinate = EntityManager.CreateArchetype(ComponentType.Create<Position>(), ComponentType.Create<MeshInstanceRenderer>(), ComponentType.Create<Enemy>(), ComponentType.Create<MoveSpeed>(), ComponentType.Create<Heading2D>(), ComponentType.Create<DestroyEnemyOutOfBoundsSystem.Tag>());
            gDead = GetComponentGroup(ComponentType.ReadOnly<DeadMan>());
            gAlive = GetComponentGroup(ComponentType.ReadOnly<Position>(), ComponentType.ReadOnly<MeshInstanceRenderer>(), ComponentType.ReadOnly<Enemy>());
            gLeader = GetComponentGroup(ComponentType.ReadOnly<Leader>());
        }
        protected override void OnDestroyManager()
        {
            if (leaders.IsCreated)
                leaders.Dispose();
            if (leadersRest.IsCreated)
                leadersRest.Dispose();
            if (leadersRestRest.IsCreated)
                leadersRestRest.Dispose();
        }

        private const float DISTANCE = 0.6f;
        private const int SUBORDINATE_REPETITION = 4;
        private readonly uint repetitionCount;
        private readonly uint respwan;
        private readonly Entity player;
        private readonly uint clearCount;
        private readonly uint spawnLimit;
        private NativeArray<Entity> leaders;
        private NativeArray<Entity> leadersRest;
        private NativeArray<Entity> leadersRestRest;
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
        private int frame = -1;

        protected override void OnUpdate()
        {
            DeathCount.Value += (uint)gDead.CalculateLength();
            var manager = EntityManager;
            manager.DestroyEntity(gDead);
            if (DeathCount.Value >= clearCount) return;
            SpawnBoss(manager, DeathCount.Value >> 10);
            SpawnLeaders(manager);
        }

        void SpawnBoss(EntityManager manager, uint spawnCount)
        {
            if (spawnCount < spawnTime) return;
            spawnTime = spawnCount + 1;
            var bosses = new NativeArray<Entity>(System.Math.Max((int)(spawnCount * 0.75f), 2), Allocator.Temp, NativeArrayOptions.UninitializedMemory);
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
            if (frame == -1)
            {
                NearToRespawn.Value = diff1 < 0.1f * respwan || diff0 < 0.1 * respwan;
                if (diff0 >= 0 && diff1 >= 0) return;
                NearToRespawn.Value = false;
                frame = 0;
            }
            var playerPosition = manager.GetComponentData<Position>(player).Value;
            var _range = new float3(range.x - 0.0001f, 0, range.y - 0.0001f);
            if (frame++ == DIVISION)
            {
                frame = -1;
                if (leadersRestRest.Length == 0) return;
                Spawn_Inner(archetypeLeader, leadersRestRest, manager, playerPosition);
                for (int i = 0; i < SUBORDINATE_REPETITION; i++)
                {
                    Spawn_Inner(archetypeSubordinate, leadersRestRest, manager, playerPosition);
                }
            }
            else
            {
                for (uint i = 0; i < repetitionCount; i++)
                {
                    Spawn_Inner(archetypeLeader, leaders, manager, playerPosition);
                    for (int j = 0; j < SUBORDINATE_REPETITION; j++)
                        Spawn_Inner(archetypeSubordinate, leaders, manager, playerPosition);
                }
                if (leadersRest.Length == 0) return;
                Spawn_Inner(archetypeLeader, leadersRest, manager, playerPosition);
                for (int j = 0; j < SUBORDINATE_REPETITION; j++)
                    Spawn_Inner(archetypeSubordinate, leadersRest, manager, playerPosition);
            }
        }


        private void Spawn_Inner(EntityArchetype archetype, NativeArray<Entity> entities, EntityManager manager, float3 playerPosition)
        {
            entities[0] = manager.CreateEntity(archetype);
            manager.SetSharedComponentData(entities[0], leaderMesh);
            manager.Instantiate(entities[0], entities.SkipFirst());
            for (int i = 0; i < entities.Length; i++)
            {
                float3 float31;
                do
                {
                    float31 = random.NextFloat3(default, new float3(range.x, 0, range.y));
                }
                while (System.Math.Abs(float31.x - playerPosition.x) <= DISTANCE && System.Math.Abs(float31.z - playerPosition.z) <= DISTANCE);
                manager.SetComponentData(entities[i], new Position { Value = float31 });
                manager.SetComponentData(entities[i], new Heading2D { Value = random.NextFloat2Direction() });
                random = new Random(random.state + 1);
            }
        }

        private void SetComponentData(in float3 playerPosition, ref NativeArray<Entity> subordinates, in float3 _range, EntityManager manager, int i, in float3 pos)
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
}*/

