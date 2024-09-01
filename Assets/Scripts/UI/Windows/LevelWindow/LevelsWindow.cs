using Configs;
using Configs.TextureRepository;
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

		private ImageRepositoryConfig _config;

		private void Start()
		{
			closeButton.onClick.AddListener(Close);
		}

		protected override void OnAwakeAction()
		{
			base.OnAwakeAction();

			//Get Config,
			//Create categories
			//Fill Categories (ref config)
			_config = SharedContainer.Instance.ConfigurableRoot.ImageRepositoryConfig;
			var levels = _config.GetAllLevels();
			levelsPanel.Initialize(levels);
			levelsPanel.OnWindowCall += CallLevelInfoWindow;
		}

		private void CallLevelInfoWindow(int index)
		{
			SharedContainer.Instance.WindowsController.Show(WindowType.LevelInfoWindow,
				window =>
				{
					var unit = _config.GetConfigByIndex(index);
					if (window is LevelInfoWindow infoWindow) infoWindow.SetData(unit);
				});
		}

		protected override void OnShowAction()
		{
			base.OnShowAction();
		}
	}
}