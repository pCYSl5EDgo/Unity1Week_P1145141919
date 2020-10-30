using Unity.Collections;
using Unity.Mathematics;

namespace MyFolder.Scripts.ECS.Types
{
    public struct Player
    {
        public float Life;
        public float Temperature;
        public float2 Position;

        public readonly NativeArray<float> SkillLastTime;

        public Player(float life, float temperature, float2 position, NativeArray<float> skillLastTime)
        {
            SkillLastTime = skillLastTime;
            Position = position;
            Life = life;
            Temperature = temperature;
        }
    }
}