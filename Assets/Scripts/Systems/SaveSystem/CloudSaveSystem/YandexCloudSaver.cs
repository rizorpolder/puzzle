using Data;
using Systems.SaveSystem.Serializers;

#if UNITY_WEBGL && YANDEX

namespace Systems.SaveSystem.CloudSaveSystem
{
	public class YandexCloudSaver : ISaver
	{
		private IDataSerializer _dataSerializer;

		public void SetSerializer(IDataSerializer dataSerializer)
		{
			throw new System.NotImplementedException();
		}

		public void SaveData<T>(T data, string key) where T : ASavedData
		{
			throw new System.NotImplementedException();
		}

		public T LoadData<T>(string key) where T : ASavedData, new ()
		{
			throw new System.NotImplementedException();
		}

		public void ForceSave()
		{
			throw new System.NotImplementedException();
		}

		public void ClearData(string key)
		{
			throw new System.NotImplementedException();
		}

		public void ClearAllData()
		{
			throw new System.NotImplementedException();
		}
	}
}
#endif