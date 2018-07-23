using System;
using System.Linq.Expressions;

namespace SSPLibrary.Infrastructure
{
	public class StringSearchExpressionProvider : DefaultSearchExpressionProvider
	{

		public override Expression GetComparison(MemberExpression left, string op, ConstantExpression right)
        {
            switch (op)
            {
                case SearchOperator.Contains:
				{
					var call = Expression.Call(left, typeof(string).GetMethod("IndexOf", new[] { typeof(string), typeof(StringComparison) }), right, Expression.Constant(StringComparison.Ordinal));			
					return Expression.NotEqual(call, Expression.Constant(-1));
				}
				case SearchOperator.ContainsCaseInsensitive:
				{
					var call = Expression.Call(left, typeof(string).GetMethod("IndexOf", new[] { typeof(string), typeof(StringComparison) }), right, Expression.Constant(StringComparison.OrdinalIgnoreCase));
					return Expression.NotEqual(call, Expression.Constant(-1));
				}
				case SearchOperator.EqualCaseInsensitive:
				{
					var call = Expression.Call(left, 
						typeof(string).GetMethod("Equals", new[] { typeof(string), typeof(StringComparison) }), 
						right, 
						Expression.Constant(StringComparison.OrdinalIgnoreCase));

					return Expression.Equal(call, Expression.Constant(true));

				}
				case SearchOperator.NotEqualCaseInsensitive:
				{
					var call = Expression.Call(left, 
						typeof(string).GetMethod("Equals", new[] { typeof(string), typeof(StringComparison) }), 
						right, 
						Expression.Constant(StringComparison.OrdinalIgnoreCase));

					return Expression.NotEqual(call, Expression.Constant(true));
				}
                default: 
					return base.GetComparison(left, op, right);
            }
        }

	}
}