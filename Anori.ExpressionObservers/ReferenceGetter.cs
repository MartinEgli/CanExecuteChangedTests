using System;
using System.Linq.Expressions;

namespace Anori.ExpressionObservers
{
    public static class ReferenceGetter
    {
        public static Func<TParameter, TResult> CreateGetter<TParameter, TResult>(
            Expression<Func<TParameter, TResult>> propertyExpression)
            where TResult : class
        {
            return ExpressionGetter.CreateReferenceGetter(propertyExpression);
        }
    }
}