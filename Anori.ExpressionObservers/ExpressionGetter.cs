using System;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using Anori.ExpressionObservers.Nodes;

namespace Anori.ExpressionObservers
{
    public static class ExpressionGetter
    {
       public static Func<TResult?> CreateValueGetter<TResult>(
            Expression<Func<TResult>> expression)
            where TResult : struct
        {
            var parameters = expression.Parameters;
            var body = ExpressionCreator.CreateValueBody(typeof(TResult?), expression.Body);
            var lambda = Expression.Lambda<Func<TResult?>>(body, parameters);
            return lambda.Compile();
        }

        public static Func<TResult?> CreateValueGetter<TResult>(
            ReadOnlyCollection<ParameterExpression> parameters, Tree tree)
            where TResult : struct
        {
            var body = ExpressionCreator.CreateValueBody(typeof(TResult?), tree);
            var lambda = Expression.Lambda<Func<TResult?>>(body, parameters);
            return lambda.Compile();
        }

        public static Func<TResult> CreateReferenceGetter<TResult>(
            ReadOnlyCollection<ParameterExpression> parameters, Tree tree)
            where TResult : class
        {
            var body = ExpressionCreator.CreateValueBody(typeof(TResult), tree);
            var lambda = Expression.Lambda<Func<TResult>>(body, parameters);
            return lambda.Compile();
        }

        public static Func<TResult> CreateValueGetter<TResult>(
            ReadOnlyCollection<ParameterExpression> parameters, Tree tree, TResult fallback)
            where TResult : struct
        {
            var body = ExpressionCreator.CreateValueBody(typeof(TResult), tree, Fallback(fallback));
            var lambda = Expression.Lambda<Func<TResult>>(body, parameters);
            return lambda.Compile();
        }

        public static Func<TParameter1, TResult> CreateValueGetter<TParameter1, TResult>(
            ReadOnlyCollection<ParameterExpression> parameters, Tree tree, TResult fallback)
            where TResult : struct
        {
            var body = ExpressionCreator.CreateValueBody(typeof(TResult), tree, Fallback(fallback));
            var lambda = Expression.Lambda<Func<TParameter1, TResult>>(body, parameters);
            return lambda.Compile();
        }

        public static Func<TResult> CreateReferenceGetter<TResult>(
            ReadOnlyCollection<ParameterExpression> parameters, Tree tree, TResult fallback)
            where TResult : class
        {
            var body = ExpressionCreator.CreateValueBody(typeof(TResult), tree, Fallback(fallback));
            var lambda = Expression.Lambda<Func<TResult>>(body, parameters);
            return lambda.Compile();
        }

        public static Func<TParameter, TResult?> CreateValueGetter<TParameter, TResult>(
            Expression<Func<TParameter, TResult>> expression)
            where TResult : struct
        {
            var parameters = expression.Parameters;
            var body = ExpressionCreator.CreateValueBody(typeof(TResult?), expression.Body);
            var lambda = Expression.Lambda<Func<TParameter, TResult?>>(body, parameters);
            return lambda.Compile();
        }

        public static Func<TParameter, TResult> CreateValueGetter<TParameter, TResult>(
            Expression<Func<TParameter, TResult>> expression, TResult fallback)
            where TResult : struct
        {
            var parameters = expression.Parameters;
            var body = ExpressionCreator.CreateValueBody(typeof(TResult), expression.Body, Fallback(fallback));
            var lambda = Expression.Lambda<Func<TParameter, TResult>>(body, parameters);
            return lambda.Compile();
        }

        public static Func<TResult> CreateValueGetter<TResult>(
            Expression<Func<TResult>> expression, TResult fallback)
            where TResult : struct
        {
            var parameters = expression.Parameters;
            var body = ExpressionCreator.CreateValueBody(typeof(TResult), expression.Body, Fallback(fallback));
            var lambda = Expression.Lambda<Func<TResult>>(body, parameters);
            return lambda.Compile();
        }

        public static Func<TParameter1, TParameter2, TResult?> CreateValueGetter<TParameter1, TParameter2, TResult>(
            Expression<Func<TParameter1, TParameter2, TResult>> expression)
            where TResult : struct
        {
            var parameters = expression.Parameters;
            var body = ExpressionCreator.CreateValueBody(typeof(TResult?), expression.Body);
            var lambda = Expression.Lambda<Func<TParameter1, TParameter2, TResult?>>(body, parameters);
            return lambda.Compile();
        }

