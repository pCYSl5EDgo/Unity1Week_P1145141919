using Unity.Collections;

namespace MyFolder.Scripts.ECS.Types
{
    public struct Enemy
    {
        public NativeArray<Position2D> Position;

        public NativeArray<Destination2D> Destination;

        public NativeArray<Speed2D> Speed;

        public NativeArray<IsAlive> IsAlive;
    }
}
