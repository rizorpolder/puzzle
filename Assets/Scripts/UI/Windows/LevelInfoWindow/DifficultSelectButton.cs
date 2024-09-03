using System;
using Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows.LevelInfoWindow
{
	public class DifficultSelectButton : MonoBehaviour
	{
		public Action<Difficult> OnDifficultSelected = difficult => { };

		[SerializeField] private Button _button;
		[SerializeField] private Image _image;
		[SerializeField] private TextMeshProUGUI _buttonText;
		[SerializeField] private GameDifficult _difficult;

		[SerializeField] private ButtonStateOptions _selected;
		[SerializeField] private ButtonStateOptions _deselected;


		public GameDifficult GameDifficult => _difficult;

		private void Start()
		{
			_button.onClick.AddListener(OnDifficultSelect);
			var fieldSize = Difficult.GetDifficult(_difficult).FieldSize;
			_buttonText.text = $"{fieldSize.x}x{fieldSize.y}";
		}

		private void OnDifficultSelect()
		{
			var dif = Difficult.GetDifficult(_difficult);
			OnDifficultSelected?.Invoke(dif);
		}

		public void SetSelectedState(bool isSelected)
		{
			var options = isSelected ? _selected : _deselected;
			_image.sprite = options.SpriteSprite;
			_buttonText.fontMaterial = options.Material;
		}

		[Serializable]
		private class ButtonStateOptions
		{
			public Sprite SpriteSprite;
			public Material Material;
		}

	}
}