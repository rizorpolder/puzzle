using System;
using Configs.TextureRepository;
using Data.Player;
using Global;
using Systems.LoadingSystem;
using TMPro;
using UI.Common;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows.LevelWindow
{
	public class LevelInfoWindow : BaseWindow
	{
		[SerializeField] private TextMeshProUGUI _textureCost;
		[SerializeField] private Image _image;
		[SerializeField] private Button playButton;

		private void Start()
		{
			playButton.onClick.AddListener(OnPlayButtonClick);
		}

		private void OnPlayButtonClick()
		{
			SharedContainer.Instance.RuntimeData.SetGameType(GameType.Classic);
			LoadCoreScene();
		}

		private void LoadCoreScene()
		{
			SharedContainer.Instance.LoadingController.Load(Scenes.Core);
		}


		public void SetData(TextureUnitConfig unit)
		{
			SetView(unit.Texture);
			SetCost(unit.TextureCost);
		}

		private void SetCost(int unitTextureCost)
		{
			_textureCost.text = unitTextureCost.ToString();
		}

		private void SetView(Texture2D unitTexture)
		{
			var pivot = new Vector2(0.5f, 0.5f);
			var rect = new Rect(0.0f, 0.0f, unitTexture.width, unitTexture.height);
			var sprite = Sprite.Create(unitTexture, rect, pivot);
			_image.sprite = sprite;
		}
	}
}