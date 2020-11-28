namespace ComponentTypes
{
    partial struct AliveState
    {
        public partial struct Eight
        {
            public global::Unity.Mathematics.int4x2 Value;

            public void CopyToDestination(int srcColumn, int srcRow, ref Eight destination, int dstColumn, int dstRow)
            {
                {
                    var temp = destination.Value;
                    var temp2 = temp[dstColumn];
                    temp2[dstRow] = Value[srcColumn][srcRow];
                    temp[dstColumn] = temp2;
                    destination.Value = temp;
                }
            }
        }
    }
}

namespace ComponentTypes
{
    partial struct Size
    {
        public partial struct Eight
        {
            public global::Unity.Mathematics.float4x2 Value;

            public void CopyToDestination(int srcColumn, int srcRow, ref Eight destination, int dstColumn, int dstRow)
            {
                {
                    var temp = destination.Value;
                    var temp2 = temp[dstColumn];
                    temp2[dstRow] = Value[srcColumn][srcRow];
                    temp[dstColumn] = temp2;
                    destination.Value = temp;
                }
            }
        }
    }
}

namespace ComponentTypes
{
    partial struct Destination2D
    {
        public partial struct Eight
        {
            public global::Unity.Mathematics.float4x2 X;
            public global::Unity.Mathematics.float4x2 Y;

            public void CopyToDestination(int srcColumn, int srcRow, ref Eight destination, int dstColumn, int dstRow)
            {
                {
                    var temp = destination.X;
                    var temp2 = temp[dstColumn];
                    temp2[dstRow] = X[srcColumn][srcRow];
                    temp[dstColumn] = temp2;
                    destination.X = temp;
                }
                {
                    var temp = destination.Y;
                    var temp2 = temp[dstColumn];
                    temp2[dstRow] = Y[srcColumn][srcRow];
                    temp[dstColumn] = temp2;
                    destination.Y = temp;
                }
            }
        }
    }
}

namespace ComponentTypes
{
    partial struct Position2D
    {
        public partial struct Eight
        {
            public global::Unity.Mathematics.float4x2 X;
            public global::Unity.Mathematics.float4x2 Y;

            public void CopyToDestination(int srcColumn, int srcRow, ref Eight destination, int dstColumn, int dstRow)
            {
                {
                    var temp = destination.X;
                    var temp2 = temp[dstColumn];
                    temp2[dstRow] = X[srcColumn][srcRow];
                    temp[dstColumn] = temp2;
                    destination.X = temp;
                }
                {
                    var temp = destination.Y;
                    var temp2 = temp[dstColumn];
                    temp2[dstRow] = Y[srcColumn][srcRow];
                    temp[dstColumn] = temp2;
                    destination.Y = temp;
                }
            }
        }
    }
}

namespace ComponentTypes
{
    partial struct Speed2D
    {
        public partial struct Eight
        {
            public global::Unity.Mathematics.float4x2 X;
            public global::Unity.Mathematics.float4x2 Y;

            public void CopyToDestination(int srcColumn, int srcRow, ref Eight destination, int dstColumn, int dstRow)
            {
                {
                    var temp = destination.X;
                    var temp2 = temp[dstColumn];
                    temp2[dstRow] = X[srcColumn][srcRow];
                    temp[dstColumn] = temp2;
                    destination.X = temp;
                }
                {
                    var temp = destination.Y;
                    var temp2 = temp[dstColumn];
                    temp2[dstRow] = Y[srcColumn][srcRow];
                    temp[dstColumn] = temp2;
                    destination.Y = temp;
                }
            }
        }
    }
}

namespace ComponentTypes
{
    partial struct FireStartTime
    {
        public partial struct Eight
        {
            public global::Unity.Mathematics.float4x2 Value;

            public void CopyToDestination(int srcColumn, int srcRow, ref Eight destination, int dstColumn, int dstRow)
            {
                {
                    var temp = destination.Value;
                    var temp2 = temp[dstColumn];
                    temp2[dstRow] = Value[srcColumn][srcRow];
                    temp[dstColumn] = temp2;
                    destination.Value = temp;
                }
            }
        }
    }
}

namespace ComponentTypes
{
    partial struct Enemy
    {
        public partial struct Countable
        {
            public global::Unity.Collections.NativeArray<int> Count;
            public global::Unity.Collections.NativeArray<global::ComponentTypes.Position2D.Eight> PositionArray;
            public global::Unity.Collections.NativeArray<global::ComponentTypes.Destination2D.Eight> DestinationArray;
            public global::Unity.Collections.NativeArray<global::ComponentTypes.Speed2D.Eight> SpeedArray;
            public global::Unity.Collections.NativeArray<global::ComponentTypes.AliveState.Eight> IsAliveArray;

