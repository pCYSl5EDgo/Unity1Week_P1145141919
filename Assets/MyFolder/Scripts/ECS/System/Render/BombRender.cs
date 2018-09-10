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
    [UpdateAfter(typeof(Unity.Rendering.MeshInstanceRendererSystem))]
    [UpdateAfter(typeof(TakenokoEnemyHitCheckSystem))]
    sealed class BombRenderSystem : ComponentSystem
    {
        private readonly Material material;
        private Camera mainCamera;
        private readonly int seconds;
        private readonly Mesh[] meshes = new Mesh[8];
        private readonly Matrix4x4[] matrices = new Matrix4x4[1023];
        private readonly Bounds bounds = new Bounds(new Vector3(50, 0, 50), new Vector3(1000, 1000, 1000));
        private readonly List<Matrix4x4>[] matrixList = new List<Matrix4x4>[8];
        private ComponentGroup g;
        [Inject] EndFrameBarrier barrier;

        public BombRenderSystem(Camera mainCamera, Material bombMaterial, Sprite[] boms, int seconds)
        {
            this.material = bombMaterial;
            this.mainCamera = mainCamera;
            this.seconds = seconds;
            for (int i = 0; i < meshes.Length; i++)
            {
                matrixList[i] = new List<Matrix4x4>(1024);
                meshes[i] = boms[i].FromSprite();
            }
        }

        protected override void OnCreateManager(int capacity)
        {
            g = GetComponentGroup(ComponentType.ReadOnly<Position2D>(), ComponentType.ReadOnly<BombEffect>(), ComponentType.Create<LifeTime>());
        }

        protected override void OnUpdate()
        {
            for (int i = 0; i < matrixList.Length; i++)
                matrixList[i].Clear();
            var starts = g.GetComponentDataArray<LifeTime>();
            var pos2ds = g.GetComponentDataArray<Position2D>();
            var entities = g.GetEntityArray();
            var currentTime = Time.time;
            var buf = barrier.CreateCommandBuffer();
            for (int consumed = 0, length = starts.Length; consumed < length;)
            {
                var chunkStart = starts.GetChunkArray(consumed, length - consumed);
                var chunkPos2ds = pos2ds.GetChunkArray(consumed, chunkStart.Length);
                var chunkEntities = entities.GetChunkArray(consumed, chunkStart.Length);
                for (int i = 0; i < chunkStart.Length; i++)
                {
                    var startTime = chunkStart[i].Value;
                    if (startTime + seconds <= currentTime) // そのEffectが発動から2秒以上経過している場合
                        buf.DestroyEntity(chunkEntities[i]);
                    else
                    {
                        var pos = chunkPos2ds[i].Value;
                        matrixList[(int)((currentTime - startTime) * matrixList.Length / seconds)].Add(new Matrix4x4
                        {
                            m00 = 1,
                            m11 = 1,
                            m22 = 1,
                            m33 = 1,
                            m03 = pos.x,
                            m13 = 0.1f,
                            m23 = pos.y
                        });
                    }
                }
                consumed += chunkStart.Length;
            }
            for (int i = 0; i < matrixList.Length; i++)
            {
                for (int consumed = 0, length = matrixList[i].Count; consumed < length;)
                {
                    var tryToConsume = Math.Min(1023, length - consumed);
                    matrixList[i].CopyTo(consumed, matrices, 0, tryToConsume);
                    Graphics.DrawMeshInstanced(meshes[i], 0, material, matrices, tryToConsume, null, ShadowCastingMode.Off, false, 0, mainCamera, LightProbeUsage.Off, null);
                    consumed += tryToConsume;
                }
            }
        }
    }
}