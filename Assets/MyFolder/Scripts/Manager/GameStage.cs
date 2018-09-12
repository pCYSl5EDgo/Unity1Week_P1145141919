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
                naichilab.RankingLoader.Instance.SendScoreAndShowRanking(deathCounter.Value * (titleSettings.LeaderCount * titleSettings.LeaderCount / 10000));
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
                var 文章 = vertPanel.Find("文章").GetComponent<TMPro.TMP_Text>();
                if (killScore < 100)
                    文章.text = $"お前ここは初めてか？\n力抜けよ";
                else if (killScore < 1000)
                    文章.text = $"あ　ほ　く　さ";
                else if (killScore < 10000)
                    文章.text = $"まぁ多少はね？\nじゃ、またやって、どうぞ";
                else if (killScore < 50000)
                    文章.text = $"やりますねぇ！\nま、ミスするのも多少はね？";
                else if (killScore < 100000)
                    文章.text = $"おっ、大丈夫か大丈夫か？\n緊張すっと力でないからね";
                else if (killScore < 114514)
                    文章.text = $"ファッ！？\nはぇ～～、すっごい";
                var tweetButton = vertPanel.Find("Tweet").GetComponent<UI.TweetButton>();
                tweetButton.KillScore = killScore;
                tweetButton.titleSettings = titleSettings;
                naichilab.RankingLoader.Instance.SendScoreAndShowRanking(deathCounter.Value * (titleSettings.LeaderCount / 100));
            });
        }
    }
}