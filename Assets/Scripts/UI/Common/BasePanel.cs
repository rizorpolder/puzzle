using System;
using Common.Animation;
using UnityEngine;

namespace UI.Common
{
	public enum ElementStatus
	{
		Hidden,
		Showing,
		Shown,
		Hiding
	}

	public class BasePanel : AbstractUIElement
	{
		[Header("Base panel:")]
		[Tooltip("Элемент, отвечающий за анимации показа/скрытия панели. Рекомендуется PlayableAnimation.")]
		[SerializeField] protected BaseAnimation animationElement;

		[Tooltip("Начальное состояние панели. Если она скрыта, то Hidden. Если показана - Shown.")]
		[SerializeField] protected ElementStatus status = ElementStatus.Hidden;

		[SerializeField] private bool disableAfterHide = true;

		public event Action OnShownAction;
		public event Action OnHiddenAction;

		public ElementStatus Status
		{
			get { return status; }
		}

		public override void Show(Action callback = null)
		{
			if (IsShown())
				return;

			gameObject.SetActive(true);

			if (HasAnimation())
			{
				status = ElementStatus.Showing;

				animationElement.Show(() =>
				{
					if (status == ElementStatus.Showing)
					{
						OnShownAction?.Invoke();
						callback?.Invoke();
						status = ElementStatus.Shown;
						OnShowFinishedAction();
					}
				});
			}
			else
			{
				OnShownAction?.Invoke();
				callback?.Invoke();
				status = ElementStatus.Shown;
                OnShowFinishedAction();
            }

			OnShowAction();
		}

		public override void Hide(Action callback = null)
		{
			if (IsHidden())
				return;

			status = ElementStatus.Hiding;

			if (HasAnimation())
			{
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

		public virtual void HideInstant(Action callback = null)
		{
			if (IsHidden())
				return;

			callback?.Invoke();
			HidingComplete();

			OnHideAction();
		}

		private bool HasAnimation()
		{
			if (animationElement == null)
				animationElement = GetComponent<BaseAnimation>();

			return animationElement != null;
		}

		protected virtual void HidingComplete()
		{
			status = ElementStatus.Hidden;
			if (disableAfterHide)
				gameObject.SetActive(false);
			OnHiddenAction?.Invoke();
			OnHiddenAction = null;
		}

		public bool IsHidden() => Status == ElementStatus.Hidden || Status == ElementStatus.Hiding;
		public bool IsShown() => Status == ElementStatus.Shown || Status == ElementStatus.Showing;

		protected override void OnResetAction()
		{
			if (animationElement == null)
				animationElement = GetComponent<BaseAnimation>();
		}

		public void ResetHiddenAction()
		{
			OnHiddenAction = null;
		}
	}
}