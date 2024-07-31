using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Common.Windows;
using Configs;
using UnityEngine;

namespace Systems
{
	public class WindowsController : MonoBehaviour
	{
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
	}
}