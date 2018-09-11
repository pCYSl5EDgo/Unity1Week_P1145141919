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
                naichilab.UnityRoomTweet.Tweet("p1145141919", $"難易度:{Manager.LeaderCount}\nランク：自滅\n駆逐数:{KillScore}\nおふざけして得る最低評価は楽しいか？\nスコア:{KillScore}", "晩夏の昼の挽歌", "unity1week");
            else if (KillScore < 1000)
                naichilab.UnityRoomTweet.Tweet("p1145141919", $"難易度:{Manager.LeaderCount}\nランク：クソザコナメクジ\n駆逐数:{KillScore}\nあっ……（察し）\nスコア:{KillScore}", "晩夏の昼の挽歌", "unity1week");
            else if (KillScore < 10000)
                naichilab.UnityRoomTweet.Tweet("p1145141919", $"難易度:{Manager.LeaderCount}\nランク：クッキー☆\n駆逐数:{KillScore}\nスイーツ（笑）\nスコア:{KillScore}", "晩夏の昼の挽歌", "unity1week");
            else if (KillScore < 50000)
                naichilab.UnityRoomTweet.Tweet("p1145141919", $"難易度:{Manager.LeaderCount}\nランク：姉貴兄貴姉貴\n駆逐数:{KillScore}\nやりますねぇ！（感心）\nスコア:{KillScore}", "晩夏の昼の挽歌", "unity1week");
            else if (KillScore < 100000)
                naichilab.UnityRoomTweet.Tweet("p1145141919", $"難易度:{Manager.LeaderCount}\nランク：ナアル地獄の脱獄皇\n駆逐数:{KillScore}\nｱｰｲｷｿ\nスコア:{KillScore}", "晩夏の昼の挽歌", "unity1week");
            else if (KillScore < 114514)
                naichilab.UnityRoomTweet.Tweet("p1145141919", $"難易度:{Manager.LeaderCount}\nランク：ニアミス先輩\n駆逐数:{KillScore}\n止まるんじゃねぇぞ……\nスコア:{KillScore}", "晩夏の昼の挽歌", "unity1week");
            else naichilab.UnityRoomTweet.Tweet("p1145141919", $"難易度:{Manager.LeaderCount}\nランク：異能生存体\n駆逐数:{KillScore}\nThis way...\nスコア:{KillScore}", "晩夏の昼の挽歌", "unity1week");
        }
    }
}