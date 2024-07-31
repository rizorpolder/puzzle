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
			var sprites = _config.Sprites;
			foreach (var cellData in _fieldData.Puzzles)
			{
				var spriteIndex = cellData.SpritePartIndex;
				var sprite = sprites[spriteIndex].sprite;
				field[cellData.cellCoords.x, cellData.cellCoords.y].CreateChip(sprite);
			}
		}

		private PuzzleCell CreateCell()
		{
			var newCell = Instantiate(prefab, this.fieldRoot);
			return newCell;
		}

		private void Shuffle()
		{
		}

		public void SwitchCells(SwipeDirection direction)
		{
		}
	}
}