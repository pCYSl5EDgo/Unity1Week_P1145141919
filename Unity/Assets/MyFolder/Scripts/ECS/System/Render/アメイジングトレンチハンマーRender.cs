/*using System;
using Unity.Mathematics;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Rendering;

namespace Unity1Week
{
    [UpdateAfter(typeof(PlayerEnemyRenderSystem))]
    public sealed class KinokoRenderSystem : ComponentSystem, IRenderSystem
    {
        private readonly Camera mainCamera;
        private readonly Mesh mesh;
        private readonly Material material;
        private readonly double degree;
        private readonly double startDegree;
        private readonly double speed;
        private readonly float skillTime;
        private readonly Matrix4x4[] matrices = new Matrix4x4[1023];
        private readonly EntityArchetypeQuery q = new EntityArchetypeQuery
        {
            Any = Array.Empty<ComponentType>(),
            None = Array.Empty<ComponentType>(),
            All = new ComponentType[] { ComponentType.Create<PlayerShootSystem.アメイジングトレンチハンマーTag>() },
        };
        private readonly NativeList<EntityArchetype> f = new NativeList<EntityArchetype>(Allocator.Persistent);
        private EntityArchetype archetype;
        protected override void OnCreateManager(int capacity)
        {
            archetype = EntityManager.CreateArchetype(ComponentType.Create<Position2D>(), ComponentType.Create<PlayerAttackHitCollisionCircle>());
        }
        protected override void OnDestroyManager() => f.Dispose();

        public KinokoRenderSystem(Camera mainCamera, Sprite kinokoHammer, Material material, double degree, float skillTime)
        {
            this.mainCamera = mainCamera;
            this.mesh = kinokoHammer.FromSprite();
            this.material = material;
            this.degree = degree;
            this.startDegree = -degree * 0.5;
            this.skillTime = skillTime;
            speed = degree / skillTime;
            var identity = Matrix4x4.identity;
            unsafe
            {
                fixed (Matrix4x4* dest = this.matrices)
                    UnsafeUtility.MemCpyReplicate(dest, &identity, 64, 1023);
            }
        }

        protected override void OnUpdate()
        {
            var manager = EntityManager;
            manager.AddMatchingArchetypes(q, f);
            var アメイジングトレンチハンマーTagTypeRO = manager.GetArchetypeChunkComponentType<PlayerShootSystem.アメイジングトレンチハンマーTag>(true);
            var EntityType = manager.GetArchetypeChunkEntityType();
            int count = 0;
            var current = Time.timeSinceLevelLoad;
            var buf = PostUpdateCommands;
            using (var chunks = manager.CreateArchetypeChunkArray(f, Allocator.Temp))
            {
                for (int i = 0; i < chunks.Length; i++)
                {
                    var tags = chunks[i].GetNativeArray<PlayerShootSystem.アメイジングトレンチハンマーTag>(アメイジングトレンチハンマーTagTypeRO);
                    var entities = chunks[i].GetNativeArray(EntityType);
                    for (int j = 0; j < tags.Length; j++)
                    {
                        ref var matrix = ref matrices[count++];
                        var sinceCreation = current - tags[j].CreationTime;
                        if (sinceCreation >= skillTime)
                        {
                            buf.DestroyEntity(entities[j]);
                            continue;
                        }
                        var degree = sinceCreation * speed + tags[j].HeadingDegree + startDegree;
                        matrix.m00 = matrix.m22 = (float)Math.Cos(degree); // 実質サイン
                        matrix.m20 = -(matrix.m02 = (float)Math.Sin(degree)); // 実質コサインになっているのですがそれは
                        matrix.m03 = tags[j].X;
                        matrix.m23 = tags[j].Y;
                        buf.CreateEntity(archetype);
                        buf.SetComponent(new Position2D { Value = new float2(matrix.m03 + 0.5f * matrix.m02, matrix.m23 + 0.5f * matrix.m00) });
                        buf.SetComponent(new PlayerAttackHitCollisionCircle { RadiusSquared = 0.25f });
                        if (count != 1023) continue;
                        Graphics.DrawMeshInstanced(mesh, 0, material, matrices, 1023, null, ShadowCastingMode.Off, false, 0, mainCamera, LightProbeUsage.Off, null);
                        count = 0;
                    }
                }
                if (count != 0)
                    Graphics.DrawMeshInstanced(mesh, 0, material, matrices, count, null, ShadowCastingMode.Off, false, 0, mainCamera, LightProbeUsage.Off, null);
            }
        }
    }
}*/

