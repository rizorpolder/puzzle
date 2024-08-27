using System;
using System.Collections.Generic;

namespace Ads.Runtime
{
	public class GamePlacements<TEnum> where TEnum : Enum
	{
		private readonly string _defaultPlacement;
		private readonly Dictionary<TEnum, string> _placements;

		protected GamePlacements(Dictionary<TEnum, string> placements, string defaultPlacement)
		{
			_placements = placements;
			_defaultPlacement = defaultPlacement;
		}

		public string GetAdPlacement(TEnum gamePlacement)
		{
			if (_placements.ContainsKey(gamePlacement)) return _placements[gamePlacement];

			return _defaultPlacement;
		}

		public TEnum GetGamePlacement(string placement)
		{
			foreach (var placementData in _placements)
				if (placementData.Value == placement)
					return placementData.Key;

			return default;
		}
	}
}