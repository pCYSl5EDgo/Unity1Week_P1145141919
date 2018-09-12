using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

namespace Unity1Week.SelectPlayer
{
    public sealed class ChoosePlayerButton : Button
    {
        [SerializeField] [Multiline] string displayText;
        public uint Kind;
		[SerializeField] Unity1Week.ScriptableObjects.TitleSettings titleSettings;
        private TMP_Text 説明文;
        void Start()
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