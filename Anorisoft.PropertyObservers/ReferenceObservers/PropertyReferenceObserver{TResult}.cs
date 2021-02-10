using System;
using System.Linq.Expressions;
using JetBrains.Annotations;

namespace Anorisoft.PropertyObservers.ReferenceObservers
{
    public sealed class PropertyReferenceObserver<TResult> : PropertyReferenceObserverBase<TResult>
        where TResult : class
    {
        /// <summary>
        /// Gets the action.
        /// </summary>
        /// <value>
        /// The action.
        /// </value>
        [NotNull] public Action Action { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyReferenceObserver{TResult}"/> class.
        /// </summary>
        /// <param name="propertyExpression">The property expression.</param>
        /// <param name="action">The action.</param>
        /// <exception cref="ArgumentNullException">action</exception>
        internal PropertyReferenceObserver(
            [NotNull] Expression<Func<TResult>> propertyExpression,
            [NotNull] Action action) : base(propertyExpression) =>
            Action = action ?? throw new ArgumentNullException(nameof(action));

        /// <summary>
        /// The action
        /// </summary>
        protected override void OnAction() => Action();
    }
}