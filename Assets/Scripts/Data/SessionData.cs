using System;

namespace Data
{
	[Serializable]
	public class SessionData
	{
		public static string DataKey = "Session_Data";
		public long FirstLaunchDate;
		public long CurrentSessionDate;
		public long LastSessionDate;
		public int RewardedAdsWatchedToday;
		public long LastAdsWatchedDate;
		public long LevelStartedDate;
		public bool RateUsShowed;

		public void TrackSession()
		{
			if (FirstLaunchDate == 0)
				FirstLaunchDate = DateTime.Now.Ticks;

			LastSessionDate = CurrentSessionDate == 0 ? DateTime.Now.Ticks : CurrentSessionDate;
			CurrentSessionDate = DateTime.Now.Ticks;

			var lastDate = new DateTime(LastSessionDate).Date;
			if (lastDate != DateTime.Today.Date)
				RewardedAdsWatchedToday = 0;
		}

		public void TrackRewarded()
		{
			++RewardedAdsWatchedToday;
		}

		public void TrackAdsWatched()
		{
			LastAdsWatchedDate = DateTime.Now.Ticks;
		}

		public void TrackStartLevel()
		{
			LevelStartedDate = DateTime.Now.Ticks;
		}
	}
}