using System;
using System.ComponentModel;
using System.Linq.Expressions;
using Anorisoft.ExpressionObservers;
using JetBrains.Annotations;

namespace Anorisoft.PropertyObservers
{
    public sealed class PropertyGetterObserver<TParameter1, TResult> : PropertyObserverBase<TParameter1, TResult> where TParameter1 : INotifyPropertyChanged
        where TResult : struct
    {
        [NotNull] private readonly Action<TResult?> action;
        [NotNull] private readonly Func<TParameter1, TResult?> getter;

        internal PropertyGetterObserver(
            [NotNull] TParameter1 parameter1,
            [NotNull] Expression<Func<TParameter1, TResult>> propertyExpression,
            [NotNull] Action<TResult?> action) : base(parameter1, propertyExpression)
        {
            this.action = action ?? throw new ArgumentNullException(nameof(action));
            this.getter = ExpressionGetter.CreateValueGetter(propertyExpression);
        }


        protected override void OnAction() => action(getter(Parameter1));

    }
}