using Data;
using Data.Player;
using Systems.Ads.Data;

namespace Systems.Ads.Conditions
{
    public class WinInterstitialCondition : BaseInterstitialCondition
    {
        public WinInterstitialCondition(PlayerData data, AdsGameSettings settings) : base(data, settings)
        {
        }

        public override bool Check()
        {
            return base.Check() && CheckAdditionalCondition();
        }
        
        protected override bool CheckLevelConditions()
        {
            if (IsClassic)
                return base.CheckLevelConditions();
            
            if(PlayerData.CurrentLevel - 1 < Settings.AdventureLevelForStart.Interstitial)
                return false;

            return true;
        }
        
        private bool CheckAdditionalCondition()
        {
            var chance = IsClassic
                ? Settings.ClassicChanceForInterstitial
                : Settings.AdventureWinChanceForInterstitial;
            

            var rand = UnityEngine.Random.Range(0f, 1f);
            return rand < chance;
        }
    }
}