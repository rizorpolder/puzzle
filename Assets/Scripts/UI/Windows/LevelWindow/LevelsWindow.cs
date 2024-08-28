using Global;
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

		protected override void OnShowAction()
		{
			base.OnShowAction();
			//Get Config,
			//Create categories
			//Fill Categories (ref config)

			var config = SharedContainer.Instance.ConfigurableRoot.ImageRepositoryConfig;
		}


	}
}