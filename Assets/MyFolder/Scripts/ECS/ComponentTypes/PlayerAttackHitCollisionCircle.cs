using Unity.Entities;
using Unity.Mathematics;
using System;

namespace Unity1Week
{
    public struct PlayerAttackHitCollisionCircle : IComponentData
    {
        public float RadiusSquared;
        public bool IsInRange(ref float2 center, ref float3 position)
        {
            var diffX = position.x - center.x;
            var diffY = position.z - center.y;
            return diffX * diffX + diffY * diffY <= RadiusSquared;
        }
    }
}