using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Unity1Week.UI
{
    internal sealed class GoIsGOD : Button
    {
        private AsyncOperation op;

        protected override void Start()
        {
            base.Start();
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
            if (op != null) return;
            GameObject.Find("進行度").GetComponent<RectTransform>().GetComponentInChildren<TMP_Text>().color = Color.white;
            op = SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
        }
    }
}