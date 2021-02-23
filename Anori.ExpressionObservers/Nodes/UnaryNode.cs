using System;
using System.Linq.Expressions;

namespace Anori.ExpressionObservers.Nodes
{
    internal struct UnaryNode : IExpressionNode
    {
        public UnaryExpression UnaryExpression { get; }
        public NodeCollection Operand { get; set; }

        public UnaryNode(UnaryExpression unaryExpression)
        {
            UnaryExpression = unaryExpression;
            Operand = null;
            Type = unaryExpression.Type;
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