using Unity.Mathematics;

namespace Unity1Week
{
    public struct PlayerAttackHitCollisionCircle
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