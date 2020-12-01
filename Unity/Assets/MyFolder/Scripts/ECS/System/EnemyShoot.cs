using ComponentTypes;
using MyAttribute;
using Unity.Burst.Intrinsics;
using Unity.Mathematics;

namespace Unity1Week
{
    [SingleLoopType(
        new[] { typeof(Position2D), typeof(AliveState) }, new[] { true, true }, "Enemy",
        new[] { typeof(float), typeof(float), typeof(float), typeof(int) }, new[] { true, true, true, false }, new[] { "PlayerPositionX", "PlayerPositionY", "Speed", "SnowCount" },
        new[] { typeof(Position2D.Eight), typeof(Speed2D.Eight), typeof(AliveState.Eight) }, new[] { false, false, false }, new[] { "SnowPosition", "SnowSpeed", "SnowAliveState" }
    )]
    public static unsafe partial class EnemySnowShoot
    {
        [MethodIntrinsicsKind(IntrinsicsKind.Ordinal)]
        private static void Exe(
            ref float4 positionX,    
            ref float4 positionY,
            ref int4 alive,
            ref float4 playerPositionX,
            ref float4 playerPositionY,
            ref float4 snowSpeed,
            ref int snowCount,
            void* snowPositionArray,
            int snowPositionArrayLength,
            void* snowSpeedArray,
            int snowSpeedArrayLength,
            void* snowAliveStateArray,
            int snowAliveStateArrayLength
        )
        {
            var isAlive = alive == 0;
            var mask = math.bitmask(isAlive);
            if (mask == 0b1111)
            {
                return;
            }

            var diffX = playerPositionX - positionX;
            var diffY = playerPositionY - positionY;
            var speedAdjust = snowSpeed * math.rsqrt(diffX * diffX + diffY * diffY);
            var assignSpeedX = speedAdjust * diffX;
            var assignSpeedY = speedAdjust * diffY;
            
            var bigIndex = snowCount >> 3;
            var smallIndex = snowCount & 7;
            var position = ((Position2D.Eight*)snowPositionArray) + bigIndex;
            var speed = ((Speed2D.Eight*)snowSpeedArray) + bigIndex;
            var aliveState = (AliveState.State*)snowAliveStateArray;

            for (var i = 0; i < 4; i++)
            {
                if (!isAlive[i]) continue;
                
                ((float*)&position->X)[smallIndex] = positionX[i];
                ((float*)&position->Y)[smallIndex] = positionY[i];
                ((float*)&speed->X)[smallIndex] = assignSpeedX[i]; 
                ((float*)&speed->Y)[smallIndex] = assignSpeedY[i];
                aliveState[snowCount] = AliveState.State.Alive;
                snowCount++;
                if (++smallIndex != 8) continue;
                
                position++;
                speed++;
                smallIndex = 0;
            }
        }

