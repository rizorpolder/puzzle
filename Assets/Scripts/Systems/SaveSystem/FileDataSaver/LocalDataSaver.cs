using System.IO;
using Data;
using Systems.SaveSystem.Serializers;
using UnityEngine;

namespace Systems.SaveSystem.FileDataSaver
{
	public class LocalDataSaver : ISaver
	{
		private IDataSerializer _dataSerializer;

		public LocalDataSaver(IDataSerializer dataSerializer)
		{
			SetSerializer(dataSerializer);
		}
		public void SetSerializer(IDataSerializer dataSerializer)
		{
			_dataSerializer = dataSerializer;
		}

		public void SaveData<T>(T data, string key) where T : ASavedData
		{
			var result = _dataSerializer.FromData(data);
			var path = Application.persistentDataPath + key;
			File.WriteAllText(path, result);
		}

		public T LoadData<T>(string key) where T : ASavedData, new()
		{
			T result = new T();
			var path = Application.persistentDataPath + $"/{key}";
			if (File.Exists(path))
			{
				var text = File.ReadAllText(path);
				result = _dataSerializer.ToData<T>(text);
			}

			return result;
		}

		public void ForceSave()
		{
			throw new System.NotImplementedException();
		}

		public void ClearData(string key)
		{
			var path = Application.persistentDataPath + $"/{key}";
			File.Delete(path);
		}

		public void ClearAllData()
		{
			var path = Application.persistentDataPath;
			var files = System.IO.Directory.GetFiles(path);
			foreach (var file in files)
			{
				System.IO.File.Delete(file);
			}
			PlayerPrefs.DeleteAll();
		}
	}
}