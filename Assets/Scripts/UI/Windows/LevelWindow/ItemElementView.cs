using Configs.TextureRepository;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows.LevelWindow
{
	public class ItemElementView : MonoBehaviour
	{
		[SerializeField] private Button _button;
		[SerializeField] private TextMeshProUGUI count;
		[SerializeField] private Image[] stars; //ToDO to script for enable/initialize
 		private void Start()
		{
			_button.onClick.AddListener(OnButtonClickHandler);
		}


		public void Initialize(TextureUnitConfig textureUnitConfig)
		{

		}

		private void OnButtonClickHandler()
		{
			//Show LevelInfoWindow (with current data)
		}
	}
}