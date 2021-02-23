using System;
using System.Linq.Expressions;

namespace Anori.ExpressionObservers.Nodes
{
    public class ParameterNode : IExpressionNode
    {
        public Type Type { get; }
        public IExpressionNode Previous { get; set; }
        public IExpressionNode Next { get; set; }
        public IExpressionNode Parent { get; set; }
        public ParameterExpression Expression { get; }

        public ParameterNode(ParameterExpression expression)
        {
            Type = expression.Type;
            Expression = expression;
            Previous = null;
            Next = null;
            Parent = null;
        }
    }
}