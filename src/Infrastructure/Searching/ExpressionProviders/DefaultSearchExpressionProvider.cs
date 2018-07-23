using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SSPLibrary.Infrastructure
{
    public class DefaultSearchExpressionProvider : ISearchExpressionProvider
    {

        public virtual Expression GetComparison(MemberExpression left, string op, ConstantExpression right)
        {
            if (op.Equals(SearchOperator.Equal, StringComparison.OrdinalIgnoreCase))
                return Expression.Equal(left, right);

            if (op.Equals(SearchOperator.NotEqual, StringComparison.OrdinalIgnoreCase))
                return Expression.NotEqual(left, right);

            throw new ArgumentException($"Invalid oprator {op}");
        }

        public virtual ConstantExpression GetValue(string input, Type propertyType)
            => Expression.Constant(input);
    }
}
