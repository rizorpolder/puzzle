using UnityEngine;
using UnityEngine.UI;

namespace Field
{
	public class PuzzleChip : MonoBehaviour
	{
		[SerializeField] private Image _image;

		public Vector2Int OriginalCoords { get; private set; }

		public void SetView(Vector2Int originalCoords, Sprite sprite)
		{
			OriginalCoords = originalCoords;
			_image.sprite = sprite;
		}
	}
}