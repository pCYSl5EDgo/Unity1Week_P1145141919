using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;
using System;

namespace Unity1Week
{
    [AlwaysUpdateSystem]
    sealed class PlayerShootSystem : ComponentSystem
    {
        public struct TakenokoBullet : IComponentData { }
        private readonly Camera mainCamera;
        private readonly Entity player;
        private readonly Action playSoundEffect;
        private EntityArchetype archetype, 空蝉;

        public int WeaponType = 0;

        public PlayerShootSystem(Entity player, Camera mainCamera, Action playSoundEffect)
        {
            this.mainCamera = mainCamera;
            this.player = player;
            this.playSoundEffect = playSoundEffect;
        }
        protected override void OnCreateManager(int capacity)
        {
            archetype = EntityManager.CreateArchetype(ComponentType.Create<TakenokoBullet>(), ComponentType.Create<Position>(), ComponentType.Create<Frozen>(), ComponentType.Create<Heading2D>(), ComponentType.Create<MoveSpeed>());
            空蝉 = EntityManager.CreateArchetype(ComponentType.Create<空蝉Tag>(), ComponentType.Create<LifeTime>(), ComponentType.Create<Position>(), ComponentType.Create<Heading2D>(), ComponentType.Create<MoveSpeed>());
        }
        protected override void OnUpdate()
        {
            if (!Input.GetMouseButton(0)) return;
            var manager = EntityManager;
            var skillBuffer = manager.GetBuffer<SkillElement>(player);
            var skill = skillBuffer[WeaponType];
            if (!skill.IsActivateble) return;
            skill.SinceLastTime = 0;
            skillBuffer[WeaponType] = skill;
            var origin = manager.GetComponentData<Position>(player);
            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            var abc = ray.direction;
            var pqr = ray.origin;
            var x = pqr.x - pqr.y * abc.x / abc.y;
            var z = pqr.z - pqr.y * abc.z / abc.y;
            var buf = PostUpdateCommands;
            switch (WeaponType)
            {
                case 0: // ドウリルヴェルファー
                    playSoundEffect();
                    buf.CreateEntity(archetype);
                    break;
                case 1: // ラジエーション空蝉
                    buf.CreateEntity(空蝉);
                    buf.SetComponent(new LifeTime { Value = Time.timeSinceLevelLoad });
                    break;
            }
            buf.SetComponent(origin);
            buf.SetComponent(new Heading2D { Value = math.normalize(new float2(x - origin.Value.x, z - origin.Value.z)) });
            buf.SetComponent(new MoveSpeed(1));
        }

        public struct 空蝉Tag : IComponentData
        {
        }
    }
}