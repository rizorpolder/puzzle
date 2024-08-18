using System;
using Global;

namespace Game.Data
{
	[Serializable]
	public class GameData
	{
		public byte[] ClassicLevelSave;
		public bool IsClassicIncomplete = false;
		public byte[] AdventureLevelSave;
		public bool IsAdventureIncomplete = false;
		public int ClassicAttempt;

		public GameData()
		{
			ClassicLevelSave = new byte[] { };
			AdventureLevelSave = new byte[] { };
		}

		public bool HasClassicSaveData() => ClassicLevelSave.Length > 0 && IsClassicIncomplete;
		public bool HasAdventureSaveData() => AdventureLevelSave.Length > 0 && IsAdventureIncomplete;

		public byte[] GetLevelData(GameType type)
		{
			switch (type)
			{
				case GameType.Adventure:
					return AdventureLevelSave;

				default:
					return ClassicLevelSave;
			}
		}

		public void SaveLevelData(GameType type, byte[] data)
		{
			switch (type)
			{
				case GameType.Adventure:
					AdventureLevelSave = data;
					break;

				default:
					ClassicLevelSave = data;

					break;
			}
		}

		public void ResetData(GameType type)
		{
			switch (type)
			{
				case GameType.Adventure:
					AdventureLevelSave = new byte[] { };
					IsAdventureIncomplete = false;
					break;

				default:
					ClassicLevelSave = new byte[] { };
					IsClassicIncomplete = false;
					break;
			}
		}

		public void StartLevel(GameType type)
		{
			switch (type)
			{
				case GameType.Adventure:
					IsAdventureIncomplete = true;
					break;

				default:
					ClassicAttempt++;
					IsClassicIncomplete = true;
					break;
			}
		}
	}
}