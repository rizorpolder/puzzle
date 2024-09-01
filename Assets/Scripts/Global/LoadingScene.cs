using System;
using System.Collections;
using AudioManager.Runtime.Core.Manager;
using Data.Player;
using Global;
using Systems.LoadingSystem;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
#if UNITY_WEBGL && YANDEX
using System.Runtime.InteropServices;
#endif

public class LoadScene : MonoBehaviour
{
	private const int MinLoadDelay = 1;

	[SerializeField] private AssetReference _menuSceneAsset;
	[SerializeField] private AssetReference _coreSceneAsset;
	[SerializeField] private GameObject loading;
	[SerializeField] private bool _isInitializeScene;
	private DateTime _startLoadDate;

	private void Start()
	{
		if (_isInitializeScene)
			Initialize();

		DontDestroyOnLoad(loading);

		if (Application.platform != RuntimePlatform.WebGLPlayer)
		{
			LoadData(LoadSceneAsync);
		}
		else
		{
#if UNITY_WEBGL && YANDEX
			if (IsYandexSdkReady())
				LoadData(LoadSceneAsync);
#endif
		}
	}

#if UNITY_WEBGL && YANDEX

	//called from js code
	public void OnYandexSdkInit()
	{
		LoadData(LoadSceneAsync);
	}
#endif

	private void Initialize()
	{
		// todo check memory pressure
		GC.AddMemoryPressure(50 * 1024 * 2024);
	}

	private void LoadData(Action callback)
	{
		//TODO load player data then load scene
		SharedContainer.Instance.SaveDataSystem.LoadData<PlayerData>(PlayerData.Key);
		callback?.Invoke();
	}

	private void LoadSceneAsync()
	{
		SharedContainer.Instance.SetRuntimeData(new RuntimeData());

		_startLoadDate = DateTime.Now;


		// потому что поле загружается в фоне, отрабатывает звук, тутор и все дела, не хорошо
		StartCoroutine(Delay(MinLoadDelay,
			() =>
			{
				Addressables.LoadSceneAsync(_menuSceneAsset).Completed += handle =>
				{
					OnLoadComplete(handle.Result);
					// var loadTimeSeconds = (DateTime.Now - _startLoadDate).TotalSeconds;
					// if (loadTimeSeconds > MinLoadDelay)
					// {
					// 	OnLoadComplete(handle.Result);
					// }
					// else
					// {
					// }
					//TODO Fake loading timer
				};
			}));
	}

	private IEnumerator Delay(float time, Action callback)
	{
		yield return new WaitForSeconds(time);

		callback?.Invoke();
	}

	private void OnLoadComplete(SceneInstance sceneInstance)
	{
		DestroyImmediate(loading);
		//TODO animation menu
		ScenesContainer.Instance.AddScene(sceneInstance);
		ManagerAudio.SharedInstance.PlayMetaMusic();
	}

#if UNITY_WEBGL && YANDEX
	[DllImport("__Internal")]
	public static extern bool IsYandexSdkReady();
#endif
}