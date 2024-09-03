using UnityEngine;

namespace Extensions
{
	public static class TextureExtensions
	{
		public static Sprite CreateSprite(this Texture2D texture2D)
		{
			var pivot = new Vector2(0.5f, 0.5f);
			var rect = new Rect(0.0f, 0.0f, texture2D.width, texture2D.height);
			return CreateSprite(texture2D, rect, pivot);
		}

		public static Sprite CreateSprite(this Texture2D texture2D, Rect rect, Vector2 pivot)
		{
			return Sprite.Create(texture2D, rect, pivot);
		}
	}
}