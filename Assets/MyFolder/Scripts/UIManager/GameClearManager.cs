using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity1Week.UI
{
	public sealed class GameClearManager : MonoBehaviour {
		public ScriptableObjects.TitleSettings titleSettings;
		public ScriptableObjects.Result result;
		private bool once = true;
		void Update(){
			if(once){
				naichilab.RankingLoader.Instance.SendScoreAndShowRanking(result.CalcScore());
				once = false;
			}
		}
	}
}