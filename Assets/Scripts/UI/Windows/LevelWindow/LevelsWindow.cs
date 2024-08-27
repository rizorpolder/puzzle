using System;
using UI.Common;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows.LevelWindow
{
	public class LevelsWindow : BaseWindow
	{
		///2 вьюхи (категории и уровни)
		/// игрок клацая по категории проваливается во вторую вьюху где уже уровни со звездами
		/// звезды из правого угла вынести
		[SerializeField] private RectTransform categoriesRoot;
		[SerializeField] private RectTransform levelsDataRoot;

		[SerializeField] private Button closeButton;

		private void Start()
		{
			closeButton.onClick.AddListener(Close);
		}

		protected override void OnEnableAction()
		{
			base.OnEnableAction();
			SwitchState(WindowState.Categories);
		}

		protected override void OnShowAction()
		{
			base.OnShowAction();
			//Get Config,
			//Create categories
			//Fill Categories (ref config)
		}

		private void SwitchState(WindowState state)
		{
			switch (state)
			{
				case WindowState.Categories:
					categoriesRoot.gameObject.SetActive(true);
					levelsDataRoot.gameObject.SetActive(false);
					break;
				case WindowState.Levels:
					categoriesRoot.gameObject.SetActive(false);
					levelsDataRoot.gameObject.SetActive(true);
					break;
			}
		}

		public enum WindowState
		{
			Categories,
			Levels,
		}
	}
}