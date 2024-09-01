using System;

namespace Data.Player
{
	[Serializable]
	public class PlayerData : ASavedData
	{
		public string playerID = "228";
		public string lastPlayedLevel = "";
		public SessionData SessionData;

		public LevelsData LevelsData;
			// данные об всех уровнях (список купленных уровней с отметкой на каком уровне сложности пройден ( или сколько звезд получено)
		public PlayerData()
		{
			SessionData = new SessionData();
			LevelsData = new LevelsData();
		}

		public new static string Key => "player_data";
	}
}