using UnityEngine;
using UnityEngine.UI;

namespace Field
{
	public class PuzzleChip : MonoBehaviour
	{
		[SerializeField] private Image _image;
		private Vector2 _coord;

		public void Initialize(Vector2 coord, Sprite sprite)
		{
			_image.sprite = sprite;
			_coord = coord;
		}
	}
}
