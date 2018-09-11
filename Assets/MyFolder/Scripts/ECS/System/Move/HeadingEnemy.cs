using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Unity1Week
{
    [UpdateAfter(typeof(MoveSystem))]
    public sealed class MoveEnemySystem : ComponentSystem
    {
        public MoveEnemySystem(Entity player)
        {
            this.player = player;
        }
        protected override void OnCreateManager(int capacity)
        {
            gLeader = GetComponentGroup(ComponentType.ReadOnly<Position>(), ComponentType.ReadOnly<Teammate>(), ComponentType.Create<Heading2D>());
            gSubordinate = GetComponentGroup(ComponentType.ReadOnly<Position>(), ComponentType.ReadOnly<Enemy>(), ComponentType.Subtractive<Teammate>(), ComponentType.Create<Heading2D>());
            g空蝉 = GetComponentGroup(ComponentType.ReadOnly<Position>(), ComponentType.ReadOnly<PlayerShootSystem.空蝉Tag>());
        }
        private ComponentGroup gLeader, gSubordinate, g空蝉;
        private readonly Entity player;
        private float2 lastPos;
        private int zx;
        private int count;

        protected override void OnUpdate()
        {
            if (player.Equals(default)) return;
            if (++count == 500)
                count = 0;
            var manager = EntityManager;
            if (count == 499)
            {
                var rand = new Random((uint)System.DateTime.Now.Ticks);
                var heads = gSubordinate.GetComponentDataArray<Heading2D>();
                for (int i = 0; i < heads.Length; ++i, rand = new Random(rand.state + 1))
                    heads[i] = new Heading2D(rand.NextFloat2Direction());
            }
            if (++zx == 100)
                zx = 0;
            if (zx != 0) return;
            var 空蝉s = g空蝉.GetComponentDataArray<Position>();
            var pos = 空蝉s.Length == 0 ? manager.GetComponentData<Position>(player).Value : 空蝉s[0].Value;
            lastPos = new float2(pos.x, pos.z);
            var buf = PostUpdateCommands;
            var positions = gLeader.GetComponentDataArray<Position>();
            var headings = gLeader.GetComponentDataArray<Heading2D>();
            for (int i = 0; i < positions.Length; i++)
            {
                var head = math.normalize(pos - positions[i].Value);
                headings[i] = new Heading2D(new float2(head.x, head.z));
            }
        }
    }
}