using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;

namespace Configs.TextureRepository
{
	[CreateAssetMenu(menuName = "Project/Configs/Images Repository", fileName = "ImageRepository")]
	public class ImageRepositoryConfig : ScriptableObject
	{
		[SerializeField] private string ShortPath;
		[SerializeField] private List<TextureUnitConfig> configs;

		public List<TextureUnit> GetSprites(string name)
		{
			var config = configs.FirstOrDefault(x => x.TextureName.Equals(name));
			var unitConfig = config ?? configs[0];
			return unitConfig.Sprites;
		}

#if UNITY_EDITOR

		[UnityEditor.CustomEditor(typeof(ImageRepositoryConfig))]
		public class ImageRepositoryConfigEditor : UnityEditor.Editor
		{
			private ImageRepositoryConfig _target;

			public override void OnInspectorGUI()
			{
				_target = target as ImageRepositoryConfig;

				var style = new GUIStyle(GUI.skin.button);
				style.normal.textColor = Color.green;
				if (GUILayout.Button("Generate Config", style, GUILayout.Width(180), GUILayout.Height(30)))
				{
					GenerateConfig();
				}

				GUILayout.Space(10);
				base.OnInspectorGUI();
			}

			private void GenerateConfig()
			{
				var path = Application.dataPath + _target.ShortPath;
				var info = new DirectoryInfo(path);
				var files = info.GetFiles("*.*", SearchOption.AllDirectories);

				_target.configs.Clear();

				foreach (var fileInfo in files)
				{
					if (fileInfo.Name.Contains(".meta"))
						continue;
					var texturePath = $"Assets{_target.ShortPath}/{fileInfo.Name}";
					var texture = (Texture2D) UnityEditor.AssetDatabase.LoadAssetAtPath(texturePath, typeof(Texture2D));

					if (!texture)
						continue;


					var unitConfig = new TextureUnitConfig(texture.name);
					unitConfig.TextureName = texture.name;
					unitConfig.Texture = texture;


					var assetPath = UnityEditor.AssetDatabase.GetAssetPath(texture);
					var sprites = UnityEditor.AssetDatabase.LoadAllAssetsAtPath(assetPath).OfType<Sprite>().ToArray();

					unitConfig.FieldSize = CalculateFieldSize(sprites.Length);
					unitConfig.FillSprites(sprites);

					_target.configs.Add(unitConfig);
				}
			}

			private Vector2 CalculateFieldSize(int elementsCount) // 36
			{
				int rows = 0;
				int columns = elementsCount;

				int locker = 0;

				while (columns > rows)
				{
					rows++;
					var newValue = elementsCount / rows;
					columns = newValue;

					locker++;
					if (locker > 1000)
					{
						return Vector2.zero;
					}
				}

				return new Vector2(rows, columns);
			}
		}
#endif
	}

	[Serializable]
	public class TextureUnitConfig
	{
		/// <summary>
		/// Column / Row
		/// </summary>
		public Vector2 FieldSize;
		public string TextureName;
		public Texture2D Texture;

		public List<TextureUnit> Sprites;

		public TextureUnitConfig(string textureName)
		{
			TextureName = textureName;
			Sprites = new List<TextureUnit>();
		}

		public void FillSprites(Sprite[] sprites)
		{
			Sprites = new List<TextureUnit>();
			for (int i = 0; i < FieldSize.x; i++)
			{
				for (int j = 0; j < FieldSize.y; j++)
				{
					var spriteIndex = i * (int)FieldSize.x + j;
					var sprite = sprites[spriteIndex];
					Vector2 spritePos = new Vector2(i, j);
					var element = new TextureUnit(sprite, spritePos);
					Sprites.Add(element);
				}
			}
		}
	}

	[Serializable]
	public class TextureUnit
	{
		public Sprite sprite;

		/// <summary>
		/// Column/Row
		/// </summary>
		public Vector2 coords;

		public TextureUnit(Sprite sprite, Vector2 coords)
		{
			this.sprite = sprite;
			this.coords = coords;
		}
	}
}