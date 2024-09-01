using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Data;
using UnityEditor;
using UnityEngine;

namespace Configs.TextureRepository
{
	[CreateAssetMenu(menuName = "Project/Configs/Images Repository", fileName = "ImageRepository")]
	public class ImageRepositoryConfig : ScriptableObject // TODO RemoteConfig
	{
		[SerializeField] private string ShortPath;
		[SerializeField] private List<TextureCategoryConfig> configs;

		public TextureUnitConfig GetConfig(TextureCategory data, string textureName)
		{
			var category = configs.FirstOrDefault(x => x.Category.Equals(data));
			return category?.Textures.FirstOrDefault(x => x.TextureName.Equals(textureName));
		}

		public TextureUnitConfig GetConfig(TextureUnitConfigData fieldDataTextureData)
		{
			return GetConfig(fieldDataTextureData.Category, fieldDataTextureData.TextureName);
		}

		public List<TextureCategory> GetAllCategories()
		{
			return configs.Select(x=>x.Category).ToList();
		}

		public List<TextureUnitConfig> GetLevelsByCategory(TextureCategory category)
		{
			var result = new List<TextureUnitConfig>();

			if (category == TextureCategory.All)
			{
				foreach (var textureCategoryConfig in configs)
				{
					result.AddRange(textureCategoryConfig.Textures);
				}

				return result;
			}

			var config = configs.FirstOrDefault(x => x.Category.Equals(category));
			if (config != null)
				result = config.Textures;

			return result;
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

					var textureCategoryConfig =
						_target.configs.FirstOrDefault(x => x.Category.Equals(currentCategory));
					if (textureCategoryConfig == null)
					{
						textureCategoryConfig = new TextureCategoryConfig
						{
							Category = currentCategory
						};
						_target.configs.Add(textureCategoryConfig);
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
							Texture = texture
						};
						textureCategoryConfig.Textures.Add(unitConfig);
					}
				}
			}
		}
#endif
	}

	[Serializable]
	public class TextureCategoryConfig
	{
		public TextureCategory Category;
		public List<TextureUnitConfig> Textures = new();
	}

	[Serializable]
	public class TextureUnitConfig
	{
		public string TextureName;
		public Texture2D Texture;
		public int TextureCost;

		public TextureUnitConfig(string textureName)
		{
			TextureName = textureName;
		}
	}

	public enum TextureCategory
	{
		All,
		Abstraction,
		Cars,
		Nature,

		//Custom,
	}
}