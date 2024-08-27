using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AudioManager.Runtime.Core.Manager;
using Common.Windows;
using Configs;
using Global;
using UI.Common;
using UI.Common.Animated;
using UnityEngine;

namespace Systems
{
	public class WindowsController : MonoBehaviour
	{
		public event Action<WindowInstance> WindowHideStart;


		[SerializeField] private ShadowAnimation shadow;
		[SerializeField] private WindowsConfig windowsConfig;
		[SerializeField] private Transform parent;

		private WindowsFactory factory = new WindowsFactory();

		// список открытых окон
		private List<WindowInstance> activeWindows = new List<WindowInstance>();
		private List<WindowInstance> createdWindow = new List<WindowInstance>();
		private List<WindowInstance> closingWindows = new List<WindowInstance>();

		private SortedDictionary<WindowInstance, Action<BaseWindow>> waitedWindows =
			new SortedDictionary<WindowInstance, Action<BaseWindow>>();

		// корутина ожидания для waitedWindows
		private IEnumerator waitToShowCoroutine = null;

		public bool HasActiveWindows => activeWindows.Count > 0;
		public bool HasActiveOrInProcessWindow => activeWindows.Count > 0 || createdWindow.Count > 0;
		public bool HasCreatedWindow => createdWindow.Count > 0;

		public bool HasAnyWindow => HasActiveOrInProcessWindow || waitedWindows.Count > 0;
		public bool HasClosingProcessWindows => closingWindows.Count > 0;

		private void OnEnable()
		{
			Initialize();
		}

		private void Initialize()
		{
			factory.Initialize(windowsConfig);
		}

		#region Show

		public void Show(WindowType windowType,
			Action<BaseWindow> callback = null,
			WindowSource source = WindowSource.Default)
		{
			// блокируем запуск других окон при нажатии на кнопки, если у нас есть уже одно окно в процесс открывания
			if (source.Equals(WindowSource.Button))
				if (createdWindow.Count > 0 || IsWindowInProcess())
					return;

			if (!factory.GetWindow(windowType, out var window))
			{
				Debug.LogAssertionFormat($"No window found with type: {windowType}");
				return;
			}

			Show(window, callback);
		}

		public void Show(string windowName,
			Action<BaseWindow> callback = null,
			WindowSource source = WindowSource.Default)
		{
			// блокируем запуск других окон при нажатии на кнопки, если у нас есть уже одно окно в процесс открывания
			if (source.Equals(WindowSource.Button))
				if (createdWindow.Count > 0 || IsWindowInProcess())
					return;

			if (!factory.GetWindow(windowName, out var window))
			{
				return;
			}

			Show(window, callback);
		}

		private void Show(WindowInstance window, Action<BaseWindow> callback = null)
		{
			if (activeWindows.Contains(window) || createdWindow.Contains(window))
				return;

			if (!IsCanShowWindow(window))
			{
				if (!waitedWindows.ContainsKey(window))
				{
					AddToWait(window, callback);
				}

				return;
			}

			waitedWindows.Remove(window);

			createdWindow.Add(window);

			factory.CreateWindow(window,
				parent,
				() =>
				{
					createdWindow.Remove(window);
					OnShowAction(window);
					callback?.Invoke(window.Window);
				});
		}

