using System;
using System.Runtime.InteropServices;
using Ads.Runtime;
using Advertising;
using Game;
using Systems;
using Systems.Ads.Conditions;
using Systems.LoadingSystem;
using UnityEngine;

namespace Global
{
	public class SharedContainer : MonoBehaviour
	{
		public static SharedContainer Instance;

		private void Awake()
		{
			if (Instance != null && Instance != this)
			{
				DestroyImmediate(this);
				return;
			}

			InitializeLocalization();

			Instance = this;
			DontDestroyOnLoad(this);
		}

		private void Start()
		{
			//Analytics = _analyticInstaller.AnalyticManager;
		}

		private void InitializeLocalization()
		{
			//LocalizationManager.InitializeIfNeeded();
#if UNITY_WEBGL
			if (!Application.isEditor)
				SetLanguageFromWebUrl();
#endif
		}

#if UNITY_WEBGL
		private void SetLanguageFromWebUrl()
		{
			var languageCodePtr = GetLanguageCodeFromUrl();
			// Convert the pointer to a managed string
			var languageCode = Marshal.PtrToStringUTF8(languageCodePtr);
			// Free the allocated memory
			Marshal.FreeHGlobal(languageCodePtr);

			// string lang = string.IsNullOrEmpty(languageCode)
			// 	? LocalizationManager.GetCurrentDeviceLanguage()
			// 	: LocalizationManager.GetLanguageFromCode(languageCode);
			// var languages = LocalizationManager.GetAllLanguages();
			// if (languages.Contains(lang))
			// 	LocalizationManager.CurrentLanguage = lang;
			// else
			// 	LocalizationManager.CurrentLanguage = languages[0];
		}

#endif

		public void SetRuntimeData(RuntimeData data)
		{
			RuntimeData = data;
		}

#if UNITY_WEBGL
		[DllImport("__Internal")]
		public static extern IntPtr GetLanguageCodeFromUrl();
#endif

		#region fields

		[SerializeField] private ConfigurableRoot _configurableRoot;
		[SerializeField] private LoadingController _loaderController;
		[SerializeField] private WindowsController _windowsController;
		[SerializeField] private GlobalUI _globalUI;
		[SerializeField] private AdsController<GamePlacement> _adsController;

		[SerializeField] private AdsConditionsController _adsConditions;
		// [SerializeField] private AnalyticInstaller _analyticInstaller;
		// [SerializeField] private BaseInAppManager _inAppManager;

		#endregion

		#region props

		public ConfigurableRoot ConfigurableRoot => _configurableRoot;

		public LoadingController LoadingController => _loaderController;
		public WindowsController WindowsController => _windowsController;
		public GlobalUI GlobalUI => _globalUI;
		public IAdsController<GamePlacement> Ads => _adsController;
		public AdsConditionsController AdsConditions => _adsConditions;

		public RuntimeData RuntimeData { get; private set; }

		#endregion
	}
}