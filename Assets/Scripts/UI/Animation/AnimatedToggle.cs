using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Common.Animated
{
	[RequireComponent(typeof(Toggle))]
	public class AnimatedToggle : MonoBehaviour
	{
		[SerializeField] private Toggle _toggle;
		[SerializeField] private RectTransform handle;
		[SerializeField] private float inset;
		[SerializeField] private bool useAnimation = true;
		[SerializeField] private float animationTime = 0.1f;

		[SerializeField] private Image[] coloredImages;
		[SerializeField] private Color isOnColor;
		[SerializeField] private Color isOffColor;

		private void Start()
		{
			if (_toggle is null)
				_toggle = GetComponent<Toggle>();
			Animate(_toggle.isOn);
			_toggle.onValueChanged.AddListener(Animate);
		}

		private void OnDestroy()
		{
			handle.DOKill();
		}

		private void Animate(bool isOn)
		{
			if (useAnimation)
			{
				var anchorVector = isOn ? new Vector2(1, 0.5f) : new Vector2(0, 0.5f);
				var anchorPosX = isOn
					? -1 * Math.Abs(handle.anchoredPosition.x)
					: Math.Abs(handle.anchoredPosition.x);

				handle.DOAnchorMin(anchorVector, animationTime).SetEase(Ease.OutCirc);
				handle.DOAnchorMax(anchorVector, animationTime).SetEase(Ease.OutCirc);
				handle.DOAnchorPosX(anchorPosX, animationTime).SetEase(Ease.OutCirc);
				foreach (var coloredImage in coloredImages)
					coloredImage.DOColor(isOn ? isOnColor : isOffColor, animationTime);
			}
			else
			{
				handle.SetInsetAndSizeFromParentEdge(isOn ? RectTransform.Edge.Right : RectTransform.Edge.Left,
					inset,
					handle.rect.width);
			}
		}
	}
}