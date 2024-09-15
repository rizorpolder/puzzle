using System;
using Global;
using UnityEngine;

namespace Data.Player
{
	[Serializable]
	public class PlayerData : ASavedData
	{
		public new static string Key => "player_data";

		public string playerID = "228";
		public string lastPlayedLevel = "";
		public ResourceData ResourceData;
		public SessionData SessionData;

		public LevelsData LevelsData;

		// данные об всех уровнях (список купленных уровней с отметкой на каком уровне сложности пройден ( или сколько звезд получено)
		public PlayerData()
		{
			SessionData = new SessionData();
			LevelsData = new LevelsData();
			ResourceData = new ResourceData();
			InitializeFreeTextures();
		}

		private void InitializeFreeTextures()
		{
			var textures = SharedContainer.Instance.ConfigurableRoot.ImageRepositoryConfig.GetFreeTextures();
			foreach (var unitConfig in textures)
			{
				LevelsData.AddDataInfo(unitConfig.Category, unitConfig.TextureName);
			}
		}

		#region Resources

		private bool IsInfinityHearts()
		{
			// var timer = playerData.GetTimer(CooldownTypes.InfiniteEnergy);
			// return !timer.cooldown.IsDefault() && timer.cooldown.IsActual();
			return true;
		}

		public Resource GetResource(ResourceType resourceType)
		{
			Resource resource = new Resource(resourceType, 0);
			resource.value = GetResourceCount(resource);
			return resource;
		}

		public int GetResourceCount(Resource resource)
		{
			return GetCommonResourceCount(resource.type);
		}

		private int GetCommonResourceCount(ResourceType resourceType)
		{
			switch (resourceType)
			{
				case ResourceType.Soft:
					return ResourceData.Coins;
				case ResourceType.Hard:
					return ResourceData.Stars;
				case ResourceType.Restored:
					return ResourceData.Hearts;
				default:
					return 0;
			}
		}

		public void SpendResource(Resource resource)
		{
			if (!HasResources(resource))
			{
				Debug.Log("Not Enough resources, Open shop");
				return;
			}

			AddResource(resource);
		}

		public bool HasResources(params Resource[] resources)
		{
			foreach (var resource in resources)
			{
				var playerResourceCount = GetResourceCount(resource);
				if (playerResourceCount < resource.value)
					return false;
			}

			return true;
		}

		public void AddResource(Resource resource)
		{
			switch (resource.type)
			{
				case ResourceType.Soft:
					AddSoftCurrency(resource.value);
					break;
				case ResourceType.Hard:
					AddHardCurrency(resource.value);

					break;
				case ResourceType.Restored:
					AddRestoredCurrency(resource.value);
					break;
			}
		}

		public void AddSoftCurrency(int resourceValue)
		{
			if (resourceValue < 0)
				return;

			ResourceData.Coins += resourceValue;
		}

		public void AddHardCurrency(int resourceValue)
		{
			if (resourceValue < 0)
				return;
			ResourceData.Stars += resourceValue;
		}

		public void AddRestoredCurrency(int resourceValue)
		{
			if (resourceValue < 0 && IsInfinityHearts())
				return;
			ResourceData.Hearts += resourceValue;
		}

		#endregion
	}
}