using System;
using System.Collections.Generic;
using Configs;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Common.Windows
{
	public class WindowsFactory
	{
		private List<WindowInstance> instances = new List<WindowInstance>();

		public void Initialize(WindowsConfig config)
		{
			foreach (var properties in config.windows)
			{
				instances.Add(new WindowInstance(properties));
			}

		}

		public bool GetWindow(string name, out WindowInstance window)
		{
			window = instances.Find(x => x.Properties.windowName == name);
			return window != null;
		}

		public bool GetWindow(WindowType type, out WindowInstance window)
		{
			window = instances.Find(x => x.Properties.windowType== type);
			return window != null;
		}

		public void CreateWindow(WindowInstance window, Transform parent, Action callback)
		{
			// если есть закешированное окно
			if (window.Window != null)
			{
				window.Window.transform.SetParent(parent);
				callback?.Invoke();
				return;
			}

			// загружаем из ассета
			window.Properties.assetReference.InstantiateAsync(parent, false).Completed += delegate (AsyncOperationHandle<GameObject> task)
			{
				window.Window = task.Result.GetComponent<BaseWindow>();
				callback?.Invoke();
			};
		}

		public void DestroyWindow(WindowInstance window)
		{
			if (!window.Properties.IsCached)
			{
				window.Destroy();
			}
		}
	}
}