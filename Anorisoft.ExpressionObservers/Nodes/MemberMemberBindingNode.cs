using System.Collections.Generic;
using System.Linq.Expressions;

namespace Anorisoft.ExpressionObservers.Nodes
{
    internal class MemberMemberBindingNode : IBindingNode
    {
        public MemberMemberBinding MemberMemberBinding { get; }
        public List<IBindingNode> Bindings { get; }
        public MemberInitNode MemberInitNode { get; }

        public MemberMemberBindingNode(MemberMemberBinding memberMemberBinding,
            List<IBindingNode> bindings, MemberInitNode memberInitNode)
        {
            MemberMemberBinding = memberMemberBinding;
            Bindings = bindings;
            MemberInitNode = memberInitNode;
        }

        public MemberBinding Binding => MemberMemberBinding;
    }
}