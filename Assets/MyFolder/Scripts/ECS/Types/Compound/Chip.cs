using Unity.Collections;

namespace MyFolder.Scripts.ECS.Types
{
    public struct Chip
    {
        public NativeArray<int> Kind;
        public NativeArray<float> Temperature;
    }
}