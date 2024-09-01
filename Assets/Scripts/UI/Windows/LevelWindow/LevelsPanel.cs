using System;
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
		public Action<int> OnWindowCall = i => { };

		public void Initialize(List<TextureUnitConfig> levels)
		{
			//TODO Check save data if complete = fill stars
			var i = 0;
			for (; i < levels.Count; i++)
			{
				var textureUnitConfig = levels[i];
				if (i >= views.Count) AddViewToPool();

				var view = views[i];
				view.SetIndex(i).
					//todo from save
					SetState(true);

				view.OnButtonClick += CallLevelInfoWindow;
			}

			for (; i < views.Count; i++) views[i].SetIndex(i).SetState(false);
		}

		private void AddViewToPool()
		{
			var newView = Instantiate(viewPrefab, root);
			views.Add(newView);
		}

		private void CallLevelInfoWindow(int index)
		{
			OnWindowCall?.Invoke(index);
		}
	}
}