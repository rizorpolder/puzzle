using System;
using UnityEngine;

namespace Data
{
	[Serializable]
	public struct Difficult
	{
		public static Difficult Low => new(new Vector2Int(3, 3), GameDifficult.Low);
		public static Difficult Mid => new(new Vector2Int(5, 5), GameDifficult.Mid);
		public static Difficult Hard => new(new Vector2Int(10, 10), GameDifficult.Hard);



		public Vector2Int FieldSize;
		public GameDifficult DifficultValue;

		public Difficult(Vector2Int fieldSize, GameDifficult difficult)
		{
			FieldSize = fieldSize;
			DifficultValue = difficult;
		}

		public static Difficult GetDifficult(GameDifficult gameDifficult)
		{
			if (gameDifficult.Equals(Mid.DifficultValue))
				return Mid;

			if (gameDifficult.Equals(Hard.DifficultValue))
				return Hard;

			return Low;
		}
	}

	public enum GameDifficult
	{
		Low,
		Mid,
		Hard
	}
}