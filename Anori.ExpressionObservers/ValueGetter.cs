using System;
using System.Linq.Expressions;

namespace Anori.ExpressionObservers
{
    public static class ValueGetter
    {
        public static Func<TParameter, TResult?> CreateGetter<TParameter, TResult>(
            Expression<Func<TParameter, TResult>> propertyExpression)
            where TResult : struct
        {
            return ExpressionGetter.CreateValueGetter(propertyExpression);
        }
    }
}