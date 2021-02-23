using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Anori.ExpressionObservers.Exceptions;
using Anori.ExpressionObservers.Nodes;
using JetBrains.Annotations;

namespace Anori.ExpressionObservers
{
    public static class ExpressionTree
    {

        /// <summary>
        /// Gets the tree.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        public static Tree GetTree(
            Expression expression)
        {
            var tree = new Tree();
            tree.Nodes = GetTree(expression, tree, null);
            return tree;
        }


        /// <summary>
        /// Gets the tree.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="tree">The tree.</param>
        /// <param name="parent">The parent.</param>
        /// <returns></returns>
        /// <exception cref="ExpressionObserversException">
        /// Expression member is not a PropertyInfo
        /// or
        /// Method call has no ReturnParameter
        /// or
        /// Expression body is null
        /// or
        /// Expression body is not a supportet Expression {expression} type {expression.Type}
        /// </exception>
        public static NodeCollection GetTree([NotNull] Expression expression, [NotNull] ITree tree,
            [CanBeNull] IExpressionNode parent)
        {
            if (expression == null) throw new ArgumentNullException(nameof(expression));
            if (tree == null) throw new ArgumentNullException(nameof(tree));
            var nodeCollection = new NodeCollection(tree, parent);
            while (true)
                switch (expression)
                {
                    case MemberExpression memberExpression when memberExpression.Member is PropertyInfo propertyInfo:
                    {
                        expression = memberExpression.Expression;
                        nodeCollection.AddElement(new PropertyNode(memberExpression, propertyInfo));
                        break;
                    }
                    case MemberExpression memberExpression when memberExpression.Member is FieldInfo fieldInfo:
                        expression = memberExpression.Expression;
                        nodeCollection.AddElement(new FieldNode(memberExpression, fieldInfo));
                        break;

                    case MemberExpression _:
                        throw new ExpressionObserversException("Expression member is not a PropertyInfo");

                    case ParameterExpression parameterExpression:
                    {
                        var element = (new ParameterNode(parameterExpression));
                        nodeCollection.AddElement(element);
                        nodeCollection.Roots.Add(element);
                        return nodeCollection;
                    }

                    case MethodCallExpression methodCallExpression
                        when methodCallExpression.Method.ReturnParameter == null:
                        throw new ExpressionObserversException("Method call has no ReturnParameter");

                    case MethodCallExpression methodCallExpression:
                        expression = methodCallExpression.Object;
                        if (expression != null)
                        {
                            var element = new MethodNode(methodCallExpression);
                            element.Object = GetTree(expression, nodeCollection, element);

                            var arguments = new List<NodeCollection>();
                            foreach (var argument in methodCallExpression.Arguments)
                            {
                                arguments.Add(GetTree(argument, nodeCollection, element));
                            }

                            element.Arguments = arguments;
                            nodeCollection.AddElement(element);

                            return nodeCollection;
                        }
                        else
                        {
                            var element = new FunctionNode(methodCallExpression);
                            var parameters = new List<NodeCollection>();
                            foreach (var argument in methodCallExpression.Arguments)
                                parameters.Add(GetTree(argument, nodeCollection, element));
                            element.Parameters = parameters;
                            nodeCollection.AddElement(element);
                            return nodeCollection;
                        }

                    case ConstantExpression constantExpression:
                    {
                        var element = new ConstantNode(constantExpression);
                        nodeCollection.AddElement(element);
                        nodeCollection.Roots.Add(element);
                        return nodeCollection;
                    }

                    case BinaryExpression binaryExpression:
                    {
                        var element = new BinaryNode(binaryExpression);
                        element.LeftNodes = GetTree(binaryExpression.Left, nodeCollection, element);
                        element.Righttree = GetTree(binaryExpression.Right, nodeCollection, element);
                        nodeCollection.AddElement(element);
                        return nodeCollection;
                    }
                    case UnaryExpression unaryExpression:
                    {
                        var element = new UnaryNode(unaryExpression);
                        element.Operand = GetTree(unaryExpression.Operand, nodeCollection, element);
                        nodeCollection.AddElement(element);
                        return nodeCollection;
                    }

                    case ConditionalExpression conditionalExpression:
                    {
                        var element = new ConditionalNode(conditionalExpression);
                        element.Test = GetTree(conditionalExpression.Test, nodeCollection, element);
                        element.IfTrue = GetTree(conditionalExpression.IfTrue, nodeCollection, element);
                        element.IfFalse = GetTree(conditionalExpression.IfFalse, nodeCollection, element);

                        nodeCollection.AddElement(element);
                        return nodeCollection;
                    }

                    case NewExpression newExpression:
                    {
                        var element = new ConstructorNode(newExpression);
                        var parameters = new List<NodeCollection>();
                        foreach (var argument in newExpression.Arguments)
                        {
                            parameters.Add(GetTree(argument, nodeCollection, element));
                        }
                        element.Parameters = parameters;
                        nodeCollection.AddElement(element);
                        return nodeCollection;
                    }

                    case MemberInitExpression memberInitExpression:
                    {
                        var element = new MemberInitNode(memberInitExpression);
                        var parameters = new List<NodeCollection>();
                        foreach (var argument in memberInitExpression.NewExpression.Arguments)
                        {
                            parameters.Add(GetTree(argument, nodeCollection, element));
                        }
                        element.Parameters = parameters;

                        var bindings = memberInitExpression.Bindings;
                        var bindingtree = CreateBindingtree(nodeCollection, bindings, element);

                        element.Bindings = bindingtree;
                        nodeCollection.AddElement(element);
                        return nodeCollection;
                    }
                    case null:
                        throw new ExpressionObserversException("Expression body is null");

                    default:
                        throw new ExpressionObserversException(
                            $"Expression body is not a supportet Expression {expression} type {expression.Type}");
                }
        }

        /// <summary>
        /// Creates the bindingtree.
        /// </summary>
        /// <param name="tree">The tree.</param>
        /// <param name="bindings">The bindings.</param>
        /// <param name="node">The node.</param>
        /// <returns></returns>
        private static List<IBindingNode> CreateBindingtree(
            ITree tree,
            ReadOnlyCollection<MemberBinding> bindings,
            MemberInitNode node)
        {
            var bindingtree = new List<IBindingNode>();
            foreach (var binding in bindings)
            {
                switch (binding)
                {
                    case MemberAssignment memberAssignment:
                    {
                        var b = new MemberAssignmentNode(
                            memberAssignment,
                            GetTree(memberAssignment.Expression, tree, node));
                        bindingtree.Add(b);
                        break;
                    }
                    case MemberMemberBinding memberMemberBinding:
                    {
                        var bs = CreateBindingtree(tree, memberMemberBinding.Bindings, node);
                        var b = new MemberMemberBindingNode(memberMemberBinding, bs, node);
                        bindingtree.Add(b);
                        break;
                    }
                    case MemberListBinding memberListBinding:
                    {
                        var elementInits = new List<ElementInitNode>();
                        foreach (var i in memberListBinding.Initializers)
                        {
                            elementInits.Add(new ElementInitNode(i,
                                i.Arguments.Select(a => GetTree(a, tree, node)).ToList()));
                        }

                        var b = new MemberListBindingNode(memberListBinding, elementInits);
                        bindingtree.Add(b);
                        break;
                    }
                }
            }

            return bindingtree;
        }
        

    }
}