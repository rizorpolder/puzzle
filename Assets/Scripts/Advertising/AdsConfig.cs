using System.Collections.Generic;
using System.Linq;
using Advertising;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace Systems.Ads.Data
{
	[CreateAssetMenu(fileName = "AdsConfig", menuName = "Project/Configs/AdsConfig", order = 0)]
	public class AdsConfig : ScriptableObject
	{
		[field: SerializeField] public string SDKKey { get; private set; }
		[field: SerializeField] public string DefaultUnit { get; private set; }
		[field: SerializeField] public List<PlacementData> PlacementData { get; private set; }
		[field: SerializeField] public Color BannerColor { get; private set; }

#if UNITY_ANDROID
        [SerializeField] private AdsGameSettings _androidSettings;
#endif
#if UNITY_IOS
        [SerializeField] private AdsGameSettings _iOSSettings;
#endif
#if UNITY_WEBGL
		[SerializeField] private AdsGameSettings _webGLSettings;
#endif
		public AdsGameSettings Settings
		{
			get
			{
#if UNITY_WEBGL
				return _webGLSettings;
#endif
#if UNITY_ANDROID
                        return _androidSettings;
#endif
#if UNITY_IOS
                        return _iOSSettings;
#endif
			}
		}

		public Dictionary<GamePlacement, string> GetPlacements()
		{
			return PlacementData.ToDictionary(data => data.GamePlacement, data => data.UnitId);
		}
	}
}