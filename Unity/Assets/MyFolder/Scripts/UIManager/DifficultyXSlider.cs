using System.Globalization;
using TMPro;
using Unity1Week.ScriptableObjects;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Unity1Week.UI
{
    public sealed class DifficultyXSlider : Slider
    {
        [SerializeField] private TitleSettings titleSettings;
        [SerializeField] private TMP_Text text;

        protected override void Start()
        {
            base.Start();
            text.text = (value = titleSettings.Width).ToString(CultureInfo.InvariantCulture);
        }

        public override void OnDrag(PointerEventData eventData)
        {
            base.OnDrag(eventData);
            ValueChange(value);
        }

        internal void ValueChange(float x)
        {
            text.text = (titleSettings.Width = (uint) x).ToString();
        }
    }
}