using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace SSPLibrary.Infrastructure
{
	public static class SearchOperator
	{

		public const string Equal = "==";

		public const string NotEqual = "!=";

		public static Dictionary<string, string> GetAllOperators()
		{
			return typeof(SearchOperator).GetFields(BindingFlags.Public | BindingFlags.Static |
               BindingFlags.FlattenHierarchy)
				.Where(fi => fi.IsLiteral && !fi.IsInitOnly)
				.ToDictionary(x => x.Name, x => x.GetValue(x).ToString());
		}

	}
}