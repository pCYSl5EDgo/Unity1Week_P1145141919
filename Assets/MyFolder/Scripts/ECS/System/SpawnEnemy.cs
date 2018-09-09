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
    sealed class SpawnEnemySystem : ComponentSystem
    {
        public SpawnEnemySystem(uint spawnLimit, uint2 range, MeshInstanceRenderer bossMesh, MeshInstanceRenderer leaderMesh, MeshInstanceRenderer subordinateMesh, float skillCoolTime)
        {
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
            archetypeBoss = EntityManager.CreateArchetype(ComponentType.Create<Boss>(), ComponentType.Create<Position>(), ComponentType.Create<MeshInstanceRenderer>(), ComponentType.Create<Enemy>(), ComponentType.Create<Destination>(), ComponentType.Create<MoveSpeed>(), ComponentType.Create<Heading2D>(), ComponentType.Create<SkillElement>());
            archetypeLeader = EntityManager.CreateArchetype(ComponentType.Create<Teammate>(), ComponentType.Create<Position>(), ComponentType.Create<MeshInstanceRenderer>(), ComponentType.Create<Enemy>(), ComponentType.Create<Destination>(), ComponentType.Create<MoveSpeed>(), ComponentType.Create<Heading2D>(), ComponentType.Create<SkillElement>());
            archetypeSubordinate = EntityManager.CreateArchetype(ComponentType.Create<Position>(), ComponentType.Create<MeshInstanceRenderer>(), ComponentType.Create<Enemy>(), ComponentType.Create<Destination>(), ComponentType.Create<MoveSpeed>(), ComponentType.Create<Heading2D>(), ComponentType.Create<SkillElement>());
            gDead = GetComponentGroup(ComponentType.ReadOnly<DeadMan>());
            gAlive = GetComponentGroup(ComponentType.ReadOnly<Position>(), ComponentType.ReadOnly<MeshInstanceRenderer>(), ComponentType.ReadOnly<Enemy>());
            gLeader = GetComponentGroup(ComponentType.ReadOnly<Teammate>());
        }

        private readonly uint respwan;
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
        private uint aliveCount;

        protected override unsafe void OnUpdate()
        {
            DeathCount.Value += (uint)gDead.CalculateLength();
            var manager = EntityManager;
            manager.DestroyEntity(gDead);
            if (DeathCount.Value >= 114514u) return;
            if ((aliveCount = (uint)gAlive.CalculateLength()) >= spawnLimit && gLeader.CalculateLength() >= respwan) return;
            float3 _range = new float3(range.x - 0.0001f, 0, range.y - 0.0001f);
            var leaders = new NativeArray<Entity>((int)spawnLimit, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
            var subordinates = new NativeArray<Entity>(4 * leaders.Length, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
            try
            {
                leaders[0] = manager.CreateEntity(archetypeLeader);
                manager.SetSharedComponentData(leaders[0], leaderMesh);
                manager.GetBuffer<SkillElement>(leaders[0]).Add(new SkillElement(2, skillCoolTime));
                manager.Instantiate(leaders[0], leaders.SkipFirst());
                for (int i = 0; i < leaders.Length; i++)
                {
                    manager.SetComponentData(leaders[i], new Position { Value = random.NextFloat3(default, new float3(range.x, 0, range.y)) });
                    manager.SetComponentData(leaders[i], new Heading2D { Value = random.NextFloat2Direction() });
                    random = new Random(random.state + 1);
                }
                subordinates[0] = manager.CreateEntity(archetypeSubordinate);
                manager.SetSharedComponentData(subordinates[0], subordinateMesh);
                manager.GetBuffer<SkillElement>(subordinates[0]).Add(new SkillElement(2, skillCoolTime));
                manager.Instantiate(subordinates[0], subordinates.SkipFirst());
                for (int i = 0; i < leaders.Length; i++)
                {
                    var buf = manager.GetBuffer<Teammate>(leaders[i]);
                    var pos = manager.GetComponentData<Position>(leaders[i]).Value;
                    var index = i << 2;
                    SetComponentData(ref subordinates, _range, manager, index, pos);
                    buf.Add(subordinates[index++]);
                    SetComponentData(ref subordinates, _range, manager, index, pos);
                    random = new Random(random.state + 1);
                    buf.Add(subordinates[index++]);
                    SetComponentData(ref subordinates, _range, manager, index, pos);
                    buf.Add(subordinates[index++]);
                    SetComponentData(ref subordinates, _range, manager, index, pos);
                    buf.Add(subordinates[index]);
                }
            }
            finally
            {
                leaders.Dispose();
                subordinates.Dispose();
            }
        }

        private unsafe void SetComponentData(ref NativeArray<Entity> subordinates, in float3 _range, EntityManager manager, int i, in float3 pos)
        {
            manager.SetComponentData(subordinates[i], new Position
            {
                Value = math.clamp(pos + random.NextFloat3(new float3(-5f, 0, -5f), new float3(5f, 0, 5f)), 0, _range)
            });
            manager.SetComponentData(subordinates[i], new Heading2D { Value = random.NextFloat2Direction() });
            random = new Random(random.state + 1);
        }
    }
}