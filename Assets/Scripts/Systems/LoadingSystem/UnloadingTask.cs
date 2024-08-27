using System;

namespace Systems.LoadingSystem
{
	public class UnloadingTask
	{
		public UnloadingTask SetSceneName(string sceneName)
		{
			SceneName = sceneName;
			return this;
		}

		public UnloadingTask SetLoadingScreenType()
		{
			return this;
		}

		public UnloadingTask SetHideLoadingView(bool value)
		{
			HideLoadingView = value;
			return this;
		}

		public UnloadingTask AddPostAnimationAction(Action action)
		{
			PostAnimationAction = action;
			return this;
		}

		public UnloadingTask AddCallback(Action callback)
		{
			Callback = callback;
			return this;
		}

		#region Fields

		#endregion

		#region Props

		public string SceneName { get; private set; }

		public bool HideLoadingView { get; private set; } = true;

		public Action PostAnimationAction { get; private set; }

		public Action Callback { get; private set; }

		#endregion
	}
}