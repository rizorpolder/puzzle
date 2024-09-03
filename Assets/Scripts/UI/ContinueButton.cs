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

			//Check - have saved data?
		}

		private void OnContinueButtonClick()
		{
			//TODO
			//Set saved data to runtime
			SharedContainer.Instance.LoadingController.Load(Scenes.Core);
		}
	}
}