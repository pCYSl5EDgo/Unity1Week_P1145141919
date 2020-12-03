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
        public partial struct Countable : global::System.IDisposable
        {
            public global::Unity.Collections.NativeArray<int> Count;
            public global::Unity.Collections.NativeArray<global::ComponentTypes.Position2D.Eight> PositionArray;
            public global::Unity.Collections.NativeArray<global::ComponentTypes.Destination2D.Eight> DestinationArray;
            public global::Unity.Collections.NativeArray<global::ComponentTypes.Speed2D.Eight> SpeedArray;
            public global::Unity.Collections.NativeArray<global::ComponentTypes.AliveState.Eight> IsAliveArray;

            public int Capacity => PositionArray.Length;

            public int ChunkCount => (Count[0] + 7) >> 3;

            public unsafe void EnsureCapacity(int newCapacity)
            {
                if (Capacity >= newCapacity) return;

                {
                    var pointer = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.Malloc(sizeof(global::ComponentTypes.Position2D.Eight) * newCapacity, 32, global::Unity.Collections.Allocator.Persistent);
                    var srcPointer = global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(PositionArray);
                    global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(pointer, srcPointer, PositionArray.Length * sizeof(global::ComponentTypes.Position2D.Eight));
                    PositionArray.Dispose();
                    PositionArray = global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<global::ComponentTypes.Position2D.Eight>(pointer, newCapacity, global::Unity.Collections.Allocator.Persistent);
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                    global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.SetAtomicSafetyHandle(ref PositionArray, global::Unity.Collections.LowLevel.Unsafe.AtomicSafetyHandle.Create());
#endif
                }
                {
                    var pointer = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.Malloc(sizeof(global::ComponentTypes.Destination2D.Eight) * newCapacity, 32, global::Unity.Collections.Allocator.Persistent);
                    var srcPointer = global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(DestinationArray);
                    global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(pointer, srcPointer, DestinationArray.Length * sizeof(global::ComponentTypes.Destination2D.Eight));
                    DestinationArray.Dispose();
                    DestinationArray = global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<global::ComponentTypes.Destination2D.Eight>(pointer, newCapacity, global::Unity.Collections.Allocator.Persistent);
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                    global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.SetAtomicSafetyHandle(ref DestinationArray, global::Unity.Collections.LowLevel.Unsafe.AtomicSafetyHandle.Create());
#endif
                }
                {
                    var pointer = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.Malloc(sizeof(global::ComponentTypes.Speed2D.Eight) * newCapacity, 32, global::Unity.Collections.Allocator.Persistent);
                    var srcPointer = global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(SpeedArray);
                    global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(pointer, srcPointer, SpeedArray.Length * sizeof(global::ComponentTypes.Speed2D.Eight));
                    SpeedArray.Dispose();
                    SpeedArray = global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<global::ComponentTypes.Speed2D.Eight>(pointer, newCapacity, global::Unity.Collections.Allocator.Persistent);
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                    global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.SetAtomicSafetyHandle(ref SpeedArray, global::Unity.Collections.LowLevel.Unsafe.AtomicSafetyHandle.Create());
#endif
                }
                {
                    var pointer = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.Malloc(sizeof(global::ComponentTypes.AliveState.Eight) * newCapacity, 32, global::Unity.Collections.Allocator.Persistent);
                    var srcPointer = global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(IsAliveArray);
                    global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(pointer, srcPointer, IsAliveArray.Length * sizeof(global::ComponentTypes.AliveState.Eight));
                    IsAliveArray.Dispose();
                    IsAliveArray = global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<global::ComponentTypes.AliveState.Eight>(pointer, newCapacity, global::Unity.Collections.Allocator.Persistent);
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                    global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.SetAtomicSafetyHandle(ref IsAliveArray, global::Unity.Collections.LowLevel.Unsafe.AtomicSafetyHandle.Create());
#endif
                }
            }

            public void Dispose()
            {
                if (!Count.IsCreated) return;

                Count.Dispose();
                Count = default;
                PositionArray.Dispose();
                DestinationArray.Dispose();
                SpeedArray.Dispose();
                IsAliveArray.Dispose();
            }

            public unsafe Countable(int capacity)
            {
                Count = new global::Unity.Collections.NativeArray<int>(1, global::Unity.Collections.Allocator.Persistent);
                {
                    var pointer = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.Malloc(sizeof(global::ComponentTypes.Position2D.Eight) * capacity, 32, global::Unity.Collections.Allocator.Persistent);
                    PositionArray = global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<global::ComponentTypes.Position2D.Eight>(pointer, capacity, global::Unity.Collections.Allocator.Persistent);
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                    global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.SetAtomicSafetyHandle(ref PositionArray, global::Unity.Collections.LowLevel.Unsafe.AtomicSafetyHandle.Create());
#endif
                }
                {
                    var pointer = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.Malloc(sizeof(global::ComponentTypes.Destination2D.Eight) * capacity, 32, global::Unity.Collections.Allocator.Persistent);
                    DestinationArray = global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<global::ComponentTypes.Destination2D.Eight>(pointer, capacity, global::Unity.Collections.Allocator.Persistent);
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                    global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.SetAtomicSafetyHandle(ref DestinationArray, global::Unity.Collections.LowLevel.Unsafe.AtomicSafetyHandle.Create());
#endif
                }
                {
                    var pointer = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.Malloc(sizeof(global::ComponentTypes.Speed2D.Eight) * capacity, 32, global::Unity.Collections.Allocator.Persistent);
                    SpeedArray = global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<global::ComponentTypes.Speed2D.Eight>(pointer, capacity, global::Unity.Collections.Allocator.Persistent);
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                    global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.SetAtomicSafetyHandle(ref SpeedArray, global::Unity.Collections.LowLevel.Unsafe.AtomicSafetyHandle.Create());
#endif
                }
                {
                    var pointer = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.Malloc(sizeof(global::ComponentTypes.AliveState.Eight) * capacity, 32, global::Unity.Collections.Allocator.Persistent);
                    IsAliveArray = global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<global::ComponentTypes.AliveState.Eight>(pointer, capacity, global::Unity.Collections.Allocator.Persistent);
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                    global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.SetAtomicSafetyHandle(ref IsAliveArray, global::Unity.Collections.LowLevel.Unsafe.AtomicSafetyHandle.Create());
#endif
                }
            }

            public bool IsAlive(int index)
            {
                return IsAliveArray.Reinterpret<AliveState.State>(4)[index] == AliveState.State.Alive;
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
        public partial struct Countable : global::System.IDisposable
        {
            public global::Unity.Collections.NativeArray<int> Count;
            public global::Unity.Collections.NativeArray<global::ComponentTypes.Position2D.Eight> PositionArray;
            public global::Unity.Collections.NativeArray<global::ComponentTypes.Speed2D.Eight> SpeedArray;
            public global::Unity.Collections.NativeArray<global::ComponentTypes.AliveState.Eight> IsAliveArray;

            public int Capacity => PositionArray.Length;

            public int ChunkCount => (Count[0] + 7) >> 3;

            public unsafe void EnsureCapacity(int newCapacity)
            {
                if (Capacity >= newCapacity) return;

                {
                    var pointer = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.Malloc(sizeof(global::ComponentTypes.Position2D.Eight) * newCapacity, 32, global::Unity.Collections.Allocator.Persistent);
                    var srcPointer = global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(PositionArray);
                    global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(pointer, srcPointer, PositionArray.Length * sizeof(global::ComponentTypes.Position2D.Eight));
                    PositionArray.Dispose();
                    PositionArray = global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<global::ComponentTypes.Position2D.Eight>(pointer, newCapacity, global::Unity.Collections.Allocator.Persistent);
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                    global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.SetAtomicSafetyHandle(ref PositionArray, global::Unity.Collections.LowLevel.Unsafe.AtomicSafetyHandle.Create());
#endif
                }
                {
                    var pointer = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.Malloc(sizeof(global::ComponentTypes.Speed2D.Eight) * newCapacity, 32, global::Unity.Collections.Allocator.Persistent);
                    var srcPointer = global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(SpeedArray);
                    global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(pointer, srcPointer, SpeedArray.Length * sizeof(global::ComponentTypes.Speed2D.Eight));
                    SpeedArray.Dispose();
                    SpeedArray = global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<global::ComponentTypes.Speed2D.Eight>(pointer, newCapacity, global::Unity.Collections.Allocator.Persistent);
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                    global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.SetAtomicSafetyHandle(ref SpeedArray, global::Unity.Collections.LowLevel.Unsafe.AtomicSafetyHandle.Create());
#endif
                }
                {
                    var pointer = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.Malloc(sizeof(global::ComponentTypes.AliveState.Eight) * newCapacity, 32, global::Unity.Collections.Allocator.Persistent);
                    var srcPointer = global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(IsAliveArray);
                    global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(pointer, srcPointer, IsAliveArray.Length * sizeof(global::ComponentTypes.AliveState.Eight));
                    IsAliveArray.Dispose();
                    IsAliveArray = global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<global::ComponentTypes.AliveState.Eight>(pointer, newCapacity, global::Unity.Collections.Allocator.Persistent);
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                    global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.SetAtomicSafetyHandle(ref IsAliveArray, global::Unity.Collections.LowLevel.Unsafe.AtomicSafetyHandle.Create());
#endif
                }
            }

            public void Dispose()
            {
                if (!Count.IsCreated) return;

                Count.Dispose();
                Count = default;
                PositionArray.Dispose();
                SpeedArray.Dispose();
                IsAliveArray.Dispose();
            }

            public unsafe Countable(int capacity)
            {
                Count = new global::Unity.Collections.NativeArray<int>(1, global::Unity.Collections.Allocator.Persistent);
                {
                    var pointer = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.Malloc(sizeof(global::ComponentTypes.Position2D.Eight) * capacity, 32, global::Unity.Collections.Allocator.Persistent);
                    PositionArray = global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<global::ComponentTypes.Position2D.Eight>(pointer, capacity, global::Unity.Collections.Allocator.Persistent);
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                    global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.SetAtomicSafetyHandle(ref PositionArray, global::Unity.Collections.LowLevel.Unsafe.AtomicSafetyHandle.Create());
#endif
                }
                {
                    var pointer = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.Malloc(sizeof(global::ComponentTypes.Speed2D.Eight) * capacity, 32, global::Unity.Collections.Allocator.Persistent);
                    SpeedArray = global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<global::ComponentTypes.Speed2D.Eight>(pointer, capacity, global::Unity.Collections.Allocator.Persistent);
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                    global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.SetAtomicSafetyHandle(ref SpeedArray, global::Unity.Collections.LowLevel.Unsafe.AtomicSafetyHandle.Create());
#endif
                }
                {
                    var pointer = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.Malloc(sizeof(global::ComponentTypes.AliveState.Eight) * capacity, 32, global::Unity.Collections.Allocator.Persistent);
                    IsAliveArray = global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<global::ComponentTypes.AliveState.Eight>(pointer, capacity, global::Unity.Collections.Allocator.Persistent);
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                    global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.SetAtomicSafetyHandle(ref IsAliveArray, global::Unity.Collections.LowLevel.Unsafe.AtomicSafetyHandle.Create());
#endif
                }
            }

            public bool IsAlive(int index)
            {
                return IsAliveArray.Reinterpret<AliveState.State>(4)[index] == AliveState.State.Alive;
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
        public partial struct Countable : global::System.IDisposable
        {
            public global::Unity.Collections.NativeArray<int> Count;
            public global::Unity.Collections.NativeArray<global::ComponentTypes.Position2D.Eight> PositionArray;
            public global::Unity.Collections.NativeArray<global::ComponentTypes.Speed2D.Eight> SpeedArray;
            public global::Unity.Collections.NativeArray<global::ComponentTypes.AliveState.Eight> IsAliveArray;

            public int Capacity => PositionArray.Length;

            public int ChunkCount => (Count[0] + 7) >> 3;

            public unsafe void EnsureCapacity(int newCapacity)
            {
                if (Capacity >= newCapacity) return;

                {
                    var pointer = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.Malloc(sizeof(global::ComponentTypes.Position2D.Eight) * newCapacity, 32, global::Unity.Collections.Allocator.Persistent);
                    var srcPointer = global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(PositionArray);
                    global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(pointer, srcPointer, PositionArray.Length * sizeof(global::ComponentTypes.Position2D.Eight));
                    PositionArray.Dispose();
                    PositionArray = global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<global::ComponentTypes.Position2D.Eight>(pointer, newCapacity, global::Unity.Collections.Allocator.Persistent);
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                    global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.SetAtomicSafetyHandle(ref PositionArray, global::Unity.Collections.LowLevel.Unsafe.AtomicSafetyHandle.Create());
#endif
                }
                {
                    var pointer = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.Malloc(sizeof(global::ComponentTypes.Speed2D.Eight) * newCapacity, 32, global::Unity.Collections.Allocator.Persistent);
                    var srcPointer = global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(SpeedArray);
                    global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(pointer, srcPointer, SpeedArray.Length * sizeof(global::ComponentTypes.Speed2D.Eight));
                    SpeedArray.Dispose();
                    SpeedArray = global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<global::ComponentTypes.Speed2D.Eight>(pointer, newCapacity, global::Unity.Collections.Allocator.Persistent);
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                    global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.SetAtomicSafetyHandle(ref SpeedArray, global::Unity.Collections.LowLevel.Unsafe.AtomicSafetyHandle.Create());
#endif
                }
                {
                    var pointer = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.Malloc(sizeof(global::ComponentTypes.AliveState.Eight) * newCapacity, 32, global::Unity.Collections.Allocator.Persistent);
                    var srcPointer = global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(IsAliveArray);
                    global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(pointer, srcPointer, IsAliveArray.Length * sizeof(global::ComponentTypes.AliveState.Eight));
                    IsAliveArray.Dispose();
                    IsAliveArray = global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<global::ComponentTypes.AliveState.Eight>(pointer, newCapacity, global::Unity.Collections.Allocator.Persistent);
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                    global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.SetAtomicSafetyHandle(ref IsAliveArray, global::Unity.Collections.LowLevel.Unsafe.AtomicSafetyHandle.Create());
#endif
                }
            }

            public void Dispose()
            {
                if (!Count.IsCreated) return;

                Count.Dispose();
                Count = default;
                PositionArray.Dispose();
                SpeedArray.Dispose();
                IsAliveArray.Dispose();
            }

            public unsafe Countable(int capacity)
            {
                Count = new global::Unity.Collections.NativeArray<int>(1, global::Unity.Collections.Allocator.Persistent);
                {
                    var pointer = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.Malloc(sizeof(global::ComponentTypes.Position2D.Eight) * capacity, 32, global::Unity.Collections.Allocator.Persistent);
                    PositionArray = global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<global::ComponentTypes.Position2D.Eight>(pointer, capacity, global::Unity.Collections.Allocator.Persistent);
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                    global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.SetAtomicSafetyHandle(ref PositionArray, global::Unity.Collections.LowLevel.Unsafe.AtomicSafetyHandle.Create());
#endif
                }
                {
                    var pointer = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.Malloc(sizeof(global::ComponentTypes.Speed2D.Eight) * capacity, 32, global::Unity.Collections.Allocator.Persistent);
                    SpeedArray = global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<global::ComponentTypes.Speed2D.Eight>(pointer, capacity, global::Unity.Collections.Allocator.Persistent);
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                    global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.SetAtomicSafetyHandle(ref SpeedArray, global::Unity.Collections.LowLevel.Unsafe.AtomicSafetyHandle.Create());
#endif
                }
                {
                    var pointer = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.Malloc(sizeof(global::ComponentTypes.AliveState.Eight) * capacity, 32, global::Unity.Collections.Allocator.Persistent);
                    IsAliveArray = global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<global::ComponentTypes.AliveState.Eight>(pointer, capacity, global::Unity.Collections.Allocator.Persistent);
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                    global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.SetAtomicSafetyHandle(ref IsAliveArray, global::Unity.Collections.LowLevel.Unsafe.AtomicSafetyHandle.Create());
#endif
                }
            }

            public bool IsAlive(int index)
            {
                return IsAliveArray.Reinterpret<AliveState.State>(4)[index] == AliveState.State.Alive;
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
        public partial struct Countable : global::System.IDisposable
        {
            public global::Unity.Collections.NativeArray<int> Count;
            public global::Unity.Collections.NativeArray<global::ComponentTypes.Position2D.Eight> PositionArray;
            public global::Unity.Collections.NativeArray<global::ComponentTypes.Speed2D.Eight> SpeedArray;
            public global::Unity.Collections.NativeArray<global::ComponentTypes.FireStartTime.Eight> StartTimeArray;

            public int Capacity => PositionArray.Length;

            public int ChunkCount => (Count[0] + 7) >> 3;

            public unsafe void EnsureCapacity(int newCapacity)
            {
                if (Capacity >= newCapacity) return;

                {
                    var pointer = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.Malloc(sizeof(global::ComponentTypes.Position2D.Eight) * newCapacity, 32, global::Unity.Collections.Allocator.Persistent);
                    var srcPointer = global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(PositionArray);
                    global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(pointer, srcPointer, PositionArray.Length * sizeof(global::ComponentTypes.Position2D.Eight));
                    PositionArray.Dispose();
                    PositionArray = global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<global::ComponentTypes.Position2D.Eight>(pointer, newCapacity, global::Unity.Collections.Allocator.Persistent);
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                    global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.SetAtomicSafetyHandle(ref PositionArray, global::Unity.Collections.LowLevel.Unsafe.AtomicSafetyHandle.Create());
#endif
                }
                {
                    var pointer = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.Malloc(sizeof(global::ComponentTypes.Speed2D.Eight) * newCapacity, 32, global::Unity.Collections.Allocator.Persistent);
                    var srcPointer = global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(SpeedArray);
                    global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(pointer, srcPointer, SpeedArray.Length * sizeof(global::ComponentTypes.Speed2D.Eight));
                    SpeedArray.Dispose();
                    SpeedArray = global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<global::ComponentTypes.Speed2D.Eight>(pointer, newCapacity, global::Unity.Collections.Allocator.Persistent);
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                    global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.SetAtomicSafetyHandle(ref SpeedArray, global::Unity.Collections.LowLevel.Unsafe.AtomicSafetyHandle.Create());
#endif
                }
                {
                    var pointer = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.Malloc(sizeof(global::ComponentTypes.FireStartTime.Eight) * newCapacity, 32, global::Unity.Collections.Allocator.Persistent);
                    var srcPointer = global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(StartTimeArray);
                    global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(pointer, srcPointer, StartTimeArray.Length * sizeof(global::ComponentTypes.FireStartTime.Eight));
                    StartTimeArray.Dispose();
                    StartTimeArray = global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<global::ComponentTypes.FireStartTime.Eight>(pointer, newCapacity, global::Unity.Collections.Allocator.Persistent);
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                    global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.SetAtomicSafetyHandle(ref StartTimeArray, global::Unity.Collections.LowLevel.Unsafe.AtomicSafetyHandle.Create());
#endif
                }
            }

            public void Dispose()
            {
                if (!Count.IsCreated) return;

                Count.Dispose();
                Count = default;
                PositionArray.Dispose();
                SpeedArray.Dispose();
                StartTimeArray.Dispose();
            }

            public unsafe Countable(int capacity)
            {
                Count = new global::Unity.Collections.NativeArray<int>(1, global::Unity.Collections.Allocator.Persistent);
                {
                    var pointer = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.Malloc(sizeof(global::ComponentTypes.Position2D.Eight) * capacity, 32, global::Unity.Collections.Allocator.Persistent);
                    PositionArray = global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<global::ComponentTypes.Position2D.Eight>(pointer, capacity, global::Unity.Collections.Allocator.Persistent);
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                    global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.SetAtomicSafetyHandle(ref PositionArray, global::Unity.Collections.LowLevel.Unsafe.AtomicSafetyHandle.Create());
#endif
                }
                {
                    var pointer = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.Malloc(sizeof(global::ComponentTypes.Speed2D.Eight) * capacity, 32, global::Unity.Collections.Allocator.Persistent);
                    SpeedArray = global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<global::ComponentTypes.Speed2D.Eight>(pointer, capacity, global::Unity.Collections.Allocator.Persistent);
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                    global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.SetAtomicSafetyHandle(ref SpeedArray, global::Unity.Collections.LowLevel.Unsafe.AtomicSafetyHandle.Create());
#endif
                }
                {
                    var pointer = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.Malloc(sizeof(global::ComponentTypes.FireStartTime.Eight) * capacity, 32, global::Unity.Collections.Allocator.Persistent);
                    StartTimeArray = global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<global::ComponentTypes.FireStartTime.Eight>(pointer, capacity, global::Unity.Collections.Allocator.Persistent);
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                    global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.SetAtomicSafetyHandle(ref StartTimeArray, global::Unity.Collections.LowLevel.Unsafe.AtomicSafetyHandle.Create());
#endif
                }
            }

            public bool IsAlive(int index, float deadTime)
            {
                return StartTimeArray.Reinterpret<float>(4)[index] > deadTime;
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

namespace Unity1Week.Hit
{
    static partial class  BulletEnemyHit
    {
        [global::Unity.Burst.BurstCompile]
        public unsafe partial struct Job : global::Unity.Jobs.IJob
        {
            [global::Unity.Collections.ReadOnly] public global::Unity.Collections.NativeArray<ComponentTypes.Position2D.Eight> BulletPosition2D;
            public global::Unity.Collections.NativeArray<ComponentTypes.AliveState.Eight> BulletAliveState;
            [global::Unity.Collections.ReadOnly] public global::Unity.Collections.NativeArray<ComponentTypes.Position2D.Eight> EnemyPosition2D;
            [global::Unity.Collections.ReadOnly] public global::Unity.Collections.NativeArray<ComponentTypes.AliveState.Eight> EnemyAliveState;
            public float CollisionRadiusSquare;
            public float CurrentTime;
            public global::Unity.Collections.NativeArray<int> FireCount;
            public global::Unity.Collections.NativeArray<ComponentTypes.Position2D.Eight> FirePositionArray;
            public global::Unity.Collections.NativeArray<ComponentTypes.FireStartTime.Eight> FireStartTimeArray;

            public void Execute()
            {
                var tablePointer0 = global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(FirePositionArray);
                var tablePointer1 = global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(FireStartTimeArray);

                if (global::Unity.Burst.Intrinsics.X86.Fma.IsFmaSupported)
                {
                    const int next1 = 0b00_11_10_01;
                    const int next2 = 0b01_00_11_10;
                    const int next3 = 0b10_01_00_11;
                    var other0 = new global::Unity.Burst.Intrinsics.v256(CollisionRadiusSquare, CollisionRadiusSquare, CollisionRadiusSquare, CollisionRadiusSquare, CollisionRadiusSquare, CollisionRadiusSquare, CollisionRadiusSquare, CollisionRadiusSquare);
                    var other1 = new global::Unity.Burst.Intrinsics.v256(CurrentTime, CurrentTime, CurrentTime, CurrentTime, CurrentTime, CurrentTime, CurrentTime, CurrentTime);
                    var other2 = FireCount[0];

                    var outerPointer0 = (byte*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(BulletPosition2D);
                    var outerPointer1 = (byte*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(BulletAliveState);
                    var innerOriginalPointer0 = (byte*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(EnemyPosition2D);
                    var innerOriginalPointer1 = (byte*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(EnemyAliveState);

                    for (
                        var outerIndex = 0;
                        outerIndex < BulletPosition2D.Length;
                        ++outerIndex,
                        outerPointer0 += sizeof(ComponentTypes.Position2D.Eight),
                        outerPointer1 += sizeof(ComponentTypes.AliveState.Eight)
                    )
                    {
                        var outer0_X = outerPointer0 + (0 << 5);
                        var outer0_Y = outerPointer0 + (1 << 5);
                        var outer1_Value = outerPointer1 + (0 << 5);

                        var outer0_X0 = global::Unity.Burst.Intrinsics.X86.Avx.mm256_load_ps(outer0_X);
                        var outer0_Y0 = global::Unity.Burst.Intrinsics.X86.Avx.mm256_load_ps(outer0_Y);
                        var outer1_Value0 = global::Unity.Burst.Intrinsics.X86.Avx.mm256_load_ps(outer1_Value);

                        var outer0_X1 = global::Unity.Burst.Intrinsics.X86.Avx.mm256_permute_ps(outer0_X0, next1);
                        var outer0_Y1 = global::Unity.Burst.Intrinsics.X86.Avx.mm256_permute_ps(outer0_Y0, next1);
                        var outer1_Value1 = global::Unity.Burst.Intrinsics.X86.Avx.mm256_permute_ps(outer1_Value0, next1);
                        var outer0_X2 = global::Unity.Burst.Intrinsics.X86.Avx.mm256_permute_ps(outer0_X0, next2);
                        var outer0_Y2 = global::Unity.Burst.Intrinsics.X86.Avx.mm256_permute_ps(outer0_Y0, next2);
                        var outer1_Value2 = global::Unity.Burst.Intrinsics.X86.Avx.mm256_permute_ps(outer1_Value0, next2);
                        var outer0_X3 = global::Unity.Burst.Intrinsics.X86.Avx.mm256_permute_ps(outer0_X0, next3);
                        var outer0_Y3 = global::Unity.Burst.Intrinsics.X86.Avx.mm256_permute_ps(outer0_Y0, next3);
                        var outer1_Value3 = global::Unity.Burst.Intrinsics.X86.Avx.mm256_permute_ps(outer1_Value0, next3);

                        var outer0_X4 = global::Unity.Burst.Intrinsics.X86.Avx.mm256_permute2f128_ps(outer0_X0, outer0_X0, 0b0000_0001);
                        var outer0_Y4 = global::Unity.Burst.Intrinsics.X86.Avx.mm256_permute2f128_ps(outer0_Y0, outer0_Y0, 0b0000_0001);
                        var outer1_Value4 = global::Unity.Burst.Intrinsics.X86.Avx.mm256_permute2f128_ps(outer1_Value0, outer1_Value0, 0b0000_0001);

                        var outer0_X5 = global::Unity.Burst.Intrinsics.X86.Avx.mm256_permute_ps(outer0_X4, next1);
                        var outer0_Y5 = global::Unity.Burst.Intrinsics.X86.Avx.mm256_permute_ps(outer0_Y4, next1);
                        var outer1_Value5 = global::Unity.Burst.Intrinsics.X86.Avx.mm256_permute_ps(outer1_Value4, next1);
                        var outer0_X6 = global::Unity.Burst.Intrinsics.X86.Avx.mm256_permute_ps(outer0_X4, next2);
                        var outer0_Y6 = global::Unity.Burst.Intrinsics.X86.Avx.mm256_permute_ps(outer0_Y4, next2);
                        var outer1_Value6 = global::Unity.Burst.Intrinsics.X86.Avx.mm256_permute_ps(outer1_Value4, next2);
                        var outer0_X7 = global::Unity.Burst.Intrinsics.X86.Avx.mm256_permute_ps(outer0_X4, next3);
                        var outer0_Y7 = global::Unity.Burst.Intrinsics.X86.Avx.mm256_permute_ps(outer0_Y4, next3);
                        var outer1_Value7 = global::Unity.Burst.Intrinsics.X86.Avx.mm256_permute_ps(outer1_Value4, next3);

                        var innerPointer0 = innerOriginalPointer0;
                        var innerPointer1 = innerOriginalPointer1;
                        for (
                            var innerIndex = 0;
                            innerIndex < EnemyPosition2D.Length;
                            ++innerIndex,
                            innerPointer0 += sizeof(ComponentTypes.Position2D.Eight),
                            innerPointer1 += sizeof(ComponentTypes.AliveState.Eight)
                        )
                        {
                            var inner0_X = global::Unity.Burst.Intrinsics.X86.Avx.mm256_load_ps(innerPointer0 + (0 << 5));
                            var inner0_Y = global::Unity.Burst.Intrinsics.X86.Avx.mm256_load_ps(innerPointer0 + (1 << 5));
                            var inner1_Value = global::Unity.Burst.Intrinsics.X86.Avx.mm256_load_ps(innerPointer1 + (0 << 5));

                            Exe2(ref outer0_X0, ref outer0_Y0, ref outer1_Value0, ref inner0_X, ref inner0_Y, ref inner1_Value, ref other0, ref other1, ref other2, tablePointer0, FirePositionArray.Length, tablePointer1, FireStartTimeArray.Length);
                            Exe2(ref outer0_X1, ref outer0_Y1, ref outer1_Value1, ref inner0_X, ref inner0_Y, ref inner1_Value, ref other0, ref other1, ref other2, tablePointer0, FirePositionArray.Length, tablePointer1, FireStartTimeArray.Length);
                            Exe2(ref outer0_X2, ref outer0_Y2, ref outer1_Value2, ref inner0_X, ref inner0_Y, ref inner1_Value, ref other0, ref other1, ref other2, tablePointer0, FirePositionArray.Length, tablePointer1, FireStartTimeArray.Length);
                            Exe2(ref outer0_X3, ref outer0_Y3, ref outer1_Value3, ref inner0_X, ref inner0_Y, ref inner1_Value, ref other0, ref other1, ref other2, tablePointer0, FirePositionArray.Length, tablePointer1, FireStartTimeArray.Length);
                            Exe2(ref outer0_X4, ref outer0_Y4, ref outer1_Value4, ref inner0_X, ref inner0_Y, ref inner1_Value, ref other0, ref other1, ref other2, tablePointer0, FirePositionArray.Length, tablePointer1, FireStartTimeArray.Length);
                            Exe2(ref outer0_X5, ref outer0_Y5, ref outer1_Value5, ref inner0_X, ref inner0_Y, ref inner1_Value, ref other0, ref other1, ref other2, tablePointer0, FirePositionArray.Length, tablePointer1, FireStartTimeArray.Length);
                            Exe2(ref outer0_X6, ref outer0_Y6, ref outer1_Value6, ref inner0_X, ref inner0_Y, ref inner1_Value, ref other0, ref other1, ref other2, tablePointer0, FirePositionArray.Length, tablePointer1, FireStartTimeArray.Length);
                            Exe2(ref outer0_X7, ref outer0_Y7, ref outer1_Value7, ref inner0_X, ref inner0_Y, ref inner1_Value, ref other0, ref other1, ref other2, tablePointer0, FirePositionArray.Length, tablePointer1, FireStartTimeArray.Length);
                        }

                        outer1_Value0 = CloseAlive2(outer1_Value0, global::Unity.Burst.Intrinsics.X86.Avx.mm256_permute2f128_ps(outer1_Value4, outer1_Value4, 0b0000_0001));
                        outer1_Value1 = CloseAlive2(outer1_Value1, global::Unity.Burst.Intrinsics.X86.Avx.mm256_permute2f128_ps(outer1_Value5, outer1_Value5, 0b0000_0001));
                        outer1_Value2 = CloseAlive2(outer1_Value2, global::Unity.Burst.Intrinsics.X86.Avx.mm256_permute2f128_ps(outer1_Value6, outer1_Value6, 0b0000_0001));
                        outer1_Value3 = CloseAlive2(outer1_Value3, global::Unity.Burst.Intrinsics.X86.Avx.mm256_permute2f128_ps(outer1_Value7, outer1_Value7, 0b0000_0001));
                        outer1_Value1 = global::Unity.Burst.Intrinsics.X86.Avx.mm256_permute_ps(outer1_Value1, 0b10_01_00_11);
                        outer1_Value2 = global::Unity.Burst.Intrinsics.X86.Avx.mm256_permute_ps(outer1_Value2, 0b01_00_11_10);
                        outer1_Value3 = global::Unity.Burst.Intrinsics.X86.Avx.mm256_permute_ps(outer1_Value3, 0b00_11_10_01);
                        global::Unity.Burst.Intrinsics.X86.Avx.mm256_store_ps(outer1_Value, CloseAlive2(CloseAlive2(outer1_Value0, outer1_Value1), CloseAlive2(outer1_Value2, outer1_Value3)));
                    }

                    FireCount[0] = other2;
                    return;
                }

                {
                    var other0 = new global::Unity.Mathematics.float4(CollisionRadiusSquare, CollisionRadiusSquare, CollisionRadiusSquare, CollisionRadiusSquare);
                    var other1 = new global::Unity.Mathematics.float4(CurrentTime, CurrentTime, CurrentTime, CurrentTime);
                    var other2 = FireCount[0];
                    for (var outerIndex = 0; outerIndex < BulletPosition2D.Length; ++outerIndex)
                    {
                        var outer0 = BulletPosition2D[outerIndex];
                        var outer1 = BulletAliveState[outerIndex];
                        ref var outer0_X0 = ref outer0.X;
                        ref var outer0_X0_c0 = ref outer0_X0.c0;
                        var outer0_X1_c0 = outer0_X0.c0.wxyz;
                        var outer0_X2_c0 = outer0_X0.c0.zwxy;
                        var outer0_X3_c0 = outer0_X0.c0.yzwx;
                        ref var outer0_X0_c1 = ref outer0_X0.c1;
                        var outer0_X1_c1 = outer0_X0_c1.wxyz;
                        var outer0_X2_c1 = outer0_X0_c1.zwxy;
                        var outer0_X3_c1 = outer0_X0_c1.yzwx;
                        ref var outer0_Y0 = ref outer0.Y;
                        ref var outer0_Y0_c0 = ref outer0_Y0.c0;
                        var outer0_Y1_c0 = outer0_Y0.c0.wxyz;
                        var outer0_Y2_c0 = outer0_Y0.c0.zwxy;
                        var outer0_Y3_c0 = outer0_Y0.c0.yzwx;
                        ref var outer0_Y0_c1 = ref outer0_Y0.c1;
                        var outer0_Y1_c1 = outer0_Y0_c1.wxyz;
                        var outer0_Y2_c1 = outer0_Y0_c1.zwxy;
                        var outer0_Y3_c1 = outer0_Y0_c1.yzwx;
                        ref var outer1_Value0 = ref outer1.Value;
                        ref var outer1_Value0_c0 = ref outer1_Value0.c0;
                        var outer1_Value1_c0 = outer1_Value0.c0.wxyz;
                        var outer1_Value2_c0 = outer1_Value0.c0.zwxy;
                        var outer1_Value3_c0 = outer1_Value0.c0.yzwx;
                        ref var outer1_Value0_c1 = ref outer1_Value0.c1;
                        var outer1_Value1_c1 = outer1_Value0_c1.wxyz;
                        var outer1_Value2_c1 = outer1_Value0_c1.zwxy;
                        var outer1_Value3_c1 = outer1_Value0_c1.yzwx;
                        for (var innerIndex = 0; innerIndex < EnemyPosition2D.Length; ++innerIndex)
                        {
                            var inner0 = EnemyPosition2D[innerIndex];
                            var inner1 = EnemyAliveState[innerIndex];
                            ref var inner0_X = ref inner0.X;
                            ref var inner0_Y = ref inner0.Y;
                            ref var inner1_Value = ref inner1.Value;

                            Exe(ref outer0_X0_c0, ref outer0_Y0_c0, ref outer1_Value0_c0, ref inner0_X.c0, ref inner0_Y.c0, ref inner1_Value.c0, ref other0, ref other1, ref other2, tablePointer0, FirePositionArray.Length, tablePointer1, FireStartTimeArray.Length);
                            Exe(ref outer0_X0_c0, ref outer0_Y0_c0, ref outer1_Value0_c0, ref inner0_X.c1, ref inner0_Y.c1, ref inner1_Value.c1, ref other0, ref other1, ref other2, tablePointer0, FirePositionArray.Length, tablePointer1, FireStartTimeArray.Length);
                            Exe(ref outer0_X0_c1, ref outer0_Y0_c1, ref outer1_Value0_c1, ref inner0_X.c0, ref inner0_Y.c0, ref inner1_Value.c0, ref other0, ref other1, ref other2, tablePointer0, FirePositionArray.Length, tablePointer1, FireStartTimeArray.Length);
                            Exe(ref outer0_X0_c1, ref outer0_Y0_c1, ref outer1_Value0_c1, ref inner0_X.c1, ref inner0_Y.c1, ref inner1_Value.c1, ref other0, ref other1, ref other2, tablePointer0, FirePositionArray.Length, tablePointer1, FireStartTimeArray.Length);
                            Exe(ref outer0_X1_c0, ref outer0_Y1_c0, ref outer1_Value1_c0, ref inner0_X.c0, ref inner0_Y.c0, ref inner1_Value.c0, ref other0, ref other1, ref other2, tablePointer0, FirePositionArray.Length, tablePointer1, FireStartTimeArray.Length);
                            Exe(ref outer0_X1_c0, ref outer0_Y1_c0, ref outer1_Value1_c0, ref inner0_X.c1, ref inner0_Y.c1, ref inner1_Value.c1, ref other0, ref other1, ref other2, tablePointer0, FirePositionArray.Length, tablePointer1, FireStartTimeArray.Length);
                            Exe(ref outer0_X1_c1, ref outer0_Y1_c1, ref outer1_Value1_c1, ref inner0_X.c0, ref inner0_Y.c0, ref inner1_Value.c0, ref other0, ref other1, ref other2, tablePointer0, FirePositionArray.Length, tablePointer1, FireStartTimeArray.Length);
                            Exe(ref outer0_X1_c1, ref outer0_Y1_c1, ref outer1_Value1_c1, ref inner0_X.c1, ref inner0_Y.c1, ref inner1_Value.c1, ref other0, ref other1, ref other2, tablePointer0, FirePositionArray.Length, tablePointer1, FireStartTimeArray.Length);
                            Exe(ref outer0_X2_c0, ref outer0_Y2_c0, ref outer1_Value2_c0, ref inner0_X.c0, ref inner0_Y.c0, ref inner1_Value.c0, ref other0, ref other1, ref other2, tablePointer0, FirePositionArray.Length, tablePointer1, FireStartTimeArray.Length);
                            Exe(ref outer0_X2_c0, ref outer0_Y2_c0, ref outer1_Value2_c0, ref inner0_X.c1, ref inner0_Y.c1, ref inner1_Value.c1, ref other0, ref other1, ref other2, tablePointer0, FirePositionArray.Length, tablePointer1, FireStartTimeArray.Length);
                            Exe(ref outer0_X2_c1, ref outer0_Y2_c1, ref outer1_Value2_c1, ref inner0_X.c0, ref inner0_Y.c0, ref inner1_Value.c0, ref other0, ref other1, ref other2, tablePointer0, FirePositionArray.Length, tablePointer1, FireStartTimeArray.Length);
                            Exe(ref outer0_X2_c1, ref outer0_Y2_c1, ref outer1_Value2_c1, ref inner0_X.c1, ref inner0_Y.c1, ref inner1_Value.c1, ref other0, ref other1, ref other2, tablePointer0, FirePositionArray.Length, tablePointer1, FireStartTimeArray.Length);
                            Exe(ref outer0_X3_c0, ref outer0_Y3_c0, ref outer1_Value3_c0, ref inner0_X.c0, ref inner0_Y.c0, ref inner1_Value.c0, ref other0, ref other1, ref other2, tablePointer0, FirePositionArray.Length, tablePointer1, FireStartTimeArray.Length);
                            Exe(ref outer0_X3_c0, ref outer0_Y3_c0, ref outer1_Value3_c0, ref inner0_X.c1, ref inner0_Y.c1, ref inner1_Value.c1, ref other0, ref other1, ref other2, tablePointer0, FirePositionArray.Length, tablePointer1, FireStartTimeArray.Length);
                            Exe(ref outer0_X3_c1, ref outer0_Y3_c1, ref outer1_Value3_c1, ref inner0_X.c0, ref inner0_Y.c0, ref inner1_Value.c0, ref other0, ref other1, ref other2, tablePointer0, FirePositionArray.Length, tablePointer1, FireStartTimeArray.Length);
                            Exe(ref outer0_X3_c1, ref outer0_Y3_c1, ref outer1_Value3_c1, ref inner0_X.c1, ref inner0_Y.c1, ref inner1_Value.c1, ref other0, ref other1, ref other2, tablePointer0, FirePositionArray.Length, tablePointer1, FireStartTimeArray.Length);
                        }

                        outer1_Value0.c0 = CloseAlive(CloseAlive(outer1_Value0.c0, outer1_Value1_c0.yzwx), CloseAlive(outer1_Value2_c0.zwxy, outer1_Value3_c0.wxyz));
                        outer1_Value0.c1 = CloseAlive(CloseAlive(outer1_Value0.c1, outer1_Value1_c1.yzwx), CloseAlive(outer1_Value2_c1.zwxy, outer1_Value3_c1.wxyz));
                        BulletAliveState[outerIndex] = outer1;
                    }

                    FireCount[0] = other2;
                }
            }
        }
    }
}

namespace Unity1Week.Hit
{
    static partial class  PlayerFireEnemyHitJob
    {
        [global::Unity.Burst.BurstCompile]
        public unsafe partial struct Job : global::Unity.Jobs.IJob
        {
            [global::Unity.Collections.ReadOnly] public global::Unity.Collections.NativeArray<ComponentTypes.Position2D.Eight> FirePosition2D;
            [global::Unity.Collections.ReadOnly] public global::Unity.Collections.NativeArray<ComponentTypes.FireStartTime.Eight> FireFireStartTime;
            [global::Unity.Collections.ReadOnly] public global::Unity.Collections.NativeArray<ComponentTypes.Position2D.Eight> EnemyPosition2D;
            public global::Unity.Collections.NativeArray<ComponentTypes.AliveState.Eight> EnemyAliveState;
            public float CollisionRadiusSquare;
            public float FireDeadTime;
            public global::Unity.Collections.NativeArray<int> EnemyKillCount;

            public void Execute()
            {

                if (global::Unity.Burst.Intrinsics.X86.Fma.IsFmaSupported)
                {
                    const int next1 = 0b00_11_10_01;
                    const int next2 = 0b01_00_11_10;
                    const int next3 = 0b10_01_00_11;
                    var other0 = new global::Unity.Burst.Intrinsics.v256(CollisionRadiusSquare, CollisionRadiusSquare, CollisionRadiusSquare, CollisionRadiusSquare, CollisionRadiusSquare, CollisionRadiusSquare, CollisionRadiusSquare, CollisionRadiusSquare);
                    var other1 = new global::Unity.Burst.Intrinsics.v256(FireDeadTime, FireDeadTime, FireDeadTime, FireDeadTime, FireDeadTime, FireDeadTime, FireDeadTime, FireDeadTime);
                    var other2 = EnemyKillCount[0];

                    var outerPointer0 = (byte*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(FirePosition2D);
                    var outerPointer1 = (byte*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(FireFireStartTime);
                    var innerOriginalPointer0 = (byte*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(EnemyPosition2D);
                    var innerOriginalPointer1 = (byte*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(EnemyAliveState);

                    for (
                        var outerIndex = 0;
                        outerIndex < FirePosition2D.Length;
                        ++outerIndex,
                        outerPointer0 += sizeof(ComponentTypes.Position2D.Eight),
                        outerPointer1 += sizeof(ComponentTypes.FireStartTime.Eight)
                    )
                    {
                        var outer0_X = outerPointer0 + (0 << 5);
                        var outer0_Y = outerPointer0 + (1 << 5);
                        var outer1_Value = outerPointer1 + (0 << 5);

                        var outer0_X0 = global::Unity.Burst.Intrinsics.X86.Avx.mm256_load_ps(outer0_X);
                        var outer0_Y0 = global::Unity.Burst.Intrinsics.X86.Avx.mm256_load_ps(outer0_Y);
                        var outer1_Value0 = global::Unity.Burst.Intrinsics.X86.Avx.mm256_load_ps(outer1_Value);

                        var outer0_X1 = global::Unity.Burst.Intrinsics.X86.Avx.mm256_permute_ps(outer0_X0, next1);
                        var outer0_Y1 = global::Unity.Burst.Intrinsics.X86.Avx.mm256_permute_ps(outer0_Y0, next1);
                        var outer1_Value1 = global::Unity.Burst.Intrinsics.X86.Avx.mm256_permute_ps(outer1_Value0, next1);
                        var outer0_X2 = global::Unity.Burst.Intrinsics.X86.Avx.mm256_permute_ps(outer0_X0, next2);
                        var outer0_Y2 = global::Unity.Burst.Intrinsics.X86.Avx.mm256_permute_ps(outer0_Y0, next2);
                        var outer1_Value2 = global::Unity.Burst.Intrinsics.X86.Avx.mm256_permute_ps(outer1_Value0, next2);
                        var outer0_X3 = global::Unity.Burst.Intrinsics.X86.Avx.mm256_permute_ps(outer0_X0, next3);
                        var outer0_Y3 = global::Unity.Burst.Intrinsics.X86.Avx.mm256_permute_ps(outer0_Y0, next3);
                        var outer1_Value3 = global::Unity.Burst.Intrinsics.X86.Avx.mm256_permute_ps(outer1_Value0, next3);

                        var outer0_X4 = global::Unity.Burst.Intrinsics.X86.Avx.mm256_permute2f128_ps(outer0_X0, outer0_X0, 0b0000_0001);
                        var outer0_Y4 = global::Unity.Burst.Intrinsics.X86.Avx.mm256_permute2f128_ps(outer0_Y0, outer0_Y0, 0b0000_0001);
                        var outer1_Value4 = global::Unity.Burst.Intrinsics.X86.Avx.mm256_permute2f128_ps(outer1_Value0, outer1_Value0, 0b0000_0001);

                        var outer0_X5 = global::Unity.Burst.Intrinsics.X86.Avx.mm256_permute_ps(outer0_X4, next1);
                        var outer0_Y5 = global::Unity.Burst.Intrinsics.X86.Avx.mm256_permute_ps(outer0_Y4, next1);
                        var outer1_Value5 = global::Unity.Burst.Intrinsics.X86.Avx.mm256_permute_ps(outer1_Value4, next1);
                        var outer0_X6 = global::Unity.Burst.Intrinsics.X86.Avx.mm256_permute_ps(outer0_X4, next2);
                        var outer0_Y6 = global::Unity.Burst.Intrinsics.X86.Avx.mm256_permute_ps(outer0_Y4, next2);
                        var outer1_Value6 = global::Unity.Burst.Intrinsics.X86.Avx.mm256_permute_ps(outer1_Value4, next2);
                        var outer0_X7 = global::Unity.Burst.Intrinsics.X86.Avx.mm256_permute_ps(outer0_X4, next3);
                        var outer0_Y7 = global::Unity.Burst.Intrinsics.X86.Avx.mm256_permute_ps(outer0_Y4, next3);
                        var outer1_Value7 = global::Unity.Burst.Intrinsics.X86.Avx.mm256_permute_ps(outer1_Value4, next3);

                        var innerPointer0 = innerOriginalPointer0;
                        var innerPointer1 = innerOriginalPointer1;
                        for (
                            var innerIndex = 0;
                            innerIndex < EnemyPosition2D.Length;
                            ++innerIndex,
                            innerPointer0 += sizeof(ComponentTypes.Position2D.Eight),
                            innerPointer1 += sizeof(ComponentTypes.AliveState.Eight)
                        )
                        {
                            var inner0_X = global::Unity.Burst.Intrinsics.X86.Avx.mm256_load_ps(innerPointer0 + (0 << 5));
                            var inner0_Y = global::Unity.Burst.Intrinsics.X86.Avx.mm256_load_ps(innerPointer0 + (1 << 5));
                            var inner1_Value = global::Unity.Burst.Intrinsics.X86.Avx.mm256_load_ps(innerPointer1 + (0 << 5));

                            Exe2(ref outer0_X0, ref outer0_Y0, ref outer1_Value0, ref inner0_X, ref inner0_Y, ref inner1_Value, ref other0, ref other1, ref other2);
                            Exe2(ref outer0_X1, ref outer0_Y1, ref outer1_Value1, ref inner0_X, ref inner0_Y, ref inner1_Value, ref other0, ref other1, ref other2);
                            Exe2(ref outer0_X2, ref outer0_Y2, ref outer1_Value2, ref inner0_X, ref inner0_Y, ref inner1_Value, ref other0, ref other1, ref other2);
                            Exe2(ref outer0_X3, ref outer0_Y3, ref outer1_Value3, ref inner0_X, ref inner0_Y, ref inner1_Value, ref other0, ref other1, ref other2);
                            Exe2(ref outer0_X4, ref outer0_Y4, ref outer1_Value4, ref inner0_X, ref inner0_Y, ref inner1_Value, ref other0, ref other1, ref other2);
                            Exe2(ref outer0_X5, ref outer0_Y5, ref outer1_Value5, ref inner0_X, ref inner0_Y, ref inner1_Value, ref other0, ref other1, ref other2);
                            Exe2(ref outer0_X6, ref outer0_Y6, ref outer1_Value6, ref inner0_X, ref inner0_Y, ref inner1_Value, ref other0, ref other1, ref other2);
                            Exe2(ref outer0_X7, ref outer0_Y7, ref outer1_Value7, ref inner0_X, ref inner0_Y, ref inner1_Value, ref other0, ref other1, ref other2);
                            global::Unity.Burst.Intrinsics.X86.Avx.mm256_store_ps(innerPointer1 + (0 << 5), inner1_Value);
                        }

                    }

                    EnemyKillCount[0] = other2;
                    return;
                }

                {
                    var other0 = new global::Unity.Mathematics.float4(CollisionRadiusSquare, CollisionRadiusSquare, CollisionRadiusSquare, CollisionRadiusSquare);
                    var other1 = new global::Unity.Mathematics.float4(FireDeadTime, FireDeadTime, FireDeadTime, FireDeadTime);
                    var other2 = EnemyKillCount[0];
                    for (var outerIndex = 0; outerIndex < FirePosition2D.Length; ++outerIndex)
                    {
                        var outer0 = FirePosition2D[outerIndex];
                        var outer1 = FireFireStartTime[outerIndex];
                        ref var outer0_X0 = ref outer0.X;
                        ref var outer0_X0_c0 = ref outer0_X0.c0;
                        var outer0_X1_c0 = outer0_X0.c0.wxyz;
                        var outer0_X2_c0 = outer0_X0.c0.zwxy;
                        var outer0_X3_c0 = outer0_X0.c0.yzwx;
                        ref var outer0_X0_c1 = ref outer0_X0.c1;
                        var outer0_X1_c1 = outer0_X0_c1.wxyz;
                        var outer0_X2_c1 = outer0_X0_c1.zwxy;
                        var outer0_X3_c1 = outer0_X0_c1.yzwx;
                        ref var outer0_Y0 = ref outer0.Y;
                        ref var outer0_Y0_c0 = ref outer0_Y0.c0;
                        var outer0_Y1_c0 = outer0_Y0.c0.wxyz;
                        var outer0_Y2_c0 = outer0_Y0.c0.zwxy;
                        var outer0_Y3_c0 = outer0_Y0.c0.yzwx;
                        ref var outer0_Y0_c1 = ref outer0_Y0.c1;
                        var outer0_Y1_c1 = outer0_Y0_c1.wxyz;
                        var outer0_Y2_c1 = outer0_Y0_c1.zwxy;
                        var outer0_Y3_c1 = outer0_Y0_c1.yzwx;
                        ref var outer1_Value0 = ref outer1.Value;
                        ref var outer1_Value0_c0 = ref outer1_Value0.c0;
                        var outer1_Value1_c0 = outer1_Value0.c0.wxyz;
                        var outer1_Value2_c0 = outer1_Value0.c0.zwxy;
                        var outer1_Value3_c0 = outer1_Value0.c0.yzwx;
                        ref var outer1_Value0_c1 = ref outer1_Value0.c1;
                        var outer1_Value1_c1 = outer1_Value0_c1.wxyz;
                        var outer1_Value2_c1 = outer1_Value0_c1.zwxy;
                        var outer1_Value3_c1 = outer1_Value0_c1.yzwx;
                        for (var innerIndex = 0; innerIndex < EnemyPosition2D.Length; ++innerIndex)
                        {
                            var inner0 = EnemyPosition2D[innerIndex];
                            var inner1 = EnemyAliveState[innerIndex];
                            ref var inner0_X = ref inner0.X;
                            ref var inner0_Y = ref inner0.Y;
                            ref var inner1_Value = ref inner1.Value;

                            Exe(ref outer0_X0_c0, ref outer0_Y0_c0, ref outer1_Value0_c0, ref inner0_X.c0, ref inner0_Y.c0, ref inner1_Value.c0, ref other0, ref other1, ref other2);
                            Exe(ref outer0_X0_c0, ref outer0_Y0_c0, ref outer1_Value0_c0, ref inner0_X.c1, ref inner0_Y.c1, ref inner1_Value.c1, ref other0, ref other1, ref other2);
                            Exe(ref outer0_X0_c1, ref outer0_Y0_c1, ref outer1_Value0_c1, ref inner0_X.c0, ref inner0_Y.c0, ref inner1_Value.c0, ref other0, ref other1, ref other2);
                            Exe(ref outer0_X0_c1, ref outer0_Y0_c1, ref outer1_Value0_c1, ref inner0_X.c1, ref inner0_Y.c1, ref inner1_Value.c1, ref other0, ref other1, ref other2);
                            Exe(ref outer0_X1_c0, ref outer0_Y1_c0, ref outer1_Value1_c0, ref inner0_X.c0, ref inner0_Y.c0, ref inner1_Value.c0, ref other0, ref other1, ref other2);
                            Exe(ref outer0_X1_c0, ref outer0_Y1_c0, ref outer1_Value1_c0, ref inner0_X.c1, ref inner0_Y.c1, ref inner1_Value.c1, ref other0, ref other1, ref other2);
                            Exe(ref outer0_X1_c1, ref outer0_Y1_c1, ref outer1_Value1_c1, ref inner0_X.c0, ref inner0_Y.c0, ref inner1_Value.c0, ref other0, ref other1, ref other2);
                            Exe(ref outer0_X1_c1, ref outer0_Y1_c1, ref outer1_Value1_c1, ref inner0_X.c1, ref inner0_Y.c1, ref inner1_Value.c1, ref other0, ref other1, ref other2);
                            Exe(ref outer0_X2_c0, ref outer0_Y2_c0, ref outer1_Value2_c0, ref inner0_X.c0, ref inner0_Y.c0, ref inner1_Value.c0, ref other0, ref other1, ref other2);
                            Exe(ref outer0_X2_c0, ref outer0_Y2_c0, ref outer1_Value2_c0, ref inner0_X.c1, ref inner0_Y.c1, ref inner1_Value.c1, ref other0, ref other1, ref other2);
                            Exe(ref outer0_X2_c1, ref outer0_Y2_c1, ref outer1_Value2_c1, ref inner0_X.c0, ref inner0_Y.c0, ref inner1_Value.c0, ref other0, ref other1, ref other2);
                            Exe(ref outer0_X2_c1, ref outer0_Y2_c1, ref outer1_Value2_c1, ref inner0_X.c1, ref inner0_Y.c1, ref inner1_Value.c1, ref other0, ref other1, ref other2);
                            Exe(ref outer0_X3_c0, ref outer0_Y3_c0, ref outer1_Value3_c0, ref inner0_X.c0, ref inner0_Y.c0, ref inner1_Value.c0, ref other0, ref other1, ref other2);
                            Exe(ref outer0_X3_c0, ref outer0_Y3_c0, ref outer1_Value3_c0, ref inner0_X.c1, ref inner0_Y.c1, ref inner1_Value.c1, ref other0, ref other1, ref other2);
                            Exe(ref outer0_X3_c1, ref outer0_Y3_c1, ref outer1_Value3_c1, ref inner0_X.c0, ref inner0_Y.c0, ref inner1_Value.c0, ref other0, ref other1, ref other2);
                            Exe(ref outer0_X3_c1, ref outer0_Y3_c1, ref outer1_Value3_c1, ref inner0_X.c1, ref inner0_Y.c1, ref inner1_Value.c1, ref other0, ref other1, ref other2);
                            EnemyAliveState[innerIndex] = inner1;
                        }
                    }

                    EnemyKillCount[0] = other2;
                }
            }
        }
    }
}

namespace Unity1Week
{
    static partial class  EnemySnowShoot
    {
        [global::Unity.Burst.BurstCompile]
        public unsafe partial struct Job : global::Unity.Jobs.IJob
        {
            [global::Unity.Collections.ReadOnly] public global::Unity.Collections.NativeArray<ComponentTypes.Position2D.Eight> EnemyPosition2D;
            [global::Unity.Collections.ReadOnly] public global::Unity.Collections.NativeArray<ComponentTypes.AliveState.Eight> EnemyAliveState;
            public float PlayerPositionX;
            public float PlayerPositionY;
            public float Speed;
            public global::Unity.Collections.NativeArray<int> SnowCount;
            public global::Unity.Collections.NativeArray<ComponentTypes.Position2D.Eight> SnowPosition;
            public global::Unity.Collections.NativeArray<ComponentTypes.Speed2D.Eight> SnowSpeed;
            public global::Unity.Collections.NativeArray<ComponentTypes.AliveState.Eight> SnowAliveState;

            public void Execute()
            {
                var tablePointer0 = global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(SnowPosition);
                var tablePointer1 = global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(SnowSpeed);
                var tablePointer2 = global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(SnowAliveState);
                if (global::Unity.Burst.Intrinsics.X86.Fma.IsFmaSupported)
                {
                    var other0 = new global::Unity.Burst.Intrinsics.v256(PlayerPositionX, PlayerPositionX, PlayerPositionX, PlayerPositionX, PlayerPositionX, PlayerPositionX, PlayerPositionX, PlayerPositionX);
                    var other1 = new global::Unity.Burst.Intrinsics.v256(PlayerPositionY, PlayerPositionY, PlayerPositionY, PlayerPositionY, PlayerPositionY, PlayerPositionY, PlayerPositionY, PlayerPositionY);
                    var other2 = new global::Unity.Burst.Intrinsics.v256(Speed, Speed, Speed, Speed, Speed, Speed, Speed, Speed);
                    var other3 = SnowCount[0];
                    var outerPointer0 = (byte*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(EnemyPosition2D);
                    var outerPointer1 = (byte*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(EnemyAliveState);

                    for (
                        var outerIndex = 0;
                        outerIndex < EnemyPosition2D.Length;
                        ++outerIndex,
                        outerPointer0 += sizeof(ComponentTypes.Position2D.Eight),
                        outerPointer1 += sizeof(ComponentTypes.AliveState.Eight)
                    )
                    {
                        var outer0_X = global::Unity.Burst.Intrinsics.X86.Avx.mm256_load_ps(outerPointer0 + (0 << 5));
                        var outer0_Y = global::Unity.Burst.Intrinsics.X86.Avx.mm256_load_ps(outerPointer0 + (1 << 5));
                        var outer1_Value = global::Unity.Burst.Intrinsics.X86.Avx.mm256_load_ps(outerPointer1 + (0 << 5));
                        Exe2(ref outer0_X, ref outer0_Y, ref outer1_Value, ref other0, ref other1, ref other2, ref other3, tablePointer0, SnowPosition.Length, tablePointer1, SnowSpeed.Length, tablePointer2, SnowAliveState.Length);
                    }

                    SnowCount[0] = other3;
                    return;
                }

                {
                    var other0 = new global::Unity.Mathematics.float4(PlayerPositionX, PlayerPositionX, PlayerPositionX, PlayerPositionX);
                    var other1 = new global::Unity.Mathematics.float4(PlayerPositionY, PlayerPositionY, PlayerPositionY, PlayerPositionY);
                    var other2 = new global::Unity.Mathematics.float4(Speed, Speed, Speed, Speed);
                    var other3 = SnowCount[0];

                    for (var outerIndex = 0; outerIndex < EnemyPosition2D.Length; ++outerIndex)
                    {
                        var outer0 = EnemyPosition2D[outerIndex];
                        var outer1 = EnemyAliveState[outerIndex];
                        Exe(ref outer0.X.c0, ref outer0.Y.c0, ref outer1.Value.c0, ref other0, ref other1, ref other2, ref other3, tablePointer0, SnowPosition.Length, tablePointer1, SnowSpeed.Length, tablePointer2, SnowAliveState.Length);
                        Exe(ref outer0.X.c1, ref outer0.Y.c1, ref outer1.Value.c1, ref other0, ref other1, ref other2, ref other3, tablePointer0, SnowPosition.Length, tablePointer1, SnowSpeed.Length, tablePointer2, SnowAliveState.Length);
                    }

                    SnowCount[0] = other3;
                }
            }
        }
    }
}

namespace Unity1Week.Hit
{
    static partial class  PlayerEnemyHitJob
    {
        [global::Unity.Burst.BurstCompile]
        public unsafe partial struct Job : global::Unity.Jobs.IJob
        {
            [global::Unity.Collections.ReadOnly] public global::Unity.Collections.NativeArray<ComponentTypes.Position2D.Eight> EnemyPosition2D;
            public global::Unity.Collections.NativeArray<ComponentTypes.AliveState.Eight> EnemyAliveState;
            public float PlayerPositionX;
            public float PlayerPositionY;
            public float CollisionRadiusSquare;
            public global::Unity.Collections.NativeArray<int> CollisionCount;

            public void Execute()
            {
                if (global::Unity.Burst.Intrinsics.X86.Fma.IsFmaSupported)
                {
                    var other0 = new global::Unity.Burst.Intrinsics.v256(PlayerPositionX, PlayerPositionX, PlayerPositionX, PlayerPositionX, PlayerPositionX, PlayerPositionX, PlayerPositionX, PlayerPositionX);
                    var other1 = new global::Unity.Burst.Intrinsics.v256(PlayerPositionY, PlayerPositionY, PlayerPositionY, PlayerPositionY, PlayerPositionY, PlayerPositionY, PlayerPositionY, PlayerPositionY);
                    var other2 = new global::Unity.Burst.Intrinsics.v256(CollisionRadiusSquare, CollisionRadiusSquare, CollisionRadiusSquare, CollisionRadiusSquare, CollisionRadiusSquare, CollisionRadiusSquare, CollisionRadiusSquare, CollisionRadiusSquare);
                    var other3 = CollisionCount[0];
                    var outerPointer0 = (byte*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(EnemyPosition2D);
                    var outerPointer1 = (byte*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(EnemyAliveState);

                    for (
                        var outerIndex = 0;
                        outerIndex < EnemyPosition2D.Length;
                        ++outerIndex,
                        outerPointer0 += sizeof(ComponentTypes.Position2D.Eight),
                        outerPointer1 += sizeof(ComponentTypes.AliveState.Eight)
                    )
                    {
                        var outer0_X = global::Unity.Burst.Intrinsics.X86.Avx.mm256_load_ps(outerPointer0 + (0 << 5));
                        var outer0_Y = global::Unity.Burst.Intrinsics.X86.Avx.mm256_load_ps(outerPointer0 + (1 << 5));
                        var outer1_Value = global::Unity.Burst.Intrinsics.X86.Avx.mm256_load_ps(outerPointer1 + (0 << 5));
                        Exe2(ref outer0_X, ref outer0_Y, ref outer1_Value, ref other0, ref other1, ref other2, ref other3);
                        global::Unity.Burst.Intrinsics.X86.Avx.mm256_store_ps(outerPointer1 + (0 << 5), outer1_Value);
                    }

                    CollisionCount[0] = other3;
                    return;
                }

                {
                    var other0 = new global::Unity.Mathematics.float4(PlayerPositionX, PlayerPositionX, PlayerPositionX, PlayerPositionX);
                    var other1 = new global::Unity.Mathematics.float4(PlayerPositionY, PlayerPositionY, PlayerPositionY, PlayerPositionY);
                    var other2 = new global::Unity.Mathematics.float4(CollisionRadiusSquare, CollisionRadiusSquare, CollisionRadiusSquare, CollisionRadiusSquare);
                    var other3 = CollisionCount[0];

                    for (var outerIndex = 0; outerIndex < EnemyPosition2D.Length; ++outerIndex)
                    {
                        var outer0 = EnemyPosition2D[outerIndex];
                        var outer1 = EnemyAliveState[outerIndex];
                        Exe(ref outer0.X.c0, ref outer0.Y.c0, ref outer1.Value.c0, ref other0, ref other1, ref other2, ref other3);
                        Exe(ref outer0.X.c1, ref outer0.Y.c1, ref outer1.Value.c1, ref other0, ref other1, ref other2, ref other3);
                        EnemyAliveState[outerIndex] = outer1;
                    }

                    CollisionCount[0] = other3;
                }
            }
        }
    }
}

namespace Unity1Week.Hit
{
    static partial class  EnemyAttackPlayerHitJob
    {
        [global::Unity.Burst.BurstCompile]
        public unsafe partial struct Job : global::Unity.Jobs.IJob
        {
            [global::Unity.Collections.ReadOnly] public global::Unity.Collections.NativeArray<ComponentTypes.Position2D.Eight> EnemyPosition2D;
            public global::Unity.Collections.NativeArray<ComponentTypes.AliveState.Eight> EnemyAliveState;
            public float PlayerPositionX;
            public float PlayerPositionY;
            public float CollisionRadiusSquare;
            public global::Unity.Collections.NativeArray<int> CollisionCount;

            public void Execute()
            {
                if (global::Unity.Burst.Intrinsics.X86.Fma.IsFmaSupported)
                {
                    var other0 = new global::Unity.Burst.Intrinsics.v256(PlayerPositionX, PlayerPositionX, PlayerPositionX, PlayerPositionX, PlayerPositionX, PlayerPositionX, PlayerPositionX, PlayerPositionX);
                    var other1 = new global::Unity.Burst.Intrinsics.v256(PlayerPositionY, PlayerPositionY, PlayerPositionY, PlayerPositionY, PlayerPositionY, PlayerPositionY, PlayerPositionY, PlayerPositionY);
                    var other2 = new global::Unity.Burst.Intrinsics.v256(CollisionRadiusSquare, CollisionRadiusSquare, CollisionRadiusSquare, CollisionRadiusSquare, CollisionRadiusSquare, CollisionRadiusSquare, CollisionRadiusSquare, CollisionRadiusSquare);
                    var other3 = CollisionCount[0];
                    var outerPointer0 = (byte*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(EnemyPosition2D);
                    var outerPointer1 = (byte*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(EnemyAliveState);

                    for (
                        var outerIndex = 0;
                        outerIndex < EnemyPosition2D.Length;
                        ++outerIndex,
                        outerPointer0 += sizeof(ComponentTypes.Position2D.Eight),
                        outerPointer1 += sizeof(ComponentTypes.AliveState.Eight)
                    )
                    {
                        var outer0_X = global::Unity.Burst.Intrinsics.X86.Avx.mm256_load_ps(outerPointer0 + (0 << 5));
                        var outer0_Y = global::Unity.Burst.Intrinsics.X86.Avx.mm256_load_ps(outerPointer0 + (1 << 5));
                        var outer1_Value = global::Unity.Burst.Intrinsics.X86.Avx.mm256_load_ps(outerPointer1 + (0 << 5));
                        Exe2(ref outer0_X, ref outer0_Y, ref outer1_Value, ref other0, ref other1, ref other2, ref other3);
                        global::Unity.Burst.Intrinsics.X86.Avx.mm256_store_ps(outerPointer1 + (0 << 5), outer1_Value);
                    }

                    CollisionCount[0] = other3;
                    return;
                }

                {
                    var other0 = new global::Unity.Mathematics.float4(PlayerPositionX, PlayerPositionX, PlayerPositionX, PlayerPositionX);
                    var other1 = new global::Unity.Mathematics.float4(PlayerPositionY, PlayerPositionY, PlayerPositionY, PlayerPositionY);
                    var other2 = new global::Unity.Mathematics.float4(CollisionRadiusSquare, CollisionRadiusSquare, CollisionRadiusSquare, CollisionRadiusSquare);
                    var other3 = CollisionCount[0];

                    for (var outerIndex = 0; outerIndex < EnemyPosition2D.Length; ++outerIndex)
                    {
                        var outer0 = EnemyPosition2D[outerIndex];
                        var outer1 = EnemyAliveState[outerIndex];
                        Exe(ref outer0.X.c0, ref outer0.Y.c0, ref outer1.Value.c0, ref other0, ref other1, ref other2, ref other3);
                        Exe(ref outer0.X.c1, ref outer0.Y.c1, ref outer1.Value.c1, ref other0, ref other1, ref other2, ref other3);
                        EnemyAliveState[outerIndex] = outer1;
                    }

                    CollisionCount[0] = other3;
                }
            }
        }
    }
}

namespace Unity1Week
{
    static partial class  MultiMove
    {
        [global::Unity.Burst.BurstCompile]
        public unsafe partial struct Job : global::Unity.Jobs.IJob
        {
            public global::Unity.Collections.NativeArray<ComponentTypes.Position2D.Eight> Position2D;
            [global::Unity.Collections.ReadOnly] public global::Unity.Collections.NativeArray<ComponentTypes.Speed2D.Eight> Speed2D;
            public float DeltaTime;

            public void Execute()
            {
                if (global::Unity.Burst.Intrinsics.X86.Fma.IsFmaSupported)
                {
                    var other0 = new global::Unity.Burst.Intrinsics.v256(DeltaTime, DeltaTime, DeltaTime, DeltaTime, DeltaTime, DeltaTime, DeltaTime, DeltaTime);
                    var outerPointer0 = (byte*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(Position2D);
                    var outerPointer1 = (byte*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(Speed2D);

                    for (
                        var outerIndex = 0;
                        outerIndex < Position2D.Length;
                        ++outerIndex,
                        outerPointer0 += sizeof(ComponentTypes.Position2D.Eight),
                        outerPointer1 += sizeof(ComponentTypes.Speed2D.Eight)
                    )
                    {
                        var outer0_X = global::Unity.Burst.Intrinsics.X86.Avx.mm256_load_ps(outerPointer0 + (0 << 5));
                        var outer0_Y = global::Unity.Burst.Intrinsics.X86.Avx.mm256_load_ps(outerPointer0 + (1 << 5));
                        var outer1_X = global::Unity.Burst.Intrinsics.X86.Avx.mm256_load_ps(outerPointer1 + (0 << 5));
                        var outer1_Y = global::Unity.Burst.Intrinsics.X86.Avx.mm256_load_ps(outerPointer1 + (1 << 5));
                        Exe(ref outer0_X, ref outer0_Y, ref outer1_X, ref outer1_Y, ref other0);
                        global::Unity.Burst.Intrinsics.X86.Avx.mm256_store_ps(outerPointer0 + (0 << 5), outer0_X);
                        global::Unity.Burst.Intrinsics.X86.Avx.mm256_store_ps(outerPointer0 + (1 << 5), outer0_Y);
                    }
                    return;
                }

                {
                    var other0 = new global::Unity.Mathematics.float4(DeltaTime, DeltaTime, DeltaTime, DeltaTime);

                    for (var outerIndex = 0; outerIndex < Position2D.Length; ++outerIndex)
                    {
                        var outer0 = Position2D[outerIndex];
                        var outer1 = Speed2D[outerIndex];
                        Exe(ref outer0.X.c0, ref outer0.Y.c0, ref outer1.X.c0, ref outer1.Y.c0, ref other0);
                        Exe(ref outer0.X.c1, ref outer0.Y.c1, ref outer1.X.c1, ref outer1.Y.c1, ref other0);
                        Position2D[outerIndex] = outer0;
                    }
                }
            }
        }
    }
}

namespace Unity1Week
{
    static partial class  CalculateMoveSpeedJob
    {
        [global::Unity.Burst.BurstCompile]
        public unsafe partial struct Job : global::Unity.Jobs.IJob
        {
            [global::Unity.Collections.ReadOnly] public global::Unity.Collections.NativeArray<ComponentTypes.Position2D.Eight> Position2D;
            public global::Unity.Collections.NativeArray<ComponentTypes.Speed2D.Eight> Speed2D;
            public float RcpCellSize;
            public int CellWidthCount;
            public int CellCountAdjustment;
            public int MaxCellCountInclusive;
            [global::Unity.Collections.ReadOnly] public global::Unity.Collections.NativeArray<float> SpeedSetting;
            [global::Unity.Collections.ReadOnly] public global::Unity.Collections.NativeArray<int> ChipKind;

            public void Execute()
            {
                var tablePointer0 = global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeReadOnlyPtr(SpeedSetting);
                var tablePointer1 = global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeReadOnlyPtr(ChipKind);
                if (global::Unity.Burst.Intrinsics.X86.Fma.IsFmaSupported)
                {
                    var other0 = new global::Unity.Burst.Intrinsics.v256(RcpCellSize, RcpCellSize, RcpCellSize, RcpCellSize, RcpCellSize, RcpCellSize, RcpCellSize, RcpCellSize);
                    var other1 = new global::Unity.Burst.Intrinsics.v256(CellWidthCount, CellWidthCount, CellWidthCount, CellWidthCount, CellWidthCount, CellWidthCount, CellWidthCount, CellWidthCount);
                    var other2 = new global::Unity.Burst.Intrinsics.v256(CellCountAdjustment, CellCountAdjustment, CellCountAdjustment, CellCountAdjustment, CellCountAdjustment, CellCountAdjustment, CellCountAdjustment, CellCountAdjustment);
                    var other3 = new global::Unity.Burst.Intrinsics.v256(MaxCellCountInclusive, MaxCellCountInclusive, MaxCellCountInclusive, MaxCellCountInclusive, MaxCellCountInclusive, MaxCellCountInclusive, MaxCellCountInclusive, MaxCellCountInclusive);
                    var outerPointer0 = (byte*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(Position2D);
                    var outerPointer1 = (byte*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(Speed2D);

                    for (
                        var outerIndex = 0;
                        outerIndex < Position2D.Length;
                        ++outerIndex,
                        outerPointer0 += sizeof(ComponentTypes.Position2D.Eight),
                        outerPointer1 += sizeof(ComponentTypes.Speed2D.Eight)
                    )
                    {
                        var outer0_X = global::Unity.Burst.Intrinsics.X86.Avx.mm256_load_ps(outerPointer0 + (0 << 5));
                        var outer0_Y = global::Unity.Burst.Intrinsics.X86.Avx.mm256_load_ps(outerPointer0 + (1 << 5));
                        var outer1_X = global::Unity.Burst.Intrinsics.X86.Avx.mm256_load_ps(outerPointer1 + (0 << 5));
                        var outer1_Y = global::Unity.Burst.Intrinsics.X86.Avx.mm256_load_ps(outerPointer1 + (1 << 5));
                        Exe2(ref outer0_X, ref outer0_Y, ref outer1_X, ref outer1_Y, ref other0, ref other1, ref other2, ref other3, tablePointer0, SpeedSetting.Length, tablePointer1, ChipKind.Length);
                        global::Unity.Burst.Intrinsics.X86.Avx.mm256_store_ps(outerPointer1 + (0 << 5), outer1_X);
                        global::Unity.Burst.Intrinsics.X86.Avx.mm256_store_ps(outerPointer1 + (1 << 5), outer1_Y);
                    }
                    return;
                }

                {
                    var other0 = new global::Unity.Mathematics.float4(RcpCellSize, RcpCellSize, RcpCellSize, RcpCellSize);
                    var other1 = new global::Unity.Mathematics.int4(CellWidthCount, CellWidthCount, CellWidthCount, CellWidthCount);
                    var other2 = new global::Unity.Mathematics.int4(CellCountAdjustment, CellCountAdjustment, CellCountAdjustment, CellCountAdjustment);
                    var other3 = new global::Unity.Mathematics.int4(MaxCellCountInclusive, MaxCellCountInclusive, MaxCellCountInclusive, MaxCellCountInclusive);

                    for (var outerIndex = 0; outerIndex < Position2D.Length; ++outerIndex)
                    {
                        var outer0 = Position2D[outerIndex];
                        var outer1 = Speed2D[outerIndex];
                        Exe(ref outer0.X.c0, ref outer0.Y.c0, ref outer1.X.c0, ref outer1.Y.c0, ref other0, ref other1, ref other2, ref other3, tablePointer0, SpeedSetting.Length, tablePointer1, ChipKind.Length);
                        Exe(ref outer0.X.c1, ref outer0.Y.c1, ref outer1.X.c1, ref outer1.Y.c1, ref other0, ref other1, ref other2, ref other3, tablePointer0, SpeedSetting.Length, tablePointer1, ChipKind.Length);
                        Speed2D[outerIndex] = outer1;
                    }
                }
            }
        }
    }
}

namespace Unity1Week
{
    static partial class  ChangeDestination
    {
        [global::Unity.Burst.BurstCompile]
        public unsafe partial struct Job : global::Unity.Jobs.IJob
        {
            public global::Unity.Collections.NativeArray<ComponentTypes.Destination2D.Eight> Destination2D;
            public float TargetX;
            public float TargetY;

            public void Execute()
            {
                if (global::Unity.Burst.Intrinsics.X86.Fma.IsFmaSupported)
                {
                    var other0 = new global::Unity.Burst.Intrinsics.v256(TargetX, TargetX, TargetX, TargetX, TargetX, TargetX, TargetX, TargetX);
                    var other1 = new global::Unity.Burst.Intrinsics.v256(TargetY, TargetY, TargetY, TargetY, TargetY, TargetY, TargetY, TargetY);
                    var outerPointer0 = (byte*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(Destination2D);

                    for (
                        var outerIndex = 0;
                        outerIndex < Destination2D.Length;
                        ++outerIndex,
                        outerPointer0 += sizeof(ComponentTypes.Destination2D.Eight)
                    )
                    {
                        var outer0_X = global::Unity.Burst.Intrinsics.X86.Avx.mm256_load_ps(outerPointer0 + (0 << 5));
                        var outer0_Y = global::Unity.Burst.Intrinsics.X86.Avx.mm256_load_ps(outerPointer0 + (1 << 5));
                        Exe2(ref outer0_X, ref outer0_Y, ref other0, ref other1);
                        global::Unity.Burst.Intrinsics.X86.Avx.mm256_store_ps(outerPointer0 + (0 << 5), outer0_X);
                        global::Unity.Burst.Intrinsics.X86.Avx.mm256_store_ps(outerPointer0 + (1 << 5), outer0_Y);
                    }
                    return;
                }

                {
                    var other0 = new global::Unity.Mathematics.float4(TargetX, TargetX, TargetX, TargetX);
                    var other1 = new global::Unity.Mathematics.float4(TargetY, TargetY, TargetY, TargetY);

                    for (var outerIndex = 0; outerIndex < Destination2D.Length; ++outerIndex)
                    {
                        var outer0 = Destination2D[outerIndex];
                        Exe(ref outer0.X.c0, ref outer0.Y.c0, ref other0, ref other1);
                        Exe(ref outer0.X.c1, ref outer0.Y.c1, ref other0, ref other1);
                        Destination2D[outerIndex] = outer0;
                    }
                }
            }
        }
    }
}

namespace Unity1Week
{
    static partial class  RandomlyChangeDestination
    {
        [global::Unity.Burst.BurstCompile]
        public unsafe partial struct Job : global::Unity.Jobs.IJob
        {
            public global::Unity.Collections.NativeArray<ComponentTypes.Destination2D.Eight> Destination2D;
            public global::Unity.Collections.NativeArray<Unity.Mathematics.Random> RandomArray;
            public float TwoMinInclusiveMinusMaxExclusive;
            public float MaxExclusiveMinusMinInclusive;

            public void Execute()
            {
                if (global::Unity.Burst.Intrinsics.X86.Fma.IsFmaSupported)
                {
                    var other0 = RandomArray[0];
                    var other1 = new global::Unity.Burst.Intrinsics.v256(TwoMinInclusiveMinusMaxExclusive, TwoMinInclusiveMinusMaxExclusive, TwoMinInclusiveMinusMaxExclusive, TwoMinInclusiveMinusMaxExclusive, TwoMinInclusiveMinusMaxExclusive, TwoMinInclusiveMinusMaxExclusive, TwoMinInclusiveMinusMaxExclusive, TwoMinInclusiveMinusMaxExclusive);
                    var other2 = new global::Unity.Burst.Intrinsics.v256(MaxExclusiveMinusMinInclusive, MaxExclusiveMinusMinInclusive, MaxExclusiveMinusMinInclusive, MaxExclusiveMinusMinInclusive, MaxExclusiveMinusMinInclusive, MaxExclusiveMinusMinInclusive, MaxExclusiveMinusMinInclusive, MaxExclusiveMinusMinInclusive);
                    var outerPointer0 = (byte*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(Destination2D);

                    for (
                        var outerIndex = 0;
                        outerIndex < Destination2D.Length;
                        ++outerIndex,
                        outerPointer0 += sizeof(ComponentTypes.Destination2D.Eight)
                    )
                    {
                        var outer0_X = global::Unity.Burst.Intrinsics.X86.Avx.mm256_load_ps(outerPointer0 + (0 << 5));
                        var outer0_Y = global::Unity.Burst.Intrinsics.X86.Avx.mm256_load_ps(outerPointer0 + (1 << 5));
                        Exe2(ref outer0_X, ref outer0_Y, ref other0, ref other1, ref other2);
                        global::Unity.Burst.Intrinsics.X86.Avx.mm256_store_ps(outerPointer0 + (0 << 5), outer0_X);
                        global::Unity.Burst.Intrinsics.X86.Avx.mm256_store_ps(outerPointer0 + (1 << 5), outer0_Y);
                    }

                    RandomArray[0] = other0;
                    return;
                }

                {
                    var other0 = RandomArray[0];
                    var other1 = new global::Unity.Mathematics.float4(TwoMinInclusiveMinusMaxExclusive, TwoMinInclusiveMinusMaxExclusive, TwoMinInclusiveMinusMaxExclusive, TwoMinInclusiveMinusMaxExclusive);
                    var other2 = new global::Unity.Mathematics.float4(MaxExclusiveMinusMinInclusive, MaxExclusiveMinusMinInclusive, MaxExclusiveMinusMinInclusive, MaxExclusiveMinusMinInclusive);

                    for (var outerIndex = 0; outerIndex < Destination2D.Length; ++outerIndex)
                    {
                        var outer0 = Destination2D[outerIndex];
                        Exe(ref outer0.X.c0, ref outer0.Y.c0, ref other0, ref other1, ref other2);
                        Exe(ref outer0.X.c1, ref outer0.Y.c1, ref other0, ref other1, ref other2);
                        Destination2D[outerIndex] = outer0;
                    }

                    RandomArray[0] = other0;
                }
            }
        }
    }
}

namespace Unity1Week
{
    static partial class  KillOutOfBounds
    {
        [global::Unity.Burst.BurstCompile]
        public unsafe partial struct Job : global::Unity.Jobs.IJob
        {
            [global::Unity.Collections.ReadOnly] public global::Unity.Collections.NativeArray<ComponentTypes.Position2D.Eight> Position2D;
            public global::Unity.Collections.NativeArray<ComponentTypes.AliveState.Eight> AliveState;
            public float MinInclusive;
            public float MaxExclusive;

            public void Execute()
            {
                if (global::Unity.Burst.Intrinsics.X86.Fma.IsFmaSupported)
                {
                    var other0 = new global::Unity.Burst.Intrinsics.v256(MinInclusive, MinInclusive, MinInclusive, MinInclusive, MinInclusive, MinInclusive, MinInclusive, MinInclusive);
                    var other1 = new global::Unity.Burst.Intrinsics.v256(MaxExclusive, MaxExclusive, MaxExclusive, MaxExclusive, MaxExclusive, MaxExclusive, MaxExclusive, MaxExclusive);
                    var outerPointer0 = (byte*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(Position2D);
                    var outerPointer1 = (byte*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(AliveState);

                    for (
                        var outerIndex = 0;
                        outerIndex < Position2D.Length;
                        ++outerIndex,
                        outerPointer0 += sizeof(ComponentTypes.Position2D.Eight),
                        outerPointer1 += sizeof(ComponentTypes.AliveState.Eight)
                    )
                    {
                        var outer0_X = global::Unity.Burst.Intrinsics.X86.Avx.mm256_load_ps(outerPointer0 + (0 << 5));
                        var outer0_Y = global::Unity.Burst.Intrinsics.X86.Avx.mm256_load_ps(outerPointer0 + (1 << 5));
                        var outer1_Value = global::Unity.Burst.Intrinsics.X86.Avx.mm256_load_ps(outerPointer1 + (0 << 5));
                        Exe2(ref outer0_X, ref outer0_Y, ref outer1_Value, ref other0, ref other1);
                        global::Unity.Burst.Intrinsics.X86.Avx.mm256_store_ps(outerPointer1 + (0 << 5), outer1_Value);
                    }
                    return;
                }

                {
                    var other0 = new global::Unity.Mathematics.float4(MinInclusive, MinInclusive, MinInclusive, MinInclusive);
                    var other1 = new global::Unity.Mathematics.float4(MaxExclusive, MaxExclusive, MaxExclusive, MaxExclusive);

                    for (var outerIndex = 0; outerIndex < Position2D.Length; ++outerIndex)
                    {
                        var outer0 = Position2D[outerIndex];
                        var outer1 = AliveState[outerIndex];
                        Exe(ref outer0.X.c0, ref outer0.Y.c0, ref outer1.Value.c0, ref other0, ref other1);
                        Exe(ref outer0.X.c1, ref outer0.Y.c1, ref outer1.Value.c1, ref other0, ref other1);
                        AliveState[outerIndex] = outer1;
                    }
                }
            }
        }
    }
}

