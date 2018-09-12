using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public sealed class 難易度 : MonoBehaviour
{
    Slider slider;
    [SerializeField] GameObject _難易度;
    [SerializeField] Unity1Week.ScriptableObjects.TitleSettings titleSettings;
    TMPro.TMP_Text text;
    float cached;
    void Start()
    {
        slider = GetComponent<Slider>();
        cached = slider.value;
        text = _難易度.GetComponent<TMPro.TMP_Text>();
        text.text = "難易度:" + titleSettings.LeaderCount.ToString() + "個";
        if (titleSettings.LeaderCount == 200)
            cached = slider.value = 0;
        else
            cached = slider.value = (titleSettings.LeaderCount - 200) / 9800f;
    }
    void Update()
    {
        if (slider.value == cached) return;
        cached = slider.value;
        titleSettings.LeaderCount = (uint)(200 + (cached * 9800));
        text.text = "難易度:" + titleSettings.LeaderCount.ToString() + "個";
    }
}
