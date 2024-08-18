using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace Systems.LoadingSystem
{
	public enum Scenes
	{
		Core,
		Menu
	}

	public class LoadingController : MonoBehaviour
	{
		#region LoadingProcess

		public async void Load(Scenes TScene, Action callback = null)
		{
			var sceneName = GetSceneName(TScene);
			await LoadAsync(sceneName);

			var scene = SceneManager.GetSceneByName(sceneName);
			SceneManager.SetActiveScene(scene);
			callback?.Invoke();
		}

		private async UniTask LoadAsync(string sceneName)
		{
			var loadParam = LoadSceneMode.Single; // sceneName == "Core" ? LoadSceneMode.Additive :

			if (loadParam == LoadSceneMode.Single)
				ScenesContainer.Instance.RemoveAll();

			var operation = Addressables.LoadSceneAsync(sceneName, loadParam, false);
			await operation;

			var sceneLogic = ScenesContainer.Instance.AddScene(operation.Result);
			await sceneLogic.ActivateAsync();
		}

		#endregion

		#region UnLoadingProcess

		public async void Unload(string sceneName, Action callback = null)
		{
			await UnloadSceneAsync(sceneName);

			_ = UnityEngine.Resources.UnloadUnusedAssets();

			callback?.Invoke();
		}

		public async void Unload(UnloadingTask unloadingTask)
		{
			var animationCompleted = false;

			await UniTask.WaitUntil(() => animationCompleted);

			await UnloadSceneAsync(unloadingTask.SceneName);

			await Resources.UnloadUnusedAssets();

			unloadingTask.Callback?.Invoke();
		}

		private async UniTask UnloadSceneAsync(string sceneName)
		{
			var sceneLogic = ScenesContainer.Instance.RemoveScene(sceneName);
			if (sceneLogic is not null)
			{
				var operation = Addressables.UnloadSceneAsync(sceneLogic.Instance);
				await operation;
				sceneLogic.Destroy();
			}
			else
			{
				await SceneManager.UnloadSceneAsync(sceneName);
			}
		}

		#endregion

		private string GetSceneName(Scenes scene)
		{
			var sceneName = "Menu";
			switch (scene)
			{
				case Scenes.Core:
					sceneName = "Core";
					break;
				case Scenes.Menu:
					sceneName = "Menu";
					break;
			}

			return sceneName;
		}
	}
}