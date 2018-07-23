using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace SSPLibrary.Infrastructure
{
	public static class SearchOperator
	{

		public const string Equal = "==";
		public const string EqualCaseInsensitive = "==*";
		public const string NotEqual = "!=";
		public const string NotEqualCaseInsensitive = "!=*";
		public const string Contains = "?";
		public const string ContainsCaseInsensitive = "?*";
		public const string GreaterThan = ">";
		public const string LessThan = "<";
		public const string GreaterThanOrEqual = ">=";
		public const string LessThanOrEqual = "<=";


		public static Dictionary<string, string> GetAllOperators()
		{
			return typeof(SearchOperator).GetFields(BindingFlags.Public | BindingFlags.Static |
               BindingFlags.FlattenHierarchy)
				.Where(fi => fi.IsLiteral && !fi.IsInitOnly)
				.ToDictionary(x => x.Name, x => x.GetValue(x).ToString());
		}

	}
}