using System.Collections.Generic;
using System.Linq;

namespace Anori.ExpressionObservers.Nodes
{
    public class NodeCollection : List<IExpressionNode>, ITree
    {

        public NodeCollection(ITree tree, IExpressionNode parent)
        {
            Tree = tree;
            Parent = parent;
        }

        public void AddElement(IExpressionNode node)
        {
            if (this.Any())
            {
                this.Last().Previous = node;

                node.Next = this.Last();
            }

            node.Parent = Parent;
            Add(node);
        }

        public ITree Tree { get; }
        public IExpressionNode Parent { get; }

        public IList<IExpressionNode> Roots => Tree.Roots;
    }
}