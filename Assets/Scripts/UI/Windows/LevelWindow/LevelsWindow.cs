using System;
using System.Collections.Generic;
using Configs;
using Configs.TextureRepository;
using Global;
using TMPro;
using UI.Common;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows.LevelWindow
{
	public class LevelsWindow : BaseWindow
	{
		[SerializeField] private LevelsPanel levelsPanel;
		[SerializeField] private TMP_Dropdown _dropdown;
		[SerializeField] private Button closeButton;

		private ImageRepositoryConfig _config;
		private int _selectedCategory = 0;
		private List<TextureCategory> _categoriesList;

		private void Start()
		{
			closeButton.onClick.AddListener(Close);
			_dropdown.onValueChanged.AddListener(OnDropDownValueChanged);
		}

		private void OnDropDownValueChanged(int arg)
		{
			_selectedCategory = arg;
			UpdateLevelsPanel();
		}

		protected override void OnAwakeAction()
		{
			base.OnAwakeAction();

			_config = SharedContainer.Instance.ConfigurableRoot.ImageRepositoryConfig;
			FillDropDown();
			UpdateLevelsPanel();
			levelsPanel.OnWindowCall += CallLevelInfoWindow;
		}

		private void FillDropDown()
		{
			_categoriesList = _config.GetAllCategories();
			_dropdown.ClearOptions();
			foreach (var category in _categoriesList)
			{
				_dropdown.options.Add(new TMP_Dropdown.OptionData(category.ToString()));
			}

			_dropdown.SetValueWithoutNotify(_selectedCategory);
		}

		// private void OnDropDownValueChanged(TextureCategory selectedCategory)
		// {
		// 	_selectedCategory = selectedCategory;
		// 	UpdateLevelsPanel();
		// }

		private void UpdateLevelsPanel()
		{
			var categoryConfigs = _config.GetLevelsByCategory(_categoriesList[_selectedCategory]);
			levelsPanel.Initialize(categoryConfigs);
		}

		private void CallLevelInfoWindow(TextureCategory category, string textureName)
		{
			SharedContainer.Instance.WindowsController.Show(WindowType.LevelInfoWindow,
				window =>
				{
					var unit = _config.GetConfig(category, textureName);
					if (window is LevelInfoWindow infoWindow)
						infoWindow.SetData(category, unit);
				});
		}

		protected override void OnShowAction()
		{
			base.OnShowAction();
		}
	}
}