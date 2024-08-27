using System;
using UnityEngine;

namespace UI.Common
{
	public abstract class AbstractUIElement : MonoBehaviour
	{
		#region AbstractUIElement

		/// <summary>
		///     Unique ID.
		/// </summary>
		[Tooltip("Уникальный идентификатор.")]
		public string ID = "";

		/// <summary>
		///     Use this method to show ui element.
		/// </summary>
		public abstract void Show(Action callback = null);

		/// <summary>
		///     Use this method to hide ui element.
		/// </summary>
		public abstract void Hide(Action callback = null);

		/// <summary>
		///     Use this method to update visual ui element and load resources. (called with OnEnable)
		/// </summary>
		protected virtual void OnEnableAction()
		{
		}

		/// <summary>
		///     Use this method to update visual ui element. (called when called method Show)
		/// </summary>
		protected virtual void OnShowAction()
		{
		}

		protected virtual void OnShowFinishedAction()
		{
		}

		/// <summary>
		///     Use this method to update visual ui element. (called when called method Hide)
		/// </summary>
		protected virtual void OnHideAction()
		{
		}

		/// <summary>
		///     Use this method to update visual ui element. (called with OnDisable)
		/// </summary>
		protected virtual void OnDisableAction()
		{
		}

		/// <summary>
		///     Use this method to unload resources. (called with OnDestroy)
		/// </summary>
		protected virtual void OnDestroyAction()
		{
		}

		/// <summary>
		///     Use this method to register events. (called with Awake)
		/// </summary>
		protected virtual void OnAwakeAction()
		{
		}

		/// <summary>
		///     Use this method to setup prefab. (called with Reset)
		/// </summary>
		protected virtual void OnResetAction()
		{
		}

		/// <summary>
		///     Use this method to update visual state
		/// </summary>
		public virtual void UpdateData()
		{
		}

		#endregion

		#region MonoBehaviour

		private void OnEnable()
		{
			OnEnableAction();
		}

		private void OnDisable()
		{
			OnDisableAction();
		}

		private void OnDestroy()
		{
			OnDestroyAction();
		}

		private void Awake()
		{
			OnAwakeAction();
		}

		private void Reset()
		{
			OnResetAction();

			if (ID.Length == 0) ID = gameObject.name;
		}

		#endregion
	}
}