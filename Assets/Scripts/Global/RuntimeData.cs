using Data.Player;
using Game;

namespace Global
{
	public enum GameType
	{
		Classic,
		Adventure
	}

	public class RuntimeData
	{
		public GameType CurrentGameType { get; private set; }
		public PlayerData PlayerData { get; private set; }

		public RuntimeData(GameDataManager dataManager)
		{
			LoadPlayerData(dataManager);
		}

		private void LoadPlayerData(GameDataManager dataManager)
		{
			dataManager.GetData<PlayerData>(PlayerData.key,
				(result, data) =>
				{
					if (result)
					{
						PlayerData = data;
					}
					else
					{
						PlayerData = new PlayerData();
					}

					PlayerData.SessionData.TrackSession();
					dataManager.SaveData(PlayerData.key,PlayerData);
				} );
		}



		public void SetGameType(GameType type)
		{
			CurrentGameType = type;
		}

	}
}