using System;
using Configs;

namespace Common.Windows
{
	public class WindowInstance : IComparable
	{
		private BaseWindow instance;

		public BaseWindow Window
		{
			get { return instance; }
			set { instance = value; }
		}

		private WindowProperties properties;

		public WindowProperties Properties
		{
			get { return properties; }
			set { properties = value; }
		}

		public WindowInstance(WindowProperties properties)
		{
			this.Properties = properties;
		}

		public void Destroy()
		{
			properties.assetReference.ReleaseInstance(instance.gameObject);
			instance = null;
		}

		public int CompareTo(object obj)
		{
			WindowInstance w = obj as WindowInstance;
			return properties.priority.CompareTo(w.properties.priority);
		}
	}
}