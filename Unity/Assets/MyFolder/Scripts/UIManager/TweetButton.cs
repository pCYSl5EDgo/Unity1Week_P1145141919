using Unity1Week.ScriptableObjects;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Unity1Week.UI
{
    internal sealed class TweetButton : Button
    {
        public Result result;
        public TitleSettings titleSettings;

        public override void OnPointerClick(PointerEventData eventData)
        {
        }
    }
}