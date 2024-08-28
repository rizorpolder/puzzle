using UI.Common;
using UnityEngine;

namespace UI.Buttons
{
	public class CloseButton : BaseButton
	{
		[SerializeField] private BaseWindow window;

		protected override void OnClickAction()
		{
			if (!window || window.Status != ElementStatus.Shown) return;

			base.OnClickAction();
			window.Close();
		}

		protected override void OnValidate()
		{
			base.OnValidate();
			window = gameObject.GetComponentInParent<BaseWindow>(true);
		}
	}
}