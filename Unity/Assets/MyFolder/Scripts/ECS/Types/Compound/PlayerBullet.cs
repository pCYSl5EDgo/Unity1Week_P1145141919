using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;

namespace MyFolder.Scripts.ECS.Types
{
    public struct PlayerBullet
    {
        public NativeArray<int> Count;

        public NativeArray<Position2D> Position;

        public NativeArray<Speed2D> Speed;

        public NativeArray<BulletState> BulletState;
        
        public PlayerBullet(int capacity8)
        {
            Count = new NativeArray<int>(1, Allocator.Persistent) { [0] = 0 };

            Position = new NativeArray<Position2D>(capacity8, Allocator.Persistent);
            Speed = new NativeArray<Speed2D>(capacity8, Allocator.Persistent);
            BulletState = new NativeArray<BulletState>(capacity8, Allocator.Persistent);
        }
        
        [BurstCompile]
        public struct SortJob : IJob
        {
            public NativeArray<int> Count;
            public NativeArray<Position2D> Position;
            public NativeArray<Speed2D> Speed;
            public NativeArray<int> BulletState;

            public void Execute()
            {
                
            }
        }
    }
}