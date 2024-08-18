namespace Systems.Ads.Conditions
{
    public class EmptyAdsCondition : ICondition
    {
        public bool Check()
        {
            return false;
        }
    }
}