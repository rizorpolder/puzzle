using System;
using System.Collections.Generic;

namespace Data
{
	[Serializable]
	public class FieldData : ASavedData
	{
		public override string PATH => "";


		//Данные о поле и то, что в последствии будет сохраняться
		public string LastTextureName;
		public List<PuzzleCellData> Puzzles;
	}
}