        [MethodIntrinsicsKind(IntrinsicsKind.Fma)]
        private static void Exe2(
            ref v256 positionX,
            ref v256 positionY,
            ref v256 alive,
            ref v256 playerPositionX,
            ref v256 playerPositionY,
            ref v256 snowSpeed,
            ref int snowCount,
            void* snowPositionArray,
            int snowPositionArrayLength,
            void* snowSpeedArray,
            int snowSpeedArrayLength,
            void* snowAliveStateArray,
            int snowAliveStateArrayLength
        )
        {
            if(!X86.Fma.IsFmaSupported) return;

            var movemask = X86.Avx.mm256_movemask_ps(alive);
            if (movemask == 0xff) return;

            var diffX = X86.Avx.mm256_mul_ps(playerPositionX, positionX);
            var diffY = X86.Avx.mm256_mul_ps(playerPositionY, positionY);
            var speedAdjust = X86.Avx.mm256_mul_ps(X86.Avx.mm256_rsqrt_ps(X86.Fma.mm256_fmadd_ps(diffY, diffY, X86.Avx.mm256_mul_ps(diffX, diffX))), snowSpeed);
            var assignSpeedX = X86.Avx.mm256_mul_ps(diffX, speedAdjust);
            var assignSpeedY = X86.Avx.mm256_mul_ps(diffY, speedAdjust);

            var bigIndex = snowCount >> 3;
            var smallIndex = snowCount & 7;
            var position = ((Position2D.Eight*)snowPositionArray) + bigIndex;
            var speed = ((Speed2D.Eight*)snowSpeedArray) + bigIndex;
            var aliveState = (AliveState.State*)snowAliveStateArray;

            if (((movemask >> 0) & 1) == 0)
            {
                ((float*)&position->X)[smallIndex] = positionX.Float0;
                ((float*)&position->Y)[smallIndex] = positionY.Float0;
                ((float*)&speed->X)[smallIndex] = assignSpeedX.Float0; 
                ((float*)&speed->Y)[smallIndex] = assignSpeedY.Float0;
                aliveState[snowCount] = AliveState.State.Alive;
                snowCount++;
                if (++smallIndex == 8)
                {
                    position++;
                    speed++;
                    smallIndex = 0;
                }
            }
            
            if (((movemask >> 1) & 1) == 0)
            {
                ((float*)&position->X)[smallIndex] = positionX.Float1;
                ((float*)&position->Y)[smallIndex] = positionY.Float1;
                ((float*)&speed->X)[smallIndex] = assignSpeedX.Float1; 
                ((float*)&speed->Y)[smallIndex] = assignSpeedY.Float1;
                aliveState[snowCount] = AliveState.State.Alive;
                snowCount++;
                if (++smallIndex == 8)
                {
                    position++;
                    speed++;
                    smallIndex = 0;
                }
            }
            
            if (((movemask >> 2) & 1) == 0)
            {
                ((float*)&position->X)[smallIndex] = positionX.Float2;
                ((float*)&position->Y)[smallIndex] = positionY.Float2;
                ((float*)&speed->X)[smallIndex] = assignSpeedX.Float2; 
                ((float*)&speed->Y)[smallIndex] = assignSpeedY.Float2;
                aliveState[snowCount] = AliveState.State.Alive;
                snowCount++;
                if (++smallIndex == 8)
                {
                    position++;
                    speed++;
                    smallIndex = 0;
                }
            }
            
            if (((movemask >> 3) & 1) == 0)
            {
                ((float*)&position->X)[smallIndex] = positionX.Float3;
                ((float*)&position->Y)[smallIndex] = positionY.Float3;
                ((float*)&speed->X)[smallIndex] = assignSpeedX.Float3; 
                ((float*)&speed->Y)[smallIndex] = assignSpeedY.Float3;
                aliveState[snowCount] = AliveState.State.Alive;
                snowCount++;
                if (++smallIndex == 8)
                {
                    position++;
                    speed++;
                    smallIndex = 0;
                }
            }
            
            if (((movemask >> 4) & 1) == 0)
            {
                ((float*)&position->X)[smallIndex] = positionX.Float4;
                ((float*)&position->Y)[smallIndex] = positionY.Float4;
                ((float*)&speed->X)[smallIndex] = assignSpeedX.Float4; 
                ((float*)&speed->Y)[smallIndex] = assignSpeedY.Float4;
                aliveState[snowCount] = AliveState.State.Alive;
                snowCount++;
                if (++smallIndex == 8)
                {
                    position++;
                    speed++;
                    smallIndex = 0;
                }
            }
            
            if (((movemask >> 5) & 1) == 0)
            {
                ((float*)&position->X)[smallIndex] = positionX.Float5;
                ((float*)&position->Y)[smallIndex] = positionY.Float5;
                ((float*)&speed->X)[smallIndex] = assignSpeedX.Float5; 
                ((float*)&speed->Y)[smallIndex] = assignSpeedY.Float5;
                aliveState[snowCount] = AliveState.State.Alive;
                snowCount++;
                if (++smallIndex == 8)
                {
                    position++;
                    speed++;
                    smallIndex = 0;
                }
            }
            
            if (((movemask >> 6) & 1) == 0)
            {
                ((float*)&position->X)[smallIndex] = positionX.Float6;
                ((float*)&position->Y)[smallIndex] = positionY.Float6;
                ((float*)&speed->X)[smallIndex] = assignSpeedX.Float6; 
                ((float*)&speed->Y)[smallIndex] = assignSpeedY.Float6;
                aliveState[snowCount] = AliveState.State.Alive;
                snowCount++;
                if (++smallIndex == 8)
                {
                    position++;
                    speed++;
                    smallIndex = 0;
                }
            }
            
            if (((movemask >> 7) & 1) == 0)
            {
                ((float*)&position->X)[smallIndex] = positionX.Float7;
                ((float*)&position->Y)[smallIndex] = positionY.Float7;
                ((float*)&speed->X)[smallIndex] = assignSpeedX.Float7; 
                ((float*)&speed->Y)[smallIndex] = assignSpeedY.Float7;
                aliveState[snowCount] = AliveState.State.Alive;
                snowCount++;
            }
        }
    }
}
