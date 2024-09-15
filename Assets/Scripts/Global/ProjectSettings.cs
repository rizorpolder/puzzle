using UnityEngine;

namespace Global
{
	public class ProjectSettings : MonoBehaviour
	{
		[SerializeField] private int _targetFPS = 60;
		public static bool IsDesktop { get; private set; }
		public static bool IsIos { get; private set; }

		public static bool IsWebGL
		{
			get
			{
#if UNITY_WEBGL
				return true;
#else
            return false;
#endif
			}
		}

		public static bool IsWebGLiOS => IsWebGL && IsIos;

		private void Awake()
		{
			UpdateInfo();

			Application.targetFrameRate = _targetFPS;
		}

		private void UpdateInfo()
		{
			IsDesktop = SystemInfo.operatingSystemFamily != OperatingSystemFamily.Other;

			IsIos = SystemInfo.operatingSystem.ToLower().Trim().Contains("iphone");

			// Debug.Log($"IsDesktop:{IsDesktop}");
			// Debug.Log($"IsIos:{IsIos}");
			// Debug.Log($"SystemInfo.operatingSystem:{SystemInfo.operatingSystem}");
			// Debug.Log($"SystemInfo.operatingSystemFamily:{SystemInfo.operatingSystemFamily}");
			//
			// Debug.Log($"SystemInfo.deviceType:{SystemInfo.deviceType}");
			// Debug.Log($"SystemInfo.deviceModel:{SystemInfo.deviceModel}");
			//
			// Debug.Log($"SystemInfo.processorType{SystemInfo.processorType}");
			// Debug.Log($"SystemInfo.systemMemorySize:{SystemInfo.systemMemorySize}");
		}
	}
}