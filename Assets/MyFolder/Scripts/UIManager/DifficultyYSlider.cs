using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Unity1Week.UI
{
    public sealed class DifficultyYSlider : Slider
    {
        [SerializeField] ScriptableObjects.TitleSettings titleSettings;
        [SerializeField] TMPro.TMP_Text text;

        protected override void Start()
        {
            base.Start();
            text.text = (value = titleSettings.Height).ToString();
        }
        public override void OnDrag(PointerEventData eventData)
        {
            base.OnDrag(eventData);
            ValueChange(value);
        }
        internal void ValueChange(float x) => text.text = (titleSettings.Height = (uint)x).ToString();
    }
}