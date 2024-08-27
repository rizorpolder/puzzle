using System;
using Configs;
using UI.Common;

namespace Common.Windows
{
	public class WindowInstance : IComparable
	{
		public WindowInstance(WindowProperties properties)
		{
			Properties = properties;
		}

		public BaseWindow Window { get; set; }

		public WindowProperties Properties { get; set; }

		public int CompareTo(object obj)
		{
			var w = obj as WindowInstance;
			return Properties.priority.CompareTo(w.Properties.priority);
		}

		public void Destroy()
		{
			Properties.assetReference.ReleaseInstance(Window.gameObject);
			Window = null;
		}
	}
}