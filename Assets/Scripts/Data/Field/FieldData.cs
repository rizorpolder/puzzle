using System;
using System.Collections.Generic;
using Configs.TextureRepository;

namespace Data
{
	[Serializable]
	public class FieldData : ASavedData
	{
		public new static string Key => "field_data";

		//Данные о поле и то, что в последствии будет сохраняться

		public bool HaveActualSaveData = false;

		public TextureUnitConfigData TextureData;
		public Difficult FieldDifficult;
		public List<PuzzleCellData> Puzzles;

		public FieldData()
		{
			Puzzles = new List<PuzzleCellData>();
			FieldDifficult = Difficult.Low;
		}

		public FieldData(TextureUnitConfig textureUnitConfig, Difficult difficult)
		{
			TextureData = new TextureUnitConfigData(textureUnitConfig);
			FieldDifficult = difficult;
		}

		public static FieldData Default => new FieldData()
		{
			TextureData = new TextureUnitConfigData()
			{
				TextureName = "art_template",
				Category = TextureCategory.Abstraction,
				Index = 0,
			},
			FieldDifficult = Difficult.Low
		};
	}

	[Serializable]
	public class TextureUnitConfigData
	{
		public string TextureName;
		public TextureCategory Category;
		public int Index;

		public TextureUnitConfigData(TextureUnitConfig config)
		{
			TextureName = config.TextureName;
			Category = config.Category;
		}

		public TextureUnitConfigData()
		{

		}
	}
}