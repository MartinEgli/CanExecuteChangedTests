using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Anori.ExpressionObservers.Nodes
{
    internal  struct MethodNode : IExpressionNode
    {
      
        public MethodNode(MethodCallExpression methodCallExpression)
        {
            MethodCallExpression = methodCallExpression;
            @Object = null;
            Arguments = null;
            Previous = null;
            Next = null;
            Parent = null;
        }

        public MethodCallExpression MethodCallExpression { get; }


        
        public NodeCollection @Object { get; set; }

        public List<NodeCollection> Arguments { get; set; }
        public Type Type => MethodCallExpression.Method.ReturnParameter.ParameterType;
        public IExpressionNode Previous { get; set; }
        public IExpressionNode Next { get; set; }
        public IExpressionNode Parent { get; set; }
        public MethodInfo MethodInfo => MethodCallExpression.Method;


    }
}