using Data.Player;
using Global;
using Systems.Ads.Data;

namespace Systems.Ads.Conditions
{
	public abstract class BaseAdsCondition : ICondition
	{
		protected readonly PlayerData PlayerData;
		protected readonly AdsGameSettings Settings;

		protected BaseAdsCondition(PlayerData data, AdsGameSettings settings)
		{
			PlayerData = data;
			Settings = settings;
		}

		public abstract bool Check();

		protected bool IsClassic => SharedContainer.Instance.RuntimeData.CurrentGameType == GameType.Classic;
	}
}