using System.Collections;
using Global;
using SharedLogic.UI.Common;
using Systems.LoadingSystem;
using UnityEngine;
using UnityEngine.UI;

namespace UI.HUD
{
	public class TopPanel : HUDPanel
	{
		[SerializeField] private Button settings;
		[SerializeField] private GameObject settingsRoot;
		[SerializeField] private Button backButton;

		[SerializeField] private Button _noAdsButton;
		public Button NoAdsButton => _noAdsButton;

		private void Start()
		{
			backButton.onClick.AddListener(OnBackButtonListener);
		}

		private void OnBackButtonListener()
		{
			//SharedContainer.Instance.DataManager.ForceSave();
			SharedContainer.Instance.LoadingController.Load(Scenes.Menu);
		}

		private void SetActiveSettings(bool isActive)
		{
			settingsRoot.SetActive(isActive);
			settings.gameObject.SetActive(isActive);
		}

		private void SetInteractableSettings(bool isActive)
		{
			settings.interactable = isActive;
		}

		public void RebuildTopPanelLayout()
		{
			if (gameObject.activeInHierarchy) StartCoroutine(RebuildTopPanelCoroutine());
		}

		public IEnumerator RebuildTopPanelCoroutine()
		{
			yield return new WaitForEndOfFrame();
		}

		public override void SetMode(HUDMode mode)
		{
			switch (mode)
			{
				case HUDMode.Core_Classic:
					SetActiveSettings(true);
					SetInteractableSettings(true);
					SetActivePlayerScore(true);
					break;
				case HUDMode.Core_Adventure:
					SetActiveSettings(true);
					SetInteractableSettings(true);
					SetActivePlayerScore(false);
					break;
				case HUDMode.Menu:
					SetActiveSettings(false);
					break;
			}
		}

		private void SetActivePlayerScore(bool value)
		{
		}
	}
}