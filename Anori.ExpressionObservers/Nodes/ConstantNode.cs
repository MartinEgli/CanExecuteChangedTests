using System;
using System.Linq.Expressions;

namespace Anori.ExpressionObservers.Nodes
{
    public class ConstantNode : IExpressionNode
    {
        public ConstantNode(ConstantExpression expression)
        {
            Expression = expression;
            Type = expression.Type;
            Previous = null;
            Next = null;
            Parent = null;
        }

        public ConstantExpression Expression { get; }
        public object Value => Expression.Value;
        public Type Type { get; }
        public IExpressionNode Previous { get; set; }
        public IExpressionNode Next { get; set; }
        public IExpressionNode Parent { get; set; }
    }
}