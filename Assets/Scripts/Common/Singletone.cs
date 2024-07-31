using UnityEngine;

namespace Common
{
	public class Singletone<T> : MonoBehaviour where T:MonoBehaviour
	{
		private static T _instance;

		public static T Instance
		{
			get => _instance;
			private set
			{
				if (_instance != null)
				{
					DestroyImmediate(_instance);
				}

				_instance = value;
			}
		}

		void Awake()
		{
			_instance = this as T;
		}
	}
}