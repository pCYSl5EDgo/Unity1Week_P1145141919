using UnityEngine;
using UniRx;

namespace Unity1Week
{
    partial class Manager
    {
        [SerializeField] uint[] nextStageCount;
        [SerializeField] GameObject 武器欄;
        IntReactiveProperty stageSubject = new IntReactiveProperty(0);

        private void InitializeStageWatch()
        {
            deathCounter.Subscribe(count =>
            {
                for (int i = nextStageCount.Length - 1; i >= 0; --i)
                {
                    if (count < nextStageCount[i]) continue;
                    stageSubject.Value = i + 1;
                    return;
                }
            });
            stageSubject.Subscribe(stage =>
            {
                switch (stage)
                {
                    case 0:
                        break;
                    case 1:
                        EnemyPlayerCollisionSystem.Enabled = true;
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
                        enemySpeeds[0] = 20;
                        enemySpeeds[1] = 10;
                        enemySpeeds[2] = 5;
                        enemySpeeds[3] = 4;
                        enemySpeeds[4] = 7;
                        break;
                    case 5:
                        break;
                    default: break;
                }
            });
        }
        private void InitializeBGM()
        {
            stageSubject.Subscribe((stage) =>
            {
                if (stage >= BgmClips.Length) return;
                BgmSource.Stop();
                BgmSource.clip = BgmClips[stage];
                BgmSource.Play();
            });
        }
    }
}