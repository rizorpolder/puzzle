using System.IO;
using Common;
using Data;
using Systems.SaveSystem.CloudSaveSystem;
using Systems.SaveSystem.Serializers;
using UnityEngine;

namespace Systems.SaveSystem
{
	public class SaveDataSystem : ASystem
	{
		public static SaveDataSystem Instance;
		private readonly IDataSerializer _dataSerializer;

		private ACloudManager storageManager;

		public SaveDataSystem(IDataSerializer dataSerializer)
		{
			Instance = this;
			_dataSerializer = dataSerializer;
			GetRemoteDataManager();
		}

		private void GetRemoteDataManager()
		{
#if UNITY_WEBGL && YANDEX
			storageManager = new YandexCloudManager();
#else
			storageManager = new EmptyCloudManager();
#endif

			storageManager.SetSerializer(_dataSerializer);
		}

		//TODO  Все сейвы дублировать в облако
		public void SaveData<T>(T data) where T : ASavedData
		{
			var result = _dataSerializer.FromData(data);
			File.WriteAllText(Application.persistentDataPath + data.key, result);
		}

		public T LoadData<T>() where T : ASavedData, new()
		{
			var data = new T();
			var path = Application.persistentDataPath + data.key;
			if (File.Exists(path))
			{
				var result = File.ReadAllText(path);
				data = _dataSerializer.ToData<T>(result);
			}

			return data;
		}

		public void ClearData(string key, System.Action<bool> callback = null)
		{
			//todo пройти по папке Application.persistentDataPath и удалить все файлы
			storageManager.ClearData(key);
			callback?.Invoke(true);
		}

		public void ForceSave()
		{
			storageManager.ForceSaveToCloud();
		}
	}
}