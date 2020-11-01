using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Unity1Week.UI
{
    public sealed class ChangeSliderValueButton : Button
    {
        [SerializeField] private Slider slider;
        [SerializeField] private int changeValue;

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
            slider.value += changeValue;
            switch (slider)
            {
            case DifficultyXSlider xSlider:
                xSlider.ValueChange(slider.value);
                break;
            case DifficultyYSlider ySlider:
                ySlider.ValueChange(slider.value);
                break;
            }
        }
    }
}