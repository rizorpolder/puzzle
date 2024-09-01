using Ads.Runtime;
using Advertising;
using Global;
using Systems.Ads.Data;
using UnityEngine;

namespace Systems.Ads.Conditions
{
	public class AdsConditionsController : MonoBehaviour
	{
		[SerializeField] private AdsConfig _config;
		[SerializeField] private AdsController<GamePlacement> _ads;

		private AdsConditionsFactory _conditionsFactory;

		private void Awake()
		{
			_conditionsFactory = new AdsConditionsFactory();
		}

		public bool CanStart(GamePlacement placement)
		{
			if (!_ads.IsInitialized())
				return false;

			if (_ads.AdInProgress)
				return false;

			// TODO remove singleton
			if (!SharedContainer.Instance
			    || SharedContainer.Instance.RuntimeData is null
			    || SharedContainer.Instance.RuntimeData.PlayerData is null)
				return false;

			var data = SharedContainer.Instance.RuntimeData.PlayerData;
			return AdsConditionsFactory.Create(placement, data, _config.Settings).Check();
		}

		public float GetProgressForRewarded()
		{
			return _config.Settings.AdventureProgressForRewarded;
		}
	}
}