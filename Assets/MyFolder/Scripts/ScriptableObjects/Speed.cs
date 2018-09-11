using UnityEngine;

namespace Unity1Week.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Speed", fileName = "SpeedTable")]
    public class Speed : ScriptableObject
    {
        public float[] Speeds;
    }
}