            public Countable(int count)
            {
                Count = new global::Unity.Collections.NativeArray<int>(1, global::Unity.Collections.Allocator.Persistent);
                var capacity = ((count - 1) >> 3) + 1;
                PositionArray = new global::Unity.Collections.NativeArray<global::ComponentTypes.Position2D.Eight>(capacity, global::Unity.Collections.Allocator.Persistent);
                DestinationArray = new global::Unity.Collections.NativeArray<global::ComponentTypes.Destination2D.Eight>(capacity, global::Unity.Collections.Allocator.Persistent);
                SpeedArray = new global::Unity.Collections.NativeArray<global::ComponentTypes.Speed2D.Eight>(capacity, global::Unity.Collections.Allocator.Persistent);
                IsAliveArray = new global::Unity.Collections.NativeArray<global::ComponentTypes.AliveState.Eight>(capacity, global::Unity.Collections.Allocator.Persistent);
            }

            public bool IsAlive(int index)
            {
                return IsAliveArray.Reinterpret<AliveState.State>()[index] == AliveState.State.Alive;
            }
        }

        [global::Unity.Burst.BurstCompile]
        public partial struct SortJob : global::Unity.Jobs.IJob
        {
            public Countable This;

            public void Execute()
            {
                var ____count = This.Count[0];
                for (var ____index = 0; ____index < ____count; )
                {
                    if (This.IsAlive(____index))
                    {
                        ++____index;
                        continue;
                    }

                    while (!This.IsAlive(--____count))
                    {
                        if (____index >= ____count)
                        {
                            goto END;
                        }
                    }

                    var ____srcIndex = ____index >> 3;
                    var ____srcColumn = (____index & 7) >> 2;
                    var ____srcRow = ____index & 3;
                    var ____dstIndex = ____count >> 3;
                    var ____dstColumn = (____count & 7) >> 2;
                    var ____dstRow = ____count & 3;

                    {
                        var ____dst = This.PositionArray[____dstIndex];
                        var ____src = This.PositionArray[____srcIndex];
                        ____src.CopyToDestination(____srcColumn, ____srcRow, ref ____dst, ____dstColumn, ____dstRow);
                        This.PositionArray[____dstIndex] = ____dst;
                    }
                    {
                        var ____dst = This.DestinationArray[____dstIndex];
                        var ____src = This.DestinationArray[____srcIndex];
                        ____src.CopyToDestination(____srcColumn, ____srcRow, ref ____dst, ____dstColumn, ____dstRow);
                        This.DestinationArray[____dstIndex] = ____dst;
                    }
                    {
                        var ____dst = This.SpeedArray[____dstIndex];
                        var ____src = This.SpeedArray[____srcIndex];
                        ____src.CopyToDestination(____srcColumn, ____srcRow, ref ____dst, ____dstColumn, ____dstRow);
                        This.SpeedArray[____dstIndex] = ____dst;
                    }
                    {
                        var ____dst = This.IsAliveArray[____dstIndex];
                        var ____src = This.IsAliveArray[____srcIndex];
                        ____src.CopyToDestination(____srcColumn, ____srcRow, ref ____dst, ____dstColumn, ____dstRow);
                        This.IsAliveArray[____dstIndex] = ____dst;
                    }
                }

            END:
                This.Count[0] = ____count;
            }
        }
    }
}

namespace ComponentTypes
{
    partial struct EnemyAttack
    {
        public partial struct Countable
        {
            public global::Unity.Collections.NativeArray<int> Count;
            public global::Unity.Collections.NativeArray<global::ComponentTypes.Position2D.Eight> PositionArray;
            public global::Unity.Collections.NativeArray<global::ComponentTypes.Speed2D.Eight> SpeedArray;
            public global::Unity.Collections.NativeArray<global::ComponentTypes.AliveState.Eight> IsAliveArray;

            public Countable(int count)
            {
                Count = new global::Unity.Collections.NativeArray<int>(1, global::Unity.Collections.Allocator.Persistent);
                var capacity = ((count - 1) >> 3) + 1;
                PositionArray = new global::Unity.Collections.NativeArray<global::ComponentTypes.Position2D.Eight>(capacity, global::Unity.Collections.Allocator.Persistent);
                SpeedArray = new global::Unity.Collections.NativeArray<global::ComponentTypes.Speed2D.Eight>(capacity, global::Unity.Collections.Allocator.Persistent);
                IsAliveArray = new global::Unity.Collections.NativeArray<global::ComponentTypes.AliveState.Eight>(capacity, global::Unity.Collections.Allocator.Persistent);
            }

