using Cysharp.Threading.Tasks;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace Systems.LoadingSystem
{
	public class SceneInstanceLogic
	{
		public SceneInstanceLogic(SceneInstance instance)
		{
			Instance = instance;
		}

		public SceneInstance Instance { get; }

		public virtual async UniTask ActivateAsync()
		{
			await Instance.ActivateAsync();
		}

		public virtual void Destroy()
		{
		}
	}
}