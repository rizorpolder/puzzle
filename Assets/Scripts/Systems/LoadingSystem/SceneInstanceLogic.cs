using Cysharp.Threading.Tasks;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace Systems.LoadingSystem
{
	public class SceneInstanceLogic
	{
		public SceneInstance Instance { get; private set; }

		public SceneInstanceLogic(SceneInstance instance)
		{
			Instance = instance;
		}

		public virtual async UniTask ActivateAsync()
		{
			await Instance.ActivateAsync();
		}

		public virtual void Destroy()
		{

		}
	}
}