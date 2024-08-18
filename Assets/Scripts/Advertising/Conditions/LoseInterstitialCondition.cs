using System;
using Data.Player;
using Systems.Ads.Data;

namespace Systems.Ads.Conditions
{
    public class LoseInterstitialCondition : BaseInterstitialCondition
    {
        public LoseInterstitialCondition(PlayerData data, AdsGameSettings settings) : base(data, settings)
        {
        }

        public override bool Check()
        {
            return base.Check() && CheckAdditionalCondition();
        }

        private bool CheckAdditionalCondition()
        {
            var chance = IsClassic
                ? Settings.ClassicChanceForInterstitial
                : Settings.AdventureLoseChanceForInterstitial;
            

            var rand = UnityEngine.Random.Range(0f, 1f);
            return rand < chance;
        }
    }
}