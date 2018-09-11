using Unity.Entities;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace Unity1Week
{
    public sealed class UpdateCoolTimeSystem : ComponentSystem
    {
        protected override void OnCreateManager(int capacity)
        {
            g = GetComponentGroup(ComponentType.Create<SkillElement>());
            bomb = EntityManager.CreateArchetype(ComponentType.Create<Position2D>(), ComponentType.Create<BombEffect>(), ComponentType.Create<LifeTime>());
        }
        ComponentGroup g;
        EntityArchetype bomb;
        protected override unsafe void OnUpdate()
        {
            var deltaTime = UnityEngine.Time.deltaTime;
            var time = UnityEngine.Time.time;
            var skills = g.GetBufferArray<SkillElement>();
            for (int i = 0; i < skills.Length; i++)
            {
                var dynamicBuffer = skills[i];
                var ptr = (SkillElement*)dynamicBuffer.GetBasePointer();
                for (int j = 0, length = dynamicBuffer.Length; j < length; ++j, ++ptr)
                    ptr->SinceLastTime += deltaTime;
            }
        }
    }
}