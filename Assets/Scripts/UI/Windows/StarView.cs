using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows
{
	public class StarView : MonoBehaviour
	{
		[SerializeField] private Image _image;
		[SerializeField] private GameObject _effect;
		[SerializeField] private Sprite _enabled;
		[SerializeField] private Sprite _disabled;

		public void SetState(bool isActive)
		{
			_image.sprite = isActive ? _enabled : _disabled;
			_effect.SetActive(isActive);
		}
	}
}