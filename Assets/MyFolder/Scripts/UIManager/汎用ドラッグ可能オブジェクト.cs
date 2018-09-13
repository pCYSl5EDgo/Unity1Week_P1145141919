using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Unity1Week.UI
{
    public sealed class 汎用ドラッグ可能オブジェクト : MonoBehaviour, IDragHandler
    {
        public void OnDrag(PointerEventData eventData) => _Transform.position += new Vector3(eventData.delta.x, eventData.delta.y, 0f);
        RectTransform _Transform;
        void Start() => _Transform = GetComponent<RectTransform>();
    }
}