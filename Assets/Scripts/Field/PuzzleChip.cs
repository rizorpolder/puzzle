using UnityEngine;
using UnityEngine.UI;

namespace Field
{
	public class PuzzleChip : MonoBehaviour
	{
		[SerializeField] private Image _image;

		private Vector2Int _originalCoords;
		public Vector2Int OriginalCoords => _originalCoords;

		public void SetView(Vector2Int originalCoords, Sprite sprite)
		{
			_originalCoords = originalCoords;
			_image.sprite = sprite;
		}
	}
}