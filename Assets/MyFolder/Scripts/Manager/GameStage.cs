using UniRx;
using Unity.Entities;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

namespace Unity1Week
{
    partial class Manager
    {
        IObservable<bool> isGameClear;
        IObservable<bool> isGameOver;
        private Transform rootCanvas;

        void InitializeGameOverUI()
        {
            rootCanvas = GameObject.Find("Root Canvas").GetComponent<Transform>();
            isGameClear = deathCounter.Select(count => count >= titleSettings.ClearKillScore);
            isGameClear.Where(_ => _).First().Subscribe(_ =>
            {
                UICamera.enabled = true;
                var readonlyCollection = World.AllWorlds;
                foreach (var item in readonlyCollection)
                    foreach (var manager in item.BehaviourManagers)
                        if (manager is ComponentSystemBase system)
                            system.Enabled = false;
                GameObject.Destroy(UpperLeftCanvas.gameObject);
                playerMoveObserver.Dispose();
                cameraMoveObserver.Dispose();
                resultSettings.KillScore = deathCounter.Value;
                SceneManager.LoadSceneAsync(7, LoadSceneMode.Additive);
            });
            isGameOver = life.Select(lp => lp <= 0);
            isGameOver.Where(_ => _).Skip(1).First().Subscribe(_ =>
            {
                GameObject.Destroy(UpperLeftCanvas.gameObject);
                UICamera.enabled = true;
                var readonlyCollection = World.AllWorlds;
                foreach (var item in readonlyCollection)
                    foreach (var manager in item.BehaviourManagers)
                        if (manager is ComponentSystemBase system)
                            system.Enabled = false;
                playerMoveObserver.Dispose();
                cameraMoveObserver.Dispose();
                resultSettings.KillScore = deathCounter.Value;
                SceneManager.LoadSceneAsync(6, LoadSceneMode.Additive);
            });
        }
    }
}