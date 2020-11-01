using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace Unity1Week
{
    internal static class NativeArrayUtility
    {
        public static unsafe NativeArray<T> SkipFirst<T>(ref this NativeArray<T> array)
            where T : struct
        {
            var dataPointer = (byte*) array.GetUnsafePtr() + UnsafeUtility.SizeOf<T>();
            var answer = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>(dataPointer, array.Length - 1, Allocator.Invalid);
            #if ENABLE_UNITY_COLLECTIONS_CHECKS
            NativeArrayUnsafeUtility.SetAtomicSafetyHandle(ref answer, AtomicSafetyHandle.GetTempUnsafePtrSliceHandle());
            #endif
            return answer;
        }

        public static unsafe NativeArray<T> Skip<T>(ref this NativeArray<T> array, uint length)
            where T : struct
        {
            var dataPointer = (byte*) array.GetUnsafePtr() + UnsafeUtility.SizeOf<T>() * length;
            var answer = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>(dataPointer, (int) (array.Length - length), Allocator.Invalid);
            #if ENABLE_UNITY_COLLECTIONS_CHECKS
            NativeArrayUnsafeUtility.SetAtomicSafetyHandle(ref answer, AtomicSafetyHandle.GetTempUnsafePtrSliceHandle());
            #endif
            return answer;
        }
    }
}