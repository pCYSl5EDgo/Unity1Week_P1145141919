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
            if (titleSettings.LeaderCount == 200)
                slider.value = 0;
            else
                slider.value = (titleSettings.LeaderCount - 200) / 9800f;
        }
        void Update()
        {
            var value = (uint)(200 + (slider.value * 9800));
            if (titleSettings.LeaderCount == value) return;
            text.text = (titleSettings.LeaderCount = value).ToString();
        }
    }
}