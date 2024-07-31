using System;
using Common.Animation;
using UnityEngine;

namespace Common.Windows
{
	public abstract class BaseWindow : MonoBehaviour
	{
		public event Action OnShownAction;
		public event Action OnHiddenAction;

		[SerializeField] protected ElementStatus status = ElementStatus.Hidden;
		[SerializeField] private bool disableAfterHide = true;
		[SerializeField] protected BaseAnimation animationElement;
		[SerializeField] protected BaseAnimation[] additionalAnimationElements;
		public ElementStatus Status => status;
		public bool IsHidden() => Status == ElementStatus.Hidden || Status == ElementStatus.Hiding;
		public bool IsShown() => Status == ElementStatus.Shown || Status == ElementStatus.Showing;

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

		protected virtual void OnShowAction()
		{
		}

		protected virtual void OnHideAction()
		{
		}

		public void ResetHiddenAction()
		{
			OnHiddenAction = null;
		}
	}

	public enum ElementStatus
	{
		Hidden,
		Showing,
		Shown,
		Hiding
	}
}