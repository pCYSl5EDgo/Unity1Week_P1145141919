using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Unity1Week.UI
{
    public sealed class 閉じるボタン : Button
    {
        [SerializeField] internal int sceneIndex;

        public override void OnPointerClick(PointerEventData eventData)
        {
            SceneManager.UnloadSceneAsync(sceneIndex);
        }
    }
}