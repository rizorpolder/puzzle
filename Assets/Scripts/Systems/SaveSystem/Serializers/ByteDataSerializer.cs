using System;
using System.Text;

namespace Systems.SaveSystem.Serializers
{
	public class ByteDataSerializer: IDataSerializer
	{
		private readonly Encoding _encoding = Encoding.UTF8;

		public T ToData<T>(string data)
		{
			throw new NotImplementedException();
			var bytes = _encoding.GetBytes(data);
			// bytes -> to T
			return default;
		}

		public string FromData<T>(T data)
		{
			throw new NotImplementedException();
			// data -> to string
			return _encoding.GetString(default);
		}
	}
}