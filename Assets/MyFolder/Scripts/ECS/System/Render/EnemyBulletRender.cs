using System;
using Unity.Entities;
using Unity.Transforms;
using Unity.Rendering;
using Unity.Mathematics;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Rendering;

namespace Unity1Week
{
    [UpdateAfter(typeof(MoveSystem))]
    public sealed class EnemyBulletRenderSystem : ComponentSystem
    {
        private readonly Camera mainCamera;
        private readonly Mesh mesh;
        private readonly Material material;
        private readonly Matrix4x4[] matrices = new Matrix4x4[1023];
        private ComponentGroup g;

        public EnemyBulletRenderSystem(Camera mainCamera, Sprite snowBullet, Material material)
        {
            this.mainCamera = mainCamera;
            this.mesh = snowBullet.FromSprite();
            this.material = material;
        }

        protected override void OnCreateManager(int capacity)
        {
            g = GetComponentGroup(ComponentType.ReadOnly<Position>(), ComponentType.ReadOnly<Heading2D>(), ComponentType.ReadOnly<SnowBulletTag>());
        }
        protected override void OnUpdate()
        {
            var positions = g.GetComponentDataArray<Position>();
            var headings = g.GetComponentDataArray<Heading2D>();
            var sin = (float)Math.Sin(Time.time);
            var cos = (float)Math.Cos(Time.time);
            for (int consumed = 0; consumed < positions.Length;)
            {
                var tryToConsume = Math.Min(1023, positions.Length - consumed);
                for (int i = 0; i < tryToConsume; i++)
                {
                    var pos = positions[i + consumed].Value;
                    ref var matrix = ref matrices[i];
                    matrix.m00 = matrix.m22 = cos;
                    matrix.m02 = -sin;
                    matrix.m20 = sin;
                    matrix.m11 = matrix.m33 = 1;
                    matrix.m03 = pos.x;
                    matrix.m23 = pos.z;
                }
                Graphics.DrawMeshInstanced(mesh, 0, material, matrices, tryToConsume, null, ShadowCastingMode.Off, false, 0, mainCamera, LightProbeUsage.Off, null);
                consumed += tryToConsume;
            }
        }
    }
}