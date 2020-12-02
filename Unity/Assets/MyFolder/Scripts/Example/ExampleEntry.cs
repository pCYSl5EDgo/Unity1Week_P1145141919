using ComponentTypes;
using Unity.Collections;
using Unity1Week.ScriptableObjects;
using UnityEngine;
using UnityEngine.Rendering;

namespace MyFolder.Scripts.Example
{
    public class ExampleEntry : MonoBehaviour
    {
        public Sprite sprite;
        public EnemyDisplay Display;
        public Map MapSetting;
        public Mesh mesh;
        public int ShiftCount;

        private NativeArray<int> chipKindArray;
        private Enemy.Countable normal;
        private Enemy.Countable leader;
        private Enemy.Countable boss;
        private GraphicsBuffer normalBuffer;
        private GraphicsBuffer leaderBuffer;
        private GraphicsBuffer bossBuffer;
        private static readonly int positions = Shader.PropertyToID("positions");

        private void Awake()
        {
            normal = new Enemy.Countable(1024);
            leader = new Enemy.Countable(1024);
            boss = new Enemy.Countable(1024);
            normal.Count[0] = 1;

            normalBuffer = new GraphicsBuffer(GraphicsBuffer.Target.IndirectArguments | GraphicsBuffer.Target.Structured, 1024, 64);
            leaderBuffer = new GraphicsBuffer(GraphicsBuffer.Target.IndirectArguments | GraphicsBuffer.Target.Structured, 1024, 64);
            bossBuffer = new GraphicsBuffer(GraphicsBuffer.Target.IndirectArguments | GraphicsBuffer.Target.Structured, 1024, 64);
            Display.Initialize();
            Display.NormalBlock.SetBuffer(positions, normalBuffer);
            Display.LeaderBlock.SetBuffer(positions, leaderBuffer);
            Display.BossBlock.SetBuffer(positions, bossBuffer);
            MapSetting.Initialize(ShiftCount);
            chipKindArray = new NativeArray<int>(MapSetting.ChipCount, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
            for (var i = 0; i < chipKindArray.Length; i++)
            {
                chipKindArray[i] = i % 6;
            }
        }

        private void LateUpdate()
        {
            MapSetting.Buffer.SetData(chipKindArray);
            Graphics.DrawMeshInstancedProcedural(mesh, 0, MapSetting.ChipMaterial, MapSetting.Bounds, MapSetting.ChipCount, castShadows: ShadowCastingMode.Off, receiveShadows: false);
            
            var count = normal.Count[0];
            if (count != 0)
            {
                normalBuffer.SetData(normal.PositionArray, 0, 0, normal.ChunkCount);
                Graphics.DrawMeshInstancedProcedural(Display.EnemyMesh, 0, Display.EnemyMaterial, MapSetting.Bounds, count, Display.NormalBlock, ShadowCastingMode.Off, false);
            }

            count = leader.Count[0];
            if (count != 0)
            {
                leaderBuffer.SetData(leader.PositionArray, 0, 0, leader.ChunkCount);
                Graphics.DrawMeshInstancedProcedural(Display.EnemyMesh, 0, Display.EnemyMaterial, MapSetting.Bounds, count, Display.LeaderBlock, ShadowCastingMode.Off, false);
            }
            
            count = boss.Count[0];
            if (count != 0)
            {
                bossBuffer.SetData(boss.PositionArray, 0, 0, boss.ChunkCount);
                Graphics.DrawMeshInstancedProcedural(Display.EnemyMesh, 0, Display.EnemyMaterial, MapSetting.Bounds, count, Display.BossBlock, ShadowCastingMode.Off, false);
            }
        }

        private void OnApplicationQuit()
        {
            chipKindArray.Dispose();
            MapSetting.Dispose();
            normal.Dispose();
            leader.Dispose();
            boss.Dispose();
            normalBuffer?.Dispose();
            leaderBuffer?.Dispose();
            bossBuffer?.Dispose();
        }
    }
}
