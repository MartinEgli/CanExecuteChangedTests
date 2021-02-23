using System;
using System.ComponentModel;
using System.Linq.Expressions;
using Anori.ExpressionObservers;
using Anori.PropertyObservers.ValueObservers;
using JetBrains.Annotations;

namespace Anori.PropertyObservers.ReferenceObservers
{
    public sealed class PropertyReferenceGetterObserver<TParameter1, TResult> : PropertyReferenceObserverBase<TParameter1, TResult> where TParameter1 : INotifyPropertyChanged
        where TResult : class
    {
        /// <summary>
        /// The action
        /// </summary>
        [NotNull] private readonly Action<TResult> action;
        /// <summary>
        /// The getter
        /// </summary>
        [NotNull] private readonly Func<TParameter1, TResult> getter;

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyValueGetterObserver{TParameter1,TResult}"/> class.
        /// </summary>
        /// <param name="propertyExpression">The parameter1.</param>
        /// <param name="action">The property expression.</param>
        /// <param name="action">The action.</param>
        /// <exception cref="PropertyValueGetterObserver{TParameter1,TResult}">action</exception>
        internal PropertyReferenceGetterObserver(
            [NotNull] TParameter1 parameter1,
            [NotNull] Expression<Func<TParameter1, TResult>> propertyExpression,
            [NotNull] Action<TResult> action) : base(parameter1, propertyExpression)
        {
            this.action = action ?? throw new ArgumentNullException(nameof(action));
            this.getter = ExpressionGetter.CreateReferenceGetter(propertyExpression);
        }


        /// <summary>
        /// The action
        /// </summary>
        protected override void OnAction() => action(getter(Parameter1));

    }
}