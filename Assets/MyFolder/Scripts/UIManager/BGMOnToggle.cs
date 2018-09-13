using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Unity1Week.UI
{
    public sealed class BGMOnToggle : Toggle
    {
        [SerializeField] ScriptableObjects.TitleSettings titleSettings;
        protected override void Start() => isOn = titleSettings.IsBgmOn;
        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
            titleSettings.IsBgmOn = isOn;
        }
    }
}