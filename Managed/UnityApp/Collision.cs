using MyAttribute;
using Unity.Mathematics;
using ComponentTypes;

namespace Workflow
{
    [CollisionType(
        new[] { typeof(Position2D), typeof(AliveState) }, new[] { true, false },
        new[] { typeof(Position2D) }, new[] { true }
    )]
    public static partial class CollisionHolder
    {
        [CollisionMethod(CollisionIntrinsicsKind.None)]
        private static void Exe(
            [CollisionParameter(true, 0, nameof(Position2D.X))] ref float4x2 enemyX, 
            [CollisionParameter(true, 0, nameof(Position2D.Y))] ref float4x2 enemyY,
            [CollisionParameter(true, 1, nameof(AliveState.Value))] ref int4x2 enemyAliveState,
            [CollisionParameter(false, 0, nameof(Position2D.X))] ref float4x2 playerX,
            [CollisionParameter(false, 0, nameof(Position2D.Y))] ref float4x2 playerY
        )
        {
        }

        [CollisionCloseMethod(CollisionIntrinsicsKind.None, true, 1, nameof(AliveState.Value))]
        private static int4x2 Close(int4x2 a0, int4x2 a1, int4x2 a2, int4x2 a3, int4x2 a4, int4x2 a5, int4x2 a6, int4x2 a7)
        {
            return new(
                a0.c0 & a1.c0 & a2.c0 & a3.c0 & a4.c0 & a5.c0 & a6.c0 & a7.c0,
                a0.c1 & a1.c1 & a2.c1 & a3.c1 & a4.c1 & a5.c1 & a6.c1 & a7.c1
            );
        }
    }
}
