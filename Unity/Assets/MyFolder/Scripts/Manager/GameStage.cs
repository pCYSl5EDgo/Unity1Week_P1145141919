using System;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Unity1Week
{
    internal partial class Manager
    {
        private IObservable<bool> isGameClear;
        private IObservable<bool> isGameOver;
        private Transform rootCanvas;

        private void InitializeGameOverUI()
        {
            rootCanvas = GameObject.Find("Root Canvas").GetComponent<Transform>();
            isGameClear = deathCounter.Select(count => count >= titleSettings.ClearKillScore);
            isGameClear.First().Subscribe(_ =>
            {
                UICamera.enabled = true;
                Destroy(UpperLeftCanvas.gameObject);
                playerMoveObserver.Dispose();
                cameraMoveObserver.Dispose();
                resultSettings.KillScore = deathCounter.Value;
                SceneManager.LoadSceneAsync(7, LoadSceneMode.Additive);
            });
            isGameOver = life.Select(lp => lp <= 0);
            isGameOver.Skip(1).First().Subscribe(_ =>
            {
                Destroy(UpperLeftCanvas.gameObject);
                UICamera.enabled = true;
                playerMoveObserver.Dispose();
                cameraMoveObserver.Dispose();
                resultSettings.KillScore = deathCounter.Value;
                SceneManager.LoadSceneAsync(6, LoadSceneMode.Additive);
            });
        }
    }
}