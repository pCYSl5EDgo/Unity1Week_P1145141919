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
        [SerializeField] float CoolTime;
        [SerializeField] Sprite playerSprite;
        [SerializeField] Material playerMaterial;

        void InitializePlayer(uint2 range, int maxLife, float temperature, float thermalDeathPoint)
        {
            player = manager.CreateEntity(ComponentType.Create<Position>(), ComponentType.Create<MeshInstanceRenderer>(), ComponentType.Create<Controlable>(), ComponentType.Create<MoveSpeed>(), ComponentType.Create<PlayerSettings>(), ComponentType.Create<Heading2D>(), ComponentType.Create<SkillElement>());
            var buffer = manager.GetBuffer<SkillElement>(player);
            buffer.Add(new SkillElement(1, CoolTime));
            buffer.Add(new SkillElement(2, CoolTime * 4));
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