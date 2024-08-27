using System;
using AudioManager.Runtime.Core.Manager;
using UI.Common;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Buttons
{
	[RequireComponent(typeof(Button))]
	public class BaseButton : AbstractUIElement
	{
		[SerializeField] protected Button button;
		[SerializeField] protected TAudio clickType = TAudio.Plop;

		public Button ButtonInstance => button;

		protected virtual void OnValidate()
		{
			if (button == null) button = GetComponent<Button>();
		}

		public event Action Clicked;

		public void Click()
		{
			OnClickAction();
		}

		public override void Hide(Action callback = null)
		{
			gameObject.SetActive(false);
		}

		public override void Show(Action callback = null)
		{
			OnShowAction();

			gameObject.SetActive(true);
		}

		protected override void OnAwakeAction()
		{
			base.OnAwakeAction();
			button.onClick.AddListener(OnClick);
		}

		private void OnClick()
		{
			OnClickAction();
		}

		protected virtual void OnClickAction()
		{
			ManagerAudio.SharedInstance.PlayAudioClip(clickType.ToString());
			Clicked?.Invoke();
		}
	}
}