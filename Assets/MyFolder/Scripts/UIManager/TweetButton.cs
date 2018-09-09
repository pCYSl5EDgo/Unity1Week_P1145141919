using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Unity1Week.UI
{
    sealed class TweetButton : Button
    {
        public uint KillScore;
        public override void OnPointerClick(PointerEventData eventData)
        {
#if UNITY_EDITOR
            Debug.Log(KillScore);
#else
            naichilab.UnityRoomTweet.Tweet("p1145141919", $"スコア:{KillScore}", "unity1week", "晩夏の昼の挽歌");
#endif
        }
    }
}