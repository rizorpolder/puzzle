using UnityEngine;

namespace Common
{
	public class Singletone : MonoBehaviour
	{
		private static Singletone _instance;

		public static Singletone Instance
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

		void Start()
		{
			_instance = this;
		}
	}
}