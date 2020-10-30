using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Unity1Week.UI
{
    public sealed class AdditiveOpenButton : Button
    {
        [SerializeField] internal int sceneIndex;

        public override void OnPointerClick(PointerEventData eventData)
        {
            var notOpen = true;
            for (int i = 0, length = SceneManager.sceneCount; i < length; i++)
            {
                if (SceneManager.GetSceneAt(i).buildIndex != sceneIndex) continue;
                notOpen = false;
                break;
            }
            if (notOpen)
                SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);
        }
    }
}