		private void OnShowAction(WindowInstance windowInstance)
		{
			windowInstance.Window.Show();
			//ManagerAudio.SharedInstance.PlayAudioClip(TAudio.window_open.ToString());

			windowInstance.Window.transform.SetAsLastSibling();

			// if (windowInstance.Properties.IsHasShadow)
			// {
			// 	if (windowInstance.Properties.IsOverrideShadowColor)
			// 		shadow.SetActiveColor(windowInstance.Properties.ShadowColor);
			// 	else
			// 		shadow.ResetColor();
			//
			// 	shadow.Show();
			// 	var newIndex = windowInstance.Window.transform.GetSiblingIndex() - 1;
			// 	shadow.transform.SetSiblingIndex(newIndex < 0 ? 0 : newIndex);
			// 	shadow.ChangeCanvasState(!windowInstance.Properties.IsShadowUpperHUD);
			// }
			// else
			// {
			// 	shadow.Hide();
			// }

			// Debug.Log(
			// 	$"{windowInstance.Window.name} OnShowAction, hideOther: {windowInstance.Properties.IsHideOtherWindows}, keepOtherWindows:{windowInstance.IsHideOtherWindowsBehaviourOverriden()}");
			// if (windowInstance.Properties.IsHideOtherWindows && !windowInstance.IsHideOtherWindowsBehaviourOverriden())
			// {
			// 	SetActiveAllWindows(false);
			// }

			activeWindows.Add(windowInstance);
			// contexts.gameUI.ReplaceTopWindow((int) windowInstance.Properties.type);
			// contexts.playerData.ReplaceLastMetaActivityTime(DateTime.Now);
			// AnalyticData.Send(new OpenWindowAnalyticData(windowInstance.Window.ID));
		}

		private bool IsCanShowWindow(WindowInstance window)
		{
			// // нельзя показать из-за важного активного мета-действия
			// if (contexts.metaData.IsImportantAction && !window.Properties.IsShowInMetaAction)
			// 	return false;

			// нельзя показать, так как открыто окно с большим приоритетом
			if (activeWindows.Count > 0)
			{
				if (activeWindows[0].Properties.priority > window.Properties.priority)
				{
					return false;
				}
			}

			return true;
		}

		#endregion

		#region Hide

		public void HideAllWindows()
		{
			if (waitToShowCoroutine != null)
			{
				StopCoroutine(waitToShowCoroutine);
				waitToShowCoroutine = null;
				waitedWindows.Clear();
			}

			int max = 0;
			while (activeWindows.Count > 0 && max < 10)
			{
				var window = activeWindows[0];
				if (!window.Window.IsShown())
				{
					window.Window.Hide();
					OnHideAction(window);
					OnHiddenAction(window);
					ClosingComplete(window);
				}

				++max;
			}

			max = 0;
			while (activeWindows.Count > 0 && max < 10)
			{
				Hide(activeWindows.Last());
				++max;
			}
		}

		public void Hide(WindowType windowType)
		{
			Hide(activeWindows.Find(x => x.Properties.windowType == windowType));
		}

		public void Hide(string windowName)
		{
			Hide(activeWindows.Find(x => x.Properties.windowName == windowName));
		}

		private void Hide(WindowInstance window)
		{
			if (window == null || window.Window == null)
				return;

			if (waitedWindows.ContainsKey(window))
				waitedWindows.Remove(window);

			if (window.Window.Status == ElementStatus.Hiding || window.Window.Status == ElementStatus.Hidden)
				return;

			ManagerAudio.SharedInstance.PlayAudioClip(TAudio.Close.ToString());

			OnHideAction(window);

			window.Window.Hide(() => { OnHiddenAction(window); });
		}

		private void OnHiddenAction(WindowInstance window)
		{
			if (activeWindows.Count > 0 && !activeWindows.Last().Properties.IsShadowUpperHUD)
				shadow.ChangeCanvasState(true);
			else
				shadow.ChangeCanvasState(false);

			if (window.Properties.IsHasShadow && window.Properties.IsHideShadowOnEndAnimation)
			{
				var windowWithShadow = GetClosestWindowWithShadow();
				if (windowWithShadow != null)
				{
					var newIndex = windowWithShadow.Window.transform.GetSiblingIndex();
					var shadownIndex = shadow.transform.GetSiblingIndex();
					if (shadownIndex + 1 != newIndex)
						shadow.transform.SetSiblingIndex(newIndex);
				}
				else
				{
					shadow.Hide();
				}
			}

			if (window.Properties.IsHideOtherWindows && window.Properties.IsShowHiddenWindowsOnEndAnimations)
			{
				var windowWithHide =
					activeWindows.Find(x => x.Window.gameObject.activeSelf && x.Properties.IsHideOtherWindows);
				if (windowWithHide == null)
					SetActiveAllWindows(true);
			}

			factory.DestroyWindow(window);
		}

