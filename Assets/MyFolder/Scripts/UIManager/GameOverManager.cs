using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity1Week.UI
{
	public sealed class GameOverManager : MonoBehaviour {
		public ScriptableObjects.TitleSettings titleSettings;
		public ScriptableObjects.Result result;
		public GameObject TEXTOBJECT;
		private bool once = true;
		void Start () 
		{
			var TEXT = TEXTOBJECT.GetComponent<TMPro.TMP_Text>();
			if (result.KillScore < titleSettings.NextStageCount[0])
				TEXT.text = titleSettings.lastMessages[0];
			else if (result.KillScore < titleSettings.NextStageCount[1])
				TEXT.text = titleSettings.lastMessages[1];
			else if (result.KillScore < titleSettings.NextStageCount[2])
				TEXT.text = titleSettings.lastMessages[2];
			else if (result.KillScore < titleSettings.NextStageCount[3])
				TEXT.text = titleSettings.lastMessages[3];
			else if (result.KillScore < titleSettings.NextStageCount[4])
				TEXT.text = titleSettings.lastMessages[4];
			else
				TEXT.text = titleSettings.lastMessages[5];
		}
		void Update(){
			if(once){
				naichilab.RankingLoader.Instance.SendScoreAndShowRanking(result.CalcScore());
				once = false;
			}
		}
	}
}