using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
	[Serializable]
	public class FieldData : ASavedData
	{
		//Данные о поле и то, что в последствии будет сохраняться
		public string LastTextureName;
		public Vector2Int fieldSize;
		public List<PuzzleCellData> Puzzles;

		public FieldData()
		{
			Puzzles = new List<PuzzleCellData>();
			fieldSize = new Vector2Int(10, 10);
		}

		public override string key => "field_data";
	}
}