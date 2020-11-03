using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;

namespace MyFolder.Scripts.ECS.Types
{
    public partial struct Enemy
    {
        public NativeArray<int> Count;

        public NativeArray<Position2D> Position;

        public NativeArray<Destination2D> Destination;

        public NativeArray<Speed2D> Speed;

        public NativeArray<AliveState> AliveState;

        [StringLiteral.Utf8Attribute("aαあ😊")]
        public static partial System.ReadOnlySpan<byte> S();

        public Enemy(int capacity8)
        {
            Count = new(1, Allocator.Persistent);

            Position = new NativeArray<Position2D>(capacity8, Allocator.Persistent);
            Destination = new NativeArray<Destination2D>(capacity8, Allocator.Persistent);
            Speed = new NativeArray<Speed2D>(capacity8, Allocator.Persistent);
            AliveState = new NativeArray<AliveState>(capacity8, Allocator.Persistent);
        }

        [BurstCompile]
        public struct SortJob : IJob
        {
            public NativeArray<int> Count;
            public NativeArray<Position2D> PositionArray;
            public NativeArray<Destination2D> DestinationArray;
            public NativeArray<Speed2D> SpeedArray;
            public NativeArray<int> AliveStateArray;

            public void Execute()
            {
                var count = Count[0];

                for (var i = 0; i < count;)
                {
                    if (AliveStateArray[i] == (int)Types.AliveState.State.Alive)
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

                    {
                        var dest = PositionArray[destIndex];
                        dest.X[destColumn][destRow] = PositionArray[srcIndex].X[srcColumn][srcRow];
                        dest.Y[destColumn][destRow] = PositionArray[srcIndex].Y[srcColumn][srcRow];
                        PositionArray[destIndex] = dest;
                    }
                    {
                        var dest = DestinationArray[destIndex];
                        dest.X[destColumn][destRow] = DestinationArray[srcIndex].X[srcColumn][srcRow];
                        dest.Y[destColumn][destRow] = DestinationArray[srcIndex].Y[srcColumn][srcRow];
                        DestinationArray[destIndex] = dest;
                    }
                    {
                        var dest = SpeedArray[destIndex];
                        dest.X[destColumn][destRow] = SpeedArray[srcIndex].X[srcColumn][srcRow];
                        dest.Y[destColumn][destRow] = SpeedArray[srcIndex].Y[srcColumn][srcRow];
                        SpeedArray[destIndex] = dest;
                    }
                    
                    AliveStateArray[i] = AliveStateArray[count];
                }
            
                Count[0] = count;
            }
        }
    }
}