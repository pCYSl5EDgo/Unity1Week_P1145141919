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
    public sealed class PlayerEnemyRenderSystem : ComponentSystem
    {
        private readonly Material playerMaterial;
        private readonly Mesh playerMesh;
        private readonly Material[] enemyMaterials;
        private readonly Mesh enemyMesh;
        private readonly Matrix4x4[] matrices = new Matrix4x4[1023];
        private readonly Camera mainCamera;
        private readonly EntityArchetypeQuery qEnemyBoss = new EntityArchetypeQuery
        {
            Any = Array.Empty<ComponentType>(),
            None = Array.Empty<ComponentType>(),
            All = new[] { ComponentType.Create<Position>(), ComponentType.Create<Boss>() },
        };
        private readonly EntityArchetypeQuery qEnemyLeader = new EntityArchetypeQuery
        {
            Any = Array.Empty<ComponentType>(),
            None = Array.Empty<ComponentType>(),
            All = new[] { ComponentType.Create<Position>(), ComponentType.Create<Leader>() },
        };
        private readonly EntityArchetypeQuery qEnemySubordinate = new EntityArchetypeQuery
        {
            Any = Array.Empty<ComponentType>(),
            None = new[] { ComponentType.Create<Leader>(), ComponentType.Create<Boss>() },
            All = new[] { ComponentType.Create<Position>(), ComponentType.Create<Enemy>() },
        };
        private readonly EntityArchetypeQuery qPlayer = new EntityArchetypeQuery
        {
            Any = Array.Empty<ComponentType>(),
            None = Array.Empty<ComponentType>(),
            All = new[] { ComponentType.Create<Position>(), ComponentType.Create<Controlable>() },
        };
        private readonly NativeList<EntityArchetype> fEnemyBoss = new NativeList<EntityArchetype>(Allocator.Persistent);
        private readonly NativeList<EntityArchetype> fEnemyLeader = new NativeList<EntityArchetype>(Allocator.Persistent);
        private readonly NativeList<EntityArchetype> fEnemySubordinate = new NativeList<EntityArchetype>(Allocator.Persistent);
        private readonly NativeList<EntityArchetype> fPlayer = new NativeList<EntityArchetype>(Allocator.Persistent);
        public PlayerEnemyRenderSystem(Camera mainCamera, Sprite playerSprite, Material playerMaterial, Mesh enemyMesh, params Material[] enemyMaterials)
        {
            this.mainCamera = mainCamera;
            this.playerMesh = playerSprite.FromSprite();
            this.playerMaterial = playerMaterial;
            this.enemyMesh = enemyMesh;
            this.enemyMaterials = enemyMaterials;
            var identity = Matrix4x4.identity;
            unsafe
            {
                fixed (Matrix4x4* ptr = this.matrices)
                    UnsafeUtility.MemCpyReplicate(ptr, &identity, 4 * 16, 1023);
            }
        }
        private EntityManager manager;
        protected override void OnCreateManager(int capacity) => manager = EntityManager;
        protected override void OnDestroyManager()
        {
            fEnemyBoss.Dispose();
            fEnemyLeader.Dispose();
            fEnemySubordinate.Dispose();
            fPlayer.Dispose();
        }

        protected override void OnUpdate()
        {
            manager.AddMatchingArchetypes(qEnemyBoss, fEnemyBoss);
            manager.AddMatchingArchetypes(qEnemyLeader, fEnemyLeader);
            manager.AddMatchingArchetypes(qEnemySubordinate, fEnemySubordinate);
            manager.AddMatchingArchetypes(qPlayer, fPlayer);
            var PositionTypeRO = manager.GetArchetypeChunkComponentType<Position>(true);
            Draw_DisposeNativeArray(enemyMesh, enemyMaterials[0], ref PositionTypeRO, manager.CreateArchetypeChunkArray(fEnemyBoss, Allocator.Temp));
            Draw_DisposeNativeArray(enemyMesh, enemyMaterials[1], ref PositionTypeRO, manager.CreateArchetypeChunkArray(fEnemyLeader, Allocator.Temp));
            Draw_DisposeNativeArray(enemyMesh, enemyMaterials[2], ref PositionTypeRO, manager.CreateArchetypeChunkArray(fEnemySubordinate, Allocator.Temp));
            Draw_DisposeNativeArray(playerMesh, playerMaterial, ref PositionTypeRO, manager.CreateArchetypeChunkArray(fPlayer, Allocator.Temp));
        }
        void Draw_DisposeNativeArray(Mesh mesh, Material material, ref ArchetypeChunkComponentType<Position> type, NativeArray<ArchetypeChunk> chunks)
        {
            int count = 0;
            for (int i = 0; i < chunks.Length; i++)
            {
                var positions = chunks[i].GetNativeArray(type);
                for (int j = 0; j < positions.Length; j++)
                {
                    ref var mat = ref matrices[count++];
                    mat.m03 = positions[j].Value.x;
                    mat.m23 = positions[j].Value.z;
                    if (count < 1023) continue;
                    Graphics.DrawMeshInstanced(mesh, 0, material, matrices, 1023, null, ShadowCastingMode.Off, false, 0, mainCamera, LightProbeUsage.Off, null);
                    count = 0;
                }
            }
            if (count != 0)
                Graphics.DrawMeshInstanced(mesh, 0, material, matrices, count, null, ShadowCastingMode.Off, false, 0, mainCamera, LightProbeUsage.Off, null);
            chunks.Dispose();
        }
    }
}