            public bool IsAlive(int index)
            {
                return IsAliveArray.Reinterpret<AliveState.State>()[index] == AliveState.State.Alive;
            }
        }

        [global::Unity.Burst.BurstCompile]
        public partial struct SortJob : global::Unity.Jobs.IJob
        {
            public Countable This;

            public void Execute()
            {
                var ____count = This.Count[0];
                for (var ____index = 0; ____index < ____count; )
                {
                    if (This.IsAlive(____index))
                    {
                        ++____index;
                        continue;
                    }

                    while (!This.IsAlive(--____count))
                    {
                        if (____index >= ____count)
                        {
                            goto END;
                        }
                    }

                    var ____srcIndex = ____index >> 3;
                    var ____srcColumn = (____index & 7) >> 2;
                    var ____srcRow = ____index & 3;
                    var ____dstIndex = ____count >> 3;
                    var ____dstColumn = (____count & 7) >> 2;
                    var ____dstRow = ____count & 3;

                    {
                        var ____dst = This.PositionArray[____dstIndex];
                        var ____src = This.PositionArray[____srcIndex];
                        ____src.CopyToDestination(____srcColumn, ____srcRow, ref ____dst, ____dstColumn, ____dstRow);
                        This.PositionArray[____dstIndex] = ____dst;
                    }
                    {
                        var ____dst = This.SpeedArray[____dstIndex];
                        var ____src = This.SpeedArray[____srcIndex];
                        ____src.CopyToDestination(____srcColumn, ____srcRow, ref ____dst, ____dstColumn, ____dstRow);
                        This.SpeedArray[____dstIndex] = ____dst;
                    }
                    {
                        var ____dst = This.IsAliveArray[____dstIndex];
                        var ____src = This.IsAliveArray[____srcIndex];
                        ____src.CopyToDestination(____srcColumn, ____srcRow, ref ____dst, ____dstColumn, ____dstRow);
                        This.IsAliveArray[____dstIndex] = ____dst;
                    }
                }

            END:
                This.Count[0] = ____count;
            }
        }
    }
}

namespace ComponentTypes
{
    partial struct PlayerBullet
    {
        public partial struct Countable
        {
            public global::Unity.Collections.NativeArray<int> Count;
            public global::Unity.Collections.NativeArray<global::ComponentTypes.Position2D.Eight> PositionArray;
            public global::Unity.Collections.NativeArray<global::ComponentTypes.Speed2D.Eight> SpeedArray;
            public global::Unity.Collections.NativeArray<global::ComponentTypes.AliveState.Eight> IsAliveArray;

            public Countable(int count)
            {
                Count = new global::Unity.Collections.NativeArray<int>(1, global::Unity.Collections.Allocator.Persistent);
                var capacity = ((count - 1) >> 3) + 1;
                PositionArray = new global::Unity.Collections.NativeArray<global::ComponentTypes.Position2D.Eight>(capacity, global::Unity.Collections.Allocator.Persistent);
                SpeedArray = new global::Unity.Collections.NativeArray<global::ComponentTypes.Speed2D.Eight>(capacity, global::Unity.Collections.Allocator.Persistent);
                IsAliveArray = new global::Unity.Collections.NativeArray<global::ComponentTypes.AliveState.Eight>(capacity, global::Unity.Collections.Allocator.Persistent);
            }

            public bool IsAlive(int index)
            {
                return IsAliveArray.Reinterpret<AliveState.State>()[index] == AliveState.State.Alive;
            }
        }

        [global::Unity.Burst.BurstCompile]
        public partial struct SortJob : global::Unity.Jobs.IJob
        {
            public Countable This;

