using Unity.Mathematics;

namespace MyFolder.Scripts.ECS.Types
{
    public struct BulletState
    {
        public int4x2 Value;
        
        public enum State
        {
            Alive,
            Fire,
            Dead = -1,
        }
    }
}