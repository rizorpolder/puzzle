using System.IO;
using Common;
using Data;
using Systems.SaveSystem.Serializers;
using UnityEngine;

namespace Systems.SaveSystem
{
	public class SaveDataSystem : ASystem
	{
		private IDataSerializer _dataSerializer;

		public static SaveDataSystem Instance;

		public SaveDataSystem(IDataSerializer dataSerializer)
		{
			Instance = this;
			_dataSerializer = dataSerializer;
		}

		public void SaveData<T>(T data) where T:ASavedData
		{
			var result = _dataSerializer.FromData(data);
			File.WriteAllText(Application.persistentDataPath + data.PATH, result);
		}

		public T LoadData<T>() where T : ASavedData, new()
		{
			var data = new T();
			var path = data.PATH;
			var result = File.ReadAllText(path);
			data = _dataSerializer.ToData<T>(result);
			return data;
		}


	}


}