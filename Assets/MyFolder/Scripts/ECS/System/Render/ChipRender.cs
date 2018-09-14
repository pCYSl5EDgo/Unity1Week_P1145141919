using Unity.Entities;
using Unity.Mathematics;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Transforms;
using Unity.Rendering;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Unity1Week
{
    [AlwaysUpdateSystem]
    [UpdateAfter(typeof(PlayerEnemyRenderSystem))]
    public sealed class ChipRenderSystem : ComponentSystem, IRenderSystem
    {
        public struct Tag : IComponentData
        {
            public int X, Y;
            public float TemperatureDelta;
        }

        private readonly Camera mainCamera;
        private readonly uint2 range;
        private readonly Chip[] chips;
        private readonly Mesh mesh;
        private readonly Material material;
        private readonly Matrix4x4[][] matrices;
        private readonly float[] chipTemperature;
        private readonly int[] lengths;
        private readonly Matrix4x4[] drawMatrices = new Matrix4x4[1023];
        private readonly Texture2D[] renderers;
        private readonly int _MainTex = Shader.PropertyToID("_MainTex");
        private readonly MaterialPropertyBlock block = new MaterialPropertyBlock();

        public ChipRenderSystem(Camera mainCamera, uint2 range, Chip[] chips, float[] chipTemperature, Texture2D[] renderers, Material material)
        {
            this.mainCamera = mainCamera;
            this.range = range;
            this.chips = chips;
            this.renderers = renderers;
            this.material = new Material(material);
            this.matrices = new Matrix4x4[renderers.Length][];
            this.chipTemperature = chipTemperature;
            var identity = Matrix4x4.identity;
            unsafe
            {
                Matrix4x4* iPtr = &identity;
                for (int i = 0; i < matrices.Length; i++)
                {
                    matrices[i] = new Matrix4x4[range.x * range.y];
                    fixed (Matrix4x4* ptr = matrices[i])
                    {
                        UnsafeUtility.MemCpyReplicate(ptr, iPtr, 64, matrices[i].Length);
                    }
                }
            }
            lengths = new int[renderers.Length];
            for (int y = 0, index = 0; y < range.y; ++y)
            {
                for (int x = 0; x < range.x; ++x, ++index)
                {
                    var key = chips[index].Value - 1;
                    chips[index].Temperature = chipTemperature[key];
                    ref var mat = ref matrices[key][lengths[key]++];
                    mat.m03 = x;
                    mat.m23 = y;
                }
            }
            mesh = new Mesh();
            mesh.SetVertices(new List<Vector3>(4){
                new Vector3(0, 0, 0),
                new Vector3(0, 0, 1),
                new Vector3(1, 0, 0),
                new Vector3(1, 0, 1),
            });
            mesh.SetTriangles(new int[]{
                0, 1, 2,
                2, 1, 3
            }, 0);
            mesh.SetUVs(0, new List<Vector2>(4){
                new Vector2(0, 0),
                new Vector2(0, 1),
                new Vector2(1, 0),
                new Vector2(1, 1)
            });
            mesh.RecalculateNormals();
        }

        private ComponentGroup g;
        protected override void OnCreateManager(int capacity) => g = GetComponentGroup(ComponentType.ReadOnly<Tag>());

        private void MoveChip(int x, int y, int fromIndex, int toIndex)
        {
            ref var toMatrix = ref matrices[toIndex][lengths[toIndex]++];
            toMatrix.m03 = x;
            toMatrix.m23 = y;
            var fromMatrices = matrices[fromIndex];
            ref var last = ref fromMatrices[--lengths[fromIndex]];
            for (int i = 0; i < fromMatrices.Length; ++i)
            {
                ref var fromMatrix = ref fromMatrices[i];
                if (fromMatrix.m03 != x || fromMatrix.m23 != y) continue;
                fromMatrix.m03 = last.m03;
                fromMatrix.m23 = last.m23;
                return;
            }
        }

        protected override unsafe void OnUpdate()
        {
            fixed (Matrix4x4* draw = drawMatrices)
            {
                for (int i = 0; i < matrices.Length; i++)
                {
                    if (lengths[i] == 0) continue;
                    block.SetTexture(_MainTex, renderers[i]);
                    var src = matrices[i];
                    if (lengths[i] <= 1023)
                    {
                        Graphics.DrawMeshInstanced(mesh, 0, material, src, lengths[i], block, ShadowCastingMode.Off, false, 0, mainCamera, LightProbeUsage.Off, null);
                        continue;
                    }
                    fixed (Matrix4x4* ptr = src)
                    {
                        for (int used = 0; used < lengths[i];)
                        {
                            var tryToUse = Math.Min(1023, lengths[i] - used);
                            UnsafeUtility.MemCpy(draw, ptr + used, 4 * 16 * tryToUse);
                            Graphics.DrawMeshInstanced(mesh, 0, material, drawMatrices, tryToUse, block, ShadowCastingMode.Off, false, 0, mainCamera, LightProbeUsage.Off, null);
                            used += tryToUse;
                        }
                    }
                }
            }
            var tags = g.GetComponentDataArray<Tag>();
            var length = tags.Length;
            if (length == 0) return;
            for (int i = 0; i < length; i++)
            {
                var tag = tags[i];
                if (tag.X < 0 || tag.X >= range.x || tag.Y < 0 || tag.Y >= range.y) continue;
                var key = tag.X + tag.Y * range.x;
                chips[key].Temperature += tag.TemperatureDelta;
                var oldChipValue = chips[key].Value;
                for (int j = chipTemperature.Length - 1; j >= 0; j--)
                {
                    if (chips[key].Temperature < chipTemperature[j]) continue;
                    chips[key].Value = j + 1;
                    if (chips[key].Value == oldChipValue) break;
                    MoveChip(tag.X, tag.Y, oldChipValue - 1, chips[key].Value - 1);
                    break;
                }
            }
            EntityManager.DestroyEntity(g);
        }
    }
}