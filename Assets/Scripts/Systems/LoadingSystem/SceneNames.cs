using UnityEngine.SceneManagement;

namespace Systems.LoadingSystem
{
	public class SceneNames
	{
		public static readonly string Core = "Core";
		public static readonly string Menu = "Menu";

		/// <summary>
		///     Level editor for merge/match3
		/// </summary>
		public static readonly string LevelEditor = "LevelEditor";

		public static bool IsMenuScene(string sceneName)
		{
			return sceneName == Menu;
		}

		public static bool IsCoreScene(string sceneName)
		{
			return sceneName == Core || sceneName == LevelEditor;
		}

		public static bool IsCoreScene()
		{
			var sceneName = SceneManager.GetActiveScene().name;
			return IsCoreScene(sceneName);
		}

		public static bool IsMenuScene()
		{
			var sceneName = SceneManager.GetActiveScene().name;
			return IsMenuScene(sceneName);
		}
	}
}