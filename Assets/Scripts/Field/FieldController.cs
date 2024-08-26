using Configs.TextureRepository;
using Data;
using Global;
using Systems;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Field
{
	public class FieldController : MonoBehaviour
	{
		[SerializeField] private PuzzleCell prefab;
		[SerializeField] private Transform fieldRoot;

		private PuzzleCell[,] field;
		private ImageRepositoryConfig.TextureUnitConfig _config;
		private FieldData _fieldData;
		private PuzzleCell _emptyCell;

		public void CreateField(FieldData fieldData)
		{
			_fieldData = fieldData;

			var repositoryConfig = ConfigurableRoot.Instance.ImageRepositoryConfig;
			_config = repositoryConfig.GetConfig(_fieldData.LastTextureName);

			GenerateField();
			// Shuffle();
		}

		private void GenerateField()
		{
			var fieldSize = _fieldData.fieldSize;
			field = new PuzzleCell[fieldSize.x, fieldSize.y];

			for (var i = 0; i < fieldSize.x; i++)
			{
				for (int j = 0; j < fieldSize.y; j++)
				{
					var cellCoords = new Vector2Int(j, i);
					var cell = field[cellCoords.x, cellCoords.y];
					if (!cell)
					{
						cell = AddCell(cellCoords);
					}

					ChipToCell(cell);
				}
			}
		}

		private PuzzleCell AddCell(Vector2Int cellCoords)
		{
			var cell = CreateCell();
			cell.Initialize(cellCoords);
			cell.name = $"{cellCoords.x}x{cellCoords.y}_cell";
			field[cellCoords.x, cellCoords.y] = cell;
			return cell;
		}

		private PuzzleCell CreateCell()
		{
			var newCell = Instantiate(prefab, this.fieldRoot);
			return newCell;
		}

		private void ChipToCell(PuzzleCell puzzleCell)
		{
			var cellCoords = puzzleCell.CellCoord;

			//calculating sprite data
			var fieldSize = _fieldData.fieldSize;
			Vector2Int spriteSize =
				new Vector2Int(_config.Texture.height / fieldSize.x, _config.Texture.width / fieldSize.y);
			var pivot = new Vector2(.5f, .5f);

			Rect rect = new Rect(cellCoords.x * spriteSize.x, cellCoords.y * spriteSize.y, spriteSize.x, spriteSize.y);
			var sprite = Sprite.Create(_config.Texture, rect, pivot);
			puzzleCell.CreateChip(sprite, cellCoords);
		}

		private void Shuffle()
		{
			int iterations = 100;
			for (int i = 0; i < iterations; i++)
			{
				var cell1 = GetRandomCell();
				var cell2 = GetRandomCell();
				SwapCellData(cell1, cell2);
			}
		}

		private PuzzleCell GetRandomCell()
		{
			var x = Random.Range(0, _fieldData.fieldSize.x);
			var y = Random.Range(0, _fieldData.fieldSize.y);
			return field[x, y];
		}

		public void SwitchCells(SwipeDirection direction)
		{
			if (!CanMove(direction, out var newCoords)) return;

			var puzzleCell = field[newCoords.x, newCoords.y];
			SwapCellData(_emptyCell, puzzleCell);
			if (CheckForCompletion())
			{
				Debug.LogAssertionFormat("YOU WIN");
			}
		}

		private void SwapCellData(PuzzleCell from, PuzzleCell to)
		{
			var fromChip = from.GetChip();
			var toChip = to.GetChip();
			from.SetChip(toChip);
			to.SetChip(fromChip);
			_emptyCell = from.IsEmpty ? from : (to.IsEmpty ? to : _emptyCell);
		}

		private bool CheckForCompletion()
		{
			foreach (var puzzleCell in field)
			{
				var chip = puzzleCell.GetChip();
				if (!puzzleCell.CellCoord.Equals(chip.OriginalCoords)) return false;
			}

			return true;
		}

		private bool CanMove(SwipeDirection direction, out Vector2Int newCoords)
		{
			bool canMove = false;
			newCoords = _emptyCell.CellCoord;

			switch (direction)
			{
				case SwipeDirection.Up:
					newCoords.x++;
					canMove = newCoords.x < -_fieldData.fieldSize.x;
					break;
				case SwipeDirection.Down:
					newCoords.x--;
					canMove = newCoords.x >= 0;
					break;
				case SwipeDirection.Left:
					newCoords.y++;
					canMove = newCoords.y < _fieldData.fieldSize.y;
					break;
				case SwipeDirection.Right:
					newCoords.y--;
					canMove = newCoords.y >= 0;
					break;
			}

			return canMove;
		}
	}
}