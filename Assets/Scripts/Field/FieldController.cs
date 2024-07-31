using Configs.TextureRepository;
using Data;
using Global;
using Systems;
using UnityEngine;

namespace Field
{
	public class FieldController : MonoBehaviour
	{
		[SerializeField] private PuzzleCell prefab;
		[SerializeField] private Transform fieldRoot;

		private PuzzleCell[,] field;
		private TextureUnitConfig _config;
		private FieldData _fieldData;
		private PuzzleCell _emptyCell;

		public void CreateField(FieldData fieldData)
		{
			_fieldData = fieldData;

			var repositoryConfig = ConfigurableRoot.Instance.ImageRepositoryConfig;
			_config = repositoryConfig.GetConfig(_fieldData.LastTextureName);

			GenerateField();
			InitializeField();
			Shuffle();
		}

		private void GenerateField()
		{
			var fieldSize = _config.FieldSize;
			field = new PuzzleCell[fieldSize.x, fieldSize.y];

			var cellDatas = _fieldData.Puzzles;

			foreach (var puzzleCellData in cellDatas)
			{
				var cellCoords = puzzleCellData.cellCoords;

				var cell = field[cellCoords.x, cellCoords.y];
				if (!cell)
				{
					cell = CreateCell();
					cell.Initialize(cellCoords);
					cell.name = $"{cellCoords.x}x{cellCoords.y}_cell";
					field[cellCoords.x, cellCoords.y] = cell;
				}
			}
		}

		private void InitializeField()
		{
			var textureUnits = _config.Sprites;
			foreach (var cellData in _fieldData.Puzzles)
			{
				var spriteIndex = cellData.SpritePartIndex;
				var textureUnitData = textureUnits[spriteIndex];
				var cellCoords = cellData.cellCoords;
				field[cellCoords.x, cellCoords.y].CreateChip(textureUnitData.sprite, textureUnitData.originalCoords);
			}
		}

		private PuzzleCell CreateCell()
		{
			var newCell = Instantiate(prefab, this.fieldRoot);
			return newCell;
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
			var x = Random.Range(0, _config.FieldSize.x);
			var y = Random.Range(0, _config.FieldSize.y);
			return field[x, y];
		}

		public void SwitchCells(SwipeDirection direction)
		{
		}

		private void SwapCellData(PuzzleCell from, PuzzleCell to)
		{
			var fromChip = from.GetChip();
			var toChip = to.GetChip();
			from.SetChip(toChip);
			to.SetChip(fromChip);
		}
	}
}