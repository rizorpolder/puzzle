using UnityEngine;

namespace Common.Animation
{
	public abstract class BaseAnimation : MonoBehaviour
	{
		public delegate void PostAnimationAction();

		public void Start()
		{
			OnStart();
		}

		public abstract void OnStart();
		public abstract void Show(PostAnimationAction action = null);
		public abstract void Hide(PostAnimationAction action = null);

		public abstract void Play(string name, PostAnimationAction action = null);
	}
}