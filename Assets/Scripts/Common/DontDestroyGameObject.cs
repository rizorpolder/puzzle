using UnityEngine;

namespace Common
{
	public class DontDestroyGameObject : MonoBehaviour
	{
		protected virtual void Awake()
		{
			transform.SetParent(null);
			DontDestroyOnLoad(gameObject);
		}
	}
}