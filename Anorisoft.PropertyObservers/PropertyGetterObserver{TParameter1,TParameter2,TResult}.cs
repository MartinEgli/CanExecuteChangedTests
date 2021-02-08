using System;
using System.ComponentModel;
using System.Linq.Expressions;
using Anorisoft.ExpressionObservers;
using JetBrains.Annotations;

namespace Anorisoft.PropertyObservers
{
    public sealed class PropertyGetterObserver<TParameter1, TParameter2, TResult> : PropertyObserverBase<TParameter1, TParameter2, TResult> where TParameter1 : INotifyPropertyChanged
        where TParameter2 : INotifyPropertyChanged
        where TResult : struct
    {
        private readonly Func<TParameter1, TParameter2, TResult?> getter;
        [NotNull] public Action<TResult?> Action { get; }

        /// </exception>
        internal PropertyGetterObserver(
            [NotNull] TParameter1 parameter1,
            [NotNull] TParameter2 parameter2,
            [NotNull] Expression<Func<TParameter1, TParameter2, TResult>> propertyExpression,
            [NotNull] Action<TResult?> action) : base(parameter1, parameter2, propertyExpression)
        {
            Action = action ?? throw new ArgumentNullException(nameof(action));
            getter = ExpressionGetter.CreateValueGetter(propertyExpression);
        }

        protected override void OnAction() => Action(getter(Parameter1, Parameter2));
    }
}