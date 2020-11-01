using Unity.Collections;

namespace MyFolder.Scripts.ECS.Types
{
    public struct EnemyAttack
    {
        public NativeArray<int> Count;

        public NativeArray<Position2D> Position;
        
        public NativeArray<Speed2D> Speed;

        public NativeArray<AliveState> AliveState;
        
        public EnemyAttack(int capacity8)
        {
            Count = new NativeArray<int>(1, Allocator.Persistent) { [0] = 0 };

            Position = new NativeArray<Position2D>(capacity8, Allocator.Persistent);
            Speed = new NativeArray<Speed2D>(capacity8, Allocator.Persistent);
            AliveState = new NativeArray<AliveState>(capacity8, Allocator.Persistent);
        }
    }
}