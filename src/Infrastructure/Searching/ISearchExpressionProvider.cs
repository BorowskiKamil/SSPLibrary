using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SSPLibrary.Infrastructure
{
    public interface ISearchExpressionProvider
    {
        ConstantExpression GetValue(string input, Type propertyType = null);

        Expression GetComparison(MemberExpression left, string op, ConstantExpression right);
    }
}
