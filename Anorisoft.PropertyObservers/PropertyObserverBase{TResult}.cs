using System;
using System.Linq.Expressions;
using Anorisoft.ExpressionObservers;
using JetBrains.Annotations;

namespace Anorisoft.PropertyObservers
{
    public abstract class PropertyObserverBase<TResult> : PropertyObserverBase
        where TResult : struct
    {
        /// <summary>
        ///     The property expression
        /// </summary>
        private readonly Expression<Func<TResult>> propertyExpression;

        protected PropertyObserverBase(
            [NotNull] Expression<Func<TResult>> propertyExpression)
        {
            this.propertyExpression = propertyExpression ?? throw new ArgumentNullException(nameof(propertyExpression));
            this.ExpressionString = this.CreateChain();
        }

        /// <summary>
        ///     The expression
        /// </summary>
        public string ExpressionString { get; }

        /// <summary>
        /// Creates the chain.
        /// </summary>
        /// <param name="owner">The parameter1.</param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException">Operation not supported for the given expression type {expression.Type}. "
        ///                     + "Only MemberExpression and ConstantExpression are currently supported.</exception>
        private string CreateChain()
        {
            var elements = ExpressionTree.GetRootElements(this.propertyExpression.Body);
            var expressionString = propertyExpression.ToString();

            base.CreateChain(elements);

            return expressionString;
        }
    }
}