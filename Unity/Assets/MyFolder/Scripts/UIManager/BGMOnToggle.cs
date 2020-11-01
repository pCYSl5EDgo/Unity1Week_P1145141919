using Unity1Week.ScriptableObjects;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Unity1Week.UI
{
    public sealed class BGMOnToggle : Toggle
    {
        [SerializeField] private TitleSettings titleSettings;

        protected override void Start()
        {
            isOn = titleSettings.IsBgmOn;
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
            titleSettings.IsBgmOn = isOn;
        }
    }
}