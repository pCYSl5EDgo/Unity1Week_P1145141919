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
        protected override void Start()
        {
            base.Start();
        }
        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
            if (op != null) return;
            GameObject.Find("進行度").GetComponent<RectTransform>().GetComponentInChildren<TMPro.TMP_Text>().color = Color.white;
            op = SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
        }
    }
}