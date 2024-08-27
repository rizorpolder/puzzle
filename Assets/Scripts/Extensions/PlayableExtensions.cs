using System;
using UniRx;
using UnityEngine.Playables;

namespace Extensions
{
	public static class PlayableExtensions
	{
		public static IObservable<Unit> AsObservable(this PlayableDirector director)
		{
			return PlayAsObservable(director, director.playableAsset);
		}

		public static IObservable<Unit> PlayAsObservable(this PlayableDirector director, PlayableAsset playableAsset)
		{
			if (director == null) return Observable.Empty<Unit>();

			if (playableAsset == null) return Observable.Empty<Unit>();

			director.time = 0;
			director.initialTime = 0;

			return Observable.Create<Unit>
			(
				observer =>
				{
					Action<PlayableDirector> stopped = playableDirector => { observer.OnCompleted(); };
					director.stopped += stopped;

					var subscription = Observable.EveryLateUpdate()
						.SkipWhile(_ => director && director.time < director.duration).FirstOrDefault()
						.Subscribe(_ => { },
							() =>
							{
								if (director)
									director.Stop();
								observer.OnCompleted();
							});
					director.Play(playableAsset, DirectorWrapMode.Hold /*.None*/);
					director.Evaluate();
					return Disposable.Create
					(
						() =>
						{
							director.stopped -= stopped;
							subscription.Dispose();
						});
				}
			).TakeUntilDestroy(director);
		}
	}
}