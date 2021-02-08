﻿using System;
using System.Linq.Expressions;

namespace Anorisoft.ExpressionObservers.Nodes
{
    internal struct BinaryNode : IExpressionNode
    {
        public BinaryNode(
            BinaryExpression binaryExpression)
        {
            BinaryExpression = binaryExpression;
            NodeType = binaryExpression.NodeType;
            LeftNodes = null;
            Rightelements = null;
            Type = binaryExpression.Type;
            Previous = null;
            Next = null;
            Parent = null;
        }

        public BinaryExpression BinaryExpression { get; }
        public Type Type { get; }
        public IExpressionNode Previous { get; set; }
        public IExpressionNode Next { get; set; }
        public IExpressionNode Parent { get; set; }
        public ExpressionType NodeType { get; }
        public NodeCollection LeftNodes { get; set; }
        public NodeCollection Rightelements { get; set; }
    }
}