using System;
using Systems.SaveSystem.CloudSaveSystem;
using Systems.SaveSystem.Serializers;
using UnityEngine;

namespace Game
{
	public class GameDataManager : MonoBehaviour
	{
		[SerializeField] private ACloudManager yandexRemoteDataManager;
		private IDataSerializer _dataSerializer;

		private ACloudManager storageManager;

		public void Initialize(Action onComplete)
		{
			_dataSerializer = new JsonDataSerializer();

			storageManager = GetRemoteDataManager();
			storageManager.LoadFromCloud(data => { onComplete?.Invoke(); });
		}

		public void ClearData(string key, Action<bool> callback = null)
		{
			storageManager.ClearData(key);
			callback?.Invoke(true);
		}

		public void GetData<TData>(string key, Action<bool, TData> callback) where TData : class
		{
			//string dataStr = storageManager.GetData<TData>(key, default);
			//callback?.Invoke(!string.IsNullOrEmpty(dataStr), JsonUtility.FromJson<TData>(dataStr));
		}

		public bool HasData(string key)
		{
			return storageManager.HasData(key);
		}

		public void SaveActiveLevelData<Data>(string key, Data data, Action<bool> callback = null) where Data : class
		{
			// if (!GameManager.Instance.LevelIsActive)
			// 	return;

			storageManager.Save(key, data);
			callback?.Invoke(true);
		}

		public void SaveData<Data>(string key, Data data, Action<bool> callback = null) where Data : class
		{
			var dataStr = JsonUtility.ToJson(data);
			storageManager.Save(key, dataStr);
		}

		public void Clear()
		{
			storageManager.ResetProgress();
		}

		private ACloudManager GetRemoteDataManager()
		{
			ACloudManager cloudManager;
			if (!ACloudManager.Instance)
			{
				if (!yandexRemoteDataManager)
				{
					cloudManager = gameObject.AddComponent<EmptyCloudManager>();
				}
				else
				{
					cloudManager = Instantiate(yandexRemoteDataManager, null);
					cloudManager.name = yandexRemoteDataManager.name;
				}
			}
			else
			{
				cloudManager = ACloudManager.Instance;
			}

			cloudManager.SetSerializer(_dataSerializer);

			return cloudManager;
		}

		public void ForceSave()
		{
			storageManager.ForceSaveToCloud();
		}
	}
}