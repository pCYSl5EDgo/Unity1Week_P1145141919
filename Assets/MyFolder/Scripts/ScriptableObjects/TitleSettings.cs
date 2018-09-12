using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity1Week.ScriptableObjects
{
    public sealed class TitleSettings : ScriptableObject
    {
		public uint LeaderCount;
		public uint PlayerKind;
		public uint[] MaxLifes;
		public float[] ThermalDeathTemperatures;
    }
}