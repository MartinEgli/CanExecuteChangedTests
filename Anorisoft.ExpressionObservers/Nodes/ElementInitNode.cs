using System.Collections.Generic;
using System.Linq.Expressions;

namespace Anorisoft.ExpressionObservers.Nodes
{
    internal class ElementInitNode
    {
        public ElementInit ElementInit { get; }
        public IEnumerable<NodeCollection> Arguments { get; }

        public ElementInitNode(
            ElementInit elementInit,
            IEnumerable<NodeCollection> arguments)
        {
            ElementInit = elementInit;
            Arguments = arguments;
        }
    }
}