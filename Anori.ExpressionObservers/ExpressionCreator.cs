using Anori.ExpressionObservers.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Anori.ExpressionObservers
{
    internal static class ExpressionCreator

    {
        private static ConstantExpression FalseConstantExpression => Expression.Constant(false);

        private static ConstantExpression TrueConstantExpression => Expression.Constant(true);

        internal static BlockExpression CreateValueBody(
            Type resultType,
            Expression expression,
            Expression fallback)
        {
            var returnTarget = Expression.Label(resultType);
            var tree = ExpressionTree.GetTree(expression);
            var body = CreateValueBlock(resultType, tree, returnTarget, fallback);
            return body;
        }

        internal static BlockExpression CreateValueBody(
            Type resultType,
            Expression expression)

        {
            var returnTarget = Expression.Label(resultType);
            var tree = ExpressionTree.GetTree(expression);
            var body = CreateValueBlock(resultType, tree, returnTarget);
            return body;
        }

        internal static BlockExpression CreateValueBody(
            Type resultType,
            Tree tree)

        {
            var returnTarget = Expression.Label(resultType);
            var body = CreateValueBlock(resultType, tree, returnTarget);
            return body;
        }

        internal static BlockExpression CreateValueBody(
            Type resultType,
            Tree tree, Expression fallback)

        {
            var returnTarget = Expression.Label(resultType);
            var body = CreateValueBlock(resultType, tree, returnTarget, fallback);
            return body;
        }

        internal static Expression CreateParameterBody(
            ParameterNode resultParameter,
            Expression expression)

        {
            var returnTarget = Expression.Label(resultParameter.Type);
            var tree = ExpressionTree.GetTree(expression);
            var body = resultParameter.Expression;
            return body;
        }

        private static Expression NullExpressionOf(Type type) => Expression.Constant(null, type);
        private static BlockExpression CreateValueBlock(
            Type resultType,
            Tree nodes,
            LabelTarget returnTarget)
        {
            var expressions = new List<Expression>();
            var variables = new VaribalesCollection();
            var ifNull = Expression.Return(returnTarget, NullExpressionOf(resultType));

            CreateValueExpressions(resultType, nodes.Nodes, expressions, variables, ifNull, returnTarget);

            expressions.Add(Expression.Label(returnTarget, NullExpressionOf(resultType)));
            var body = Expression.Block(variables, expressions);
            return body;
        }

        private static BlockExpression CreateValueBlock(
            Type resultType,
            Tree nodes,
            LabelTarget returnTarget,
            Expression fallback)
        {
            var expressions = new List<Expression>();
            var variables = new VaribalesCollection();
            var ifNull = Expression.Return(returnTarget, fallback);

            CreateValueExpressions(resultType, nodes.Nodes, expressions, variables, ifNull, returnTarget);

            expressions.Add(Expression.Label(returnTarget, fallback));
            var body = Expression.Block(variables, expressions);
            return body;
        }

        private static void CreateValueExpressions(
            Type resultType,
            NodeCollection nodes,
            IList<Expression> expressions,
            VaribalesCollection variables,
            Expression ifNull,
            LabelTarget returnTarget)
        {
            switch (nodes.Count)
            {
                case 0:
                    throw new NotSupportedException();

                case 1:
                    CreateValueSingleExpressions(resultType, expressions, variables, nodes, ifNull, returnTarget);
                    break;

                default:
                    CreateValueChainExpressions(resultType, expressions, variables, nodes, ifNull, returnTarget);
                    break;
            }
        }

        private static void CreateValueChainExpressions(
            Type resultType,
            IList<Expression> expressions,
            VaribalesCollection variables,
            NodeCollection nodes,
            Expression ifNull,
            LabelTarget returnTarget)
        {
            var target = CreateVariableInnerExpressions(expressions, variables, nodes, ifNull);
            InsertEndValueExpression(resultType, expressions, variables, target, nodes.First(), ifNull,
                returnTarget);
        }

        private static Expression CreateVariableExpressions(
            IList<Expression> expressions,
            VaribalesCollection variables,
            NodeCollection nodes,
            Expression ifNull)
        {
            var source = CreateVariableInnerExpressions(expressions, variables, nodes, ifNull);
            if (nodes.Count == 1)
            {
                return source;
            }

            var target = Expression.Variable(nodes.First().Type, $"value{variables.GetNextIndex()}");
            variables.Add(target);
            InsertExpression(expressions, variables, source, nodes.First(), ifNull, target);
            return target;
        }

        private static Expression CreateVariableInnerExpressions(
            IList<Expression> expressions,
            VaribalesCollection variables,
            NodeCollection nodes,
            Expression ifNull)
        {
            var list = nodes.ToList();
            list.Reverse();
            var i = 1;
            Expression target;
            var element = list.First();
            {
                switch (element)
                {
                    case ParameterNode parameter:
                        {
                            var p = parameter.Expression;
                            target = p;
                            ParameterNextParameter(expressions, target, parameter, ifNull);
                            break;
                        }

                    case ConstantNode constant:
                        target = constant.Expression;
                        break;

                    case MethodNode method:
                        {
                            var p = Expression.Variable(method.Type, $"value{variables.GetNextIndex()}");
                            target = p;
                            variables.Add(p);
                            MethodNextElement(expressions, variables, target, method, ifNull);
                            break;
                        }
                    case ConstructorNode constructor:
                        {
                            var p = Expression.Variable(constructor.Type, $"value{variables.GetNextIndex()}");
                            target = p;
                            variables.Add(p);
                            ConstructorNextElement(expressions, variables, target, constructor, ifNull);
                            break;
                        }
                    case FunctionNode function:
                        {
                            var p = Expression.Variable(function.Type, $"value{variables.GetNextIndex()}");
                            target = p;
                            variables.Add(p);
                            FunctionNextElement(expressions, variables, target, function, ifNull);
                            break;
                        }

                    case BinaryNode binary:
                        {
                            var p = Expression.Variable(binary.Type, $"value{variables.GetNextIndex()}");
                            target = p;
                            variables.Add(p);
                            BinaryNextElement(expressions, variables, target, binary, ifNull);
                            break;
                        }
                    case UnaryNode unary:
                        {
                            var p = Expression.Variable(unary.Type, $"value{variables.GetNextIndex()}");
                            target = p;
                            variables.Add(p);
                            UnaryNextElement(expressions, variables, target, unary, ifNull);
                            break;
                        }
                    case ConditionalNode conditional:
                        {
                            var p = Expression.Variable(conditional.Type, $"value{variables.GetNextIndex()}");
                            target = p;
                            variables.Add(p);
                            ConditionalNextElement(expressions, variables, target, conditional, ifNull);
                            break;
                        }

                    case MemberInitNode memberInit:
                        {
                            var p = Expression.Variable(memberInit.Type, $"value{variables.GetNextIndex()}");
                            target = p;
                            variables.Add(p);
                            MemberInitNextElement(expressions, variables, target, memberInit, ifNull);
                            break;
                        }
                    default:
                        {
                            throw new ArgumentOutOfRangeException($"{element}");
                        }
                }
            }

            var count = list.Count;
            if (count == 1) return target;

            var source = target;
            for (; i < count - 1; i++)
            {
                element = list[i];
                var p = Expression.Variable(element.Type, $"value{variables.GetNextIndex()}");
                variables.Add(p);
                target = p;
                InsertExpression(expressions, variables, source, element, ifNull, p);
                source = target;
            }

            return target;
        }

        private static void MemberInitNextElement(IList<Expression> expressions,
            VaribalesCollection variables,
            Expression targetParameter,
            MemberInitNode memberInit,
            Expression ifNull)
        {
            expressions.Add(Expression.Assign(targetParameter,
                MemberInitMakeExpression(expressions, variables, memberInit, ifNull)));

            if (memberInit.Type.IsValueType && !memberInit.Type.IsNullable()) return;

            expressions.Add(Expression.IfThen(
                Expression.Equal(targetParameter, NullExpressionOf(memberInit.Type)),
                ifNull));
        }

        private static Expression MemberInitMakeExpression(IList<Expression> expressions, VaribalesCollection variables,
            MemberInitNode memberInit, Expression ifNull)
        {
            var args = memberInit.Parameters.Select(argument =>
                    CreateVariableExpressions(expressions, variables, argument, ifNull))
                .ToList();
            var bindings = memberInit.Bindings.Select(binding =>
                CreateBindingExpressions(expressions, variables, binding, ifNull));
            return Expression.MemberInit(
                Expression.New(memberInit.MemberInitExpression.NewExpression.Constructor, args), bindings);
        }

        private static MemberBinding CreateBindingExpressions(
            IList<Expression> expressions,
            VaribalesCollection variables,
            IBindingNode binding,
            Expression ifNull)
        {
            switch (binding)
            {
                case MemberAssignmentNode memberAssignment:
                    {
                        var expression = CreateVariableExpressions(expressions, variables, memberAssignment.Nodes, ifNull);
                        return Expression.Bind(memberAssignment.Binding.Member, expression);
                    }

                case MemberListBindingNode memberListBinding:
                    {
                        var memberInitCollection = new List<ElementInit>();
                        foreach (var initializer in memberListBinding.Initializers)
                        {
                            var args = new List<Expression>();
                            foreach (var argument in initializer.Arguments)
                            {
                                var expression = CreateVariableExpressions(expressions, variables, argument, ifNull);
                                args.Add(expression);
                            }

                            memberInitCollection.Add(Expression.ElementInit(initializer.ElementInit.AddMethod, args));
                        }

                        return Expression.ListBind(memberListBinding.Binding.Member, memberInitCollection);
                    }
                case MemberMemberBindingNode memberMemberBindingElement:
                    {
                        var m = memberMemberBindingElement.Bindings.Select(s =>
                            CreateBindingExpressions(expressions, variables, s, ifNull));
                        return Expression.MemberBind(memberMemberBindingElement.MemberMemberBinding.Member, m);
                    }

                default:
                    throw new ArgumentOutOfRangeException($"{binding}");
            }
        }

        private static void ConstructorNextElement(IList<Expression> expressions,
            VaribalesCollection variables,
            Expression targetParameter,
            ConstructorNode constructor,
            Expression ifNull)
        {
            expressions.Add(Expression.Assign(targetParameter,
                ConstructorMakeExpression(expressions, variables, constructor, ifNull)));

            if (constructor.Type.IsValueType && !constructor.Type.IsNullable()) return;

            expressions.Add(Expression.IfThen(
                Expression.Equal(targetParameter, NullExpressionOf(constructor.Type)),
                ifNull));
        }

        private static void InsertEndValueExpression(
            Type resultType,
            IList<Expression> expressions,
            VaribalesCollection variables,
            Expression source,
            IExpressionNode node,
            Expression ifNull,
            LabelTarget returnTarget)
        {
            switch (node)
            {
                case MethodNode method:
                    MethodLastElement(expressions, variables, resultType, method, ifNull, returnTarget);
                    break;

                case PropertyNode property:
                    PropertyLastElement(expressions, source, resultType, property, ifNull, returnTarget);
                    break;

                case FieldNode field:
                    FieldLastElement(expressions, source, resultType, field, returnTarget);
                    break;

                default:
                    throw new ArgumentOutOfRangeException($"{node}");
            }
        }

        private static void InsertExpression(
            IList<Expression> expressions,
            VaribalesCollection variables,
            Expression source,
            IExpressionNode node,
            Expression ifNull,
            Expression target)
        {
            switch (node)
            {
                case ParameterNode parameter:
                    ParameterNextParameter(expressions, target, parameter, ifNull);
                    break;

                case ConstantNode constant:
                    ConstantNextElement(expressions, target, constant, ifNull);
                    break;

                case FunctionNode function:
                    FunctionNextElement(expressions, variables, target, function, ifNull);
                    break;

                case MethodNode method:
                    MethodNextElement(expressions, variables, target, method, ifNull);
                    break;

                case PropertyNode property:
                    PropertyNextElement(expressions, source, target, property, ifNull);
                    break;

                case FieldNode field:
                    FieldNextElement(expressions, source, target, field, ifNull);
                    break;

                case BinaryNode binary:
                    BinaryNextElement(expressions, variables, target, binary, ifNull);
                    break;

                case UnaryNode unary:
                    UnaryNextElement(expressions, variables, target, unary, ifNull);
                    break;

                case ConditionalNode conditional:
                    ConditionalNextElement(expressions, variables, target, conditional, ifNull);
                    break;

                default:
                    throw new ArgumentOutOfRangeException($"{node}");
            }
        }

        private static void FieldLastElement(
            IList<Expression> expressions, Expression sourceParameter,
            Type resultType,
            FieldNode field,
            LabelTarget returnTarget) =>
            expressions.Add(Expression.Return(returnTarget,
                Expression.Convert(Expression.Field(sourceParameter, field.FieldInfo), resultType)));

        private static void CreateValueSingleExpressions(Type resultType,
            IList<Expression> expressions,
            VaribalesCollection variables,
            NodeCollection nodes,
            Expression ifNull,
            LabelTarget returnTarget)
        {
            var element = nodes.First();
            switch (element)
            {
                case FunctionNode function:
                    FunctionLastElement(expressions, variables, resultType, function, ifNull, returnTarget);
                    break;

                case ConstantNode constant:
                    ConstantLastElement(expressions, resultType, constant, returnTarget);
                    break;

                case ParameterNode parameter:
                    ParametertLastElement(expressions, resultType, parameter, returnTarget);
                    break;

                case MethodNode method:
                    MethodLastElement(expressions, variables, resultType, method, ifNull, returnTarget);
                    break;

                case BinaryNode binary:
                    BineryLastElement(expressions, variables, resultType, binary, ifNull, returnTarget);
                    break;

                case UnaryNode unary:
                    UnaryLastElement(expressions, variables, resultType, unary, ifNull, returnTarget);
                    break;

                case ConditionalNode conditional:
                    ConditionalLastElement(expressions, variables, resultType, conditional, ifNull, returnTarget);
                    break;

                default:
                    throw new ArgumentOutOfRangeException($"{element}");
            }
        }

        private static void ConstantLastElement(ICollection<Expression> expressions, Type resultType,
            ConstantNode constant,
            LabelTarget returnTarget) =>
            expressions.Add(Expression.Return(returnTarget, Expression.Constant(constant.Value, resultType)));

        private static void ConstantNextElement(
            ICollection<Expression> expressions,
            Expression targetParameter,
            ConstantNode constant,
            Expression ifExpression)
        {
            expressions.Add(Expression.Assign(targetParameter,
                Expression.Constant(constant.Value, constant.Type)));

            if (!constant.Type.IsValueType || constant.Type.IsNullable())
                expressions.Add(Expression.IfThen(
                    Expression.Equal(targetParameter, NullExpressionOf(constant.Type)),
                    ifExpression));
        }

        private static Expression ParametertMakeExpression(
            ParameterNode parameter)
        {
            return parameter.Expression;
        }

        private static void ParametertLastElement(
            IList<Expression> expressions,
            Type resultType,
            ParameterNode parameter,
            LabelTarget returnTarget) =>
            expressions.Add(Expression.Return(returnTarget,
                Expression.Convert(ParametertMakeExpression(parameter), resultType)));

        private static void ParameterNextParameter(
            ICollection<Expression> expressions,
            Expression targetParameter,
            IExpressionNode parameter,
            Expression ifExpression)
        {
            if (parameter.Type.IsValueType && !parameter.Type.IsNullable()) return;

            expressions.Add(
                Expression.IfThen(
                    Expression.Equal(targetParameter, NullExpressionOf(parameter.Type)),
                    ifExpression));
        }

        private static void FieldNextElement(ICollection<Expression> expressions,
            Expression sourceParameter,
            Expression targetParameter,
            FieldNode field,
            Expression ifExpression)
        {
            expressions.Add(Expression.Assign(targetParameter,
                Expression.Field(sourceParameter, field.FieldInfo)));

            if (field.Type.IsValueType && !field.Type.IsNullable()) return;

            expressions.Add(Expression.IfThen(
                Expression.Equal(targetParameter, NullExpressionOf(field.Type)),
                ifExpression)
            );
        }

        private static Expression PropertyMakeExpression(IList<Expression> expressions,
            Expression sourceParameter,
            PropertyNode property,
            Expression ifNull) =>
            Expression.Call(sourceParameter, property.MethodInfo, property.Args);

        private static void PropertyLastElement(IList<Expression> expressions,
            Expression sourceParameter,
            Type resultType,
            PropertyNode property,
            Expression ifNull,
            LabelTarget returnTarget) =>
            expressions.Add(Expression.Return(returnTarget,
                Expression.Convert(PropertyMakeExpression(expressions, sourceParameter, property, ifNull),
                    resultType)));

        private static void PropertyNextElement(
            IList<Expression> expressions,
            Expression sourceParameter,
            Expression targetParameter,
            PropertyNode property,
            Expression ifNull)
        {
            expressions.Add(Expression.Assign(targetParameter,
                PropertyMakeExpression(expressions, sourceParameter, property, ifNull)));

            if (property.Type.IsValueType && !property.Type.IsNullable()) return;

            expressions.Add(Expression.IfThen(
                Expression.Equal(targetParameter, NullExpressionOf(property.Type)),
                ifNull));
        }

        private static Expression ConstructorMakeExpression(
            IList<Expression> expressions,
            VaribalesCollection variables,
            ConstructorNode constructor,
            Expression ifNull)
        {
            var args = constructor.Parameters.Select(argument =>
                    CreateVariableExpressions(expressions, variables, argument, ifNull))
                .ToList();

            return Expression.New(constructor.Constructor, args);
        }

        private static Expression MethodMakeExpression(
            IList<Expression> expressions,
            VaribalesCollection variables,
            MethodNode method,
            Expression ifNull)
        {
            var @object = CreateVariableExpressions(expressions, variables, method.Object, ifNull);

            var args = method.Arguments.Select(argument =>
                    CreateVariableExpressions(expressions, variables, argument, ifNull))
                .ToList();

            return Expression.Call(@object, method.MethodInfo, args);
        }

        private static void MethodLastElement(
            IList<Expression> expressions,
            VaribalesCollection variables,
            Type resultType,
            MethodNode method,
            Expression ifNull,
            LabelTarget returnTarget) =>
            expressions.Add(Expression.Return(returnTarget,
                Expression.Convert(MethodMakeExpression(expressions, variables, method, ifNull),
                    resultType)));

        private static void MethodNextElement(
            IList<Expression> expressions,
            VaribalesCollection variables,
            Expression targetParameter,
            MethodNode method,
            Expression ifNull)
        {
            expressions.Add(Expression.Assign(targetParameter,
                MethodMakeExpression(expressions, variables, method, ifNull)));

            if (method.Type.IsValueType && !method.Type.IsNullable()) return;

            expressions.Add(Expression.IfThen(
                Expression.Equal(targetParameter, NullExpressionOf(method.Type)),
                ifNull));
        }

        private static void ConditionalLastElement(
            IList<Expression> expressions,
            VaribalesCollection variables,
            Type resultType,
            in ConditionalNode conditional,
            Expression ifNull,
            LabelTarget returnTarget) =>
            expressions.Add(Expression.Return(returnTarget,
                Expression.Convert(ConditionalMakeExpression(expressions, variables, conditional, ifNull),
                    resultType)));

        private static void ConditionalNextElement(
            IList<Expression> expressions,
            VaribalesCollection variables,
            Expression target,
            ConditionalNode conditional,
            Expression ifNull)
        {
            expressions.Add(Expression.Assign(target,
                ConditionalMakeExpression(expressions, variables, conditional, ifNull)));

            if (conditional.Type.IsValueType && !conditional.Type.IsNullable()) return;

            expressions.Add(Expression.IfThen(
                Expression.Equal(target, NullExpressionOf(conditional.Type)),
                ifNull));
        }

        private static Expression ConditionalMakeExpression(
            IList<Expression> expressions,
            VaribalesCollection variables,
            ConditionalNode conditional,
            Expression ifNull)
        {
            var test = CreateVariableExpressions(expressions, variables, conditional.Test, ifNull);

            var ifTrue = new List<Expression>();
            ifTrue.Add(CreateVariableExpressions(ifTrue, variables, conditional.IfTrue, ifNull));

            var ifFalse = new List<Expression>();
            ifFalse.Add(CreateVariableExpressions(ifFalse, variables, conditional.IfFalse, ifNull));

            return Expression.Condition(test, Expression.Block(ifTrue), Expression.Block(ifFalse));
        }

        private static Expression UnaryMakeExpression(
            IList<Expression> expressions,
            VaribalesCollection variables,
            UnaryNode unary,
            Expression ifNull)
        {
            var operand = CreateVariableExpressions(expressions, variables, unary.Operand, ifNull);
            return Expression.MakeUnary(unary.UnaryExpression.NodeType, operand, unary.Type);
        }

        private static void UnaryNextElement(
            IList<Expression> expressions,
            VaribalesCollection variables,
            Expression target,
            UnaryNode unary,
            Expression ifNull)
        {
            expressions.Add(Expression.Assign(target, UnaryMakeExpression(expressions, variables, unary, ifNull)));

            if (unary.Type.IsValueType && !unary.Type.IsNullable()) return;

            expressions.Add(Expression.IfThen(
                Expression.Equal(target, NullExpressionOf(unary.Type)),
                ifNull));
        }

        private static void UnaryLastElement(
            IList<Expression> expressions,
            VaribalesCollection variables,
            Type resultType,
            UnaryNode unary,
            Expression ifNull,
            LabelTarget returnTarget) =>
            expressions.Add(Expression.Return(returnTarget,
                Expression.Convert(UnaryMakeExpression(expressions, variables, unary, ifNull), resultType)));

        private static void BineryLastElement(
            IList<Expression> expressions,
            VaribalesCollection variables,
            Type resultType,
            BinaryNode binary,
            Expression ifNull,
            LabelTarget returnTarget) =>
            expressions.Add(Expression.Return(returnTarget,
                Expression.Convert(BinaryMakeExpression(expressions, variables, binary, ifNull), resultType)));

        private static void BinaryNextElement(
            IList<Expression> expressions,
            VaribalesCollection variables,
            Expression target,
            BinaryNode binary,
            Expression ifNull)
        {
            expressions.Add(Expression.Assign(target, BinaryMakeExpression(expressions, variables, binary, ifNull)));

            if (binary.Type.IsValueType && !binary.Type.IsNullable()) return;

            expressions.Add(Expression.IfThen(
                Expression.Equal(target, NullExpressionOf(binary.Type)),
                ifNull));
        }

        private static Expression BinaryMakeExpression(
            IList<Expression> expressions,
            VaribalesCollection variables,
            BinaryNode binary,
            Expression ifNull)
        {
            switch (binary.BinaryExpression.NodeType)
            {
                case ExpressionType.OrElse:
                    {
                        var right = new List<Expression>();
                        right.Add(Expression.Condition(
                            CreateVariableExpressions(right, variables, binary.Righttree, ifNull),
                            TrueConstantExpression, FalseConstantExpression));

                        var left = new List<Expression>();
                        left.Add(Expression.Condition(
                            CreateVariableExpressions(left, variables, binary.LeftNodes, ifNull),
                            TrueConstantExpression,
                            Expression.Block(right)));

                        var block = Expression.Block(left);
                        return block;
                    }
                case ExpressionType.AndAlso:
                    {
                        var right = new List<Expression>();
                        right.Add(Expression.Condition(
                            CreateVariableExpressions(right, variables, binary.Righttree, ifNull),
                            TrueConstantExpression, FalseConstantExpression));

                        var left = new List<Expression>();
                        left.Add(Expression.Condition(
                            CreateVariableExpressions(left, variables, binary.LeftNodes, ifNull),
                            Expression.Block(right), FalseConstantExpression));

                        var block = Expression.Block(left);
                        return block;
                    }
                case ExpressionType.Coalesce:
                    {
                        var lable = Expression.Label();
                        var right = new List<Expression>();
                        right.Add(CreateVariableExpressions(right, variables, binary.Righttree, ifNull));
                        var rightBlock = Expression.Block(right);
                        var left = new List<Expression>();
                        var coalesce = Expression.Coalesce(
                            CreateVariableExpressions(left, variables, binary.LeftNodes, Expression.Goto(lable)),
                            rightBlock);
                        left.Add(Expression.Label(lable));
                        left.Add(coalesce);

                        var block = Expression.Block(left);
                        return block;
                    }
                default:
                    {
                        var left = CreateVariableExpressions(expressions, variables, binary.LeftNodes, ifNull);
                        var right = CreateVariableExpressions(expressions, variables, binary.Righttree, ifNull);
                        return Expression.MakeBinary(binary.BinaryExpression.NodeType, left, right);
                    }
            }
        }

        private static Expression FunctionMakeExpression(
            IList<Expression> expressions,
            VaribalesCollection variables,
            FunctionNode function,
            Expression ifNull)
        {
            var args = function.Parameters.Select(functionParameter =>
                    CreateVariableExpressions(expressions, variables, functionParameter, ifNull))
                .ToList();

            return Expression.Call(function.MethodInfo, args);
        }

        private static void FunctionLastElement(
            IList<Expression> expressions,
            VaribalesCollection variables,
            Type resultType,
            FunctionNode function,
            Expression ifNull,
            LabelTarget returnTarget) =>
            expressions.Add(Expression.Return(returnTarget,
                Expression.Convert(FunctionMakeExpression(expressions, variables, function, ifNull), resultType)));

        private static void FunctionNextElement(
            IList<Expression> expressions,
            VaribalesCollection variables,
            Expression targetParameter,
            FunctionNode function,
            Expression ifNull)
        {
            expressions.Add(Expression.Assign(targetParameter,
                FunctionMakeExpression(expressions, variables, function, ifNull)));

            if (!function.Type.IsValueType || function.Type.IsNullable())
                expressions.Add(Expression.IfThen(
                    Expression.Equal(targetParameter, NullExpressionOf(function.Type)),
                    ifNull));
        }
    }
}