using UnityEngine;

namespace Unity1Week.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Map", fileName = "MapTable")]
    public class Map : ScriptableObject
    {
        public Texture2D[] map;
        public float[] chipTemperatures;
    }
}