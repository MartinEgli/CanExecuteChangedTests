using System;
using System.Linq.Expressions;
using Anori.ExpressionObservers;
using Anori.PropertyObservers.Common;
using JetBrains.Annotations;

namespace Anori.PropertyObservers.ValueObservers
{
    public sealed class PropertyValueObserverAndGetter<TResult> : PropertyObserverBase
        where TResult : struct
    {
        /// <summary>
        /// The action
        /// </summary>
        [NotNull] private readonly Action action;
        /// <summary>
        /// The getter
        /// </summary>
        [NotNull] private readonly Func<TResult?> getter;

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyValueObserverAndGetter{TResult}"/> class.
        /// </summary>
        /// <param name="propertyExpression">The property expression.</param>
        /// <param name="action">The action.</param>
        /// <exception cref="ArgumentNullException">
        /// action
        /// or
        /// propertyExpression
        /// </exception>
        internal PropertyValueObserverAndGetter(
            [NotNull] Expression<Func<TResult>> propertyExpression,
            [NotNull] Action action)
        {
            this.action = action ?? throw new ArgumentNullException(nameof(action));
            propertyExpression = propertyExpression ?? throw new ArgumentNullException(nameof(propertyExpression));
            var tree = ExpressionTree.GetTree(propertyExpression.Body);
            ExpressionString = propertyExpression.ToString();

            base.CreateChain(tree);
            this.getter = ExpressionGetter.CreateValueGetter<TResult>(propertyExpression.Parameters, tree);
        }

        /// <summary>
        /// The action
        /// </summary>
        protected override void OnAction() => action();


        /// <summary>
        /// The expression
        /// </summary>
        public override string ExpressionString { get; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <returns></returns>
        public TResult? GetValue() => getter();
    }
}