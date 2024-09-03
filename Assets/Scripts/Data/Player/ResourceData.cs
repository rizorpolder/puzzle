using System;

namespace Data.Player
{
	[Serializable]
	public class ResourceData : ASavedData
	{
		public int Coins = 100;
		public int Stars = 0;
		public int Hearts = 5;
	}
}