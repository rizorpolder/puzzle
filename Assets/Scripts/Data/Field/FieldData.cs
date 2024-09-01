using System;
using System.Collections.Generic;
using Configs.TextureRepository;
using UnityEngine;

namespace Data
{
	[Serializable]
	public class FieldData : ASavedData
	{
		//Данные о поле и то, что в последствии будет сохраняться
		public TextureUnitConfigData TextureData;
		public Difficult FieldDifficult;
		public List<PuzzleCellData> Puzzles;

		public FieldData()
		{
			Puzzles = new List<PuzzleCellData>();
			FieldDifficult = Data.FieldDifficult.Low;
		}

		public static FieldData Default => new FieldData()
		{
			TextureData = new TextureUnitConfigData()
			{
				TextureName = "art_template",
				Category = TextureCategory.Abstraction,
				Index = 0,
			},
			FieldDifficult = Data.FieldDifficult.Low
		};
	}

	[Serializable]
	public class TextureUnitConfigData
	{
		public string TextureName;
		public TextureCategory Category;
		public int Index;
	}
}