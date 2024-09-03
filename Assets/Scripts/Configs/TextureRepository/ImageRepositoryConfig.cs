using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Helpers;
using UnityEditor;
using UnityEngine;

namespace Configs.TextureRepository
{
	[CreateAssetMenu(menuName = "Project/Configs/Images Repository", fileName = "ImageRepository")]
	public class ImageRepositoryConfig : ScriptableObject // TODO RemoteConfig
	{
		[SerializeField, Header("URL")] private string url;


		[SerializeField] private string ShortPath;
		[SerializeField] private SerializableDictionary<TextureCategory, TextureUnitHolder> configs = new();


		public List<TextureUnitConfig> GetFreeTextures()
		{
			List<TextureUnitConfig> result = new List<TextureUnitConfig>();
			foreach (var kv in configs)
			{
				foreach (var unit in kv.Value.Textures)
				{
					if(!unit.TextureCost.value.Equals(0))
						continue;
					result.Add(unit);
				}
			}

			return result;
		}
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

		#region Editor

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
						_target.configs.Add(currentCategory, new TextureUnitHolder());
					}

					var files = directoryInfo.GetFiles("*.*", SearchOption.AllDirectories);

					foreach (var fileInfo in files)
					{
						if (fileInfo.Name.Contains(".meta"))
							continue;
						var texturePath = $"Assets{_target.ShortPath}/{directoryInfo.Name}/{fileInfo.Name}";
						var texture = (Sprite) AssetDatabase.LoadAssetAtPath(texturePath, typeof(Sprite));

						if (!texture)
							continue;

						var unitConfig = new TextureUnitConfig(texture.name)
						{
							TextureName = texture.name,
							Category = currentCategory,
							Sprite = texture,
						};
						_target.configs[currentCategory].Textures.Add(unitConfig);
					}
				}
			}
		}

		#endregion

		#region RemoteDownload

		[ContextMenu("DownloadDataToConfig")]
		public void SetupConfig()
		{
			ConfigLoad.SetupDownloadedItems(url,
				(string[] items) =>
				{
					if (!Enum.TryParse(items[1], out TextureCategory category))
						return;

					var tableName = items[2];
					if (!configs.ContainsKey(category))
						return;

					var textures = configs[category].Textures;
					var textureData = textures.FirstOrDefault(x => x.TextureName.Equals(tableName));
					if (textureData != null)
					{
						textureData.TextureCost = ConfigParser.ParseResource(items[3]);
					}
				});

			EditorUtility.SetDirty(this);
			Debug.Log($"{name} update Complited");
		}

		#endregion
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
		public Sprite Sprite;
		public TextureCategory Category;
		public string TextureName;
		public Resource TextureCost;

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