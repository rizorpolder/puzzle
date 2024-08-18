using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Ads.Runtime
{
    public abstract class AdsController<TEnum>: MonoBehaviour, IAdsController<TEnum> where TEnum : Enum
    {
        private Dictionary<string, AdStatus> _placementsStatus;
        protected GamePlacements<TEnum> PlacementData;
        
        public abstract bool IsInitialized();

        public float SecondsFromLastAd { get; private set; }

        public abstract bool AdInProgress { get; }

        public void Initialize(GamePlacements<TEnum> placements, string sdkKey, string playerId, bool ageRestrictedFlag)
        {
            PlacementData = placements;
            _placementsStatus = new Dictionary<string, AdStatus>();
            Initialize(sdkKey, playerId, ageRestrictedFlag);
        }

        protected abstract void Initialize(string sdkKey, string playerId, bool ageRestrictedFlag);
        
        private void Update()
        {
            if (!IsInitialized())
                return;
            
            if (AdInProgress)
                return;
            
            SecondsFromLastAd += Time.deltaTime;
        }

        public bool TryGetStatusForPlacement(TEnum gamePlacement, out AdStatus status)
        {
            var placement = PlacementData.GetAdPlacement(gamePlacement);
            return _placementsStatus.TryGetValue(placement, out status);
        }

        protected AdStatus SetStatusForPlacement(TEnum gamePlacement, AdStatusValue statusValue)
        {
            if (TryGetStatusForPlacement(gamePlacement, out var status))
            {
                status.Set(statusValue);
                return status;
            }
            else
            {
                var placement = PlacementData.GetAdPlacement(gamePlacement);
                status = new AdStatus(statusValue);
                _placementsStatus.Add(placement, status);
                return status;
            }
        }

        protected AdStatus SetStatusForPlacement(string placement, AdStatusValue statusValue)
        {
            return SetStatusForPlacement(PlacementData.GetGamePlacement(placement), statusValue);
        }

        protected IEnumerable<KeyValuePair<TEnum, AdStatus>> GetAllPlacementsWithStatus(AdStatusValue statusValue)
        {
            return _placementsStatus.Where(x => x.Value.Value == statusValue)
                .Select(x => new KeyValuePair<TEnum, AdStatus>(PlacementData.GetGamePlacement(x.Key), x.Value));
        }
        
        public abstract AdStatus InitializeAdForPlacement(TEnum placement);
        public abstract void ShowRewardedAd(TEnum placement);
        public abstract void ShowInterstitialAd(TEnum placement);
        public abstract void PreloadRewardedAd(TEnum placement);
        public abstract void PreloadInterstitial(TEnum placement);
        public abstract bool IsRewardedAdReady(TEnum placement);
        public abstract void CreateBanner(IBannerData data);
        public abstract void ShowBanner(TEnum placement);
        public abstract void HideBanner(TEnum placement);
        public abstract void DestroyBanner(TEnum placement);

        public void ResetLastAdTimer()
        {
            SecondsFromLastAd = 0;
        }
        
        public void AddAdSuccessWatchedListener(TEnum gamePlacement, UnityAction callback)
        {
            if (TryGetStatusForPlacement(gamePlacement, out var status))
            {
                status.OnSuccessWatched.AddListener(callback);
            }
        }

        public void RemoveAdSuccessWatchedListener(TEnum gamePlacement, UnityAction callback)
        {
            if (TryGetStatusForPlacement(gamePlacement, out var status))
            {
                status.OnSuccessWatched.RemoveListener(callback);
            }
        }
    }
}
