using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Anori.ExpressionObservers.Nodes
{
    internal class MemberInitNode : IExpressionNode
    {
        public MemberInitNode(MemberInitExpression memberInitExpression)
        {
            MemberInitExpression = memberInitExpression;
        }

        public MemberInitExpression MemberInitExpression { get; }
        public Type Type => MemberInitExpression.Type;
        public IExpressionNode Previous { get; set; }
        public IExpressionNode Next { get; set; }
        public IExpressionNode Parent { get; set; }
        public List<NodeCollection> Parameters { get; set; }
        public List<IBindingNode> Bindings { get; set; }
    }
}