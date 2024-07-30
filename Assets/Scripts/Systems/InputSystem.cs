using UnityEngine;

namespace Systems
{
	public class InputSystem : MonoBehaviour
	{
		public System.Action<SwipeDirection> OnSwipe = direction => { };

		[SerializeField, Range(0f, 1f)] private Vector3 deadZone = new Vector3(0.1f, 0.1f, 0.1f);

		private Vector3 _endPosition;
		private Vector3 _startPosition;

		public Camera Camera => Camera.main;

		public void Update()
		{
			//TODO сделать мертвую зону = клик (select by click),

			if (Application.isMobilePlatform)
			{
				TouchInput();
			}
			else
			{
				MouseInput();
			}
		}

		private void TouchInput()
		{
			if (Input.touchCount <= 0)
				return;

			var lastTouchIndex = Input.touchCount - 1;

			var touch = Input.GetTouch(lastTouchIndex);
			var touchPosition = touch.position;

			switch (touch.phase)
			{
				case TouchPhase.Began:
					TouchDown(touchPosition);
					break;
				case TouchPhase.Ended:
					TouchUp(touchPosition);
					break;
			}
		}

		private void MouseInput()
		{
			if (!Input.GetMouseButton(0) && !Input.GetKeyUp(KeyCode.Mouse0))
				return;

			var touchPosition = Input.mousePosition;

			if (Input.GetKeyDown(KeyCode.Mouse0))
				TouchDown(touchPosition);

			if (Input.GetKeyUp(KeyCode.Mouse0))
				TouchUp(touchPosition);
		}

		private void TouchDown(Vector3 touchPosition)
		{
			_startPosition = touchPosition;
		}

		private void TouchUp(Vector3 touchPosition)
		{
			_endPosition = touchPosition;
			var direction = GetSwipeDirection();
			OnSwipe?.Invoke(direction);
		}

		private SwipeDirection GetSwipeDirection()
		{
			var delta = Camera.ScreenToWorldPoint(_endPosition) - Camera.ScreenToWorldPoint(_startPosition);
			var normalized = delta.normalized;

			var absX = Mathf.Abs(normalized.x);
			var absY = Mathf.Abs(normalized.y);
			if (absX > absY)
			{
				return normalized.x > 0 ? SwipeDirection.Right : SwipeDirection.Left;
			}

			return normalized.y > 0 ? SwipeDirection.Up : SwipeDirection.Down;
		}
	}

	public enum SwipeDirection
	{
		Up,
		Down,
		Left,
		Right,
	}
}