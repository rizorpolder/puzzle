using Data;
using Data.Player;

namespace Global
{
	public class RuntimeData
	{
		public RuntimeData()
		{
			LoadPlayerData();
		}

		public PlayerData PlayerData { get; private set; }
		public ResourceData ResourceData  { get; private set; }
		public FieldData FieldData  { get; private set; }
		private void LoadPlayerData()
		{
			PlayerData = SharedContainer.Instance.SaveDataSystem.LoadData<PlayerData>(PlayerData.Key);
		}
	}
}