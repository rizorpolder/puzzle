using System;
using UnityEngine;

namespace Field
{
	public class PuzzleCell : MonoBehaviour
	{
		[SerializeField] private PuzzleChip prefab;

		private Vector2 _spriteCoords;
		public Vector2 SpriteCoord => _spriteCoords;


		private PuzzleChip _currentChip;
		public bool IsEmpty => _currentChip is null;

		public void Initialize(Vector2 coords)
		{
			_spriteCoords = coords;
		}

		public void CreateChip(Sprite sprite, Vector2Int originalCoords)
		{
			if (!_currentChip)
				_currentChip = Instantiate(prefab, transform);
			_currentChip.SetView(sprite);
			_spriteCoords = originalCoords;
		}

		public void SetChip(PuzzleChip chip)
		{
			_currentChip = chip;
			if(IsEmpty)
				return;
			chip.transform.SetParent(this.transform);
			_currentChip.transform.localPosition = Vector3.zero;
		}

		public PuzzleChip GetChip()
		{
			return _currentChip;
		}

		public void Clear()
		{
			if(_currentChip)
				DestroyImmediate(_currentChip.gameObject);
			_currentChip = null;
		}
}

}