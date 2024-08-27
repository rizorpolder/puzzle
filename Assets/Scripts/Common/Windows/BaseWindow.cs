using Global;
using UnityEngine;

namespace UI.Common
{
	public class BaseWindow : BasePanel
	{
		[SerializeField] private bool _closableByBack = true;
		public bool BlockScroll { get; protected set; } = true;

		public void Open()
		{
			SharedContainer.Instance.WindowsController.Show(ID);
		}

		public virtual void Close()
		{
			SharedContainer.Instance.WindowsController.Hide(ID);
		}

		//implementation of behaviour for back button event
		public virtual bool ClosableByBack()
		{
			return _closableByBack;
		}

		public virtual void OnBackButton()
		{
		}
	}
}