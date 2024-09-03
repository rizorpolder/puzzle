using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Extensions;
using Helpers;
using UnityEditor;
using UnityEngine;

namespace Configs.TextureRepository
{
	[CreateAssetMenu(menuName = "Project/Configs/Images Repository", fileName = "ImageRepository")]
	public class ImageRepositoryConfig : ScriptableObject // TODO RemoteConfig
	{
		[SerializeField] private string ShortPath;
		[SerializeField] private SerializableDictionary<TextureCategory,TextureUnitHolder> configs = new();

		public TextureUnitConfig GetConfig(TextureCategory category, string textureName)
		{
			if (!configs.ContainsKey(category)) return null;
			var textures = configs[category];
			return textures.Textures.FirstOrDefault(x => x.TextureName == textureName);
		}

		public List<TextureCategory> GetAllCategories()
		{
			return configs.Keys.ToList();
		}

		public List<TextureUnitConfig> GetLevelsByCategory(TextureCategory category)
		{
			return configs[category].Textures;
		}

#if UNITY_EDITOR

		[CustomEditor(typeof(ImageRepositoryConfig))]
		public class ImageRepositoryConfigEditor : Editor
		{
			private ImageRepositoryConfig _target;

			public override void OnInspectorGUI()
			{
				_target = target as ImageRepositoryConfig;

				var style = new GUIStyle(GUI.skin.button);
				style.normal.textColor = Color.green;
				if (GUILayout.Button("Generate Config", style, GUILayout.Width(180), GUILayout.Height(30)))
					GenerateConfig();

				GUILayout.Space(10);
				base.OnInspectorGUI();
			}

			private void GenerateConfig()
			{
				var path = Application.dataPath + _target.ShortPath;
				var info = new DirectoryInfo(path);
				var directories = info.GetDirectories("*.*", SearchOption.AllDirectories);

				_target.configs.Clear();

				foreach (var directoryInfo in directories)
				{
					if (!Enum.TryParse<TextureCategory>(directoryInfo.Name, out var currentCategory))
						continue;

					if (!_target.configs.ContainsKey(currentCategory))
					{
						_target.configs.Add(currentCategory,new TextureUnitHolder());
					}

					var files = directoryInfo.GetFiles("*.*", SearchOption.AllDirectories);

					foreach (var fileInfo in files)
					{
						if (fileInfo.Name.Contains(".meta"))
							continue;
						var texturePath = $"Assets{_target.ShortPath}/{directoryInfo.Name}/{fileInfo.Name}";
						var texture = (Texture2D) AssetDatabase.LoadAssetAtPath(texturePath, typeof(Texture2D));

						if (!texture)
							continue;

						var unitConfig = new TextureUnitConfig(texture.name)
						{
							TextureName = texture.name,
							Texture = texture,
							Category = currentCategory,
							Sprite = texture.CreateSprite()
						};
						_target.configs[currentCategory].Textures.Add(unitConfig);
					}
				}
			}
		}
#endif
	}

	[Serializable]
	public class TextureUnitHolder
	{
		public List<TextureUnitConfig> Textures = new();
	}

	[Serializable]
	public class TextureUnitConfig
	{
		public Texture2D Texture;
		public Sprite Sprite;
		public TextureCategory Category;
		public string TextureName;
		public int TextureCost;

		public TextureUnitConfig(string textureName)
		{
			TextureName = textureName;
		}
	}

	[Serializable]
	public enum TextureCategory
	{
		All,
		Abstraction,
		Cars,
		Nature,

		//Custom,
	}
}