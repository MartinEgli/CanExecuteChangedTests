using System;

namespace Anori.ExpressionObservers.Nodes
{
    public interface IExpressionNode
    {
       Type Type { get; }
       IExpressionNode Previous { get; set; }
       IExpressionNode Next { get; set; }
       IExpressionNode Parent { get; set; }
    }
}