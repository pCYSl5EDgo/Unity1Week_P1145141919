/*using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace Unity1Week
{
    [UpdateAfter(typeof(PlayerEnemyRenderSystem))]
    public sealed class AnimationSkillRenderSystem : ComponentSystem
    {
        private readonly Camera mainCamera;
        private readonly Matrix4x4[][] matrixArray;
        private readonly int[] countArray;
        private readonly float[] timeArray;
        private readonly Mesh[][] meshArray;
        private readonly Material[] materialArray;
        private readonly EntityArchetypeQuery[] queryArray;
        private readonly NativeList<EntityArchetype>[] foundArchetypeListArray;
        private readonly HashSet<Entity> toDestroy = new HashSet<Entity>();
        private EntityManager manager;

        public AnimationSkillRenderSystem(Camera mainCamera, (EntityArchetypeQuery query, Sprite[] sprites, Material material, float time)[] array)
        {
            if (array == null) throw new ArgumentOutOfRangeException();
            this.mainCamera = mainCamera;
            int length = array.Length;
            this.timeArray = new float[length];
            this.meshArray = new Mesh[length][];
            this.materialArray = new Material[length];
            this.queryArray = new EntityArchetypeQuery[length];
            this.foundArchetypeListArray = new NativeList<EntityArchetype>[length];
            int maxSpriteVariationCount = 0;
            for (int i = 0; i < array.Length; i++)
            {
                ref var element = ref array[i];
                if (element.time == 0) throw new ArgumentException();
                this.timeArray[i] = element.time;
                ref var meshes = ref meshArray[i];
                meshes = new Mesh[element.sprites.Length];
                maxSpriteVariationCount = Math.Max(element.sprites.Length, maxSpriteVariationCount);
                for (int j = 0; j < meshes.Length; j++)
                    meshes[j] = element.sprites[j].FromSprite(0.02f);
                this.materialArray[i] = element.material;
                this.queryArray[i] = element.query;
                this.foundArchetypeListArray[i] = new NativeList<EntityArchetype>(Allocator.Persistent);
            }
            var identity = Matrix4x4.identity;
            this.matrixArray = new Matrix4x4[maxSpriteVariationCount][];
            for (int i = 0; i < this.matrixArray.Length; i++)
            {
                this.matrixArray[i] = new Matrix4x4[1023];
                unsafe
                {
                    var srcPtr = &identity;
                    fixed (Matrix4x4* destPtr = this.matrixArray[i])
                    {
                        UnsafeUtility.MemCpyReplicate(destPtr, srcPtr, 64, 1023);
                    }
                }
            }
            this.countArray = new int[maxSpriteVariationCount];
        }
        protected override void OnCreateManager(int capacity) => manager = EntityManager;
        protected override void OnDestroyManager()
        {
            for (int i = 0; i < foundArchetypeListArray.Length; i++)
                foundArchetypeListArray[i].Dispose();
        }

        protected override void OnUpdate()
        {
            toDestroy.Clear();
            var PositionTypeRO = manager.GetArchetypeChunkComponentType<Position>(true);
            var Position2DTypeRO = manager.GetArchetypeChunkComponentType<Position2D>(true);
            var LifeTimeTypeRO = manager.GetArchetypeChunkComponentType<LifeTime>(true);
            var EntityType = manager.GetArchetypeChunkEntityType();
            var currentTime = Time.timeSinceLevelLoad;
            for (int i = 0; i < queryArray.Length; i++)
            {
                manager.AddMatchingArchetypes(queryArray[i], foundArchetypeListArray[i]);
                unsafe
                {
                    fixed (int* ptr = countArray)
                    {
                        UnsafeUtility.MemClear(ptr, countArray.LongLength << 2);
                    }
                }
                using (var chunks = manager.CreateArchetypeChunkArray(foundArchetypeListArray[i], Allocator.Temp))
                {
                    ref var material = ref materialArray[i];
                    ref var meshes = ref meshArray[i];
                    for (int j = 0; j < chunks.Length; j++)
                    {
                        var lifeTimes = chunks[j].GetNativeArray(LifeTimeTypeRO);
                        if (lifeTimes.Length == 0) continue;
                        var entities = chunks[j].GetNativeArray(EntityType);
                        var positions2D = chunks[j].GetNativeArray(Position2DTypeRO);
                        if (positions2D.Length == 0)
                        {
                            var positions3D = chunks[j].GetNativeArray(PositionTypeRO);
                            if (positions3D.Length == 0) continue;
                            Execute3D(i, currentTime, entities, lifeTimes, positions3D);
                            continue;
                        }
                        Execute2D(i, currentTime, entities, lifeTimes, positions2D);
                    }
                    CleanUp(i);
                }
            }
            foreach (var entity in toDestroy)
                if (manager.Exists(entity))
                    manager.DestroyEntity(entity);
        }

        private void CleanUp(int index)
        {
            var meshes = meshArray[index];
            var material = materialArray[index];
            for (int i = 0; i < meshes.Length; i++)
                if (countArray[i] != 0)
                    Graphics.DrawMeshInstanced(meshes[i], 0, material, matrixArray[i], countArray[i], null, ShadowCastingMode.Off, false, 0, mainCamera, LightProbeUsage.Off, null);
        }

        private void Execute2D(int index, float currentTime, NativeArray<Entity> entities, NativeArray<LifeTime> lifeTimes, NativeArray<Position2D> positions)
        {
            var deathTime = timeArray[index];
            var meshes = meshArray[index];
            var material = materialArray[index];
            for (int i = 0; i < lifeTimes.Length; i++)
            {
                var time = (currentTime - lifeTimes[i].Value) / deathTime;
                if (time >= 1) // currentTime >= lifeTimes[i].Value + deathTime
                {
                    toDestroy.Add(entities[i]);
                    continue;
                }
                var stage = (int)(meshes.Length * time);
                var mesh = meshes[stage];
                ref var matrixes = ref matrixArray[stage];
                ref var count = ref countArray[stage];
                ref var matrix = ref matrixes[count++];
                matrix.m03 = positions[i].Value.x;
                matrix.m23 = positions[i].Value.y;
                if (count < 1023) continue;
                Graphics.DrawMeshInstanced(mesh, 0, material, matrixes, 1023, null, ShadowCastingMode.Off, false, 0, mainCamera, LightProbeUsage.Off, null);
                count = 0;
            }
        }

        private void Execute3D(int index, float currentTime, NativeArray<Entity> entities, NativeArray<LifeTime> lifeTimes, NativeArray<Position> positions)
        {
            var deathTime = timeArray[index];
            var meshes = meshArray[index];
            var material = materialArray[index];
            for (int i = 0; i < lifeTimes.Length; i++)
            {
                var time = (currentTime - lifeTimes[i].Value) / deathTime;
                if (time >= 1) // currentTime >= lifeTimes[i].Value + deathTime
                {
                    toDestroy.Add(entities[i]);
                    continue;
                }
                var stage = (int)(meshes.Length * time);
                var mesh = meshes[stage];
                ref var matrixes = ref matrixArray[stage];
                ref var count = ref countArray[stage];
                ref var matrix = ref matrixes[count++];
                matrix.m03 = positions[i].Value.x;
                matrix.m23 = positions[i].Value.z;
                if (count < 1023) continue;
                Graphics.DrawMeshInstanced(mesh, 0, material, matrixes, 1023, null, ShadowCastingMode.Off, false, 0, mainCamera, LightProbeUsage.Off, null);
                count = 0;
            }
        }
    }
}*/

