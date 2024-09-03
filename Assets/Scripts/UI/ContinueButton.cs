using Global;
using Systems.LoadingSystem;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class ContinueButton : MonoBehaviour
	{
		[SerializeField] private Button _button;

		private void Start()
		{
			_button.onClick.AddListener(OnContinueButtonClick);

			var haveSavedGame = SharedContainer.Instance.RuntimeData.FieldData.HaveActualSaveData;
			gameObject.SetActive(haveSavedGame);
		}

		private void OnContinueButtonClick()
		{
			SharedContainer.Instance.LoadingController.Load(Scenes.Core);
		}
	}
}