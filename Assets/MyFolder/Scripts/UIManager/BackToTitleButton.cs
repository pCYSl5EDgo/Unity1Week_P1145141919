using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Unity1Week.UI
{
    internal sealed class BackToTitleButton : Button
    {
        private AsyncOperation operation;

        public override void OnPointerClick(PointerEventData eventData)
        {
            if (operation == null)
                operation = SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
        }
    }
}