using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Anori.ExpressionObservers.Nodes
{
    public struct PropertyNode : IExpressionNode
    {
        public PropertyNode(MemberExpression memberExpression, PropertyInfo propertyInfo)
        {
            MemberExpression = memberExpression;
            PropertyInfo = propertyInfo;
            Type = memberExpression.Type;
            MethodInfo = propertyInfo.GetGetMethod();
            Args = NullArgs;
            Previous = null;
            Next = null;
            Parent = null;
        }

        public MemberExpression MemberExpression { get; }
        public PropertyInfo PropertyInfo { get; }
        public Type Type { get; }
        public IExpressionNode Previous { get; set; }
        public IExpressionNode Next { get; set; }
        public IExpressionNode Parent { get; set; }
        public MethodInfo MethodInfo { get; }
        public IEnumerable<Expression> Args { get; }

        private static readonly IEnumerable<Expression> NullArgs = new Expression[0];

    }


   
}