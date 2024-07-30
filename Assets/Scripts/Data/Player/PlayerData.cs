using System;

namespace Data.Player
{
	[Serializable]
	public class PlayerData : ASavedData
	{
		public override string PATH => "player_data";

		public int playerID;
		public string lastPlayedLevel;
	}
}