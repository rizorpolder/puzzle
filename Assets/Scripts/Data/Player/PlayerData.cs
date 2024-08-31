using System;
using System.Collections.Generic;
using Game.Data;
using UnityEngine;

namespace Data.Player
{
	[Serializable]
	public class PlayerData : ASavedData
	{
		public string playerID = "228";
		public string lastPlayedLevel = "";
		public SessionData SessionData;
		public List<string> PurchasedIds;

		public int CurrentLevel;
		public GameData GameData;

		public PlayerData()
		{
			SessionData = new SessionData();
			PurchasedIds = new List<string>();
		}

		public new static string Key => "player_data";

		public void ResetAdventureData()
		{
		}
	}
}