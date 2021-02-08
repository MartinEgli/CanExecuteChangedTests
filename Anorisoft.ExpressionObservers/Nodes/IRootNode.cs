using System.Collections.Generic;

namespace Anorisoft.ExpressionObservers.Nodes
{
    public interface IRootNode
    {
        IList<IExpressionNode> Roots { get; }
    }
}