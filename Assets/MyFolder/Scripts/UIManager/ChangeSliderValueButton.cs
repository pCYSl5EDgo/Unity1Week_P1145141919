using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Unity1Week.UI
{
    public sealed class ChangeSliderValueButton : Button
    {
        [SerializeField] Slider slider;
        [SerializeField] int changeValue;
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