using Data.Player;
using Systems.Ads.Data;

namespace Systems.Ads.Conditions
{
	public class BaseRewardedCondition : BaseAdsCondition
	{
		public BaseRewardedCondition(PlayerData data, AdsGameSettings settings) : base(data, settings)
		{
		}

		public override bool Check()
		{
			if (Settings.MaxAmountRewardedAdvInDay <= PlayerData.SessionData.RewardedAdsWatchedToday)
				return false;

			return true;
		}
	}
}