            public void Execute()
            {
                var ____count = This.Count[0];
                for (var ____index = 0; ____index < ____count; )
                {
                    if (This.IsAlive(____index))
                    {
                        ++____index;
                        continue;
                    }

                    while (!This.IsAlive(--____count))
                    {
                        if (____index >= ____count)
                        {
                            goto END;
                        }
                    }

                    var ____srcIndex = ____index >> 3;
                    var ____srcColumn = (____index & 7) >> 2;
                    var ____srcRow = ____index & 3;
                    var ____dstIndex = ____count >> 3;
                    var ____dstColumn = (____count & 7) >> 2;
                    var ____dstRow = ____count & 3;

                    {
                        var ____dst = This.PositionArray[____dstIndex];
                        var ____src = This.PositionArray[____srcIndex];
                        ____src.CopyToDestination(____srcColumn, ____srcRow, ref ____dst, ____dstColumn, ____dstRow);
                        This.PositionArray[____dstIndex] = ____dst;
                    }
                    {
                        var ____dst = This.SpeedArray[____dstIndex];
                        var ____src = This.SpeedArray[____srcIndex];
                        ____src.CopyToDestination(____srcColumn, ____srcRow, ref ____dst, ____dstColumn, ____dstRow);
                        This.SpeedArray[____dstIndex] = ____dst;
                    }
                    {
                        var ____dst = This.IsAliveArray[____dstIndex];
                        var ____src = This.IsAliveArray[____srcIndex];
                        ____src.CopyToDestination(____srcColumn, ____srcRow, ref ____dst, ____dstColumn, ____dstRow);
                        This.IsAliveArray[____dstIndex] = ____dst;
                    }
                }

            END:
                This.Count[0] = ____count;
            }
        }
    }
}

namespace ComponentTypes
{
    partial struct PlayerFire
    {
        public partial struct Countable
        {
            public global::Unity.Collections.NativeArray<int> Count;
            public global::Unity.Collections.NativeArray<global::ComponentTypes.Position2D.Eight> PositionArray;
            public global::Unity.Collections.NativeArray<global::ComponentTypes.Speed2D.Eight> SpeedArray;
            public global::Unity.Collections.NativeArray<global::ComponentTypes.FireStartTime.Eight> StartTimeArray;

            public Countable(int count)
            {
                Count = new global::Unity.Collections.NativeArray<int>(1, global::Unity.Collections.Allocator.Persistent);
                var capacity = ((count - 1) >> 3) + 1;
                PositionArray = new global::Unity.Collections.NativeArray<global::ComponentTypes.Position2D.Eight>(capacity, global::Unity.Collections.Allocator.Persistent);
                SpeedArray = new global::Unity.Collections.NativeArray<global::ComponentTypes.Speed2D.Eight>(capacity, global::Unity.Collections.Allocator.Persistent);
                StartTimeArray = new global::Unity.Collections.NativeArray<global::ComponentTypes.FireStartTime.Eight>(capacity, global::Unity.Collections.Allocator.Persistent);
            }

            public bool IsAlive(int index, float deadTime)
            {
                return StartTimeArray.Reinterpret<float>()[index] > deadTime;
            }
        }

        [global::Unity.Burst.BurstCompile]
        public partial struct SortJob : global::Unity.Jobs.IJob
        {
            public Countable This;
            public float DeadTime;

            public void Execute()
            {
                var ____count = This.Count[0];
                for (var ____index = 0; ____index < ____count; )
                {
                    if (This.IsAlive(____index, this.DeadTime))
                    {
                        ++____index;
                        continue;
                    }

                    while (!This.IsAlive(--____count, this.DeadTime))
                    {
                        if (____index >= ____count)
                        {
                            goto END;
                        }
                    }

                    var ____srcIndex = ____index >> 3;
                    var ____srcColumn = (____index & 7) >> 2;
                    var ____srcRow = ____index & 3;
                    var ____dstIndex = ____count >> 3;
                    var ____dstColumn = (____count & 7) >> 2;
                    var ____dstRow = ____count & 3;

                    {
                        var ____dst = This.PositionArray[____dstIndex];
                        var ____src = This.PositionArray[____srcIndex];
                        ____src.CopyToDestination(____srcColumn, ____srcRow, ref ____dst, ____dstColumn, ____dstRow);
                        This.PositionArray[____dstIndex] = ____dst;
                    }
                    {
                        var ____dst = This.SpeedArray[____dstIndex];
                        var ____src = This.SpeedArray[____srcIndex];
                        ____src.CopyToDestination(____srcColumn, ____srcRow, ref ____dst, ____dstColumn, ____dstRow);
                        This.SpeedArray[____dstIndex] = ____dst;
                    }
                    {
                        var ____dst = This.StartTimeArray[____dstIndex];
                        var ____src = This.StartTimeArray[____srcIndex];
                        ____src.CopyToDestination(____srcColumn, ____srcRow, ref ____dst, ____dstColumn, ____dstRow);
                        This.StartTimeArray[____dstIndex] = ____dst;
                    }
                }

            END:
                This.Count[0] = ____count;
            }
        }
    }
}

