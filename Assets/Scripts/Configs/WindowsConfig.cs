using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Configs
{
	[CreateAssetMenu(menuName = "Project/Configs/WindowsConfig", fileName = "WindowsConfig")]
	public class WindowsConfig : ScriptableObject
	{
		public List<WindowProperties> windows;
	}

	[Serializable]
	public class WindowProperties
	{
		public string windowName;
		public WindowType windowType;
		public AssetReference assetReference;
		public int priority;
		public bool IsCached;

		[Tooltip("Скрывает ли окно HUD")]
		public bool IsHideHUD = true;

		[Tooltip("Скрывает ли окно остальные открытые окна при показе")]
		public bool IsHideOtherWindows;

		[Tooltip("Показывает скрытые под ним окна по окончании анимации скрытия")]
		public bool IsShowHiddenWindowsOnEndAnimations;

		//Shadow
		[Tooltip("Есть ли у окна затемнение")]
		public bool IsHasShadow = true;

		public bool IsHideShadowOnEndAnimation;
		public bool IsOverrideShadowColor;
		public Color ShadowColor = Color.black;

		[Tooltip("Рисуется ли тень над худом")]
		public bool IsShadowUpperHUD = true;
	}

	public enum WindowType
	{
		Custom = 0,
		WinWindow = 1,
		LooseWindow = 2,
		SettingsWindow = 3,
		LevelsWindow = 4,
		LevelInfoWindow = 5,
		HintWindow = 6,

		SupportSubmit = 99
	}
}