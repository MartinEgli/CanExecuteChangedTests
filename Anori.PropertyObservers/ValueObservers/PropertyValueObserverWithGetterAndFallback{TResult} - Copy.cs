using System;
using System.ComponentModel;
using System.Linq.Expressions;
using Anori.ExpressionObservers;
using Anori.PropertyObservers.Common;
using JetBrains.Annotations;

namespace Anori.PropertyObservers.ValueObservers
{
    /// <summary>
    /// Property Value Observer With Getter And Fallback
    /// </summary>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <seealso cref="PropertyObserverBase" />
    public sealed class PropertyValueObserverWithGetterAndFallback<TParameter1, TResult> : PropertyObserverBase
        where TResult : struct 
        where TParameter1 : INotifyPropertyChanged
    {
        public TParameter1 Parameter { get; }

        /// <summary>
        /// The action
        /// </summary>
        [NotNull] private readonly Action action;
        /// <summary>
        /// The getter
        /// </summary>
        [NotNull] private readonly Func<TParameter1, TResult> getter;

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyValueObserverWithGetterAndFallback{TResult}"/> class.
        /// </summary>
        /// <param name="propertyExpression">The property expression.</param>
        /// <param name="action">The action.</param>
        /// <param name="fallback">The fallback.</param>
        /// <exception cref="ArgumentNullException">
        /// action
        /// or
        /// propertyExpression
        /// </exception>
        internal PropertyValueObserverWithGetterAndFallback(
            [NotNull] Expression<Func<TParameter1, TResult>> propertyExpression,
            [NotNull] TParameter1 parameter,
            [NotNull] Action action, TResult fallback)
        {
            Parameter = parameter ?? throw new ArgumentNullException(nameof(parameter));
            this.action = action ?? throw new ArgumentNullException(nameof(action));
            propertyExpression = propertyExpression ?? throw new ArgumentNullException(nameof(propertyExpression));
            var tree = ExpressionTree.GetTree(propertyExpression.Body);
            ExpressionString = propertyExpression.ToString();

            base.CreateChain(parameter, tree);
            this.getter = ExpressionGetter.CreateValueGetter<TParameter1, TResult>(propertyExpression.Parameters, tree, fallback);
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
        public TResult GetValue(TParameter1 p) => getter(p);
    }
}