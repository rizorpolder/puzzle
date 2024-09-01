using System;
using System.Collections.Generic;
using UnityEngine;

namespace Helpers
{
	/// <summary>
	/// Dictionary with proxy lists from serialize data in Unity
	/// /// </summary>
	[Serializable]
	public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
	{
		[SerializeField]
		private List<TKey> keys = new List<TKey>();

		[SerializeField]
		private List<TValue> values = new List<TValue>();

		public void OnBeforeSerialize()
		{
			keys.Clear();
			values.Clear();
			foreach (KeyValuePair<TKey, TValue> pair in this)
			{
				keys.Add(pair.Key);
				values.Add(pair.Value);
			}
		}

		public void OnAfterDeserialize()
		{
			this.Clear();
#if UNITY_EDITOR
			bool isStringKey = typeof(TKey) == typeof(string);
			TKey key = isStringKey ? (TKey)("New" as object) : default(TKey);

			if (keys.Count != values.Count)
			{
				int count = Mathf.Max(keys.Count, values.Count);
				for (int i = 0; i < count; i++)
				{
					if (keys.Count == i) {
						if (isStringKey)
							key = (TKey)($"New{i}" as object);
						keys.Add(key);
					}
					if (values.Count == i)
						values.Add(default(TValue));
				}
			}
#endif
			for (int i = 0; i < keys.Count; i++)
			{
#if UNITY_EDITOR

				if (ContainsKey(keys[i]))
				{
					if (isStringKey)
						key = (TKey)($"New{i}" as object);
					keys[i] = key;
				}
#endif
				this.Add(keys[i], values[i]);
			}
		}

	}
}