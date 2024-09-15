using System;
using Configs;
using Configs.TextureRepository;
using Data;
using Extensions;
using Global;
using Systems;
using UI.Windows;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Field
{
	public class FieldController : MonoBehaviour
	{
		[SerializeField] private PuzzleCell prefab;
		[SerializeField] private Transform fieldRoot;
		[SerializeField] private Vector3[] _fieldScale;
		private TextureUnitConfig _config;
		private PuzzleCell _emptyCell;
		private FieldData _fieldData;

		private PuzzleCell[,] field;

		public void CreateField(FieldData fieldData)
		{
			_fieldData = fieldData;

			var repositoryConfig = ConfigurableRoot.Instance.ImageRepositoryConfig;
			_config = repositoryConfig.GetConfig(fieldData.TextureData.Category, fieldData.TextureData.TextureName);
			GenerateField();
			//Shuffle();
			UpdateFieldScale();
		}

		private void GenerateField()
		{
			var fieldSize = _fieldData.FieldDifficult.FieldSize;
			field = new PuzzleCell[fieldSize.x, fieldSize.y];

			for (var row = 0; row < fieldSize.x; row++)
			for (var column = 0; column < fieldSize.y; column++)
			{
				var cellCoords = new Vector2Int(column, row);
				var cell = field[cellCoords.x, cellCoords.y];
				if (!cell)
					cell = AddCell(cellCoords);

				ChipToCell(cell);
			}

			//TODO подумать над формированием и сохранением "пустой клетки"
			_emptyCell = field[fieldSize.x - 1, 0];
			_emptyCell.Clear();
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
			var newCell = Instantiate(prefab, fieldRoot);
			return newCell;
		}

		private void ChipToCell(PuzzleCell puzzleCell)
		{
			var cellCoords = puzzleCell.CellCoord;

			//calculating sprite data
			var fieldSize = _fieldData.FieldDifficult.FieldSize;

			var spriteTexture = _config.Sprite.texture;

			var spriteSize = new Vector2Int(spriteTexture.height / fieldSize.x, spriteTexture.width / fieldSize.y);
			var pivot = new Vector2(0f, .5f);

			var rect = new Rect(cellCoords.x * spriteSize.x, cellCoords.y * spriteSize.y, spriteSize.x, spriteSize.y);
			var sprite = spriteTexture.CreateSprite(rect, pivot);
			puzzleCell.CreateChip(sprite, cellCoords);
		}

		private void Shuffle()
		{
			var iterations = 10;
			int shuffles = 0;
			for (var i = 0; i < iterations; i++)
			{
				var cell1 = _emptyCell;

				var dice = Random.Range(0, 4);
				var direction = (SwipeDirection) dice;

				if (!CanMove(direction, out var coords))
					continue;

				var cell2 = field[coords.x, coords.y];

				SwapCellData(cell1, cell2);
				Debug.Log(direction);
			}

			Debug.Log(shuffles);
		}

		public void SwitchCells(SwipeDirection direction)
		{
			if (!CanMove(direction, out var newCoords)) return;

			var puzzleCell = field[newCoords.x, newCoords.y];
			SwapCellData(_emptyCell, puzzleCell);
			if (CheckForCompletion())
			{
				SharedContainer.Instance.WindowsController.Show(WindowType.WinWindow,
					window =>
					{
						if (window is WinWindow winWindow)
						{
							winWindow.Initialize();
						}
					} );
			}
		}

		private void SwapCellData(PuzzleCell from, PuzzleCell to)
		{
			var fromChip = from.GetChip();
			var toChip = to.GetChip();
			from.SetChip(toChip);
			to.SetChip(fromChip);
			_emptyCell = from.IsEmpty ? from : to.IsEmpty ? to : _emptyCell;
		}

		private bool CheckForCompletion()
		{
			foreach (var puzzleCell in field)
			{
				var chip = puzzleCell.GetChip();
				if (!chip)
					continue;

				if (!puzzleCell.CellCoord.Equals(chip.OriginalCoords))
					return false;
			}

			return true;
		}

		private bool CanMove(SwipeDirection direction, out Vector2Int newCoords)
		{
			var canMove = false;
			newCoords = _emptyCell.CellCoord;
			switch (direction)
			{
				case SwipeDirection.Up:
					newCoords.y--;
					canMove = newCoords.y >= 0;
					break;

				case SwipeDirection.Down:
					newCoords.y++;
					canMove = newCoords.y < _fieldData.FieldDifficult.FieldSize.y;
					break;

				case SwipeDirection.Left:
					newCoords.x++;
					canMove = newCoords.x < _fieldData.FieldDifficult.FieldSize.x;
					break;

				case SwipeDirection.Right:
					newCoords.x--;
					canMove = newCoords.x >= 0;
					break;
			}

			return canMove;
		}

		private void UpdateFieldScale()
		{
			//TODO Refactor this
			switch (_fieldData.FieldDifficult.DifficultValue)
			{
				case GameDifficult.Low:
					fieldRoot.localScale = _fieldScale[0];

					break;
				case GameDifficult.Mid:
					fieldRoot.localScale = _fieldScale[1];
					break;
				case GameDifficult.Hard:
					fieldRoot.localScale = _fieldScale[2];
					break;
			}
		}
	}
}