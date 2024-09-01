using System;
using Data;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows.LevelInfoWindow
{
	public class DifficultSelectButton : MonoBehaviour
	{
		public Action<GameDifficult> OnDifficultSelected = difficult => { };

		[SerializeField] private Button _button;
		[SerializeField] private GameDifficult _difficult;
		[SerializeField] private float animationTime = 0.4f;

		public GameDifficult GameDifficult => _difficult;

		private void Start()
		{
			_button.onClick.AddListener(OnDifficultSelect);
		}

		private void OnDifficultSelect()
		{
			OnDifficultSelected?.Invoke(_difficult);
		}

		public void SetSelectedState(bool isSelected)
		{
			var scale = isSelected ? new Vector3(1.1f, 1.1f, 1.1f) : Vector3.one;
			this.transform.DOScale(scale, animationTime);
		}
	}
}