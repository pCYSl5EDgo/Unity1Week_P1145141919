using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UniRx.Async;

namespace Unity1Week.UI
{
    sealed class BackToTitle : Button
    {
        public override void OnPointerClick(PointerEventData eventData)
        {
            SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
        }
    }
}