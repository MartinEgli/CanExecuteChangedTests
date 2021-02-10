using System;
using System.Linq.Expressions;
using Anorisoft.ExpressionObservers;
using Anorisoft.PropertyObservers.Common;
using JetBrains.Annotations;

namespace Anorisoft.PropertyObservers.ValueObservers
{
    public abstract class PropertyValueObserverBase<TResult> : PropertyObserverBase
    {
        /// <summary>
        ///     The property expression
        /// </summary>
        private readonly Expression<Func<TResult>> propertyExpression;

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyValueObserverBase{TResult}"/> class.
        /// </summary>
        /// <param name="propertyExpression">The property expression.</param>
        /// <exception cref="ArgumentNullException">propertyExpression</exception>
        protected PropertyValueObserverBase(
            [NotNull] Expression<Func<TResult>> propertyExpression)
        {
            this.propertyExpression = propertyExpression ?? throw new ArgumentNullException(nameof(propertyExpression));
            this.ExpressionString = this.CreateChain();
        }
        

        /// <summary>
        ///     The expression
        /// </summary>
        public override string ExpressionString { get; }

        /// <summary>
        /// Creates the chain.
        /// </summary>
        /// <param name="owner">The parameter1.</param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException">Operation not supported for the given expression type {expression.Type}. "
        ///                     + "Only MemberExpression and ConstantExpression are currently supported.</exception>
        protected string CreateChain()
        {
            var tree = ExpressionTree.GetTree(this.propertyExpression.Body);
            var expressionString = propertyExpression.ToString();

            base.CreateChain(tree);

            return expressionString;
        }
    }
}