using Unity1Week.ScriptableObjects;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Unity1Week.UI
{
    public sealed class SEOnToggle : Toggle
    {
        [SerializeField] private TitleSettings titleSettings;

        protected override void Start()
        {
            isOn = titleSettings.IsSEOn;
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
            titleSettings.IsSEOn = isOn;
        }
    }
}