using System;
using UnityEngine;

namespace Data
{
	[Serializable]
	public class PuzzleCellData
	{
		/// <summary>
		/// Индек по которому лежит спрайт в конфиге
		/// </summary>
		public int SpritePartIndex;

		/// <summary>
		/// Координаты на поле
		/// </summary>
		public Vector2Int cellCoords;
	}
}