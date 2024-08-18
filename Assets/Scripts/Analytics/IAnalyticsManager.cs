using System.Collections.Generic;

namespace Analytics.Core.Runtime
{
	public interface IAnalyticManager
	{
		void Initialize();
		public void SendEvent(IAnalyticData data);
		public void SendEventRevenue(RevenueData product, Dictionary<string, object> param = null);
		public void SendEventsBuffer();
	}
}