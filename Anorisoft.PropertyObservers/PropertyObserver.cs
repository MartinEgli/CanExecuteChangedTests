using System;
using System.ComponentModel;
using System.Linq.Expressions;
using JetBrains.Annotations;

namespace Anorisoft.PropertyObservers
{
    public static class PropertyObserver
    {
        [NotNull]
        public static PropertyObserver<TResult> Observes<TResult>(
            [NotNull] Expression<Func<TResult>> propertyExpression,
            [NotNull] Action action)
            where TResult : struct =>
            new PropertyObserver<TResult>(propertyExpression, action);

        [NotNull]
        public static PropertyGetterObserver<TResult> Observes<TResult>(
            [NotNull] Expression<Func<TResult>> propertyExpression,
            [NotNull] Action<TResult?> action)
            where TResult : struct =>
            new PropertyGetterObserver<TResult>(propertyExpression, action);


        [NotNull]
        public static PropertyObserver<TParameter1, TResult> Observes<TParameter1, TResult>(
            [NotNull] TParameter1 parameter1,
            [NotNull] Expression<Func<TParameter1, TResult>> propertyExpression,
            [NotNull] Action action)
            where TParameter1 : INotifyPropertyChanged
            where TResult : struct =>
            new PropertyObserver<TParameter1, TResult>(parameter1, propertyExpression, action);

        [NotNull]
        public static PropertyGetterObserver<TParameter1, TResult> Observes<TParameter1, TResult>(
            [NotNull] TParameter1 parameter1,
            [NotNull] Expression<Func<TParameter1, TResult>> propertyExpression,
            [NotNull] Action<TResult?> action)
            where TParameter1 : INotifyPropertyChanged
            where TResult : struct =>
            new PropertyGetterObserver<TParameter1, TResult>(parameter1, propertyExpression, action);

        [NotNull]
        public static PropertyObserver<TParameter1, TParameter2, TResult> Observes<TParameter1, TParameter2, TResult>(
            [NotNull] TParameter1 parameter1,
            [NotNull] TParameter2 parameter2,
            [NotNull] Expression<Func<TParameter1, TParameter2, TResult>> propertyExpression,
            [NotNull] Action action)
            where TParameter1 : INotifyPropertyChanged
            where TParameter2 : INotifyPropertyChanged
            where TResult : struct =>
            new PropertyObserver<TParameter1, TParameter2, TResult>(parameter1, parameter2, propertyExpression, action);

        [NotNull]
        public static PropertyGetterObserver<TParameter1, TParameter2, TResult> Observes<TParameter1, TParameter2,
            TResult>(
            [NotNull] TParameter1 parameter1,
            [NotNull] TParameter2 parameter2,
            [NotNull] Expression<Func<TParameter1, TParameter2, TResult>> propertyExpression,
            [NotNull] Action<TResult?> action)
            where TParameter1 : INotifyPropertyChanged
            where TParameter2 : INotifyPropertyChanged
            where TResult : struct =>
            new PropertyGetterObserver<TParameter1, TParameter2, TResult>(parameter1, parameter2, propertyExpression,
                action);
    }
}