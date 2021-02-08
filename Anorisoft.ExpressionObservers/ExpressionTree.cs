using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Anorisoft.ExpressionObservers.Exceptions;
using Anorisoft.ExpressionObservers.Nodes;

namespace Anorisoft.ExpressionObservers
{
    public static class ExpressionTree
    {

        public static RootNodeCollection GetRootElements(
            Expression expression)
        {
            var elements = new RootNodeCollection();
            elements.Nodes = GetTree(expression, elements, null);
            return elements;
        }


        public static NodeCollection GetTree(Expression expression, IRootNode rootNode, IExpressionNode parent)
        {
            var elements = new NodeCollection(rootNode, parent);
            while (true)
                switch (expression)
                {
                    case MemberExpression memberExpression when memberExpression.Member is PropertyInfo propertyInfo:
                    {
                        expression = memberExpression.Expression;
                        elements.AddElement(new PropertyNode(memberExpression, propertyInfo));
                        break;
                    }
                    case MemberExpression memberExpression when memberExpression.Member is FieldInfo fieldInfo:
                        expression = memberExpression.Expression;
                        elements.AddElement(new FieldNode(memberExpression, fieldInfo));
                        break;

                    case MemberExpression _:
                        throw new ExpressionObserversException("Expression member is not a PropertyInfo");

                    case ParameterExpression parameterExpression:
                    {
                        var element = (new ParameterNode(parameterExpression));
                        elements.AddElement(element);
                        elements.Roots.Add(element);
                        return elements;
                    }

                    case MethodCallExpression methodCallExpression
                        when methodCallExpression.Method.ReturnParameter == null:
                        throw new ExpressionObserversException("Method call has no ReturnParameter");

                    case MethodCallExpression methodCallExpression:
                        expression = methodCallExpression.Object;
                        if (expression != null)
                        {
                            var element = new MethodNode(methodCallExpression);
                            element.Object = GetTree(expression, rootNode, element);

                            var arguments = new List<NodeCollection>();
                            foreach (var argument in methodCallExpression.Arguments)
                            {
                                arguments.Add(GetTree(argument, rootNode, element));
                            }

                            element.Arguments = arguments;
                            elements.AddElement(element);

                            return elements;
                        }
                        else
                        {
                            var element = new FunctionNode(methodCallExpression);
                            var parameters = new List<NodeCollection>();
                            foreach (var argument in methodCallExpression.Arguments)
                                parameters.Add(GetTree(argument, rootNode, element));
                            element.Parameters = parameters;
                            elements.AddElement(element);
                            return elements;
                        }

                    case ConstantExpression constantExpression:
                    {
                        var element = new ConstantNode(constantExpression);
                        elements.AddElement(element);
                        elements.Roots.Add(element);
                        return elements;
                    }

                    case BinaryExpression binaryExpression:
                    {
                        var element = new BinaryNode(binaryExpression);
                        element.LeftNodes = GetTree(binaryExpression.Left, rootNode, element);
                        element.Rightelements = GetTree(binaryExpression.Right, rootNode, element);
                        elements.AddElement(element);
                        return elements;
                    }
                    case UnaryExpression unaryExpression:
                    {
                        var element = new UnaryNode(unaryExpression);
                        element.Operand = GetTree(unaryExpression.Operand, rootNode, element);
                        elements.AddElement(element);
                        return elements;
                    }

                    case ConditionalExpression conditionalExpression:
                    {
                        var element = new ConditionalNode(conditionalExpression);
                        element.Test = GetTree(conditionalExpression.Test, rootNode, element);
                        element.IfTrue = GetTree(conditionalExpression.IfTrue, rootNode, element);
                        element.IfFalse = GetTree(conditionalExpression.IfFalse, rootNode, element);

                        elements.AddElement(element);
                        return elements;
                    }

                    case NewExpression newExpression:
                    {
                        var element = new ConstructorNode(newExpression);
                        var parameters = new List<NodeCollection>();
                        foreach (var argument in newExpression.Arguments)
                        {
                            parameters.Add(GetTree(argument, rootNode, element));
                        }
                        element.Parameters = parameters;
                        elements.AddElement(element);
                        return elements;
                    }

                    case MemberInitExpression memberInitExpression:
                    {
                        var element = new MemberInitNode(memberInitExpression);
                        var parameters = new List<NodeCollection>();
                        foreach (var argument in memberInitExpression.NewExpression.Arguments)
                        {
                            parameters.Add(GetTree(argument, rootNode, element));
                        }
                        element.Parameters = parameters;

                        var bindings = memberInitExpression.Bindings;
                        var bindingElements = CreateBindingElements(rootNode, bindings, element);

                        element.Bindings = bindingElements;
                        elements.AddElement(element);
                        return elements;
                    }
                    case null:
                        throw new ExpressionObserversException("Expression body is null");

                    default:
                        throw new ExpressionObserversException(
                            $"Expression body is not a supportet Expression {expression} type {expression.Type}");
                }
        }

        private static List<IBindingNode> CreateBindingElements(
            IRootNode rootNode,
            ReadOnlyCollection<MemberBinding> bindings,
            MemberInitNode node)
        {
            var bindingElements = new List<IBindingNode>();
            foreach (var binding in bindings)
            {
                switch (binding)
                {
                    case MemberAssignment memberAssignment:
                    {
                        var b = new MemberAssignmentNode(
                            memberAssignment,
                            GetTree(memberAssignment.Expression, rootNode, node));
                        bindingElements.Add(b);
                        break;
                    }
                    case MemberMemberBinding memberMemberBinding:
                    {
                        var bs = CreateBindingElements(rootNode, memberMemberBinding.Bindings, node);
                        var b = new MemberMemberBindingNode(memberMemberBinding, bs, node);
                        bindingElements.Add(b);
                        break;
                    }
                    case MemberListBinding memberListBinding:
                    {
                        var elementInits = new List<ElementInitNode>();
                        foreach (var i in memberListBinding.Initializers)
                        {
                            elementInits.Add(new ElementInitNode(i,
                                i.Arguments.Select(a => GetTree(a, rootNode, node)).ToList()));
                        }

                        var b = new MemberListBindingNode(memberListBinding, elementInits);
                        bindingElements.Add(b);
                        break;
                    }
                }
            }

            return bindingElements;
        }
        

    }
}