using Data;
using Systems.SaveSystem.Serializers;

namespace Systems.SaveSystem
{
	public interface ISaver
	{
		public void SetSerializer(IDataSerializer dataSerializer);
		public void SaveData<T>(T data, string key) where T : ASavedData;
		public T LoadData<T>(string key) where T : ASavedData, new();
		public void ForceSave();
		public void ClearData(string key);
		public void ClearAllData();

	}
}