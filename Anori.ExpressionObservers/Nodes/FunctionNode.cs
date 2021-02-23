using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Reflection;

namespace Anori.ExpressionObservers.Nodes
{
    internal struct FunctionNode : IExpressionNode
    {
        public MethodCallExpression Method { get; }
        public List<NodeCollection> Parameters { get; set; }
        public Type ReturnType { get; }
        public MethodInfo MethodInfo { get; }
        public ReadOnlyCollection<Expression> Arguments { get; }


        public FunctionNode(MethodCallExpression method)
        {
            Method = method;
            Parameters = null;
            ReturnType = method.Method.ReturnType;
            MethodInfo = method.Method;
            Arguments = method.Arguments;
            Type = method.Method.ReturnType;
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