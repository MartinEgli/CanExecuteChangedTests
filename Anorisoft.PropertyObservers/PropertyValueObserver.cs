using System;
using System.ComponentModel;
using System.Linq.Expressions;
using Anorisoft.PropertyObservers.ValueObservers;
using JetBrains.Annotations;

namespace Anorisoft.PropertyObservers
{


    public static class PropertyObserver
    {
        [NotNull]
        public static PropertyObserver<TResult> Observes<TResult>(
            [NotNull] Expression<Func<TResult>> propertyExpression,
            [NotNull] Action action) =>
            new PropertyObserver<TResult>(propertyExpression, action);

        [NotNull]
        public static PropertyObserver<TParameter1, TResult> Observes<TParameter1, TResult>(
            [NotNull] TParameter1 parameter1,
            [NotNull] Expression<Func<TParameter1, TResult>> propertyExpression,
            [NotNull] Action action)
            where TParameter1 : INotifyPropertyChanged =>
            new PropertyObserver<TParameter1, TResult>(parameter1, propertyExpression, action);

        [NotNull]
        public static PropertyObserver<TParameter1, TParameter2, TResult> Observes<TParameter1, TParameter2, TResult>(
            [NotNull] TParameter1 parameter1,
            [NotNull] TParameter2 parameter2,
            [NotNull] Expression<Func<TParameter1, TParameter2, TResult>> propertyExpression,
            [NotNull] Action action)
            where TParameter1 : INotifyPropertyChanged
            where TParameter2 : INotifyPropertyChanged =>
            new PropertyObserver<TParameter1, TParameter2, TResult>(parameter1, parameter2, propertyExpression, action);

    }

    public static class PropertyValueObserver
    {
        [NotNull]
        public static PropertyValueObserver<TResult> Observes<TResult>(
            [NotNull] Expression<Func<TResult>> propertyExpression,
            [NotNull] Action action)
            where TResult : struct =>
            new PropertyValueObserver<TResult>(propertyExpression, action);

        [NotNull]
        public static PropertyValueObserverAndGetter<TResult> ObservesAndGet<TResult>(
            [NotNull] Expression<Func<TResult>> propertyExpression,
            [NotNull] Action action)
            where TResult : struct =>
            new PropertyValueObserverAndGetter<TResult>(propertyExpression, action);

        [NotNull]
        public static PropertyValueObserverWithGetterAndFallback<TResult> ObservesAndGet<TResult>(
            [NotNull] Expression<Func<TResult>> propertyExpression,
            [NotNull] Action action, TResult fallback)
            where TResult : struct =>
            new PropertyValueObserverWithGetterAndFallback<TResult>(propertyExpression, action, fallback);


        [NotNull]
        public static PropertyValueObserverWithGetterAndFallback<TParameter1, TResult> ObservesAndGet<TParameter1, TResult>(
            [NotNull] Expression<Func<TParameter1, TResult>> propertyExpression,
            [NotNull] TParameter1 parameter,
            [NotNull] Action action, TResult fallback)
            where TResult : struct =>
            new PropertyValueObserverWithGetterAndFallback<TParameter1, TResult>(propertyExpression, parameter, action, fallback);

        [NotNull]
        public static PropertyValueGetterObserver<TResult> Observes<TResult>(
            [NotNull] Expression<Func<TResult>> propertyExpression,
            [NotNull] Action<TResult?> action)
            where TResult : struct =>
            new PropertyValueGetterObserver<TResult>(propertyExpression, action);

        [NotNull]
        public static PropertyValueObserver<TParameter1, TResult> Observes<TParameter1, TResult>(
            [NotNull] TParameter1 parameter1,
            [NotNull] Expression<Func<TParameter1, TResult>> propertyExpression,
            [NotNull] Action action)
            where TParameter1 : INotifyPropertyChanged
            where TResult : struct =>
            new PropertyValueObserver<TParameter1, TResult>(parameter1, propertyExpression, action);

        [NotNull]
        public static PropertyValueGetterObserver<TParameter1, TResult> Observes<TParameter1, TResult>(
            [NotNull] TParameter1 parameter1,
            [NotNull] Expression<Func<TParameter1, TResult>> propertyExpression,
            [NotNull] Action<TResult?> action)
            where TParameter1 : INotifyPropertyChanged
            where TResult : struct =>
            new PropertyValueGetterObserver<TParameter1, TResult>(parameter1, propertyExpression, action);

        [NotNull]
        public static PropertyValueObserver<TParameter1, TParameter2, TResult> Observes<TParameter1, TParameter2,
            TResult>(
            [NotNull] TParameter1 parameter1,
            [NotNull] TParameter2 parameter2,
            [NotNull] Expression<Func<TParameter1, TParameter2, TResult>> propertyExpression,
            [NotNull] Action action)
            where TParameter1 : INotifyPropertyChanged
            where TParameter2 : INotifyPropertyChanged
            where TResult : struct =>
            new PropertyValueObserver<TParameter1, TParameter2, TResult>(parameter1, parameter2, propertyExpression,
                action);

        [NotNull]
        public static PropertyValueGetterObserver<TParameter1, TParameter2, TResult> Observes<TParameter1, TParameter2,
            TResult>(
            [NotNull] TParameter1 parameter1,
            [NotNull] TParameter2 parameter2,
            [NotNull] Expression<Func<TParameter1, TParameter2, TResult>> propertyExpression,
            [NotNull] Action<TResult?> action)
            where TParameter1 : INotifyPropertyChanged
            where TParameter2 : INotifyPropertyChanged
            where TResult : struct =>
            new PropertyValueGetterObserver<TParameter1, TParameter2, TResult>(parameter1, parameter2,
                propertyExpression,
                action);
    }
}