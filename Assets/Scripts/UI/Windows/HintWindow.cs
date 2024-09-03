using System;
using Extensions;
using Global;
using UI.Common;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows
{
	public class HintWindow : BaseWindow
	{
		[SerializeField] private Image _image;

		public override void Show(Action callback = null)
		{
			//todo перед показом показывать интер

			base.Show(callback);
		}

		protected override void OnShowAction()
		{
			base.OnShowAction();
			var _config = SharedContainer.Instance.ConfigurableRoot.ImageRepositoryConfig;
			var fieldData = SharedContainer.Instance.RuntimeData.FieldData;

			var textureUnit = _config.GetConfig(fieldData.TextureData.Category, fieldData.TextureData.TextureName);
			_image.sprite = textureUnit.Texture.CreateSprite();

		}

	}
}
