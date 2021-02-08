using System.Linq.Expressions;

namespace Anorisoft.ExpressionObservers.Nodes
{
    internal class MemberAssignmentNode : IBindingNode
    {
        public MemberAssignment MemberAssignment { get; }
        public NodeCollection Nodes { get; }

        public MemberAssignmentNode(MemberAssignment memberAssignment, NodeCollection nodes)
        {
            MemberAssignment = memberAssignment;
            Nodes = nodes;
        }

        public MemberBinding Binding => MemberAssignment;
    }
}