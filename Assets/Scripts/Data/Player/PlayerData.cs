using System;
using System.Collections.Generic;
using Global;
using UnityEngine;

namespace Data.Player
{
	[Serializable]
	public class PlayerData : ASavedData
	{
		public override string key => "player_data";

		public string playerID = Application.identifier;
		public string lastPlayedLevel = "";
		public SessionData SessionData;
		public List<string> PurchasedIds;

		public PlayerData()
		{
			SessionData = new SessionData();
			PurchasedIds = new List<string>();
		}

		public void ResetAdventureData()
		{
		}
	}
}