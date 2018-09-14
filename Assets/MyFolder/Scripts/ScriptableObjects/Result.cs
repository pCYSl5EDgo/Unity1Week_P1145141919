using UnityEngine;

namespace Unity1Week.ScriptableObjects
{
    public class Result : ScriptableObject
    {
        public uint KillScore;
        public TitleSettings titleSettings;
        public double CalcScore()
        {
            var times = (double)10000 / (double)(titleSettings.Width * titleSettings.Height);
            return KillScore * times * (KillScore >= titleSettings.ClearKillScore ?
               (double)(titleSettings.LeaderCount * titleSettings.LeaderCount / 100)
               : (double)(titleSettings.LeaderCount / 100));
        }
    }
}