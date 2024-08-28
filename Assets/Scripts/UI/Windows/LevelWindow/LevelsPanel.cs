using System.Collections.Generic;
using Configs.TextureRepository;
using UnityEngine;

namespace UI.Windows.LevelWindow
{
	public class LevelsPanel : MonoBehaviour
	{
		[SerializeField] private ItemElementView viewPrefab;
		[SerializeField] private RectTransform root;
		[SerializeField] private List<ItemElementView> views;

		public void Initialize(List<TextureUnitConfig> levels)
		{
			//TODO Check save data if complete = fill stars

			for (var i = 0; i < levels.Count; i++)
			{
				var textureUnitConfig = levels[i];
				if (i >= views.Count)
				{
					AddViewToPool();
				}
				var view = views[i];
				view.Initialize(textureUnitConfig);
			}
		}

		private void AddViewToPool()
		{
			var newView = Instantiate(viewPrefab, root);
			views.Add(newView);
		}
	}
}
