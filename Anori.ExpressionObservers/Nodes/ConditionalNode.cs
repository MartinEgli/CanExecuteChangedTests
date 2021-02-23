using System;
using System.Linq.Expressions;

namespace Anori.ExpressionObservers.Nodes
{
    internal struct ConditionalNode : IExpressionNode
    {
        public ConditionalExpression ConditionalExpression { get; }
        public NodeCollection Test { get; set; }
        public NodeCollection IfTrue { get; set; }
        public NodeCollection IfFalse { get; set; }

        public ConditionalNode(
            ConditionalExpression conditionalExpression)
        {
            ConditionalExpression = conditionalExpression;
            Test = null;
            IfTrue = null;
            IfFalse = null;
            Type = conditionalExpression.Type;
            Previous = null;
            Next = null;
            Parent = null;
        }

        public Type Type { get; }
        public IExpressionNode Previous { get; set; }
        public IExpressionNode Next { get; set; }
        public IExpressionNode Parent { get; set; }
    }
}