using System.Runtime.InteropServices;
using Common;
using UnityEngine;

public class RateUs : Singletone<RateUs>
{
	[SerializeField] private int _adventureLevelToShow;
	[SerializeField] private int _classicAttemptToShow;

	public bool TryShow()
	{
		if (!NeedShow())
			return false;

		Show();

		return true;
	}

	public bool NeedShow()
	{
		// if (SharedContainer.Instance.RuntimeData.PlayerData.SessionData.RateUsShowed)
		// 	return false;

		if (!ShowConditionsMet())
			return false;

		return true;
	}

	private bool ShowConditionsMet()
	{
		// if (CoreController.Instance.IsClassic)
		// {
		// 	if (!CoreController.Instance.IsNewRecord)
		// 		return false;
		//
		// 	return CoreController.Instance.CurrentAttempt >= _classicAttemptToShow;
		// }
		// else
		// {
		// 	return SharedContainer.Instance.RuntimeData.PlayerData.CurrentLevel > _adventureLevelToShow;
		// }
		return false;
	}

	private void Show()
	{
#if UNITY_ANDROID
#endif

#if UNITY_WEBGL
		if (Application.isEditor)
			Debug.Log($"<color=green>RATEUS SHOWED!</color>");
		else
			RateAppYandex();
#endif
		// var playerData = SharedContainer.Instance.RuntimeData.PlayerData;
		// playerData.SessionData.RateUsShowed = true;
		// SharedContainer.Instance.DataManager.SaveData(PlayerData.DataKey, playerData);
	}

#if UNITY_WEBGL
	[DllImport("__Internal")]
	public static extern void RateAppYandex();
#endif
}