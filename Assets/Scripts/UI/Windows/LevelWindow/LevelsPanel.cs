using System;
using System.Collections.Generic;
using Configs.TextureRepository;
using Data.Player;
using Global;
using UnityEngine;

namespace UI.Windows.LevelWindow
{
	public class LevelsPanel : MonoBehaviour
	{
		[SerializeField] private ItemElementView viewPrefab;
		[SerializeField] private RectTransform root;
		[SerializeField] private List<ItemElementView> views;
		public Action<TextureUnitConfig> OnWindowCall = t => { };

		private void Start()
		{
			foreach (var view in views)
			{
				view.OnButtonClick += CallLevelInfoWindow;
			}
		}

		public void UpdatePanelView(List<TextureUnitConfig> textures)
		{
			var levelsData = SharedContainer.Instance.RuntimeData.PlayerData.LevelsData;

			ResetState();
			var i = 0;
			for (; i < textures.Count; i++)
			{
				var textureUnitConfig = textures[i];
				if (i >= views.Count)
					AddViewToPool();

				views[i].gameObject.SetActive(true);

				var view = views[i];
				var haveSavedData = levelsData.HaveLevelData(textureUnitConfig.Category,
					textureUnitConfig.TextureName,
					out var data);

				view.Initialize(textureUnitConfig);
				view.SetIndexText(i + 1)
					.SetLocked(haveSavedData);
			}
		}

		private void ResetState()
		{
			foreach (var itemElementView in views)
			{
				itemElementView.gameObject.SetActive(false);
			}
		}

		private void AddViewToPool()
		{
			var newView = Instantiate(viewPrefab, root);
			views.Add(newView);
			newView.OnButtonClick += CallLevelInfoWindow;
		}

		private void CallLevelInfoWindow(TextureUnitConfig textureUnitConfig)
		{
			OnWindowCall?.Invoke(textureUnitConfig);
		}
	}
}