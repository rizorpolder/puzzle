using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Configs.TextureRepository
{
	[CreateAssetMenu(menuName = "Project/Configs/Images Repository", fileName = "ImageRepository")]
	public class ImageRepositoryConfig : ScriptableObject
	{
		[SerializeField] private string ShortPath;
		[SerializeField] private List<TextureCategoryConfig> configs;

		//TODO Это будет репозиторий изображений разделенных по категориям ( и с ценами на них)

		public TextureUnitConfig GetConfig(TextureCategory data,string textureName)
		{
			var category = configs.FirstOrDefault(x=>x.Category.Equals(data));
			if (category != null)
			{
				return category.Textures.FirstOrDefault(x => x.TextureName.Equals(textureName));
			}

			return null;
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
						textureCategoryConfig = new TextureCategoryConfig()
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
		public List<TextureUnitConfig> Textures;

		public TextureCategoryConfig()
		{
			Textures = new List<TextureUnitConfig>();
		}
	}

	[Serializable]
	public class TextureUnitConfig
	{
		/// <summary>
		///     Column / Row
		/// </summary>
		public string TextureName;

		public Texture2D Texture;

		public TextureUnitConfig(string textureName)
		{
			TextureName = textureName;
		}
	}

	public enum TextureCategory
	{
		None,
		Custom,
		Abstraction,
	}
}
