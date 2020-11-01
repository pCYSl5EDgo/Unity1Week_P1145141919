using Unity.Burst.Intrinsics;
using Unity.Mathematics;

namespace MyFolder.Scripts.ECS.Types
{
    public struct Position2D
    {
        public float4x2 X;
        public float4x2 Y;

        public static float4x2 CalculateDistanceSquared(in Position2D obj0, in Position2D obj1)
        {
            if (X86.Fma.IsFmaSupported)
            {
                unsafe
                {
                    fixed (void* ptr0 = &obj0)
                    fixed (void* ptr1 = &obj1)
                    {
                        var x0 = X86.Avx.mm256_load_ps(ptr0);
                        var x1 = X86.Avx.mm256_load_ps(ptr1);
                        var diffX = X86.Avx.mm256_sub_ps(x0, x1);
                        var lengthSquared = X86.Avx.mm256_mul_ps(diffX, diffX);

                        var y0 = X86.Avx.mm256_load_ps((byte*)ptr0 + sizeof(v256));
                        var y1 = X86.Avx.mm256_load_ps((byte*)ptr1 + sizeof(v256));
                        var diffY = X86.Avx.mm256_sub_ps(y0, y1);
                        lengthSquared = X86.Fma.mm256_fmadd_ps(diffY, diffY, lengthSquared);
                            
                        return *(float4x2*)&lengthSquared;
                    }
                }
            }
            
            {
                var diffX = obj0.X - obj1.X;
                var diffY = obj0.Y - obj1.Y;
                return diffX * diffX + diffY * diffY;
            }
        }
    }
}