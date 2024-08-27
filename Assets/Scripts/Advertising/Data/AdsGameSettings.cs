using System;
using UnityEngine;

namespace Systems.Ads.Data
{
	[Serializable]
	public class AdsGameSettings
	{
		[Header("Global")]
		[Tooltip("Time from application launch to banner launch.")]
		public float TimeToStartBannerAdv;

		[Tooltip("Time from first application launch to interstitial launch.")]
		public float TimeFromFirstStartInterstitial;

		public int MaxAmountRewardedAdvInDay;
		public float InterstitialTimeout;

		[Range(0f, 1f)]
		public float ReturnToGameChanceForInterstitial;

		[Header("Classic")]
		public AdsLevelConditionData ClassicAttemptForStart;

		[Range(0f, 1f)]
		public float ClassicProgressForRewarded;

		[Range(0f, 1f)]
		public float ClassicChanceForInterstitial;

		[Header("Adventure")]
		public AdsLevelConditionData AdventureLevelForStart;

		[Range(0f, 1f)]
		public float AdventureProgressForRewarded;

		[Range(0f, 1f)]
		public float AdventureWinChanceForInterstitial;

		[Range(0f, 1f)]
		public float AdventureLoseChanceForInterstitial;
	}
}