		private void OnHideAction(WindowInstance window)
		{
			WindowHideStart?.Invoke(window);

			activeWindows.Remove(window);
			closingWindows.Add(window);
			window.Window.OnHiddenAction += () => { ClosingComplete(window); };

			if (window.Properties.IsHideOtherWindows && !window.Properties.IsShowHiddenWindowsOnEndAnimations)
			{
				var windowWithHide =
					activeWindows.Find(x => x.Window.gameObject.activeSelf && x.Properties.IsHideOtherWindows);
				if (windowWithHide == null)
					SetActiveAllWindows(true);
			}

			if (window.Properties.IsHasShadow && !window.Properties.IsHideShadowOnEndAnimation)
			{
				var windowWithShadow = GetClosestWindowWithShadow();
				if (windowWithShadow != null)
				{
					var newIndex = windowWithShadow.Window.transform.GetSiblingIndex();
					var shadownIndex = shadow.transform.GetSiblingIndex();
					if (shadownIndex + 1 != newIndex)
						shadow.transform.SetSiblingIndex(newIndex);
				}
				else
				{
					shadow.Hide();
				}
			}

			if (window.Properties.IsHideHUD)
			{
				SharedContainer.Instance.GlobalUI.HUD.Show(window.Properties.windowName);
			}
		}

		private void ClosingComplete(WindowInstance window)
		{
			closingWindows.Remove(window);
		}

		private WindowInstance GetClosestWindowWithShadow()
		{
			var windowsWithShadow =
				activeWindows.FindAll(x => x.Window.gameObject.activeSelf && x.Properties.IsHasShadow);
			WindowInstance closest = null;
			foreach (var w in windowsWithShadow)
			{
				if (closest == null)
				{
					closest = w;
				}
				else
				{
					int currentSiblingIndex = closest.Window.transform.GetSiblingIndex();
					int wSiblingIndex = w.Window.transform.GetSiblingIndex();
					if (wSiblingIndex > currentSiblingIndex)
						closest = w;
				}
			}

			return closest;
		}

		#endregion

		private void AddToWait(WindowInstance window, Action<BaseWindow> callback)
		{
			waitedWindows.Add(window, callback);

			if (waitToShowCoroutine == null)
			{
				waitToShowCoroutine = WaitToShow();
				StartCoroutine(waitToShowCoroutine);
			}
		}

		IEnumerator WaitToShow()
		{
			while (waitedWindows.Count > 0)
			{
				yield return new WaitForSeconds(0.05f);
				if (waitedWindows.Count > 0)
					Show(waitedWindows.First().Key, waitedWindows.First().Value);
			}

			waitToShowCoroutine = null;
			yield return null;
		}

		public bool GetWindow(WindowType windowType, out BaseWindow baseWindow)
		{
			if (factory.GetWindow(windowType, out WindowInstance window))
			{
				if (window.Window != null)
				{
					baseWindow = window.Window;
					return true;
				}
			}

			baseWindow = null;
			return false;
		}

		public bool GetWindowInstance(string windowID, out WindowInstance instance)
		{
			if (factory.GetWindow(windowID, out WindowInstance windowInstance))
			{
				if (windowInstance.Window != null)
				{
					instance = windowInstance;
					return true;
				}
			}

			instance = null;
			return false;
		}

		public bool IsWindowInProcess()
		{
			foreach (var window in activeWindows)
			{
				if (window.Window.Status != ElementStatus.Shown)
					return true;
			}

			return false;
		}

		private void SetActiveAllWindows(bool value)
		{
			for (var index = activeWindows.Count - 1; index >= 0; index--)
			{
				var wp = activeWindows[index];
				//при обычном выключении отваливаются спайн анимации окон (не отрабатывает show)

				if (value)
				{
					wp.Window.Show();
					if (wp.Properties.IsHasShadow)
					{
						shadow.Show();
						var newIndex = wp.Window.transform.GetSiblingIndex();
						shadow.transform.SetSiblingIndex( newIndex < 0 ? 0 : newIndex );
					}
					if (wp.Properties.IsHideOtherWindows)
						break;
				}
				else
					wp.Window.HideInstant();
			}
		}
	}
}