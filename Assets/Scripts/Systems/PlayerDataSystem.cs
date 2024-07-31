using Common;
using Data.Player;
using Systems.SaveSystem;

namespace Systems
{
	public class PlayerDataSystem : ASystem
	{
		private PlayerData _currentPlayerData;
		public PlayerData CurrentPlayerData => _currentPlayerData;

		public override void Initialize()
		{
			_currentPlayerData = SaveDataSystem.Instance.LoadData<PlayerData>();
			//подумать над загрузкой / кэшированием данных
		}
	}
}