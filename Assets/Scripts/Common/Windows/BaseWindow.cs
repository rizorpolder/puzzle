using System;
using Common.Animation;
using UI.Common;
using UnityEngine;

namespace Common.Windows
{
	public abstract class BaseWindow : BasePanel
	{
		public event Action OnShownAction;
		public event Action OnHiddenAction;

		[SerializeField] protected ElementStatus status = ElementStatus.Hidden;
		[SerializeField] private bool disableAfterHide = true;
		[SerializeField] protected BaseAnimation animationElement;
		[SerializeField] protected BaseAnimation[] additionalAnimationElements;

		public virtual void Hide()
		{
		}

		public virtual void Show(Action callback = null)
		{
			if (IsShown())
				return;

			gameObject.SetActive(true);

			if (HasAnimation())
			{
				status = ElementStatus.Showing;

				foreach (var anim in additionalAnimationElements)
				{
					anim.Show();
				}

				animationElement.Show(() =>
				{
					if (status == ElementStatus.Showing)
					{
						OnShownAction?.Invoke();
						callback?.Invoke();
						status = ElementStatus.Shown;
					}
				});
			}
			else
			{
				OnShownAction?.Invoke();
				callback?.Invoke();
				status = ElementStatus.Shown;
			}

			OnShowAction();
		}

		public virtual void Hide(Action callback = null)
		{
			if (IsHidden())
				return;

			status = ElementStatus.Hiding;

			if (HasAnimation())
			{
				foreach (var anim in additionalAnimationElements)
				{
					anim.Hide();
				}

				animationElement.Hide(() =>
				{
					if (status == ElementStatus.Hiding)
					{
						callback?.Invoke();
						HidingComplete();
					}
				});
			}
			else
			{
				callback?.Invoke();
				HidingComplete();
			}

			OnHideAction();
		}

		protected virtual void HidingComplete()
		{
			status = ElementStatus.Hidden;
			if (disableAfterHide && gameObject)
				gameObject.SetActive(false);

			var hidedCallback = OnHiddenAction;
			ResetHiddenAction();
			hidedCallback?.Invoke();
		}

		private bool HasAnimation()
		{
			if (!animationElement)
				animationElement = GetComponent<BaseAnimation>();

			return animationElement;
		}

		public void ResetHiddenAction()
		{
			OnHiddenAction = null;
		}
	}
}