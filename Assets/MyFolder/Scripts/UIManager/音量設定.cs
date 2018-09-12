using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Unity1Week.UI
{
    public sealed class 音量設定 : Button
    {
        public override void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
        {
            SceneManager.LoadSceneAsync(4, LoadSceneMode.Additive);
        }
    }
}