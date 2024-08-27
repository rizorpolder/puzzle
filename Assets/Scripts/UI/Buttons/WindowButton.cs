using Common.Windows;
using Configs;
using Global;
using UI.Common;
using UnityEngine;

namespace UI.Buttons
{
	public class WindowButton : BaseButton
	{
		[SerializeField] private WindowType windowType;

		[SerializeField] private string windowName;
		[SerializeField] private bool show;
		[SerializeField] private bool _requiredNetwork;

		public WindowType WindowType
		{
			get => windowType;
			set => windowType = value;
		}

		public string WindowName
		{
			get => windowName;
			set => windowName = value;
		}

		protected override void OnClickAction()
		{
			if (_requiredNetwork)
				if (Application.internetReachability == NetworkReachability.NotReachable)
				{
					Debug.Log("NO INTERNET BEHAVIOUR");
					return;
				}

			base.OnClickAction();
			WindowAction();
		}

		protected virtual void WindowAction()
		{
			if (windowType == WindowType.Custom)
				WindowByName();
			else
				WindowByType();
		}

		protected virtual void OnShownWindowAction(BaseWindow window)
		{
		}

		protected virtual void OnHiddenWindowAction()
		{
		}

		private void WindowByType()
		{
			var windowsController = SharedContainer.Instance.WindowsController;
			if (show)
				windowsController.Show(windowType,
					window =>
					{
						OnShownWindowAction(window);
						window.OnHiddenAction += OnHiddenWindowAction;
					},
					WindowSource.Button);
			else
				windowsController.Hide(windowType);
		}

		private void WindowByName()
		{
			var windowsController = SharedContainer.Instance.WindowsController;

			if (show)
				windowsController.Show(WindowName,
					window =>
					{
						OnShownWindowAction(window);
						window.OnHiddenAction += OnHiddenWindowAction;
					},
					WindowSource.Button);
			else
				windowsController.Hide(WindowName);
		}
	}
}