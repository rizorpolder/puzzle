using System.Collections.Generic;

namespace Analytics.Core.Runtime
{
	public interface IAnalyticData
	{
		string Event { get; }
		Dictionary<string, object> ToDictionary(AnalyticType analyticType);
	}
}