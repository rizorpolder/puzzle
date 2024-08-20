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
		public bool IsCached = false;
	}

	public enum WindowType
	{
		WinWindow = 1,
		LooseWindow = 2,
		SettingsWindow = 3,

	}
}