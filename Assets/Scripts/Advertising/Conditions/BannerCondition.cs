using System;
using Data;
using Data.Player;
using Systems.Ads.Data;

namespace Systems.Ads.Conditions
{
    public class BannerCondition : BaseAdsCondition
    {
        public BannerCondition(PlayerData data, AdsGameSettings settings) : base(data, settings)
        {
        }

        public override bool Check()
        {
            var startTime = new DateTime(PlayerData.SessionData.CurrentSessionDate)
                .AddMinutes(Settings.TimeToStartBannerAdv);

            if (DateTime.Now < startTime)
                return false;

            if (IsClassic)
            {
                return PlayerData.GameData.ClassicAttempt >= Settings.ClassicAttemptForStart.Banner;
            }
            else
            {
                return PlayerData.CurrentLevel >= Settings.AdventureLevelForStart.Banner;
            }
        }
    }
}