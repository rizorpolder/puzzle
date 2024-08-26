using Data;
using Field;
using Global;
using Systems;
using UnityEngine;

namespace Managers
{
	public class GameManager : MonoBehaviour
	{
		[SerializeField] private FieldController _controller;

		private void Start()
		{
			InputSystem.Instance.OnSwipe += OnSwipeAction;

			//TODO LOAD
			var fieldData = CreateFieldData();

			_controller.CreateField(fieldData);
		}

		private FieldData CreateFieldData()
		{
			//TODO repository data to field data
			var result = new FieldData();

			result.LastTextureName = "grid";
			var repositoryConfig = ConfigurableRoot.Instance.ImageRepositoryConfig;
			var config = repositoryConfig.GetConfig(result.LastTextureName);

			//Берем из сейва последнюю текстуру и размер поля (будет 3 размера)

			for (var index = 0; index < config.Sprites.Count; index++)
			{
				var configSprite = config.Sprites[index];
				result.Puzzles.Add(new PuzzleCellData()
					{cellCoords = configSprite.originalCoords, SpritePartIndex = index});
			}

			result.Puzzles[^1].SpritePartIndex = -1;
			return result;
		}

		private void OnSwipeAction(SwipeDirection direction)
		{
			_controller.SwitchCells(direction);
		}

	}
}