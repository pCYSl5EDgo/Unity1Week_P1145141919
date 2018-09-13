using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Unity1Week.UI
{
    public sealed class AdditiveOpenButton : Button
    {
        [SerializeField] internal int sceneIndex;
        public override void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
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