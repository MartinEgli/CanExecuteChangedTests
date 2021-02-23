using System;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Anori.ExpressionObservers.ConsoleTests
{
    public static class DebugExpression
    {
        public static Expression WriteLine(ConstantExpression expression)
        {
            return Expression.Call(typeof(Debug).GetMethod("WriteLine", new Type[]
            {
                typeof(string)
            }), expression);
        }

        public static Expression WriteLine(ConstantExpression expression1, ConstantExpression expression2)
        {
            return Expression.Call(typeof(Debug).GetMethod("WriteLine", new Type[]
            {
                typeof(string)
            }), Expression.Call(typeof(string).GetMethod("Concat", new[]
            {
                typeof(string), typeof(string)
            }), expression1, expression2));
        }
    }
}