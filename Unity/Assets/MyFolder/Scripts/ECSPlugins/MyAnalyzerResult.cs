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

namespace Unity1Week.Hit
{
    static partial class  BulletEnemyHit
    {
        [global::Unity.Burst.BurstCompile]
        public unsafe partial struct CollisionJob : global::Unity.Jobs.IJob
        {
            [global::Unity.Collections.ReadOnly] public global::Unity.Collections.NativeArray<ComponentTypes.Position2D.Eight> Outer0;
            public global::Unity.Collections.NativeArray<ComponentTypes.AliveState.Eight> Outer1;
            [global::Unity.Collections.ReadOnly] public global::Unity.Collections.NativeArray<ComponentTypes.Position2D.Eight> Inner0;
            [global::Unity.Collections.ReadOnly] public global::Unity.Collections.NativeArray<ComponentTypes.AliveState.Eight> Inner1;
            public float Other0;
            public float Other1;
            public global::Unity.Collections.NativeArray<int> Other2;
            public global::Unity.Collections.NativeArray<ComponentTypes.Position2D.Eight> Table0;
            public global::Unity.Collections.NativeArray<ComponentTypes.FireStartTime.Eight> Table1;

            public void Execute()
            {
                var tablePointer0 = global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(Table0);
                var tablePointer1 = global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(Table1);

                if (global::Unity.Burst.Intrinsics.X86.Fma.IsFmaSupported)
                {
                    const int next1 = 0b00_11_10_01;
                    const int next2 = 0b01_00_11_10;
                    const int next3 = 0b10_01_00_11;
                    var other0 = new global::Unity.Burst.Intrinsics.v256(Other0, Other0, Other0, Other0, Other0, Other0, Other0, Other0);
                    var other1 = new global::Unity.Burst.Intrinsics.v256(Other1, Other1, Other1, Other1, Other1, Other1, Other1, Other1);
                    var other2 = Other2[0];

                    var outerPointer0 = (byte*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(Outer0);
                    var outerPointer1 = (byte*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(Outer1);
                    var innerOriginalPointer0 = (byte*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(Inner0);
                    var innerOriginalPointer1 = (byte*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(Inner1);

                    for (
                        var outerIndex = 0;
                        outerIndex < Outer0.Length;
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
                            innerIndex < Inner0.Length;
                            ++innerIndex,
                            innerPointer0 += sizeof(ComponentTypes.Position2D.Eight),
                            innerPointer1 += sizeof(ComponentTypes.AliveState.Eight)
                        )
                        {
                            var inner0_X = global::Unity.Burst.Intrinsics.X86.Avx.mm256_load_ps(innerPointer0 + (0 << 5));
                            var inner0_Y = global::Unity.Burst.Intrinsics.X86.Avx.mm256_load_ps(innerPointer0 + (1 << 5));
                            var inner1_Value = global::Unity.Burst.Intrinsics.X86.Avx.mm256_load_ps(innerPointer1 + (0 << 5));

                            Exe2(ref outer0_X0, ref outer0_Y0, ref outer1_Value0, ref inner0_X, ref inner0_Y, ref inner1_Value, ref other0, ref other1, ref other2, tablePointer0, Table0.Length, tablePointer1, Table1.Length);
                            Exe2(ref outer0_X1, ref outer0_Y1, ref outer1_Value1, ref inner0_X, ref inner0_Y, ref inner1_Value, ref other0, ref other1, ref other2, tablePointer0, Table0.Length, tablePointer1, Table1.Length);
                            Exe2(ref outer0_X2, ref outer0_Y2, ref outer1_Value2, ref inner0_X, ref inner0_Y, ref inner1_Value, ref other0, ref other1, ref other2, tablePointer0, Table0.Length, tablePointer1, Table1.Length);
                            Exe2(ref outer0_X3, ref outer0_Y3, ref outer1_Value3, ref inner0_X, ref inner0_Y, ref inner1_Value, ref other0, ref other1, ref other2, tablePointer0, Table0.Length, tablePointer1, Table1.Length);
                            Exe2(ref outer0_X4, ref outer0_Y4, ref outer1_Value4, ref inner0_X, ref inner0_Y, ref inner1_Value, ref other0, ref other1, ref other2, tablePointer0, Table0.Length, tablePointer1, Table1.Length);
                            Exe2(ref outer0_X5, ref outer0_Y5, ref outer1_Value5, ref inner0_X, ref inner0_Y, ref inner1_Value, ref other0, ref other1, ref other2, tablePointer0, Table0.Length, tablePointer1, Table1.Length);
                            Exe2(ref outer0_X6, ref outer0_Y6, ref outer1_Value6, ref inner0_X, ref inner0_Y, ref inner1_Value, ref other0, ref other1, ref other2, tablePointer0, Table0.Length, tablePointer1, Table1.Length);
                            Exe2(ref outer0_X7, ref outer0_Y7, ref outer1_Value7, ref inner0_X, ref inner0_Y, ref inner1_Value, ref other0, ref other1, ref other2, tablePointer0, Table0.Length, tablePointer1, Table1.Length);
                        }

                        outer1_Value1 = global::Unity.Burst.Intrinsics.X86.Avx.mm256_permute_ps(outer1_Value1, 0b10_01_00_11);
                        outer1_Value2 = global::Unity.Burst.Intrinsics.X86.Avx.mm256_permute_ps(outer1_Value2, 0b01_00_11_10);
                        outer1_Value3 = global::Unity.Burst.Intrinsics.X86.Avx.mm256_permute_ps(outer1_Value3, 0b00_11_10_01);
                        outer1_Value4 = global::Unity.Burst.Intrinsics.X86.Avx.mm256_permute2f128_ps(outer1_Value4, outer1_Value4, 0b0000_0001);
                        outer1_Value5 = global::Unity.Burst.Intrinsics.X86.Avx.mm256_permute_ps(global::Unity.Burst.Intrinsics.X86.Avx.mm256_permute2f128_ps(outer1_Value5, outer1_Value5, 0b0000_0001), 0b10_01_00_11);
                        outer1_Value6 = global::Unity.Burst.Intrinsics.X86.Avx.mm256_permute_ps(global::Unity.Burst.Intrinsics.X86.Avx.mm256_permute2f128_ps(outer1_Value6, outer1_Value6, 0b0000_0001), 0b01_00_11_10);
                        outer1_Value7 = global::Unity.Burst.Intrinsics.X86.Avx.mm256_permute_ps(global::Unity.Burst.Intrinsics.X86.Avx.mm256_permute2f128_ps(outer1_Value7, outer1_Value7, 0b0000_0001), 0b00_11_10_01);
                        global::Unity.Burst.Intrinsics.X86.Avx.mm256_store_ps(outer1_Value, CloseAlive2(CloseAlive2(CloseAlive2(outer1_Value0, outer1_Value1), CloseAlive2(outer1_Value2, outer1_Value3)), CloseAlive2(CloseAlive2(outer1_Value4, outer1_Value5), CloseAlive2(outer1_Value6, outer1_Value7))));
                    }

                    Other2[0] = other2;
                    return;
                }

                {
                    var other0 = new global::Unity.Mathematics.float4(Other0, Other0, Other0, Other0);
                    var other1 = new global::Unity.Mathematics.float4(Other1, Other1, Other1, Other1);
                    var other2 = Other2[0];
                    for (var outerIndex = 0; outerIndex < Outer0.Length; ++outerIndex)
                    {
                        var outer0 = Outer0[outerIndex];
                        var outer1 = Outer1[outerIndex];
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
                        for (var innerIndex = 0; innerIndex < Inner0.Length; ++innerIndex)
                        {
                            var inner0 = Inner0[innerIndex];
                            var inner1 = Inner1[innerIndex];
                            ref var inner0_X = ref inner0.X;
                            ref var inner0_Y = ref inner0.Y;
                            ref var inner1_Value = ref inner1.Value;

                            Exe(ref outer0_X0_c0, ref outer0_Y0_c0, ref outer1_Value0_c0, ref inner0_X.c0, ref inner0_Y.c0, ref inner1_Value.c0, ref other0, ref other1, ref other2, tablePointer0, Table0.Length, tablePointer1, Table1.Length);
                            Exe(ref outer0_X0_c0, ref outer0_Y0_c0, ref outer1_Value0_c0, ref inner0_X.c1, ref inner0_Y.c1, ref inner1_Value.c1, ref other0, ref other1, ref other2, tablePointer0, Table0.Length, tablePointer1, Table1.Length);
                            Exe(ref outer0_X0_c1, ref outer0_Y0_c1, ref outer1_Value0_c1, ref inner0_X.c0, ref inner0_Y.c0, ref inner1_Value.c0, ref other0, ref other1, ref other2, tablePointer0, Table0.Length, tablePointer1, Table1.Length);
                            Exe(ref outer0_X0_c1, ref outer0_Y0_c1, ref outer1_Value0_c1, ref inner0_X.c1, ref inner0_Y.c1, ref inner1_Value.c1, ref other0, ref other1, ref other2, tablePointer0, Table0.Length, tablePointer1, Table1.Length);
                            Exe(ref outer0_X1_c0, ref outer0_Y1_c0, ref outer1_Value1_c0, ref inner0_X.c0, ref inner0_Y.c0, ref inner1_Value.c0, ref other0, ref other1, ref other2, tablePointer0, Table0.Length, tablePointer1, Table1.Length);
                            Exe(ref outer0_X1_c0, ref outer0_Y1_c0, ref outer1_Value1_c0, ref inner0_X.c1, ref inner0_Y.c1, ref inner1_Value.c1, ref other0, ref other1, ref other2, tablePointer0, Table0.Length, tablePointer1, Table1.Length);
                            Exe(ref outer0_X1_c1, ref outer0_Y1_c1, ref outer1_Value1_c1, ref inner0_X.c0, ref inner0_Y.c0, ref inner1_Value.c0, ref other0, ref other1, ref other2, tablePointer0, Table0.Length, tablePointer1, Table1.Length);
                            Exe(ref outer0_X1_c1, ref outer0_Y1_c1, ref outer1_Value1_c1, ref inner0_X.c1, ref inner0_Y.c1, ref inner1_Value.c1, ref other0, ref other1, ref other2, tablePointer0, Table0.Length, tablePointer1, Table1.Length);
                            Exe(ref outer0_X2_c0, ref outer0_Y2_c0, ref outer1_Value2_c0, ref inner0_X.c0, ref inner0_Y.c0, ref inner1_Value.c0, ref other0, ref other1, ref other2, tablePointer0, Table0.Length, tablePointer1, Table1.Length);
                            Exe(ref outer0_X2_c0, ref outer0_Y2_c0, ref outer1_Value2_c0, ref inner0_X.c1, ref inner0_Y.c1, ref inner1_Value.c1, ref other0, ref other1, ref other2, tablePointer0, Table0.Length, tablePointer1, Table1.Length);
                            Exe(ref outer0_X2_c1, ref outer0_Y2_c1, ref outer1_Value2_c1, ref inner0_X.c0, ref inner0_Y.c0, ref inner1_Value.c0, ref other0, ref other1, ref other2, tablePointer0, Table0.Length, tablePointer1, Table1.Length);
                            Exe(ref outer0_X2_c1, ref outer0_Y2_c1, ref outer1_Value2_c1, ref inner0_X.c1, ref inner0_Y.c1, ref inner1_Value.c1, ref other0, ref other1, ref other2, tablePointer0, Table0.Length, tablePointer1, Table1.Length);
                            Exe(ref outer0_X3_c0, ref outer0_Y3_c0, ref outer1_Value3_c0, ref inner0_X.c0, ref inner0_Y.c0, ref inner1_Value.c0, ref other0, ref other1, ref other2, tablePointer0, Table0.Length, tablePointer1, Table1.Length);
                            Exe(ref outer0_X3_c0, ref outer0_Y3_c0, ref outer1_Value3_c0, ref inner0_X.c1, ref inner0_Y.c1, ref inner1_Value.c1, ref other0, ref other1, ref other2, tablePointer0, Table0.Length, tablePointer1, Table1.Length);
                            Exe(ref outer0_X3_c1, ref outer0_Y3_c1, ref outer1_Value3_c1, ref inner0_X.c0, ref inner0_Y.c0, ref inner1_Value.c0, ref other0, ref other1, ref other2, tablePointer0, Table0.Length, tablePointer1, Table1.Length);
                            Exe(ref outer0_X3_c1, ref outer0_Y3_c1, ref outer1_Value3_c1, ref inner0_X.c1, ref inner0_Y.c1, ref inner1_Value.c1, ref other0, ref other1, ref other2, tablePointer0, Table0.Length, tablePointer1, Table1.Length);
                        }

                        outer1_Value0.c0 = CloseAlive(CloseAlive(outer1_Value0.c0, outer1_Value1_c0.yzwx), CloseAlive(outer1_Value2_c0.zwxy, outer1_Value3_c0.wxyz));
                        outer1_Value0.c1 = CloseAlive(CloseAlive(outer1_Value0.c1, outer1_Value1_c1.yzwx), CloseAlive(outer1_Value2_c1.zwxy, outer1_Value3_c1.wxyz));
                        Outer1[outerIndex] = outer1;
                    }

                    Other2[0] = other2;
                }
            }
        }
    }
}


