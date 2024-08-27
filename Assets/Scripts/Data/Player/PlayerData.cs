using System;
using System.Collections.Generic;
using Game.Data;
using UnityEngine;

namespace Data.Player
{
	[Serializable]
	public class PlayerData : ASavedData
	{
		public string playerID = Application.identifier;
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

		public override string key => "player_data";

		public void ResetAdventureData()
		{
		}
	}
}