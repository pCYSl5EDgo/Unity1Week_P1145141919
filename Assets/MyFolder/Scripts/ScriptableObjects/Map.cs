using UnityEngine;

namespace Unity1Week.ScriptableObjects
{
    public class Map : ScriptableObject
    {
        public Texture2D[] map;
        public float[] chipTemperatures;
    }
}