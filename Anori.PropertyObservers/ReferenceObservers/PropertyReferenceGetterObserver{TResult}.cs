using System;
using System.Linq.Expressions;
using Anori.ExpressionObservers;
using Anori.PropertyObservers.Common;
using JetBrains.Annotations;

namespace Anori.PropertyObservers.ReferenceObservers
{
    public sealed class PropertyReferenceGetterObserver<TResult> : PropertyObserverBase
        where TResult : class
    {
        /// <summary>
        /// The action
        /// </summary>
        [NotNull] private readonly Action<TResult> action;

        /// <summary>
        /// The getter
        /// </summary>
        [NotNull] private readonly Func<TResult> getter;

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyReferenceGetterObserver{TResult}"/> class.
        /// </summary>
        /// <param name="propertyExpression">The property expression.</param>
        /// <param name="action">The action.</param>
        /// <exception cref="ArgumentNullException">
        /// action
        /// or
        /// propertyExpression
        /// </exception>
        internal PropertyReferenceGetterObserver(
            [NotNull] Expression<Func<TResult>> propertyExpression,
            [NotNull] Action<TResult> action)
        {
            this.action = action ?? throw new ArgumentNullException(nameof(action));
            propertyExpression = propertyExpression ?? throw new ArgumentNullException(nameof(propertyExpression));
            var tree = ExpressionTree.GetTree(propertyExpression.Body);
            ExpressionString = propertyExpression.ToString();
            base.CreateChain(tree);
            this.getter = ExpressionGetter.CreateReferenceGetter<TResult>(propertyExpression.Parameters, tree);
        }

        /// <summary>
        /// The expression
        /// </summary>
        public override string ExpressionString { get; }

        /// <summary>
        /// The action
        /// </summary>
        protected override void OnAction() => action(getter());

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <returns></returns>
        public TResult GetValue() => getter();
    }
}