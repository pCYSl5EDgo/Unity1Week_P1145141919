using UniRx;
using UniRx.Triggers;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace Unity1Week
{
    partial class Manager
    {
        IObservable<bool> isGameClear;
        IObservable<bool> isGameOver;
        [SerializeField] GameObject gameOverPrefab;
        [SerializeField] GameObject gameClearPrefab;
        private Transform rootCanvas;

        void InitializeGameOverUI()
        {
            rootCanvas = GameObject.Find("Root Canvas").GetComponent<Transform>();
            isGameClear = deathCounter.Select(count => count >= 114514u);
            isGameClear.Where(_ => _).First().Subscribe(_ =>
            {
                UICamera.enabled = true;
                World.DisposeAllWorlds();
                GameObject.Destroy(UpperLeftCanvas.gameObject);
                playerMoveObserver.Dispose();
                cameraMoveObserver.Dispose();
                var clear = GameObject.Instantiate<GameObject>(gameClearPrefab, rootCanvas);
            });
            isGameOver = life.Select(lp => lp <= 0);
            isGameOver.Where(_ => _).Skip(1).First().Subscribe(_ =>
            {
                UICamera.enabled = true;
                World.DisposeAllWorlds();
                GameObject.Destroy(UpperLeftCanvas.gameObject);
                playerMoveObserver.Dispose();
                cameraMoveObserver.Dispose();
                var over = GameObject.Instantiate<GameObject>(gameOverPrefab, rootCanvas);
                var killScore = deathCounter.Value;
                var vertPanel = over.transform.Find("Vertical Panel");
                vertPanel.Find("Horizontal Panel").Find("結果").GetComponent<TMPro.TMP_Text>().text = killScore.ToString();
                vertPanel.Find("Tweet").GetComponent<UI.TweetButton>().KillScore = killScore;
            });
        }
    }
}