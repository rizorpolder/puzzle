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

		public SaveDataSystem(IDataSerializer dataSerializer)
		{
			_dataSerializer = dataSerializer;
		}

		public void SaveData<T>(T data) where T:ASavedData
		{
			var result = _dataSerializer.FromData(data);
			File.WriteAllText(Application.persistentDataPath + data.PATH, result);
		}

		public T LoadData<T>(string path) where T:ASavedData
		{
			var result = File.ReadAllText(path);
			return _dataSerializer.ToData<T>(result);
		}


	}


}