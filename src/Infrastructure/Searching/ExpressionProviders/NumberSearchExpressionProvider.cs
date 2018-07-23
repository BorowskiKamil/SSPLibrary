using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SSPLibrary.Infrastructure
{
    public class NumberSearchExpressionProvider : DefaultSearchExpressionProvider
    {

        public override Expression GetComparison(MemberExpression left, string op, ConstantExpression right)
        {
            switch (op)
            {
                case SearchOperator.GreaterThan: return Expression.GreaterThan(left, right);
                case SearchOperator.GreaterThanOrEqual: return Expression.GreaterThanOrEqual(left, right);
                case SearchOperator.LessThan: return Expression.LessThan(left, right);
                case SearchOperator.LessThanOrEqual: return Expression.LessThanOrEqual(left, right);
                default: return base.GetComparison(left, op, right);
            }
        }

        public override ConstantExpression GetValue(string input, Type propertyType)
        {
            switch (Type.GetTypeCode(propertyType))
			{
				case TypeCode.Byte:
                    if (byte.TryParse(input, out var parsedByte))
                        return Expression.Constant(parsedByte);
                    break;
                case TypeCode.SByte:
                    if (sbyte.TryParse(input, out var parsedSByte))
                        return Expression.Constant(parsedSByte);
                    break;
				case TypeCode.UInt16:
                    if (UInt16.TryParse(input, out var parsedUShort))
                        return Expression.Constant(parsedUShort);
                    break;
				case TypeCode.UInt32:
                    if (UInt32.TryParse(input, out var parsedUInt))
                        return Expression.Constant(parsedUInt);
                    break;
				case TypeCode.UInt64:
                    if (UInt64.TryParse(input, out var parsedULong))
                        return Expression.Constant(parsedULong);
                    break;
				case TypeCode.Int16:
                    if (Int16.TryParse(input, out var parsedShort))
                        return Expression.Constant(parsedShort);
                    break;
				case TypeCode.Int32:
                    if (int.TryParse(input, out var parsedint))
                        return Expression.Constant(parsedint);
                    break;
				case TypeCode.Int64:
                    if (Int64.TryParse(input, out var parsedLong))
                        return Expression.Constant(parsedLong);
                    break;
				case TypeCode.Decimal:
                    if (decimal.TryParse(input, out var parsedDecimal))
                        return Expression.Constant(parsedDecimal);
                    break;
				case TypeCode.Double:
                    if (double.TryParse(input, out var parsedDouble))
                        return Expression.Constant(parsedDouble);
                    break;
				case TypeCode.Single:
                    if (float.TryParse(input, out var parsedFloat))
                        return Expression.Constant(parsedFloat);
                    break;
			}

            throw new ArgumentException("Invalid search value");
        }

    }
}
