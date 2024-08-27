using AudioManager.Runtime.Core.Manager;
using SharedLogic.UI.Common;
using UnityEngine;

namespace UI.HUD
{
	public class HUD : MonoBehaviour
	{
		public HUDPanel TopPanel;
		private HUDMode _lastMode;
		private HUDMode _mode;

		public void Reset()
		{
			TopPanel.Reset();
		}

		public void Hide(string reason, bool top = true, bool bottom = true, bool left = true, bool right = true)
		{
			if (top)
			{
				TopPanel.AddLock(reason);
				ManagerAudio.SharedInstance.PlayAudioClip(TAudio.None.ToString());
			}
		}

		public void Show(string reason, bool top = true, bool bottom = true, bool left = true, bool right = true)
		{
			if (top && TopPanel.RemoveLock(reason))
				ManagerAudio.SharedInstance.PlayAudioClip(TAudio.None.ToString());
		}

		public bool IsLockedBy(string reason, bool top = true, bool bottom = true, bool left = true, bool right = true)
		{
			if (top && TopPanel.HasLock(reason))
				return true;

			return false;
		}

		public void SetMode(HUDMode mode)
		{
			if (_mode == mode)
				return;

			_lastMode = _mode;
			_mode = mode;

			TopPanel.SetMode(mode);
		}

		public void RestoreLastMode()
		{
			SetMode(_lastMode);
		}
	}
}