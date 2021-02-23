using System;
using System.ComponentModel;
using System.Linq.Expressions;
using Anori.ExpressionObservers;
using Anori.PropertyObservers.ValueObservers;
using JetBrains.Annotations;

namespace Anori.PropertyObservers.ReferenceObservers
{
    public sealed class PropertyReferenceGetterObserver<TParameter1, TParameter2, TResult> :
        PropertyReferenceObserverBase<TParameter1, TParameter2, TResult> 
        where TParameter1 : INotifyPropertyChanged
        where TParameter2 : INotifyPropertyChanged
        where TResult : class
    {
        /// <summary>
        /// The getter
        /// </summary>
        private readonly Func<TParameter1, TParameter2, TResult> getter;
        /// <summary>
        /// Gets the action.
        /// </summary>
        /// <value>
        /// The action.
        /// </value>
        [NotNull] public Action<TResult> Action { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyValueGetterObserver{TParameter1,TParameter2,TResult}"/> class.
        /// </summary>
        /// <param name="parameter1">The parameter1.</param>
        /// <param name="parameter2">The parameter2.</param>
        /// <param name="propertyExpression">The property expression.</param>
        /// <param name="action">The action.</param>
        /// <exception cref="ArgumentNullException">action</exception>
        internal PropertyReferenceGetterObserver(
            [NotNull] TParameter1 parameter1,
            [NotNull] TParameter2 parameter2,
            [NotNull] Expression<Func<TParameter1, TParameter2, TResult>> propertyExpression,
            [NotNull] Action<TResult> action) : base(parameter1, parameter2, propertyExpression)
        {
            Action = action ?? throw new ArgumentNullException(nameof(action));
            getter = ExpressionGetter.CreateReferenceGetter(propertyExpression);
        }

        /// <summary>
        /// The action
        /// </summary>
        protected override void OnAction() => Action(getter(Parameter1, Parameter2));
    }
}