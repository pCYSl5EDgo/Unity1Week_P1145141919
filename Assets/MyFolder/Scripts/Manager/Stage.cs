using UnityEngine;
using UniRx;

namespace Unity1Week
{
    partial class Manager
    {
        IntReactiveProperty stageReactiveProperty = new IntReactiveProperty(0);

        private void InitializeStageWatch()
        {
            deathCounter.Subscribe(count =>
            {
                for (int i = nextStageCount.Length - 1; i >= 0; --i)
                {
                    if (count < nextStageCount[i]) continue;
                    stageReactiveProperty.Value = i + 1;
                    return;
                }
            });
            stageReactiveProperty.Subscribe(stage =>
            {
                switch (stage)
                {
                    case 0:
                        break;
                    case 1:
#if !UNITY_EDITOR
                        EnemyPlayerCollisionSystem.Enabled = true;
#endif
                        break;
                    case 2:
                        RainSystem.Enabled = true;
                        break;
                    case 3:
                        Input1.Subscribe(ChangeWeapon1);
                        Input2.Subscribe(ChangeWeapon2);
                        武器欄.SetActive(true);
                        武器名 = 武器欄.transform.Find(nameof(武器名)).GetComponent<TMPro.TMP_Text>();
                        break;
                    case 4:
                        System.Buffer.BlockCopy(stage4EnemySpeed.Speeds, 0, enemySpeeds.Speeds, 0, enemySpeeds.Speeds.Length);
                        break;
                    case 5:
                        break;
                    default: break;
                }
            });
        }
        private void InitializeBGM()
        {
            stageReactiveProperty.Subscribe((stage) =>
            {
                if (stage >= BgmClips.Length) return;
                BgmSource.Stop();
                BgmSource.clip = BgmClips[stage];
                BgmSource.Play();
            });
        }
    }
}