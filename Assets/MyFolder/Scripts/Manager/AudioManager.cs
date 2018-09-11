using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity1Week
{
    partial class Manager
    {
        private bool canPlaySE = true;
        (float startTime, float endTime, AudioClip clip)[] sourceInfos;

        void TryToPlayTakenokoShoot()
        {
            if (!canPlaySE) return;
            var current = Time.timeSinceLevelLoad;
            for (int i = 0; i < sourceInfos.Length; i++)
            {
                ref var p = ref sourceInfos[i];
                if (p.endTime >= current) continue;
                sources[i].Stop();
                if (!object.ReferenceEquals(p.clip, takenokoBulletShoot))
                    sources[i].clip = takenokoBulletShoot;
                sources[i].Play();
                p = (current, current + takenokoBulletShoot.length, takenokoBulletShoot);
                return;
            }
        }
        void TryToPlayTakenokoBurst()
        {
            if (!canPlaySE) return;
            var current = Time.timeSinceLevelLoad;
            for (int i = 0; i < sourceInfos.Length; i++)
            {
                ref var p = ref sourceInfos[i];
                if (p.endTime >= current) continue;
                sources[i].Stop();
                if (!object.ReferenceEquals(p.clip, takenokoBulletBurst))
                    sources[i].clip = takenokoBulletBurst;
                sources[i].Play();
                sourceInfos[i] = (current, current + takenokoBulletBurst.length, takenokoBulletBurst);
            }
        }
        void TryToPlaySnowBurst()
        {
            if (!canPlaySE) return;
            var current = Time.timeSinceLevelLoad;
            for (int i = 0; i < sourceInfos.Length; i++)
            {
                ref var p = ref sourceInfos[i];
                if (p.endTime >= current) continue;
                sources[i].Stop();
                if (!object.ReferenceEquals(p.clip, snowBurst))
                    sources[i].clip = snowBurst;
                sources[i].Play();
                sourceInfos[i] = (current, current + snowBurst.length, snowBurst);
            }
        }
    }
}