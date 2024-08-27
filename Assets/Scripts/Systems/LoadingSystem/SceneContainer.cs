using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace Systems.LoadingSystem
{
	public class ScenesContainer : MonoBehaviour
	{
		public static ScenesContainer Instance;
		private readonly Dictionary<string, SceneInstanceLogic> _scenes = new();

		public void Awake()
		{
			Instance = this;
			DontDestroyOnLoad(this);
		}

		public SceneInstanceLogic AddScene(SceneInstance sceneInstance)
		{
			var logic = CreateLogic(sceneInstance);
			_scenes.Add(sceneInstance.Scene.name, logic);
			return logic;
		}

		public SceneInstanceLogic RemoveScene(string sceneName)
		{
			if (!_scenes.ContainsKey(sceneName))
				return null;

			var logic = _scenes[sceneName];
			_scenes.Remove(sceneName);
			return logic;
		}

		/// <summary>
		///     Call when all scenes in unloaded, for example single mode loading for new scene
		/// </summary>
		public void RemoveAll()
		{
			_scenes.Clear();
		}

		private SceneInstanceLogic CreateLogic(SceneInstance instance)
		{
			return new SceneInstanceLogic(instance);
		}
	}
}