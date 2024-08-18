using System.Collections.Generic;

namespace Analytics.Core.Runtime
{
	public interface IAnalyticData
	{
		Dictionary<string, object> ToDictionary(AnalyticType analyticType);
		string Event { get; }
	}
}