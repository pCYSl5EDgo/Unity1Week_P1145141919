/*using Unity.Entities;
using UnityEngine;
using System;

namespace Unity1Week
{
    partial class Manager
    {
        private void InitializeWorld()
        {
            var enemyMesh = RotateSprite(enemyDisplay.enemySprite);
            var world = World.Active = new World("default");
            world.SetDefaultCapacity(1 << 18);
            manager = world.CreateManager<EntityManager>();
            var range = new Unity.Mathematics.uint2(titleSettings.Width, titleSettings.Height);
            var decidePositionHashCodeSystem = world.CreateManager<DecidePositionHashCodeSystem>(range);
            var enemyHashCodes = decidePositionHashCodeSystem.EnemyHashCodes;
            var snowHashCodes = decidePositionHashCodeSystem.SnowBulletCodes;
            var playerBulletHashCodes = decidePositionHashCodeSystem.PlayerBulletCodes;
            var playerBulletPositionHashSet = decidePositionHashCodeSystem.PlayerBulletPositionHashCodeSet;
            var snowBulletPositionHashSet = decidePositionHashCodeSystem.SnowBulletPositionHashCodeSet;
            var chips = InitializePlane(range.x, range.y);
            InitializePlayer(range, 100, InitialTemperature, ThermalDeathPoint);
            world.CreateManager(typeof(PlayerEnemyRenderSystem), mainCamera, playerSprite, playerMaterial, enemyMesh, new Material[] { enemyDisplay.bossMaterial, enemyDisplay.leaderMaterial, enemyDisplay.subordinateMaterial });
            world.CreateManager(typeof(MoveSystem));
            world.CreateManager(typeof(EnemyBulletRenderSystem), mainCamera, snowSkillSetting.Sprites[0], snowSkillSetting.Material);
            world.CreateManager(typeof(MoveEnemySystem), player);
            world.CreateManager(typeof(EnemySnowShootSystem), player, 4);
            world.CreateManager(typeof(ConfinePlayerPositionSystem), player, range, mainCamera.transform);
            world.CreateManager(typeof(KinokoRenderSystem), mainCamera, kinokoHammer, kinokoMaterial, 120 * Math.PI / 180, 1f);
            PlayerShootSystem = world.CreateManager<PlayerShootSystem>(player, mainCamera, new Action(TryToPlayTakenokoShoot));
            var SpawnEnemySystem = InitializeSpawnEnemy(player, enemyMesh, world, range, titleSettings.LeaderCount);
            deathCounter = SpawnEnemySystem.DeathCount;
            nearToRespawn = SpawnEnemySystem.NearToRespawn;
            world.CreateManager(typeof(TakenokoEnemyHitCheckSystem), 0.16f, enemyHashCodes, playerBulletHashCodes, playerBulletPositionHashSet, new Action(TryToPlayTakenokoBurst));
            world.CreateManager(typeof(PlayerMoveSystem), player, mainCamera.transform);
            InitializeAnimationRenderSystem(world, takenokoBulletBurst.length);
            world.CreateManager(typeof(DestroyEnemyOutOfBoundsSystem), range);
            world.CreateManager(typeof(DecideMoveSpeedSystem), range, chips, playerSpeeds.Speeds, enemySpeeds.Speeds);
            world.CreateManager(typeof(UpdateCoolTimeSystem));
            world.CreateManager(typeof(TakenokoRenderSystem), mainCamera, playerSkills[0].Sprites[0], playerSkills[0].Material);
            world.CreateManager(typeof(BombHitCheckSystem), player, 4, enemyHashCodes);
            world.CreateManager(typeof(ChipRenderSystem), mainCamera, range, chips, mapTable.chipTemperatures, mapTable.map, unlit);
            world.CreateManager(typeof(SnowPlayerHitCheckSystem), player, snowSkillSetting.UtilityNumber, deathCounter, 0.5f, snowHashCodes, playerBulletHashCodes, snowBulletPositionHashSet, new Action(TryToPlaySnowBurst));
            (this.RainSystem = world.CreateManager<RainSystem>(range, rainCoolTimeSpan, rainCoolPower, rainCoolFrequency)).Enabled = false;
            (this.EnemyPlayerCollisionSystem = world.CreateManager<EnemyPlayerCollisionSystem>(player, enemyHashCodes, 0.16f, deathCounter)).Enabled = false;
            world.CreateManager(typeof(PlayerTemperatureSystem), player, range, chips, heatDamageRatio, coolRatio);
            world.CreateManager(typeof(UtusemiRenderSystem), mainCamera, playerSkills[1].Sprites[0], playerSkills[1].Material, 15);
            ScriptBehaviourUpdateOrder.UpdatePlayerLoop(world);
        }
    }
}*/

