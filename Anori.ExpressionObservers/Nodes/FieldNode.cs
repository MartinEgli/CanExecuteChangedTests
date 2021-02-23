using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Anori.ExpressionObservers.Nodes
{
    public struct FieldNode : IExpressionNode
    {
        public FieldNode(MemberExpression expression, FieldInfo fieldInfo)
        {
            Expression = expression;
            Type = expression.Type;
            FieldInfo = fieldInfo;
            Previous = null;
            Next = null;
            Parent = null;
        }

        public MemberExpression Expression { get; }
        public Type Type { get; }
        public IExpressionNode Previous { get; set; }
        public IExpressionNode Next { get; set; }
        public IExpressionNode Parent { get; set; }
        public FieldInfo FieldInfo { get; }
    }
}