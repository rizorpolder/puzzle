using System;
using Configs.TextureRepository;

namespace Data
{
	[Serializable]
	public class LevelUnitSaveData
	{
		public TextureCategory Category;
		public string LevelName;
		public int Index;


		public LevelUnitSaveData(TextureCategory category, string levelName, int index)
		{
			Category = category;
			LevelName = levelName;
			Index = index;
		}
	}
}