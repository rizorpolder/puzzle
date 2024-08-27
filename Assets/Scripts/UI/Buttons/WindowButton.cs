using Common.Windows;
using Configs;
using Global;
using Systems;
using UI.Windows;
using UnityEngine;

namespace UI.Buttons
{
    public class WindowButton : BaseButton
    {
        [SerializeField] private WindowType windowType;
        public WindowType WindowType
        {
	        get { return windowType; }
	        set { windowType = value; }
        }

        [SerializeField] private string windowName;
		public string WindowName
		{
			get { return windowName; }
			set { windowName = value; }
		}
		[SerializeField] private bool show;
		[SerializeField] private bool _requiredNetwork;


		protected override void OnClickAction()
		{


            if (_requiredNetwork)
            {
                if (Application.internetReachability == NetworkReachability.NotReachable)
                {
                    Debug.Log("NO INTERNET BEHAVIOUR");
                    return;
                }
            }

            base.OnClickAction();
			WindowAction();
		}

		protected virtual void WindowAction()
        {
			if (windowType == WindowType.Custom)
			{
				WindowByName();
			}
			else
			{
				WindowByType();
			}
		}

		protected virtual void OnShownWindowAction(BaseWindow window)
		{

		}

        protected virtual void OnHiddenWindowAction()
        {

        }

        void WindowByType()
        {
	        var windowsController = SharedContainer.Instance.WindowsController;
            if (show)
            {

                windowsController.Show(windowType, (BaseWindow window) =>
                {
                    OnShownWindowAction(window);
                    window.OnHiddenAction += OnHiddenWindowAction;
                }, WindowSource.Button);
            }
            else
	            windowsController.Hide(windowType);
        }

        void WindowByName()
        {
	        var windowsController = SharedContainer.Instance.WindowsController;

            if (show)
            {
	            windowsController.Show(WindowName, (BaseWindow window) =>
                {
                    OnShownWindowAction(window);
                    window.OnHiddenAction += OnHiddenWindowAction;
                }, WindowSource.Button);
            }
            else
	            windowsController.Hide(WindowName);
        }
    }
}