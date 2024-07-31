using UnityEngine;
using UnityEngine.UI;

namespace Field
{
	public class PuzzleChip : MonoBehaviour
	{
		[SerializeField] private Image _image;

		public void SetView(Sprite sprite)
		{
			_image.sprite = sprite;
		}
	}
}
