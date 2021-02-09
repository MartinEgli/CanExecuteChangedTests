﻿using System.Collections.Generic;

namespace Anorisoft.ExpressionObservers.Nodes
{
    public class Tree : ITree
    {
        public Tree()
        {
        }

        public IList<IExpressionNode> Roots { get; } = new List<IExpressionNode>();
        public NodeCollection Nodes { get; set; } 
    }
}