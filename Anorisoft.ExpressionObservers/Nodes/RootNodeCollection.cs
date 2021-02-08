using System.Collections.Generic;

namespace Anorisoft.ExpressionObservers.Nodes
{
    public class RootNodeCollection : IRootNode
    {
        public RootNodeCollection()
        {
        }

        public IList<IExpressionNode> Roots { get; } = new List<IExpressionNode>();
        public NodeCollection Nodes { get; set; } 
    }
}