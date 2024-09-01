using System;
using Configs.TextureRepository;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows.LevelWindow
{
	public class ItemElementView : MonoBehaviour
	{
		[SerializeField] private GameObject _activeRoot;
		[SerializeField] private GameObject _inactiveRoot;

		[SerializeField] private Button _button;
		[SerializeField] private TextMeshProUGUI _count;
		[SerializeField] private StarView[] _stars; //ToDO to script for enable/initialize
		private bool _isActive;

		private int _spriteIndex;
		private TextureCategory _category;
		public Action<int> OnButtonClick = i => { };

		private void Start()
		{
			_button.onClick.AddListener(OnButtonClickHandler);
		}

		public ItemElementView SetIndex(int index)
		{
			_count.text = index.ToString();
			_spriteIndex = index;
			return this;
		}

		public ItemElementView SetCategory(TextureCategory category)
		{
			_category = category;
			return this;
		}

		public ItemElementView SetStars(int count)
		{
			var i = 0;
			for (; i < count; i++) _stars[i].SetState(true);

			for (; i < _stars.Length; i++) _stars[i].SetState(false);

			return this;
		}

		public ItemElementView SetState(bool isActive)
		{
			_isActive = isActive;
			_activeRoot.SetActive(isActive);
			_inactiveRoot.SetActive(!isActive);
			return this;
		}

		private void OnButtonClickHandler()
		{
			if (!_isActive)
				return;

			OnButtonClick?.Invoke(_spriteIndex);
		}
	}
}