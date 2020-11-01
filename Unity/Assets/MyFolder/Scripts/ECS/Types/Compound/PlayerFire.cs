using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;

namespace MyFolder.Scripts.ECS.Types
{
    public struct PlayerFire
    {
        public NativeArray<int> Count;

        public NativeArray<Position2D> Position;

        public NativeArray<Position2D> PositionSpan => Position.GetSubArray(0, ((Count[0] - 1) >> 3) + 1);
        
        public NativeArray<FireStartTime> FireStartTime;
        
        public NativeArray<FireStartTime> FireStartTimeSpan => FireStartTime.GetSubArray(0, ((Count[0] - 1) >> 3) + 1);
        
        public PlayerFire(int capacity8)
        {
            Count = new NativeArray<int>(1, Allocator.Persistent) { [0] = 0 };

            Position = new NativeArray<Position2D>(capacity8, Allocator.Persistent);
            FireStartTime = new NativeArray<FireStartTime>(capacity8, Allocator.Persistent);
        }
        
        [BurstCompile]
        public struct SortJob : IJob
        {
            public NativeArray<int> Count;

            public NativeArray<Position2D> PositionArray;

            public NativeArray<float> FireStartTimeArray;
        
            public float DeadTime;
        
            public void Execute()
            {
                var count = Count[0];

                for (var i = 0; i < count;)
                {
                    if (FireStartTimeArray[i] >= DeadTime)
                    {
                        i++;
                        continue;
                    }

                    var destIndex = ((i - 1) >> 3) + 1;
                    var destColumn = i & 7;
                    var destRow = destColumn & 3;
                    destColumn >>= 2;

                    var srcIndex = ((--count) >> 3) + 1;
                    var srcColumn = count & 7;
                    var srcRow = srcColumn & 3;
                    srcColumn >>= 2;

                    var dest = PositionArray[destIndex];
                    dest.X[destColumn][destRow] = PositionArray[srcIndex].X[srcColumn][srcRow];
                    dest.Y[destColumn][destRow] = PositionArray[srcIndex].Y[srcColumn][srcRow];
                    PositionArray[destIndex] = dest;
                    FireStartTimeArray[i] = FireStartTimeArray[count];
                }
            
                Count[0] = count;
            }
        }
    }
}