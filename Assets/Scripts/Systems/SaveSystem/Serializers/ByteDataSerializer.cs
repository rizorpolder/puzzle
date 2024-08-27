using System.Text;

namespace Systems.SaveSystem.Serializers
{
	public class ByteDataSerializer : IDataSerializer
	{
		private readonly Encoding _encoding = Encoding.UTF8;

		public bool IsInitialized => true;

		public void Initialize()
		{
		}

		public T ToData<T>(string data)
		{
			var bytes = _encoding.GetBytes(data);
			// bytes -> to T
			return default;
		}

		public string FromData<T>(T data)
		{
			// data -> to string
			return _encoding.GetString(default);
		}
	}
}