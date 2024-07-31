using System;
using UnityEngine;

namespace Field
{
	public class PuzzleCell : MonoBehaviour
	{
		[SerializeField] private PuzzleChip prefab;

		private Vector2 _coords;
		public Vector2 Coord => _coords;


		private PuzzleChip _currentChip;
		public bool IsEmpty => _currentChip is null;

		public void Initialize(Vector2 coords)
		{
			_coords = coords;
		}

		public void CreateChip(Sprite sprite)
		{
			if (!_currentChip)
				_currentChip = Instantiate(prefab, transform);
			_currentChip.SetView(sprite);
		}

		public void SetChip(PuzzleChip chip)
		{
			_currentChip = chip;
		}

		public void Clear()
		{
			if(_currentChip)
				DestroyImmediate(_currentChip.gameObject);
			_currentChip = null;
		}
}

}