using System;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Transforms;

namespace Unity1Week
{
    [AlwaysUpdateSystem]
    sealed class MoveSystem : ComponentSystem
    {
        NativeList<EntityArchetype> f2d = new NativeList<EntityArchetype>(16, Allocator.Persistent);
        NativeList<EntityArchetype> f3d = new NativeList<EntityArchetype>(16, Allocator.Persistent);
        readonly EntityArchetypeQuery q2d = new EntityArchetypeQuery
        {
            All = new[] { ComponentType.ReadOnly<MoveSpeed>(), ComponentType.ReadOnly<Heading2D>(), ComponentType.Create<Position>() },
            None = Array.Empty<ComponentType>(),
            Any = Array.Empty<ComponentType>(),
        };
        readonly EntityArchetypeQuery q3d = new EntityArchetypeQuery
        {
            All = new[] { ComponentType.ReadOnly<MoveSpeed>(), ComponentType.ReadOnly<Heading3D>(), ComponentType.Create<Position>() },
            None = Array.Empty<ComponentType>(),
            Any = Array.Empty<ComponentType>(),
        };
        protected override void OnCreateManager(int capacity)
        {
        }
        protected override void OnDestroyManager()
        {
            f2d.Dispose();
            f3d.Dispose();
        }
        protected override unsafe void OnUpdate()
        {
            var deltaTime = UnityEngine.Time.deltaTime;
            EntityManager.AddMatchingArchetypes(q2d, f2d);
            EntityManager.AddMatchingArchetypes(q3d, f3d);
            var positionType = EntityManager.GetArchetypeChunkComponentType<Position>(false);
            var speedType = EntityManager.GetArchetypeChunkComponentType<MoveSpeed>(true);
            var h2d = EntityManager.GetArchetypeChunkComponentType<Heading2D>(true);
            var h3d = EntityManager.GetArchetypeChunkComponentType<Heading3D>(true);
            using (var c2d = EntityManager.CreateArchetypeChunkArray(f2d, Allocator.Temp))
            {
                for (int i = 0; i < c2d.Length; i++)
                {
                    var h2dAccessor = c2d[i].GetNativeArray(h2d);
                    var speedAccessor = c2d[i].GetNativeArray(speedType);
                    var posAccessor = c2d[i].GetNativeArray(positionType);
                    var ptr = (Position*)NativeArrayUnsafeUtility.GetUnsafePtr(posAccessor);
                    for (int j = 0; j < posAccessor.Length; j++, ptr++)
                    {
                        var times = speedAccessor[j].Value * deltaTime;
                        ptr->Value = new float3(ptr->Value.x + times * h2dAccessor[j].Value.x, ptr->Value.y, ptr->Value.z + times * h2dAccessor[j].Value.y);
                    }
                }
            }
            using (var c3d = EntityManager.CreateArchetypeChunkArray(f3d, Allocator.Temp))
            {
                for (int i = 0; i < c3d.Length; i++)
                {
                    var h3dAccessor = c3d[i].GetNativeArray(h3d);
                    var speedAccessor = c3d[i].GetNativeArray(speedType);
                    var posAccessor = c3d[i].GetNativeArray(positionType);
                    var ptr = (Position*)NativeArrayUnsafeUtility.GetUnsafePtr(posAccessor);
                    for (int j = 0; j < posAccessor.Length; j++)
                        ptr->Value += speedAccessor[j].Value * deltaTime * h3dAccessor[j].Value;
                }
            }
        }
    }
}