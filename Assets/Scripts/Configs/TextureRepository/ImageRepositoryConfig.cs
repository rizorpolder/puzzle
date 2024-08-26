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


		//TODO Это будет репозиторий изображений разделенных по категориям ( и с ценами на них)

		public TextureUnitConfig GetConfig(string textureName)
		{
			return configs.FirstOrDefault(x => x.TextureName.Equals(textureName));
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


					var unitConfig = new TextureUnitConfig(texture.name)
					{
						TextureName = texture.name,
						Texture = texture
					};

					_target.configs.Add(unitConfig);
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
			public string TextureName;

			public Texture2D Texture;

			public TextureUnitConfig(string textureName)
			{
				TextureName = textureName;
			}
		}

		[Serializable]
		public class TextureUnit
		{
			/// <summary>
			/// Column/Row
			/// </summary>
			public Vector2Int originalCoords;

			public TextureUnit(Vector2Int originalCoords)
			{
				this.originalCoords = originalCoords;
			}
		}
	}
}