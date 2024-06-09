using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Configs.TextureRepository
{
	[CreateAssetMenu(menuName = "Project/Configs/Images Repository", fileName = "ImageRepository")]
	public class ImageRepositoryConfig : ScriptableObject
	{
		[SerializeField] private List<Texture2D> _textures;

		[SerializeField] private List<TextureUnitConfig> configs;

		public List<Sprite> GetSprites()
		{
			return configs[0].sprites;
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
				_target.configs.Clear();
				foreach (var texture in _target._textures)
				{
					var path = UnityEditor.AssetDatabase.GetAssetPath(texture);
					var sprites = UnityEditor.AssetDatabase.LoadAllAssetsAtPath(path).OfType<Sprite>().ToList();
					_target.configs.Add(new TextureUnitConfig()
					{
						textureName = texture.name,
						sprites = sprites,
					});
				}
			}
		}
#endif
	}

	[Serializable]
	public class TextureUnitConfig
	{
		public List<Sprite> sprites;
		public string textureName; //TODO To enum?
	}
}