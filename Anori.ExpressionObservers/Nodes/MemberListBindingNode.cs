using System.Collections.Generic;
using System.Linq.Expressions;

namespace Anori.ExpressionObservers.Nodes
{
    internal class MemberListBindingNode : IBindingNode
    {
        public MemberListBinding MemberListBinding { get; }
        public List<ElementInitNode> Initializers { get; }

        public MemberListBindingNode(MemberListBinding memberListBinding, List<ElementInitNode> initializers)
        {
            MemberListBinding = memberListBinding;
            Initializers = initializers;
        }

        public MemberBinding Binding => MemberListBinding;
    }
}