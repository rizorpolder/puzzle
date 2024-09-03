using System;
using Configs.TextureRepository;
using Data;
using Extensions;
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

		private GameDifficult _selectedDifficult = GameDifficult.Low;

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
			foreach (var selectButtons in _difficultSelectButtons)
			{
				selectButtons.SetSelectedState(false);
			}
		}

		private void OnBuyButtonClick()
		{
			//TODO Spend cost
		}

		private void OnPlayButtonClick()
		{
			//Change runtimeData to settings (Categories, name, diff)
			SharedContainer.Instance.WindowsController.HideAllWindows();
			SharedContainer.Instance.LoadingController.Load(Scenes.Core);
		}

		public void SetData(TextureUnitConfig unit)
		{
			SetView(unit.Texture);
			SetCost(unit.TextureCost);
			SetHeader(unit.Category.ToString());
			//SetLockedState()
		}

		private void SetHeader(string header)
		{
			_header.text = header;
		}

		private void SetCost(int unitTextureCost)
		{
			_textureCost.text = unitTextureCost.ToString();
		}

		private void SetView(Texture2D unitTexture)
		{
			_image.sprite = unitTexture.CreateSprite();
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

		private void SelectGameDifficult(GameDifficult diff)
		{
			_selectedDifficult = diff;
			foreach (var difficultSelectButton in _difficultSelectButtons)
			{
				var isSelected = difficultSelectButton.GameDifficult.Equals(diff);
				difficultSelectButton.SetSelectedState(isSelected);
			}
		}
	}
}