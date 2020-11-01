using UnityEngine;

namespace Unity1Week
{
    internal partial class Manager
    {
        private readonly bool canPlaySE = true;
        private (float startTime, float endTime, AudioClip clip)[] sourceInfos;

        private void TryToPlayTakenokoShoot()
        {
            if (!canPlaySE) return;
            var current = Time.timeSinceLevelLoad;
            for (var i = 0; i < sourceInfos.Length; i++)
            {
                ref var p = ref sourceInfos[i];
                if (p.endTime >= current) continue;
                sources[i].Stop();
                if (!ReferenceEquals(p.clip, takenokoBulletShoot))
                    sources[i].clip = takenokoBulletShoot;
                sources[i].Play();
                p = (current, current + takenokoBulletShoot.length, takenokoBulletShoot);
                return;
            }
        }

        private void TryToPlayTakenokoBurst()
        {
            if (!canPlaySE) return;
            var current = Time.timeSinceLevelLoad;
            for (var i = 0; i < sourceInfos.Length; i++)
            {
                ref var p = ref sourceInfos[i];
                if (p.endTime >= current) continue;
                sources[i].Stop();
                if (!ReferenceEquals(p.clip, takenokoBulletBurst))
                    sources[i].clip = takenokoBulletBurst;
                sources[i].Play();
                sourceInfos[i] = (current, current + takenokoBulletBurst.length, takenokoBulletBurst);
            }
        }

        private void TryToPlaySnowBurst()
        {
            if (!canPlaySE) return;
            var current = Time.timeSinceLevelLoad;
            for (var i = 0; i < sourceInfos.Length; i++)
            {
                ref var p = ref sourceInfos[i];
                if (p.endTime >= current) continue;
                sources[i].Stop();
                if (!ReferenceEquals(p.clip, snowBurst))
                    sources[i].clip = snowBurst;
                sources[i].Play();
                sourceInfos[i] = (current, current + snowBurst.length, snowBurst);
            }
        }
    }
}