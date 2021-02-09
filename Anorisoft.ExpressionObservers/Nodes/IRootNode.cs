using System.Collections.Generic;

namespace Anorisoft.ExpressionObservers.Nodes
{
    public interface ITree
    {
        IList<IExpressionNode> Roots { get; }
    }
}