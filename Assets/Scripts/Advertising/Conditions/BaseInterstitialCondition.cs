using System;
using Data.Player;
using Systems.Ads.Data;

namespace Systems.Ads.Conditions
{
	public class BaseInterstitialCondition : BaseAdsCondition
	{
		protected BaseInterstitialCondition(PlayerData data, AdsGameSettings settings) : base(data, settings)
		{
		}

		public override bool Check()
		{
			if (!CheckLevelConditions())
				return false;

			return CheckTimeFromFirstLaunch() && CheckLastAdsTimeout();
		}

		protected virtual bool CheckLevelConditions()
		{
			if (IsClassic && PlayerData.GameData.ClassicAttempt < Settings.ClassicAttemptForStart.Interstitial)
				return false;

			if (!IsClassic && PlayerData.CurrentLevel < Settings.AdventureLevelForStart.Interstitial)
				return false;

			return true;
		}

		private bool CheckTimeFromFirstLaunch()
		{
			var timeToStart = new DateTime(PlayerData.SessionData.FirstLaunchDate)
				.AddMinutes(Settings.TimeFromFirstStartInterstitial);

			return DateTime.Now >= timeToStart;
		}

		private bool CheckLastAdsTimeout()
		{
			var nextAdsTime = new DateTime(PlayerData.SessionData.LastAdsWatchedDate)
				.AddMinutes(Settings.InterstitialTimeout);

			return DateTime.Now >= nextAdsTime;
		}
	}
}