using Data;
using Field;
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
			// грузим из сейва

			if (LoadFieldData(out var fieldData))
				return fieldData;

			//грузим из выбранного уровня
			var result = new FieldData
			{
				LastTextureName = "grid"
			};

			return result;
		}

		private bool LoadFieldData(out FieldData fieldData)
		{
			fieldData = null;
			return false;
		}

		private void OnSwipeAction(SwipeDirection direction)
		{
			_controller.SwitchCells(direction);
		}
	}
}