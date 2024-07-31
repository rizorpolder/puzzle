using System;
using System.Collections.Generic;
using Extensions;
using UniRx;
using UnityEngine;
using UnityEngine.Playables;

namespace Common.Animation
{
	[RequireComponent(typeof(Animator), typeof(PlayableDirector))]
	public class PlayableAnimation : BaseAnimation
	{
		[SerializeField] private PlayableDirector Director;
		[SerializeField] private PlayableAsset ShowPlayable;
		[SerializeField] private PlayableAsset HidePlayable;
		private List<IDisposable> _subscriptions = new List<IDisposable>();

		public override IObservable<Unit> Show(PostAnimationAction action = null)
		{
			var observable = Director.PlayAsObservable(ShowPlayable);
			var subs = observable.Subscribe(_ => { }, e => { }, () => { action?.Invoke(); });

			_subscriptions.Add(subs);

			return observable;
		}

		public override IObservable<Unit> Hide(PostAnimationAction action = null)
		{
			var observable = Director.PlayAsObservable(HidePlayable);

			var subs = observable.Subscribe(_ => { }, e => { }, () => { action?.Invoke(); });

			_subscriptions.Add(subs);

			return observable;
		}

		private void Reset()
		{
			if (Director == null)
				Director = GetComponent<PlayableDirector>();
		}

		public override void OnStart()
		{
		}

		public override IObservable<Unit> Play(string name, PostAnimationAction action = null)
		{
			return Observable.Empty<Unit>();
		}
	}
}