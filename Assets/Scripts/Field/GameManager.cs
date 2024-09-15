using Data;
using Field;
using Global;
using Systems;
using Systems.LoadingSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
	public class GameManager : MonoBehaviour
	{
		[SerializeField] private FieldController _controller;
		[SerializeField] private Button _backButton;
		private void Start()
		{
			InputSystem.Instance.OnSwipe += OnSwipeAction;
			_backButton.onClick.AddListener(BackToMenu);
			//TODO LOAD

			var fieldData = CreateFieldData();

			_controller.CreateField(fieldData);
			SharedContainer.Instance.GlobalUI.SetActiveHud(true);
			//SharedContainer.Instance.GlobalUI.HUD.SetMode();
		}

		private void BackToMenu()
		{
			SharedContainer.Instance.LoadingController.Load(Scenes.Menu);
		}

		private FieldData CreateFieldData()
		{
			// грузим из сейва
			var runtime = SharedContainer.Instance.RuntimeData;
			if (runtime.FieldData != null)
			{
				return runtime.FieldData;
			}

			if (LoadFieldData(out var fieldData))
				return fieldData;

			//грузим из выбранного уровня
			var result = FieldData.Default;
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