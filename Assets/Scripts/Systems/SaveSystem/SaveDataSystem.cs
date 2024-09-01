using System.Collections.Generic;
using Common;
using Data;
using Systems.SaveSystem.FileDataSaver;
using Systems.SaveSystem.Serializers;

namespace Systems.SaveSystem
{
	public class SaveDataSystem : ASystem
	{
		private readonly IDataSerializer _dataSerializer;

		private readonly Dictionary<string, ASavedData> _dataCache = new();

		private readonly List<ISaver> savers = new();

		public SaveDataSystem(IDataSerializer dataSerializer)
		{
			savers.Add(new LocalDataSaver(dataSerializer));
#if UNITY_WEBGL
#if YANDEX
			//savers.Add(new YandexCloudSaver());
#endif
#endif
		}

		public void SaveData<T>(T data, string key) where T : ASavedData
		{
			if (!_dataCache.TryAdd(key, data)) _dataCache[key] = data;

			foreach (var saver in savers) saver.SaveData(data, key);
		}

		public T LoadData<T>(string key) where T : ASavedData, new()
		{
			if (!_dataCache.ContainsKey(key))
				foreach (var saver in savers)
				{
					var data = saver.LoadData<T>(key);
					_dataCache.Add(key, data);
				}

			// if cache is empty = Load cloud data -> check local data; resave
			return (T) _dataCache[key];
		}
	}
}