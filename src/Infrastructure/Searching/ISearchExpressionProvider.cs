using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SSPLibrary.Infrastructure
{
    public interface ISearchExpressionProvider
    {
        IEnumerable<string> GetOperators();

        ConstantExpression GetValue(string input);

        Expression GetComparison(MemberExpression left, string op, ConstantExpression right);
    }
}
