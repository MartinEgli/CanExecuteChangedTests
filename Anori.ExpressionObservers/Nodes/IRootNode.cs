using System.Collections.Generic;

namespace Anori.ExpressionObservers.Nodes
{
    public interface ITree
    {
        IList<IExpressionNode> Roots { get; }
    }
}