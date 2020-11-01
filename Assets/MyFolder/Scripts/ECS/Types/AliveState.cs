using Unity.Mathematics;

namespace MyFolder.Scripts.ECS.Types
{
    public struct AliveState
    {
        public int4x2 Value;
        
        public enum State
        {
            Alive,
            Dead = -1,
        }
    }
}