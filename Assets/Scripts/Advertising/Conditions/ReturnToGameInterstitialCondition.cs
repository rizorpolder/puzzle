using Data.Player;
using Systems.Ads.Data;
using Systems.LoadingSystem;
using UnityEngine;

namespace Systems.Ads.Conditions
{
	public class ReturnToGameInterstitialCondition : BaseInterstitialCondition
	{
		public ReturnToGameInterstitialCondition(PlayerData data, AdsGameSettings settings) : base(data, settings)
		{
		}

		public override bool Check()
		{
			if (!base.Check())
				return false;

			if (!SceneNames.IsCoreScene())
				return false;

			var rand = Random.Range(0f, 1f);
			return rand < Settings.ReturnToGameChanceForInterstitial;
		}
	}
}