// using UnityEngine;
//
// namespace Systems.SaveSystem.Serializers
// {
// 	public class MessagePackDataSerializer : IDataSerializer
// 	{
// 		public bool IsInitialized => _resolver != null;
// 		private IFormatterResolver _resolver;
//
// 		public void Initialize()
// 		{
// 			_resolver = CompositeResolver.Create(GeneratedResolver.Instance,
// 				BuiltinResolver.Instance,
// 				AttributeFormatterResolver.Instance,
// 				UnityResolver.Instance,
// 				PrimitiveObjectResolver.Instance,
// 				UnityBlitWithPrimitiveArrayResolver.Instance,
// 				StandardResolver.Instance
// 			);
// 			var option = MessagePackSerializerOptions.Standard.WithResolver(_resolver);
// 			MessagePackSerializer.DefaultOptions = option;
// 		}
//
// 		public T ToData<T>(string data)
// 		{
// 			if (!IsInitialized)
// 				Debug.LogError("Invalid deserialization, Initialize first!");
// 			return MessagePackSerializer.Deserialize<Data>(data);
// 		}
//
// 		public string FromData<T>(T data)
// 		{
// 			if (!IsInitialized)
// 				Debug.LogError("Invalid serialization, Initialize first!");
// 			return MessagePackSerializer.Serialize(data);
// 		}
// 	}
// }

