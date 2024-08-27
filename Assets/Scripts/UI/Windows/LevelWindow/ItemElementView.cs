using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows.LevelWindow
{
	public class ItemElementView : MonoBehaviour
	{
		[SerializeField] private Button _button;

		private void Start()
		{
			_button.onClick.AddListener(OnButtonClickHandler);
		}


		public void Initialize()
		{

		}

		private void OnButtonClickHandler()
		{

		}
	}
}