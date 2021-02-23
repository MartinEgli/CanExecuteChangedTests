using System;
using System.ComponentModel;
using System.Linq.Expressions;
using Anori.ExpressionObservers;
using JetBrains.Annotations;

namespace Anori.PropertyObservers.ValueObservers
{
    public sealed class PropertyValueGetterObserver<TParameter1, TResult> : PropertyValueObserverBase<TParameter1, TResult>
        where TParameter1 : INotifyPropertyChanged
        where TResult : struct
    {
        /// <summary>
        /// The action
        /// </summary>
        [NotNull] private readonly Action<TResult?> action;

        /// <summary>
        /// The getter
        /// </summary>
        [NotNull] private readonly Func<TParameter1, TResult?> getter;

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyValueGetterObserver{TParameter1, TResult}"/> class.
        /// </summary>
        /// <param name="parameter1">The parameter1.</param>
        /// <param name="propertyExpression">The property expression.</param>
        /// <param name="action">The action.</param>
        /// <exception cref="ArgumentNullException">action</exception>
        internal PropertyValueGetterObserver(
            [NotNull] TParameter1 parameter1,
            [NotNull] Expression<Func<TParameter1, TResult>> propertyExpression,
            [NotNull] Action<TResult?> action) : base(parameter1, propertyExpression)
        {
            this.action = action ?? throw new ArgumentNullException(nameof(action));
            this.getter = ExpressionGetter.CreateValueGetter(propertyExpression);
        }

        /// <summary>
        /// The action
        /// </summary>
        protected override void OnAction() => action(getter(Parameter1));
    }
}