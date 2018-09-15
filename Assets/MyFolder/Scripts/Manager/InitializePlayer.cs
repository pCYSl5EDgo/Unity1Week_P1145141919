using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Rendering;

using UnityEngine;
using UnityEngine.Rendering;

namespace Unity1Week
{
    partial class Manager
    {
        Entity player;

        void InitializePlayer(uint2 range, int maxLife, float temperature, float thermalDeathPoint)
        {
            player = manager.CreateEntity(ComponentType.Create<Position>(), ComponentType.Create<MeshInstanceRenderer>(), ComponentType.Create<Controlable>(), ComponentType.Create<MoveSpeed>(), ComponentType.Create<PlayerSettings>(), ComponentType.Create<Heading2D>(), ComponentType.Create<SkillElement>());
            var buffer = manager.GetBuffer<SkillElement>(player);
            for (int i = 0; i < this.playerSkills.Length; ++i)
                buffer.Add(new SkillElement((uint)(i + 1), playerSkills[i].CoolTime));
            manager.SetSharedComponentData(player, new MeshInstanceRenderer
            {
                mesh = RotateSprite(playerSprite),
                material = playerMaterial,
                castShadows = ShadowCastingMode.Off,
                receiveShadows = false,
                subMesh = 0,
            });
            manager.SetComponentData(player, new Position
            {
                Value = new float3(range.x * 0.5f, 0, range.y * 0.5f)
            });
            manager.SetComponentData(player, new PlayerSettings
            {
                Life = maxLife,
                MaxLife = maxLife,
                Temperature = temperature,
                ThermalDeathPoint = thermalDeathPoint,
            });
        }
    }
}