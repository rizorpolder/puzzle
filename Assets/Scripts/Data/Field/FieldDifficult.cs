using System;
using UnityEngine;

namespace Data
{
	[Serializable]
	public class FieldDifficult
	{
		public static Difficult Low => new(new Vector2Int(3, 3), GameDifficult.Low);
		public static Difficult Mid => new(new Vector2Int(5, 5), GameDifficult.Mid);
		public static Difficult Hard => new(new Vector2Int(10, 10), GameDifficult.Hard);
	}

	[Serializable]
	public struct Difficult
	{
		public Vector2Int FieldSize;
		public GameDifficult DifficultValue;

		public Difficult(Vector2Int fieldSize, GameDifficult difficult)
		{
			FieldSize = new Vector2Int(3, 3);
			DifficultValue = GameDifficult.Low;
		}
	}

	public enum GameDifficult
	{
		Low,
		Mid,
		Hard
	}
}