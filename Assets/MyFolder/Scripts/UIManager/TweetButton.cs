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
            if (KillScore < 100)
                naichilab.UnityRoomTweet.Tweet("p1145141919", $"#unity1week ランク：自滅\n「晩夏の昼の挽歌」をプレイ\nスコア:{KillScore}\nhttps://unityroom.com/games/p1145141919");
            else if (KillScore < 1000)
                naichilab.UnityRoomTweet.Tweet("p1145141919", $"#unity1week ランク：クソザコナメクジ\n「晩夏の昼の挽歌」をプレイ\nスコア:{KillScore}\nhttps://unityroom.com/games/p1145141919");
            else if (KillScore < 10000)
                naichilab.UnityRoomTweet.Tweet("p1145141919", $"#unity1week ランク：クッキー☆\n「晩夏の昼の挽歌」をプレイ\nスコア:{KillScore}\nhttps://unityroom.com/games/p1145141919");
            else if (KillScore < 50000)
                naichilab.UnityRoomTweet.Tweet("p1145141919", $"#unity1week ランク：姉貴兄貴姉貴\n「晩夏の昼の挽歌」をプレイ\nスコア:{KillScore}\nhttps://unityroom.com/games/p1145141919");
            else if (KillScore < 100000)
                naichilab.UnityRoomTweet.Tweet("p1145141919", $"#unity1week ランク：ナアル地獄の脱獄皇\n「晩夏の昼の挽歌」をプレイ\nスコア:{KillScore}\nhttps://unityroom.com/games/p1145141919");
            else if (KillScore < 114514)
                naichilab.UnityRoomTweet.Tweet("p1145141919", $"#unity1week ランク：ニアミス先輩\n「晩夏の昼の挽歌」をプレイ\nスコア:{KillScore}\nhttps://unityroom.com/games/p1145141919");
            else naichilab.UnityRoomTweet.Tweet("p1145141919", $"#unity1week ランク：異能生存体\n「晩夏の昼の挽歌」をプレイ\nスコア:{KillScore}\nhttps://unityroom.com/games/p1145141919");
        }
    }
}