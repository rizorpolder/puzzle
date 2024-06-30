using UnityEngine;

namespace Systems.SaveSystem.Serializers
{
	public class JsonDataSerializer : IDataSerializer
	{
		public T ToData<T>(string data)
		{
			return JsonUtility.FromJson<T>(data);
		}

		public string FromData<T>(T data)
		{
			return JsonUtility.ToJson(data);
		}
	}
}