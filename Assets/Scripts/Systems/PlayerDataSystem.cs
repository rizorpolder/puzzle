using Common;
using Data.Player;
using Systems.SaveSystem;

namespace Systems
{
	public class PlayerDataSystem : ASystem
	{
		public PlayerData CurrentPlayerData { get; private set; }

		public override void Initialize()
		{
			CurrentPlayerData = SaveDataSystem.Instance.LoadData<PlayerData>();
			//подумать над загрузкой / кэшированием данных
		}
	}
}