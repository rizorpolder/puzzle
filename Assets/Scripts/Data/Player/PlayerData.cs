using System;
using UnityEngine;

namespace Data.Player
{
	[Serializable]
	public class PlayerData : ASavedData
	{
		public override string PATH => "player_data";

		public string playerID = Application.identifier;
		public string lastPlayedLevel = "";
	}
}