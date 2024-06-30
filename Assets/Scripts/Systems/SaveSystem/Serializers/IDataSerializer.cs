namespace Systems.SaveSystem.Serializers
{
	public interface IDataSerializer
	{
		public T ToData<T>(string data);
		public string FromData<T>(T data);
	}
}