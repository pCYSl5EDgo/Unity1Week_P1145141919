using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity1Week
{
    partial class Manager
    {
        [SerializeField] AudioClip takenokoBulletShoot;
        [SerializeField] AudioClip takenokoBulletBurst;

        [SerializeField] AudioSource[] sources;
        (float startTime, float endTime, AudioClip clip)[] sourceInfos;

        void TryToPlayTakenokoShoot()
        {
            var current = Time.timeSinceLevelLoad;
            for (int i = 0; i < sourceInfos.Length; i++)
            {
                var p = sourceInfos[i];
                if (p.endTime >= current) continue;
                sources[i].Stop();
                if (!object.ReferenceEquals(p.clip, takenokoBulletShoot))
                    sources[i].clip = takenokoBulletShoot;
                sources[i].Play();
                sourceInfos[i] = (current, current + takenokoBulletShoot.length, takenokoBulletShoot);
                return;
            }
        }
        void TryToPlayTakenokoBurst()
        {
            var current = Time.timeSinceLevelLoad;
            for (int i = 0; i < sourceInfos.Length; i++)
            {
                var p = sourceInfos[i];
                if (p.endTime >= current) continue;
                sources[i].Stop();
                if (!object.ReferenceEquals(p.clip, takenokoBulletBurst))
                    sources[i].clip = takenokoBulletBurst;
                sources[i].Play();
                sourceInfos[i] = (current, current + takenokoBulletBurst.length, takenokoBulletBurst);
            }
        }
    }
}