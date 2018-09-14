using System;
using System.Collections.Generic;

using Unity.Entities;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

namespace Unity1Week
{
    [UpdateAfter(typeof(PlayerEnemyRenderSystem))]
    public sealed class 空蝉RenderSystem : ComponentSystem, IRenderSystem
    {
        private readonly Material material;
        private readonly Camera mainCamera;
        private readonly float seconds;
        private readonly Mesh mesh;
        private readonly List<Matrix4x4> matrixList = new List<Matrix4x4>();
        private readonly Matrix4x4[] matrices = new Matrix4x4[1023];
        private readonly Bounds bounds = new Bounds(new Vector3(50, 0, 50), new Vector3(1000, 1000, 1000));
        private ComponentGroup g;

        public 空蝉RenderSystem(Camera mainCamera, Material material, Sprite sprite, float seconds)
        {
            this.material = material;
            this.mainCamera = mainCamera;
            this.mesh = sprite.FromSprite();
            this.seconds = seconds;
        }

        protected override void OnCreateManager(int capacity)
        {
            g = GetComponentGroup(ComponentType.ReadOnly<Position>(), ComponentType.ReadOnly<LifeTime>(), ComponentType.ReadOnly<PlayerShootSystem.空蝉Tag>());
        }

        protected override void OnUpdate()
        {
            matrixList.Clear();
            var starts = g.GetComponentDataArray<LifeTime>();
            var pos3ds = g.GetComponentDataArray<Position>();
            var entities = g.GetEntityArray();
            var currentTime = Time.timeSinceLevelLoad;
            var buf = PostUpdateCommands;
            var manager = EntityManager;
            for (int consumed = 0, length = starts.Length; consumed < length;)
            {
                var chunkStart = starts.GetChunkArray(consumed, length - consumed);
                var chunkPos2ds = pos3ds.GetChunkArray(consumed, chunkStart.Length);
                var chunkEntities = entities.GetChunkArray(consumed, chunkStart.Length);
                for (int i = 0; i < chunkStart.Length; i++)
                {
                    var startTime = chunkStart[i].Value;
                    if (startTime + seconds <= currentTime)
                        manager.DestroyEntity(chunkEntities[i]);
                    else
                    {
                        var pos = chunkPos2ds[i].Value;
                        matrixList.Add(new Matrix4x4
                        {
                            m00 = 1,
                            m11 = 1,
                            m22 = 1,
                            m33 = 1,
                            m03 = pos.x,
                            m13 = 0.1f,
                            m23 = pos.z
                        });
                    }
                }
                consumed += chunkStart.Length;
            }
            for (int consumed = 0, length = matrixList.Count; consumed < length;)
            {
                var tryToConsume = Math.Min(1023, length - consumed);
                matrixList.CopyTo(consumed, matrices, 0, tryToConsume);
                Graphics.DrawMeshInstanced(mesh, 0, material, matrices, tryToConsume, null, ShadowCastingMode.Off, false, 0, mainCamera, LightProbeUsage.Off, null);
                consumed += tryToConsume;
            }
        }
    }
}