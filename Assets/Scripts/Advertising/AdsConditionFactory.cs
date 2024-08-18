using Advertising;
using Data;
using Data.Player;
using Systems.Ads.Data;

namespace Systems.Ads.Conditions
{
	public class AdsConditionsFactory
	{
		public static ICondition Create(GamePlacement placement, PlayerData playerData, AdsGameSettings settings)
		{
			return placement switch
			{
				GamePlacement.InterstitialReturn => new ReturnToGameInterstitialCondition(playerData, settings),
				GamePlacement.Banner => new BannerCondition(playerData, settings),
				GamePlacement.Rewarded => new BaseRewardedCondition(playerData, settings),
				GamePlacement.InterstitialLose => new LoseInterstitialCondition(playerData, settings),
				GamePlacement.InterstitialWin => new WinInterstitialCondition(playerData, settings),
				_ => new EmptyAdsCondition()
			};
		}
	}
}