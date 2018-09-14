using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Unity1Week.UI
{
    public sealed class SEOnToggle : Toggle
    {
        [SerializeField] ScriptableObjects.TitleSettings titleSettings;
        protected override void Start() => isOn = titleSettings.IsSEOn;
        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
            titleSettings.IsSEOn = isOn
        }
    }
}