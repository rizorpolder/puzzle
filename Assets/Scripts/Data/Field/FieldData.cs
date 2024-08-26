using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
	[Serializable]
	public class FieldData : ASavedData
	{
		public override string key => "field_data";

		//Данные о поле и то, что в последствии будет сохраняться
		public string LastTextureName;
		public Vector2Int fieldSize;
		public List<PuzzleCellData> Puzzles;

		public FieldData()
		{
			Puzzles = new List<PuzzleCellData>();
			fieldSize = new Vector2Int(10, 10);
		}
	}
}