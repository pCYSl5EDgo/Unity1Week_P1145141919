using UnityEngine;
using UnityEngine.EventSystems;

namespace Unity1Week.UI
{
    public sealed class 汎用ドラッグ可能オブジェクト
        : MonoBehaviour,
            IDragHandler
    {
        private RectTransform _Transform;

        private void Start()
        {
            _Transform = GetComponent<RectTransform>();
        }

        public void OnDrag(PointerEventData eventData)
        {
            _Transform.position += new Vector3(eventData.delta.x, eventData.delta.y, 0f);
        }
    }
}