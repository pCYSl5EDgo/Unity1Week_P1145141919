using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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