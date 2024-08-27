using Common.Animation;

namespace UI.Common.Animated
{
	public class EmptyAnimation : BaseAnimation
	{
		public override void OnStart()
		{
		}

		public override void Show(PostAnimationAction action = null)
		{
			action?.Invoke();
		}

		public override void Hide(PostAnimationAction action = null)
		{
			action?.Invoke();
		}

		public override void Play(string name, PostAnimationAction action = null)
		{
			action?.Invoke();
		}
	}
}