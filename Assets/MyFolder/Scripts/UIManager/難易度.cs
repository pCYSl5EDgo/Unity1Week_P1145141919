using UnityEngine;
using UnityEngine.UI;

namespace Unity1Week.UI
{
    [RequireComponent(typeof(Slider))]
    public sealed class 難易度 : MonoBehaviour
    {
        Slider slider;
        [SerializeField] GameObject _難易度;
        [SerializeField] Unity1Week.ScriptableObjects.TitleSettings titleSettings;
        TMPro.TMP_Text text;
        void Start()
        {
            slider = GetComponent<Slider>();
            text = _難易度.GetComponent<TMPro.TMP_Text>();
            text.text = titleSettings.LeaderCount.ToString();
            slider.value = titleSettings.LeaderCount;
        }
        void Update()
        {
            var _value = (uint)slider.value;
            if (titleSettings.LeaderCount == _value) return;
            text.text = (titleSettings.LeaderCount = _value).ToString();
        }
    }
}