using System;
using ComponentTypes;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
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
        private NativeArray<Random> randoms;
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

            public unsafe EnemyDisplayable(int capacity, Material material, float minInclusive, float maxExclusive)
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
            
            public bool DrawAndScheduleJob(JobHandle? oldJobHandle, EnemyDisplay display, Bounds bounds, float deltaTime, out JobHandle job, string name)
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
                Debug.Log(name+ " -> " + count + ", chunk count : " + chunkCount + "\n" + position2D[0]);
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
            randoms = new NativeArray<Random>(1, Allocator.Persistent) { [0] = new Random(1) };

            normal = new EnemyDisplayable(1024, Display.EnemyNormalMaterial, MapSetting.MinInclusive, MapSetting.MaxExclusive);
            leader = new EnemyDisplayable(1024, Display.EnemyLeaderMaterial, MapSetting.MinInclusive, MapSetting.MaxExclusive);
            boss = new EnemyDisplayable(1024, Display.EnemyBossMaterial, MapSetting.MinInclusive, MapSetting.MaxExclusive);
            normal.Sort.This.Count[0] = 32;
            unsafe
            {
                var positionPointer = (Position2D.Eight*)normal.Sort.This.PositionArray.GetUnsafePtr();
                positionPointer->X.c0 = default;
                positionPointer->Y.c0 = default;
                positionPointer->X.c1 = default;
                positionPointer->Y.c1 = default;
                UnsafeUtility.MemCpyReplicate(positionPointer + 1, positionPointer, sizeof(Position2D.Eight), 3);
                var speedPointer = (Speed2D.Eight*)normal.Sort.This.SpeedArray.GetUnsafePtr();
                for (var i = 0; i < 4; i++, speedPointer++)
                {
                    speedPointer->X.c0 = new float4(10, 0, -10, 0) * (2 + i);
                    speedPointer->Y.c0 = new float4(0, 10, 0, -10) * (2 + i);
                    speedPointer->X.c1 = new float4(6, 6, -6, -6) * (2 + i);
                    speedPointer->Y.c1 = new float4(6, -6, 6, -6) * (2 + i);    
                }
                
                UnsafeUtility.MemClear(normal.Sort.This.IsAliveArray.GetUnsafePtr(), sizeof(AliveState.Eight) * 4);
            }
            
            chipKindArray = new NativeArray<int>(MapSetting.ChipCount, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
            for (var i = 0; i < chipKindArray.Length; i++) chipKindArray[i] = i % 6;
        }

        private void Update()
        {
            handle?.Complete();

            MapSetting.Buffer.SetData(chipKindArray);
            Graphics.DrawMeshInstancedProcedural(mesh, 0, MapSetting.ChipMaterial, MapSetting.Bounds, MapSetting.ChipCount, castShadows: ShadowCastingMode.Off, receiveShadows: false);

            var deltaTime = Time.deltaTime;
            var normalExists = normal.DrawAndScheduleJob(handle, Display, MapSetting.Bounds, deltaTime, out var normalJob, "normal");
            var leaderExists = leader.DrawAndScheduleJob(handle, Display, MapSetting.Bounds, deltaTime, out var leaderJob, "leader");
            var bossExists = boss.DrawAndScheduleJob(handle, Display, MapSetting.Bounds, deltaTime, out var bossJob, "boss");

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
