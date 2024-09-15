using Configs.TextureRepository;
using Data;
using Data.Player;
using Global;
using Systems.LoadingSystem;
using TMPro;
using UI.Common;
using UI.Windows.LevelInfoWindow;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows.LevelWindow
{
	public class LevelInfoWindow : BaseWindow
	{
		[SerializeField] private StarView[] _starViews;
		[SerializeField] private TextMeshProUGUI _header;
		[SerializeField] private TextMeshProUGUI _textureCost;
		[SerializeField] private Image _image;

		[SerializeField] private GameObject difficultPanel;
		[SerializeField] private GameObject buyPanel;

		[SerializeField] private DifficultSelectButton[] _difficultSelectButtons;

		[SerializeField] private Button buyButton;
		[SerializeField] private Button _playButton;

		private Difficult _selectedDifficult = Difficult.Low;
		private TextureUnitConfig _config;
		private void Start()
		{
			_playButton.onClick.AddListener(OnPlayButtonClick);
			buyButton.onClick.AddListener(OnBuyButtonClick);
			foreach (var difficultSelectButton in _difficultSelectButtons)
			{
				difficultSelectButton.OnDifficultSelected += SelectGameDifficult;
			}
		}

		protected override void OnShowAction()
		{
			SelectGameDifficult(_selectedDifficult);
		}

		private void OnBuyButtonClick()
		{
			SharedContainer.Instance.RuntimeData.PlayerData.SpendResource(_config.TextureCost);
		}

		private void OnPlayButtonClick()
		{
			//Change runtimeData to settings (Categories, name, diff)
			SharedContainer.Instance.RuntimeData.StartCoreGame(_config, _selectedDifficult);
			SharedContainer.Instance.WindowsController.HideAllWindows();
			SharedContainer.Instance.LoadingController.Load(Scenes.Core);
		}

		public void SetData(TextureUnitConfig unit)
		{
			_config = unit;

			SetView(unit.Sprite);
			SetCost(unit.TextureCost);
			SetHeader(unit.Category.ToString());

			var haveSavedData = SharedContainer.Instance.RuntimeData.PlayerData.LevelsData.HaveLevelData(unit.Category, unit.TextureName, out var dataInfo);
			SetLockedState(!haveSavedData);
			SetStarsEnabled(0);
		}

		private void SetHeader(string header)
		{
			_header.text = header;
		}

		private void SetCost(Resource resource)
		{
			_textureCost.text = resource.value.ToString();
		}

		private void SetView(Sprite sprite)
		{
			_image.sprite = sprite;
		}

		private void SetLockedState(bool isLocked)
		{
			difficultPanel.gameObject.SetActive(!isLocked);
			buyPanel.gameObject.SetActive(isLocked);
		}

		private void SetStarsEnabled(int count)
		{
			int i = 0;
			for (; i < count; i++)
			{
				_starViews[i].SetState(true);
			}

			for (; i < _starViews.Length; i++)
			{
				_starViews[i].SetState(false);
			}
		}

		private void SelectGameDifficult(Difficult diff)
		{
			_selectedDifficult = diff;
			foreach (var difficultSelectButton in _difficultSelectButtons)
			{
				var isSelected = difficultSelectButton.GameDifficult.Equals(diff.DifficultValue);
				difficultSelectButton.SetSelectedState(isSelected);
			}
		}
	}
}