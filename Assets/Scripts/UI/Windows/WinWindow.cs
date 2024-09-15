using System;
using UI.Common;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows
{
	public class WinWindow : BaseWindow
	{
		[SerializeField] private Button _claimButton;
		[SerializeField] private StarView[] _starViews;
		public void Initialize()
		{
		}

		private void Start()
		{
			_claimButton.onClick.AddListener(OnClaimButtonClick);
		}

		private void OnClaimButtonClick()
		{
			ClaimReward();
		}

		private void ClaimReward()
		{
		}
	}
}