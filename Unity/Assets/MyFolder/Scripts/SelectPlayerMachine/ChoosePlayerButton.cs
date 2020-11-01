using TMPro;
using Unity1Week.ScriptableObjects;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Unity1Week.SelectPlayer
{
    public sealed class ChoosePlayerButton : Button
    {
        [SerializeField] [Multiline] private string displayText;
        public uint Kind;
        [SerializeField] private TitleSettings titleSettings;
        private TMP_Text 説明文;

        protected override void Start()
        {
            説明文 = GameObject.Find(nameof(説明文)).GetComponent<TMP_Text>();
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
            説明文.text = displayText;
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
            titleSettings.PlayerKind = Kind;
        }
    }
}