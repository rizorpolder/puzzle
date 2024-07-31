using UnityEngine;

namespace Field
{
	public class PuzzleCell : MonoBehaviour
	{
		[SerializeField] private PuzzleChip prefab;

		private Vector2Int _cellCoords;
		public Vector2Int CellCoord => _cellCoords;

		private PuzzleChip _currentChip;
		public bool IsEmpty => _currentChip is null;

		public void Initialize(Vector2Int coords)
		{
			_cellCoords = coords;
		}

		public void CreateChip(Sprite sprite, Vector2Int originalCoords)
		{
			if (!_currentChip)
				_currentChip = Instantiate(prefab, transform);
			_currentChip.SetView(originalCoords, sprite);
			_cellCoords = originalCoords;
		}

		public void SetChip(PuzzleChip chip)
		{
			_currentChip = chip;
			if (IsEmpty)
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
			if (_currentChip)
				DestroyImmediate(_currentChip.gameObject);
			_currentChip = null;
		}

		public bool IsCorrectChip()
		{
			return _currentChip.OriginalCoords.Equals(CellCoord);
		}
	}
}