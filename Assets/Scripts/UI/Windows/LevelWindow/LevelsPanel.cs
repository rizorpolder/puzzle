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
		public Action<TextureCategory, string> OnWindowCall = (category, s) => { };
		public void Initialize(List<TextureUnitConfig> textures)
		{

		}

		private void InitializeViews(List<TextureUnitConfig> items)
		{
			int i = 0;
			for (; i < items.Count; i++)
			{

			}
			for (; i < views.Count; i++) views[i].SetIndex(i).SetState(false);
		}

		private void SetView(int index, TextureCategory category)
		{


		}

		private void AddViewToPool()
		{
			var newView = Instantiate(viewPrefab, root);
			views.Add(newView);
		}

		private void CallLevelInfoWindow(TextureCategory category, string textureName)
		{
			OnWindowCall?.Invoke(category, textureName);
		}
	}
}