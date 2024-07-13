using UnityEngine;

namespace Field
{
	public class PuzzleCell : MonoBehaviour
	{
		private Vector2 Coord;

		private PuzzleChip _currentChip;
		public bool IsEmpty => _currentChip is null;

		public void Initialize(int row,int column)
		{
			Coord = new Vector2(column, row);
		}
}

}