using Global;
using UI.Common;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows.LevelWindow
{
	public class LevelsWindow : BaseWindow
	{
		[SerializeField] private LevelsPanel levelsPanel;

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
			var repositoryConfig = SharedContainer.Instance.ConfigurableRoot.ImageRepositoryConfig;
			var levels = repositoryConfig.GetAllLevels();
			levelsPanel.Initialize(levels);
		}
	}
}