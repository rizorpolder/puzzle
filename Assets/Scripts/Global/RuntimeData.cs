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
			PlayerData  =SharedContainer.Instance.SaveDataSystem.LoadData<PlayerData>(PlayerData.Key);
		}

		public void SetGameType(GameType type)
		{
			CurrentGameType = type;
		}
	}
}