using System;
using Data.Player;
using Game;
using Systems.SaveSystem;

namespace Global
{
	public enum GameType
	{
		Classic,
		Adventure
	}

	public class RuntimeData
	{
		public RuntimeData()
		{
			LoadPlayerData();
		}

		public GameType CurrentGameType { get; private set; }
		public PlayerData PlayerData { get; private set; }

		private void LoadPlayerData()
		{
			throw new NotImplementedException();
			// var dataManager = SaveDataSystem.Instance;
			// dataManager.LoadData<PlayerData>(PlayerData.key,
			// 	(result, data) =>
			// 	{
			// 		if (result)
			// 			PlayerData = data;
			// 		else
			// 			PlayerData = new PlayerData();
			//
			// 		PlayerData.SessionData.TrackSession();
			// 		dataManager.SaveData(PlayerData.key, PlayerData);
			// 	});
		}

		public void SetGameType(GameType type)
		{
			CurrentGameType = type;
		}
	}
}