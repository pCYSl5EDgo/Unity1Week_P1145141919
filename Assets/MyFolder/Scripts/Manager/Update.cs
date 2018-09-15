using UniRx;
using UniRx.Triggers;
using UniRx.Async;
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
        Camera mainCamera;

        private Transform UpperLeftCanvas;

        private TMP_Text 残ライフ;
        private RectTransform 残中島敦;
        private TMP_Text 現在気温;
        private TMP_Text 機体温度;
        private TMP_Text 致死温度;
        private TMP_Text 駆逐数説明, 駆逐数, ノルマ;
        private PlayerSettings cached;

        private readonly Color deathColor = Color.red;
        private readonly Color normalColor = Color.blue;
        private readonly Color clearColor = Color.yellow;
        private readonly Color zeroColor = Color.gray;

        private ReactiveProperty<uint> deathCounter;
        private BoolReactiveProperty nearToRespawn;
        private FloatReactiveProperty life;
        private TMP_Text 武器名;
        private IDisposable cameraMoveObserver;
        private IDisposable playerMoveObserver;
        private void ChangeWeapon1(bool _)
        {
            PlayerShootSystem.WeaponType = 0;
            武器名.text = playerSkills[0].Name;
            武器名.SetLayoutDirty();
        }
        private void ChangeWeapon2(bool _)
        {
            PlayerShootSystem.WeaponType = 1;
            武器名.text = playerSkills[1].Name;
            武器名.SetLayoutDirty();
        }
        private GameObject 注意;
        void InitializeUGUI()
        {
            life = new FloatReactiveProperty();
            UpperLeftCanvas = GameObject.Find(nameof(UpperLeftCanvas)).transform;
            Transform 体力, 気温, 殺害数;
            体力 = UpperLeftCanvas.Find(nameof(体力)).transform;
            残ライフ = 体力.Find(nameof(残ライフ)).GetComponent<TMP_Text>();
            残中島敦 = 体力.Find(nameof(残中島敦)).GetComponent<RectTransform>();
            気温 = UpperLeftCanvas.Find(nameof(気温)).transform;
            現在気温 = 気温.Find(nameof(現在気温)).GetComponent<TMP_Text>();
            機体温度 = 気温.Find(nameof(機体温度)).GetComponent<TMP_Text>();
            致死温度 = 気温.Find(nameof(致死温度)).GetComponent<TMP_Text>();
            殺害数 = UpperLeftCanvas.Find(nameof(殺害数)).transform;
            駆逐数説明 = 殺害数.Find(nameof(駆逐数説明)).GetComponent<TMP_Text>();
            駆逐数 = 殺害数.Find(nameof(駆逐数)).GetComponent<TMP_Text>();
            ノルマ = 殺害数.Find(nameof(ノルマ)).GetComponent<TMP_Text>();
            ノルマ.text = "体/" + titleSettings.ClearKillScore + "体";
            deathCounter.Subscribe(deadCount =>
            {
                var color = Color.Lerp(zeroColor, clearColor, deadCount / (float)titleSettings.ClearKillScore);
                駆逐数.text = deadCount.ToString();
                駆逐数説明.color = color;
                駆逐数.color = color;
                ノルマ.color = color;
            });
            nearToRespawn.Skip(1).Subscribe((bool _) =>
            {
                if (_)
                    注意 = GameObject.Instantiate(respawnDisplay, UpperLeftCanvas);
                else
                    GameObject.Destroy(注意);
            });
            this.UpdateAsObservable().Select(_ => Input.GetKeyDown(KeyCode.Backspace)).ThrottleFirst(System.TimeSpan.FromMilliseconds(200)).Where(_ => _).Subscribe(_ =>
            {
                UICamera.enabled = !UICamera.enabled;
            });
            cameraMoveObserver = this.UpdateAsObservable().Subscribe(_ =>
            {
                var deltaY = Input.mouseScrollDelta.y;
                if (deltaY != 0)
                {
                    var @position = mainCamera.transform.position;
                    @position.y = System.Math.Max(0.5f, @position.y - deltaY);
                    mainCamera.transform.position = @position;
                }
            });
            playerMoveObserver = this.UpdateAsObservable().Where(_ => UICamera.enabled).Subscribe(_ =>
            {
                var settings = manager.GetComponentData<PlayerSettings>(player);
                if (cached.Temperature != settings.Temperature)
                {
                    var temperatureColor = Color.Lerp(normalColor, deathColor, settings.Temperature / settings.ThermalDeathPoint);
                    現在気温.color = temperatureColor;
                    現在気温.text = settings.Temperature.ToString();
                    機体温度.color = temperatureColor;
                    致死温度.color = temperatureColor;
                }
                if (cached.Life != settings.Life)
                {
                    life.Value = settings.Life;
                    float t = (float)settings.Life / (float)settings.MaxLife;
                    残ライフ.color = Color.Lerp(deathColor, normalColor, t);
                    var siz = 残中島敦.sizeDelta;
                    siz.x = 500f * t;
                    残中島敦.sizeDelta = siz;
                }
                cached = settings;
            });
        }
    }
}