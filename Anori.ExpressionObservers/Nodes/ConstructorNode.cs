using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Anori.ExpressionObservers.Nodes
{
    internal class ConstructorNode : IExpressionNode
    {
        public NewExpression NewExpression { get; }

        public ConstructorNode(NewExpression newExpression)
        {
            NewExpression = newExpression;
        }

        public Type Type => NewExpression.Type;

        public ConstructorInfo Constructor => NewExpression.Constructor;


        public IExpressionNode Previous { get; set; }
        public IExpressionNode Next { get; set; }
        public IExpressionNode Parent { get; set; }
        public List<NodeCollection> Parameters { get; set; }
    }
}