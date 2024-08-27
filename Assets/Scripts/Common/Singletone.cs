using UnityEngine;

namespace Common
{
	public class Singletone<T> : MonoBehaviour where T : MonoBehaviour
	{
		public static T Instance { get; private set; }

		protected void Awake()
		{
			if (Instance)
			{
				Debug.LogError($"{name} singleton duplication!");
				return;
			}

			Instance = this as T;

			OnAwakeAction();
		}

		private void OnDestroy()
		{
			Instance = null;
		}

		protected virtual void OnAwakeAction()
		{
		}
	}
}