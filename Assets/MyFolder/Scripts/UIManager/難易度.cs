using TMPro;
using Unity1Week.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Unity1Week.UI
{
    [RequireComponent(typeof(Slider))]
    public sealed class 難易度 : MonoBehaviour
    {
        [SerializeField] private GameObject _難易度;
        [SerializeField] private TitleSettings titleSettings;
        private Slider slider;
        private TMP_Text text;

        private void Start()
        {
            slider = GetComponent<Slider>();
            text = _難易度.GetComponent<TMP_Text>();
            text.text = titleSettings.LeaderCount.ToString();
            slider.value = titleSettings.LeaderCount;
        }

        private void Update()
        {
            var _value = (uint) slider.value;
            if (titleSettings.LeaderCount == _value) return;
            text.text = (titleSettings.LeaderCount = _value).ToString();
        }
    }
}