        public static Func<TParameter1, TParameter2, TResult?> CreateValueGetter<TParameter1, TParameter2, TResult>(
            Expression<Func<TParameter1, TParameter2, TResult>> expression, TResult fallback)
            where TResult : struct
        {
            var parameters = expression.Parameters;
            var body = ExpressionCreator.CreateValueBody(typeof(TResult?), expression.Body, Fallback(fallback));
            var lambda = Expression.Lambda<Func<TParameter1, TParameter2, TResult?>>(body, parameters);
            return lambda.Compile();
        }

        public static Func<TParameter1, TParameter2, TParameter3, TResult?> CreateValueGetter<TParameter1, TParameter2,
            TParameter3, TResult>(
            Expression<Func<TParameter1, TParameter2, TParameter3, TResult>> expression)
            where TResult : struct
        {
            var parameters = expression.Parameters;
            var body = ExpressionCreator.CreateValueBody(typeof(TResult?), expression.Body);
            var lambda = Expression.Lambda<Func<TParameter1, TParameter2, TParameter3, TResult?>>(body, parameters);
            return lambda.Compile();
        }

        public static Func<TParameter1, TParameter2, TParameter3, TResult?> CreateValueGetter<TParameter1, TParameter2,
            TParameter3, TResult>(
            Expression<Func<TParameter1, TParameter2, TParameter3, TResult>> expression, TResult fallback)
            where TResult : struct
        {
            var parameters = expression.Parameters;
            var body = ExpressionCreator.CreateValueBody(typeof(TResult?), expression.Body, Fallback(fallback));
            var lambda = Expression.Lambda<Func<TParameter1, TParameter2, TParameter3, TResult?>>(body, parameters);
            return lambda.Compile();
        }

        public static Func<TParameter, TResult> CreateReferenceGetter<TParameter, TResult>(
            Expression<Func<TParameter, TResult>> expression)
            where TResult : class
        {
            var parameters = expression.Parameters;
            var body = ExpressionCreator.CreateValueBody(typeof(TResult), expression.Body);
            var lambda = Expression.Lambda<Func<TParameter, TResult>>(body, parameters);
            return lambda.Compile();
        }

        public static Func<TParameter, TResult> CreateReferenceGetter<TParameter, TResult>(
            Expression<Func<TParameter, TResult>> expression, TResult fallback)
            where TResult : class
        {
            var parameters = expression.Parameters;
            var body = ExpressionCreator.CreateValueBody(typeof(TResult), expression.Body, Fallback(fallback));
            var lambda = Expression.Lambda<Func<TParameter, TResult>>(body, parameters);
            return lambda.Compile();
        }

        public static Func<TParameter1, TParameter2, TResult> CreateReferenceGetter<TParameter1, TParameter2, TResult>(
            Expression<Func<TParameter1, TParameter2, TResult>> expression)
            where TResult : class
        {
            var parameters = expression.Parameters;
            var body = ExpressionCreator.CreateValueBody(typeof(TResult), expression.Body);
            var lambda = Expression.Lambda<Func<TParameter1, TParameter2, TResult>>(body, parameters);
            return lambda.Compile();
        }

        public static Func<TParameter1, TParameter2, TParameter3, TResult> CreateReferenceGetter<TParameter1,
            TParameter2, TParameter3, TResult>(
            Expression<Func<TParameter1, TParameter2, TParameter3, TResult>> expression)
            where TResult : class
        {
            var parameters = expression.Parameters;
            var body = ExpressionCreator.CreateValueBody(typeof(TResult), expression.Body);
            var lambda = Expression.Lambda<Func<TParameter1, TParameter2, TParameter3, TResult>>(body, parameters);
            return lambda.Compile();
        }

        public static Func<TParameter1, TParameter2, TParameter3, TResult> CreateReferenceGetter<TParameter1,
            TParameter2, TParameter3, TResult>(
            Expression<Func<TParameter1, TParameter2, TParameter3, TResult>> expression, TResult fallback)
            where TResult : class
        {
            var parameters = expression.Parameters;
            var body = ExpressionCreator.CreateValueBody(typeof(TResult), expression.Body, Fallback(fallback));
            var lambda = Expression.Lambda<Func<TParameter1, TParameter2, TParameter3, TResult>>(body, parameters);
            return lambda.Compile();
        }

        public static Func<TParameter1, TParameter2, object> CreateParameterGetter<TParameter1, TParameter2, TResult>(
            ParameterNode parameter, Expression<Func<TParameter1, TParameter2, TResult>> expression)
        {
            var parameters = expression.Parameters;
            var body = ExpressionCreator.CreateParameterBody(parameter, expression.Body);
            var lambda = Expression.Lambda<Func<TParameter1, TParameter2, object>>(body, parameters);
            return lambda.Compile();
        }


        private static Expression Fallback<TFallback>(TFallback fallback) =>
            Expression.Constant(fallback, typeof(TFallback));
    }
}