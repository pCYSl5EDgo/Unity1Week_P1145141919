using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity1Week.ScriptableObjects
{
    public sealed class TitleSettings : ScriptableObject
    {
        public uint ClearKillScore;
        public uint[] NextStageCount;
        [Multiline]
        public string[] lastMessages;
        [Multiline]
        public string[] tweetMessages;
        [Range(25, 1024)]
        public uint Width;
        [Range(25, 1024)]
        public uint Height;
        public uint LeaderCount;
        public uint PlayerKind;
        public uint[] MaxLifes;
        public float[] ThermalDeathTemperatures;
        public bool IsBgmOn;
        public bool IsSEOn;
    }
}