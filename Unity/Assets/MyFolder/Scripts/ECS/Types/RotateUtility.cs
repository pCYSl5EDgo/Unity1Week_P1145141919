using Unity.Collections.LowLevel.Unsafe;

namespace MyFolder.Scripts.ECS.Types
{
    public static unsafe class RotateUtility
    {
        public static void RotateHeadToTail<TContainer, TElement>(ref TContainer value)
            where TContainer : unmanaged
            where TElement : unmanaged
        {
            fixed (void* valuePointer = &value)
            {
                var element = *(TElement*)valuePointer;
                UnsafeUtility.MemMove(valuePointer, (byte*)valuePointer + sizeof(TElement), sizeof(TContainer) - sizeof(TElement));
                *(TElement*)((byte*)valuePointer + sizeof(TContainer) - sizeof(TElement)) = element;
            }
        }
    }
}