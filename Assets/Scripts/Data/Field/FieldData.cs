using System;
using System.Collections.Generic;

namespace Data
{
	[Serializable]
	public class FieldData : ASavedData
	{
		//Данные о поле и то, что в последствии будет сохраняться
		public string LastTextureName;
		public Difficult FieldDifficult;
		public List<PuzzleCellData> Puzzles;

		public FieldData()
		{
			Puzzles = new List<PuzzleCellData>();
			FieldDifficult = Data.FieldDifficult.Low;
			;
		}
	}
}