using System;
using System.ComponentModel;
using System.Linq.Expressions;
using Anorisoft.PropertyObservers.ReferenceObservers;
using JetBrains.Annotations;

namespace Anorisoft.PropertyObservers
{
    public static class PropertyReferenceObserver
    {
        [NotNull]
        public static PropertyReferenceObserver<TResult> Observes<TResult>(
            [NotNull] Expression<Func<TResult>> propertyExpression,
            [NotNull] Action action)
            where TResult : class =>
            new PropertyReferenceObserver<TResult>(propertyExpression, action);

        [NotNull]
        public static PropertyReferenceObserverAndGetter<TResult> ObservesAndGet<TResult>(
            [NotNull] Expression<Func<TResult>> propertyExpression,
            [NotNull] Action action)
            where TResult : class =>
            new PropertyReferenceObserverAndGetter<TResult>(propertyExpression, action);

        [NotNull]
        public static PropertyReferenceObserverWithGetterAndFallback<TResult> ObservesAndGet<TResult>(
            [NotNull] Expression<Func<TResult>> propertyExpression,
            [NotNull] Action action, TResult fallback)
            where TResult : class =>
            new PropertyReferenceObserverWithGetterAndFallback<TResult>(propertyExpression, action, fallback);

        [NotNull]
        public static PropertyReferenceGetterObserver<TResult> Observes<TResult>(
            [NotNull] Expression<Func<TResult>> propertyExpression,
            [NotNull] Action<TResult> action)
            where TResult : class =>
            new PropertyReferenceGetterObserver<TResult>(propertyExpression, action);

        [NotNull]
        public static PropertyReferenceObserver<TParameter1, TResult> Observes<TParameter1, TResult>(
            [NotNull] TParameter1 parameter1,
            [NotNull] Expression<Func<TParameter1, TResult>> propertyExpression,
            [NotNull] Action action)
            where TParameter1 : INotifyPropertyChanged
            where TResult : class =>
            new PropertyReferenceObserver<TParameter1, TResult>(parameter1, propertyExpression, action);

        [NotNull]
        public static PropertyReferenceGetterObserver<TParameter1, TResult> Observes<TParameter1, TResult>(
            [NotNull] TParameter1 parameter1,
            [NotNull] Expression<Func<TParameter1, TResult>> propertyExpression,
            [NotNull] Action<TResult> action)
            where TParameter1 : INotifyPropertyChanged
            where TResult : class =>
            new PropertyReferenceGetterObserver<TParameter1, TResult>(parameter1, propertyExpression, action);

        [NotNull]
        public static PropertyReferenceObserver<TParameter1, TParameter2, TResult> Observes<TParameter1, TParameter2,
            TResult>(
            [NotNull] TParameter1 parameter1,
            [NotNull] TParameter2 parameter2,
            [NotNull] Expression<Func<TParameter1, TParameter2, TResult>> propertyExpression,
            [NotNull] Action action)
            where TParameter1 : INotifyPropertyChanged
            where TParameter2 : INotifyPropertyChanged
            where TResult : class =>
            new PropertyReferenceObserver<TParameter1, TParameter2, TResult>(parameter1, parameter2, propertyExpression,
                action);

        [NotNull]
        public static PropertyReferenceGetterObserver<TParameter1, TParameter2, TResult> Observes<TParameter1,
            TParameter2,
            TResult>(
            [NotNull] TParameter1 parameter1,
            [NotNull] TParameter2 parameter2,
            [NotNull] Expression<Func<TParameter1, TParameter2, TResult>> propertyExpression,
            [NotNull] Action<TResult> action)
            where TParameter1 : INotifyPropertyChanged
            where TParameter2 : INotifyPropertyChanged
            where TResult : class =>
            new PropertyReferenceGetterObserver<TParameter1, TParameter2, TResult>(parameter1, parameter2,
                propertyExpression,
                action);
    }
}