using System.Collections.Generic;
using System.Linq;

namespace Anorisoft.ExpressionObservers.Nodes
{
    public class NodeCollection : List<IExpressionNode>, IRootNode
    {

        public NodeCollection(IRootNode rootNodeCollection, IExpressionNode parent)
        {
            Root = rootNodeCollection;
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

        public IRootNode Root { get; }
        public IExpressionNode Parent { get; }

        public IList<IExpressionNode> Roots => Root.Roots;
    }
}