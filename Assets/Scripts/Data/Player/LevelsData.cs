using System;
using System.Collections.Generic;
using System.Linq;
using Configs.TextureRepository;
using Helpers;

namespace Data.Player
{
	[Serializable]
	public class LevelsData
	{
		private SerializableDictionary<TextureCategory, List<LevelDataInfo>> _levels = new();

		public void CompleteLevel(TextureCategory category, string textureName, Difficult difficult)
		{
			if (!_levels.TryGetValue(category, out var level)) return;

			var dataInfo = level.FirstOrDefault(x => x.TextureName.Equals(textureName));
			if (dataInfo == null)
			{
				dataInfo = new LevelDataInfo(textureName);
				_levels[category].Add(dataInfo);
			}

			dataInfo.DifficultCompleted = difficult;
		}

		public void AddDataInfo(TextureCategory category, string textureName)
		{
			if (!_levels.TryGetValue(category, out var level))
			{
				_levels.Add(category, new List<LevelDataInfo>(){new LevelDataInfo(textureName)});
				return;
			}

			var dataInfo = level.FirstOrDefault(x => x.TextureName.Equals(textureName));
			if (dataInfo != null)
				return;

			_levels[category].Add(new LevelDataInfo(textureName));
		}

		public bool HaveLevelData(TextureCategory category, string textureName, out LevelDataInfo dataInfo)
		{
			dataInfo = null;

			if (!_levels.TryGetValue(category, out var level))
				return false;

			dataInfo = level.FirstOrDefault(x => x.TextureName.Equals(textureName));
			return dataInfo != null;
		}
	}

	[Serializable]
	public class LevelDataInfo
	{
		public string TextureName;
		public int CategoryIndex;
		public Difficult DifficultCompleted;

		public LevelDataInfo(string textureName)
		{
			TextureName = textureName;
		}
	}
}