using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Unity1Week.UI
{
    sealed class BackToTitleButton : Button
    {
        private AsyncOperation operation;
        public override void OnPointerClick(PointerEventData eventData)
        {
            if (operation == null)
                operation = SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
        }
    }
}