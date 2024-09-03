using Configs.TextureRepository;
using Data;
using Data.Player;

namespace Global
{
	public class RuntimeData
	{
		public RuntimeData()
		{
			LoadData();

		}

		public PlayerData PlayerData { get; private set; }
		public FieldData FieldData  { get; private set; }
		private void LoadData()
		{
			PlayerData = SharedContainer.Instance.SaveDataSystem.LoadData<PlayerData>(PlayerData.Key);
			FieldData = SharedContainer.Instance.SaveDataSystem.LoadData<FieldData>(FieldData.Key);
		}

		public void StartCoreGame( TextureUnitConfig textureUnitConfig, Difficult difficult)
		{
			FieldData = new FieldData(textureUnitConfig, difficult);
		}

	}
}