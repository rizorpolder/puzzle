using UnityEngine;

namespace Common.Windows
{
	public abstract class BaseWindow : MonoBehaviour
	{
		public System.Action OnWindowsShowed = () => { };
		public System.Action OnWindowsHidden = () => { };

		public virtual void Hide()
		{
		}

		public virtual void Show()
		{
		}

		public virtual void OnShownAction()
		{
			OnWindowsShowed?.Invoke();
		}

		public virtual void OnHiddenAction()
		{
			OnWindowsHidden?.Invoke();
		}
	}
}