using System;
using ComponentTypes;
using Unity.Burst.Intrinsics;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Jobs.LowLevel.Unsafe;
using Unity.Mathematics;
using Unity1Week;
using Unity1Week.ScriptableObjects;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using Random = Unity.Mathematics.Random;

namespace MyFolder.Scripts.Example
{
    public class ExampleEntry
        : MonoBehaviour,
            OriginalInputAction.IPlayerActions
    {
        private static readonly int positions = Shader.PropertyToID("positions");
        public EnemyDisplay Display;
        public Map MapSetting;
        public Mesh mesh;
        public int ShiftCount;

        private NativeArray<int> chipKindArray;
        private NativeArray<v256> randoms;
        private JobHandle? handle;

        private OriginalInputAction.PlayerActions inputAction;
        private Vector2 inputMoveValue;
        private Transform mainCameraTransform;
        
        private EnemyDisplayable normal;
        private EnemyDisplayable leader;
        private EnemyDisplayable boss;

        private struct EnemyDisplayable : IDisposable
        {
            public Enemy.SortJob Sort;
            private GraphicsBuffer buffer;
            private KillOutOfBounds.Job killJob;
            private readonly Material material;

            public EnemyDisplayable(int capacity, Material material, float minInclusive, float maxExclusive)
            {
                Sort = new Enemy.SortJob
                {
                    This = new Enemy.Countable(capacity)
                };
                
                this.material = material;
                buffer = new GraphicsBuffer(GraphicsBuffer.Target.Structured | GraphicsBuffer.Target.IndirectArguments, capacity << 4, 4);
                material.SetBuffer(positions, buffer);

                killJob = new KillOutOfBounds.Job
                {
                    MinInclusive = minInclusive,
                    MaxExclusive = maxExclusive,
                };
            }
            
            public bool DrawAndScheduleJob(JobHandle? oldJobHandle, EnemyDisplay display, Bounds bounds, float deltaTime, out JobHandle job)
            {
                var chunkCount = Sort.This.ChunkCount;
                if (chunkCount == 0)
                {
                    if (oldJobHandle.HasValue && !oldJobHandle.Value.IsCompleted)
                    {
                        job = oldJobHandle.Value;
                        return true;
                    }
                
                    job = default;
                    return false;
                }
            
                var position2D = Sort.This.PositionArray.GetSubArray(0, chunkCount);
                buffer.SetData(position2D, 0, 0, position2D.Length);
                var count = Sort.This.Count[0];
                Graphics.DrawMeshInstancedProcedural(display.EnemyMesh, 0, material, bounds, count, null, ShadowCastingMode.Off, false);

                job = new MultiMove.Job
                {
                    DeltaTime = deltaTime,
                    Position2D = position2D,
                    Speed2D = Sort.This.SpeedArray.GetSubArray(0, chunkCount)
                }.Schedule(oldJobHandle ?? default);
                
                killJob.AliveState = Sort.This.IsAliveArray.GetSubArray(0, chunkCount);
                killJob.Position2D = position2D;
                job = killJob.Schedule(job);
                
                //job = Sort.Schedule(job);
                
                return true;
            }

            public void Dispose()
            {
                buffer?.Dispose();
                buffer = default;
                Sort.This.Dispose();
            }
        }

        private void Awake()
        {
            inputAction = new OriginalInputAction.PlayerActions(new OriginalInputAction());
            inputAction.SetCallbacks(this);
            MapSetting.Initialize(ShiftCount);
            
            mainCameraTransform = Camera.main != null ? Camera.main.transform : default;
            randoms = new NativeArray<Guid>(6, Allocator.Persistent)
            {
                [0] = Guid.NewGuid(),
                [1] = Guid.NewGuid(),
                [2] = Guid.NewGuid(),
                [3] = Guid.NewGuid(),
                [4] = Guid.NewGuid(),
                [5] = Guid.NewGuid(),
            }.Reinterpret<v256>(16);

            normal = new EnemyDisplayable(10240, Display.EnemyNormalMaterial, MapSetting.MinInclusive, MapSetting.MaxExclusive);
            leader = new EnemyDisplayable(10240, Display.EnemyLeaderMaterial, MapSetting.MinInclusive, MapSetting.MaxExclusive);
            boss = new EnemyDisplayable(10240, Display.EnemyBossMaterial, MapSetting.MinInclusive, MapSetting.MaxExclusive);
            handle = JobHandle.CombineDependencies(Initialize(ref normal, 0), Initialize(ref leader, 1), Initialize(ref boss, 2));
            chipKindArray = new NativeArray<int>(MapSetting.ChipCount, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
            for (var i = 0; i < chipKindArray.Length; i++) chipKindArray[i] = i % 6;
        }

        private JobHandle Initialize(ref EnemyDisplayable displayable, int index)
        {
            displayable.Sort.This.Count[0] = displayable.Sort.This.Capacity << 3;
            return new RandomSpawn.Job
            {
                Random = randoms.GetSubArray(index, 1),
                AliveState = displayable.Sort.This.IsAliveArray,
                Position2D = displayable.Sort.This.PositionArray,
                Speed2D = displayable.Sort.This.SpeedArray,
                MaxExclusive_Minus_MinInclusive = MapSetting.MaxExclusive - MapSetting.MinInclusive,
                TwoMinInclusive_Minus_MaxExclusive = 2 * MapSetting.MinInclusive - MapSetting.MaxExclusive,
                MaxSpeed = 30f
            }.Schedule();
        }

        private void Update()
        {
            handle?.Complete();

            MapSetting.Buffer.SetData(chipKindArray);
            Graphics.DrawMeshInstancedProcedural(mesh, 0, MapSetting.ChipMaterial, MapSetting.Bounds, MapSetting.ChipCount, castShadows: ShadowCastingMode.Off, receiveShadows: false);

            var deltaTime = Time.deltaTime;
            var normalExists = normal.DrawAndScheduleJob(handle, Display, MapSetting.Bounds, deltaTime, out var normalJob);
            var leaderExists = leader.DrawAndScheduleJob(handle, Display, MapSetting.Bounds, deltaTime, out var leaderJob);
            var bossExists = boss.DrawAndScheduleJob(handle, Display, MapSetting.Bounds, deltaTime, out var bossJob);

            if (normalExists)
            {
                if (leaderExists)
                {
                    handle = bossExists ? JobHandle.CombineDependencies(normalJob, leaderJob, bossJob) : JobHandle.CombineDependencies(normalJob, leaderJob);
                }
                else
                {
                    handle = bossExists ? JobHandle.CombineDependencies(normalJob, bossJob) : normalJob;
                }
            }
            else
            {
                if (leaderExists)
                {
                    handle = bossExists ? JobHandle.CombineDependencies(leaderJob, bossJob) : leaderJob;
                }
                else
                {
                    handle = bossExists ? bossJob : default;
                }
            }

            if (inputMoveValue != Vector2.zero)
            {
                var p = mainCameraTransform.position;
                p += (Vector3)(inputMoveValue * (deltaTime * 40f));
                mainCameraTransform.position = p;
            }
        }

        private void OnEnable()
        {
            inputAction.Enable();
        }

        private void OnDisable()
        {
            inputAction.Disable();
        }

        private void OnDestroy()
        {
            handle?.Complete();
            handle = default;
            if (chipKindArray.IsCreated)
            {
                chipKindArray.Dispose();
                chipKindArray = default;
            }
            
            MapSetting.Dispose();
            normal.Dispose();
            leader.Dispose();
            boss.Dispose();
            if (randoms.IsCreated)
            {
                randoms.Dispose();
                randoms = default;
            }
        }

        private void OnApplicationQuit()
        {
            if (handle.HasValue) OnDestroy();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            inputMoveValue = context.ReadValue<Vector2>();
        }

        public void OnLook(InputAction.CallbackContext context)
        {
        }

        public void OnFire(InputAction.CallbackContext context)
        {
        }
    }
}
