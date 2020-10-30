using TMPro;
using UniRx;

namespace Unity1Week
{
    internal partial class Manager
    {
        private readonly IntReactiveProperty stageReactiveProperty = new IntReactiveProperty(0);

        private void InitializeStageWatch()
        {
            deathCounter.Subscribe(count =>
            {
                for (var i = titleSettings.NextStageCount.Length - 1; i >= 0; --i)
                {
                    if (count < titleSettings.NextStageCount[i]) continue;
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
                    /*EnemyPlayerCollisionSystem.Enabled = true;*/
                    break;
                case 2:
                    /*RainSystem.Enabled = true;*/
                    break;
                case 3:
                    Input1.Subscribe(ChangeWeapon1);
                    Input2.Subscribe(ChangeWeapon2);
                    武器欄.SetActive(true);
                    武器名 = 武器欄.transform.Find(nameof(武器名)).GetComponent<TMP_Text>();
                    break;
                case 4:
                    // System.Buffer.BlockCopy(stage4EnemySpeed.Speeds, 0, enemySpeeds.Speeds, 0, enemySpeeds.Speeds.Length);
                    break;
                case 5:
                    // LastBossAppear();
                    break;
                }
            });
        }

        private void InitializeBGM()
        {
            stageReactiveProperty.Subscribe(stage =>
            {
                if (stage >= BgmClips.Length) return;
                BgmSource.Stop();
                BgmSource.clip = BgmClips[stage];
                BgmSource.Play();
            });
        }
    }
}