using Advertising;
using UnityEngine;

namespace Systems.Ads.Data
{
	[System.Serializable]
	public class PlacementData
	{
		[field: SerializeField] public GamePlacement GamePlacement { get; private set; }
		[field: SerializeField] public string UnitId { get; private set; }
	}
}