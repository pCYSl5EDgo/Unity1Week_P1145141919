/*using Unity.Mathematics;
using UnityEngine;
using System;

namespace Unity1Week
{
    [AlwaysUpdateSystem]
    public sealed class PlayerShootSystem : ComponentSystem
    {
        public struct TakenokoBullet : IComponentData { }
        private readonly Camera mainCamera;
        private readonly Entity player;
        private readonly Action playSoundEffect;
        private EntityArchetype ドウリルヴェルファー, 空蝉;
        private EntityArchetype アメイジングトレンチハンマー;
        private EntityManager manager;
        public int WeaponType = 0;

        public PlayerShootSystem(Entity player, Camera mainCamera, Action playSoundEffect)
        {
            this.mainCamera = mainCamera;
            this.player = player;
            this.playSoundEffect = playSoundEffect;
        }
        protected override void OnCreateManager(int capacity)
        {
            manager = EntityManager;
            ドウリルヴェルファー = manager.CreateArchetype(ComponentType.Create<TakenokoBullet>(), ComponentType.Create<Position>(), ComponentType.Create<Frozen>(), ComponentType.Create<Heading2D>(), ComponentType.Create<MoveSpeed>(), ComponentType.Create<DestroyEnemyOutOfBoundsSystem.Tag>());
            空蝉 = manager.CreateArchetype(ComponentType.Create<空蝉Tag>(), ComponentType.Create<LifeTime>(), ComponentType.Create<Position>(), ComponentType.Create<Heading2D>(), ComponentType.Create<MoveSpeed>(), ComponentType.Create<DestroyEnemyOutOfBoundsSystem.Tag>());
            アメイジングトレンチハンマー = manager.CreateArchetype(ComponentType.Create<アメイジングトレンチハンマーTag>(), ComponentType.Create<Position2D>());
        }
        protected override void OnUpdate()
        {
            if (!Input.GetMouseButton(0)) return;
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
            float diffX = x - origin.Value.x;
            float diffY = z - origin.Value.z;
            var noramlized = math.normalize(new float2(diffX, diffY));
            switch (WeaponType)
            {
                case 0: // ドウリルヴェルファー
                    playSoundEffect();
                    buf.CreateEntity(ドウリルヴェルファー);
                    buf.SetComponent(new MoveSpeed(1));
                    buf.SetComponent(origin);
                    buf.SetComponent(new Heading2D { Value = noramlized });
                    break;
                case 1: // ラジエーション空蝉
                    buf.CreateEntity(空蝉);
                    buf.SetComponent(new LifeTime { Value = Time.timeSinceLevelLoad });
                    buf.SetComponent(new MoveSpeed(1));
                    buf.SetComponent(origin);
                    buf.SetComponent(new Heading2D { Value = noramlized });
                    break;
                case 2: // アメイジングトレンチハンマー
                    buf.CreateEntity(アメイジングトレンチハンマー);
                    buf.SetComponent(new アメイジングトレンチハンマーTag
                    {
                        CreationTime = Time.timeSinceLevelLoad,
                        HeadingDegree = Math.Atan2(diffX, diffY),
                        X = origin.Value.x + 0.08f * noramlized.x,
                        Y = origin.Value.z + 0.08f * noramlized.y,
                    });
                    break;
            }
        }

        public struct 空蝉Tag : IComponentData { }
        public struct アメイジングトレンチハンマーTag : IComponentData, IEquatable<アメイジングトレンチハンマーTag>
        {
            public float X;
            public float Y;
            public float CreationTime;
            public double HeadingDegree;
            public bool Equals(アメイジングトレンチハンマーTag other) => X == other.X && other.Y == Y && CreationTime == other.CreationTime && HeadingDegree == other.HeadingDegree;
            public override bool Equals(object obj) => obj != null && Equals((アメイジングトレンチハンマーTag)obj);
            public override int GetHashCode() => math.asint(CreationTime) ^ (int)math.aslong(HeadingDegree) ^ math.asint(X) ^ math.asint(Y);
            public static bool operator ==(アメイジングトレンチハンマーTag left, アメイジングトレンチハンマーTag right) => left.X == right.X && left.Y == right.Y && left.CreationTime == right.CreationTime && left.HeadingDegree == right.HeadingDegree;
            public static bool operator !=(アメイジングトレンチハンマーTag left, アメイジングトレンチハンマーTag right) => left.X != right.X || left.Y != right.Y || left.CreationTime != right.CreationTime || left.HeadingDegree != right.HeadingDegree;
        }
    }
}*/

