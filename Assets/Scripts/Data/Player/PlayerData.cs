using System;

namespace Data.Player
{
	[Serializable]
	public class PlayerData : ASavedData
	{
		public string playerID = "228";
		public string lastPlayedLevel = "";
		public SessionData SessionData;

		public int CurrentLevel;

		public PlayerData()
		{
			SessionData = new SessionData();
		}

		public new static string Key => "player_data";
	}
}