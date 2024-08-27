using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UI.Common;
using UI.HUD;
using UnityEngine;
using UnityEngine.UI;

namespace SharedLogic.UI.Common
{
	public class HUDPanel : BasePanel
	{
		[SerializeField] private Button[] _buttons;

        /// <summary>
        ///     Задержка на показ
        /// </summary>
        [SerializeField] private int _delayShow = 200;

		private bool _isCanceledShow;
		private readonly List<Button> _runtimeButtons = new();
		private readonly HashSet<string> lockList = new();

		public void Reset()
		{
			lockList.Clear();
		}

		protected override void OnAwakeAction()
		{
			base.OnAwakeAction();
			status = ElementStatus.Shown;
		}

		public void AddLock(string reason)
		{
			lockList.Add(reason);
			SetButtonsInteractable(false);

			Hide();

			_isCanceledShow = true;
		}

		public bool RemoveLock(string reason)
		{
			lockList.Remove(reason);

			if (lockList.Count == 0)
			{
				_isCanceledShow = false;
				Show(reason);
			}

			return lockList.Count > 0;
		}

		public bool HasLock(string reason)
		{
			return lockList.Contains(reason);
		}

		public bool HasLock()
		{
			return lockList.Count > 0;
		}

		private async void Show(string reason)
		{
			var delay = reason.Equals("Core") ? 0 : _delayShow;
			delay = reason.Equals("TaskWindow") ? 280 : delay;

			await UniTask.Delay(delay);

			if (_isCanceledShow)
				return;

			status = ElementStatus.Hidden;
			Show(() => { SetButtonsInteractable(true); });
		}

		public virtual void SetMode(HUDMode mode)
		{
		}

		private void SetButtonsInteractable(bool interactable)
		{
			foreach (var button in _buttons)
				if (button != null)
					button.interactable = interactable;

			foreach (var button in _runtimeButtons)
				if (button != null)
					button.interactable = interactable;
		}

		public void AddButton(Button button)
		{
			_runtimeButtons.Add(button);
		}

		public void RemoveButton(Button button)
		{
			_runtimeButtons.Remove(button);
		}
	}
}