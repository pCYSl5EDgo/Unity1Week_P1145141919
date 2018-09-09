using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Unity1Week.UI
{
    sealed class GoIsGOD : Button
    {
        AsyncOperation op;
        RectTransform img;
        protected override void Start()
        {
            base.Start();
            img = GameObject.Find("進行度").GetComponent<RectTransform>();
        }
        public override void OnPointerClick(PointerEventData eventData)
        {
			img.GetComponentInChildren<TMPro.TMP_Text>().color = Color.white;
            op = SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
        }
        void Update()
        {
            if (object.ReferenceEquals(op, null)) return;
            var siz = img.sizeDelta;
            siz.x = op.progress * 100;
            img.sizeDelta = siz;
        }
    }
}