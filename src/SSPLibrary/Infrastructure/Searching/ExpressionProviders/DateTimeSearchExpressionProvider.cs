using System;
using System.Globalization;
using System.Linq.Expressions;

namespace SSPLibrary.Infrastructure
{
    public class DateTimeSearchExpressionProvider : ComparableSearchExpressionProvider
    {

        public override ConstantExpression GetValue(string input, Type propertyType)
        {
            if (!DateTime.TryParse(input, out var parsedDate))
                throw new ArgumentException("Invalid search value");

            return Expression.Constant(parsedDate);
        }

    }
}
