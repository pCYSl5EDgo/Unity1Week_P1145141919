using System.Text;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Unity1Week.UI
{
    sealed class TweetButton : Button
    {
        public ScriptableObjects.Result result;
        public ScriptableObjects.TitleSettings titleSettings;
        public override void OnPointerClick(PointerEventData eventData)
        {
            var buf = new StringBuilder(512);
            buf.Append("難易度:").Append(titleSettings.LeaderCount).Append(" 駆逐数:").Append(result.KillScore).Append(" スコア:").Append(result.CalcScore()).Append("\nランク:");
            if (result.KillScore < titleSettings.NextStageCount[0])
                buf.Append(titleSettings.tweetMessages[0]);
            else if (result.KillScore < titleSettings.NextStageCount[1])
                buf.Append(titleSettings.tweetMessages[1]);
            else if (result.KillScore < titleSettings.NextStageCount[2])
                buf.Append(titleSettings.tweetMessages[2]);
            else if (result.KillScore < titleSettings.NextStageCount[3])
                buf.Append(titleSettings.tweetMessages[3]);
            else if (result.KillScore < titleSettings.NextStageCount[4])
                buf.Append(titleSettings.tweetMessages[4]);
            else if (result.KillScore < titleSettings.ClearKillScore)
                buf.Append(titleSettings.tweetMessages[5]);
            else buf.Append(titleSettings.tweetMessages[6]);
            naichilab.UnityRoomTweet.Tweet("p1145141919", buf.Append('\n').ToString(), "晩夏の昼の挽歌", "unity1week");
        }
    }
}