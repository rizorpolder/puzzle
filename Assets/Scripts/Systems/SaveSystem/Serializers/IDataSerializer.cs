namespace Systems.SaveSystem.Serializers
{
	public interface IDataSerializer
	{
		public bool IsInitialized { get; }
		public void Initialize();
		public T ToData<T>(string data);
		public string FromData<T>(T data);
	}
}