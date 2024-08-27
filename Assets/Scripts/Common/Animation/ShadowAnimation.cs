using Common.Animation;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Common.Animated
{
	[RequireComponent(typeof(Image))]
	public class ShadowAnimation : BaseAnimation
	{
		[Header("Shadow parameters:")]
		[SerializeField] private Color _activeColor;

		[SerializeField] private Color _noActiveColor;

		[SerializeField] private float _timeToShow;
		[SerializeField] private float _timeToHide;
		[SerializeField] private float _timeDelayShow;
		[SerializeField] private float _timeDelayHide;

		[SerializeField] private AnimationCurve curve;
		[SerializeField] private Image image;
		[SerializeField] private GraphicRaycaster _raycaster;
		[SerializeField] private Canvas shadowCanvas;
		private Color _currentColor;

		private void Reset()
		{
			if (image == null)
				image = GetComponent<Image>();
		}

		public void ResetColor()
		{
			_currentColor = _activeColor;
		}

		public void SetActiveColor(Color color)
		{
			_currentColor = color;
		}

		public override void Show(PostAnimationAction action = null)
		{
			gameObject.SetActive(true);
			image.DOKill();
			image.DOColor(_currentColor, _timeToShow).SetEase(curve).SetDelay(_timeDelayShow).Play().OnComplete(() =>
			{
				action?.Invoke();
			});
		}

		public override void Hide(PostAnimationAction action = null)
		{
			if (!gameObject.activeSelf)
			{
				ResetColor();
				return;
			}

			image.DOKill();
			image.DOColor(_noActiveColor, _timeToHide).SetEase(curve).SetDelay(_timeDelayHide).Play().OnComplete(() =>
			{
				action?.Invoke();
				gameObject.SetActive(false);
				ResetColor();
			});
		}

		public override void Play(string name, PostAnimationAction action = null)
		{
#if UNITY_EDITOR
			Debug.LogWarningFormat("ShadowAnimation has not implemented method Play");
#endif
		}

		public override void OnStart()
		{
		}

		public void ChangeCanvasState(bool isEnabled)
		{
			// hack override not changed when object is disabled
			var isHidden = !gameObject.activeSelf;
			if (isHidden)
				gameObject.SetActive(true);

			shadowCanvas.overrideSorting = isEnabled;

			if (isHidden)
				gameObject.SetActive(false);

			if (!isEnabled)
				return;

			shadowCanvas.sortingOrder = -1;
			shadowCanvas.sortingLayerName = "UI";
		}
	}
}