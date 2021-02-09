using System;
using System.ComponentModel;
using System.Linq.Expressions;
using Anorisoft.ExpressionObservers;
using Anorisoft.ExpressionObservers.Nodes;
using JetBrains.Annotations;

namespace Anorisoft.PropertyObservers
{
    public abstract class PropertyObserverBase<TParameter1, TParameter2, TResult> : PropertyObserverBase
        where TParameter1 : INotifyPropertyChanged
        where TParameter2 : INotifyPropertyChanged
        where TResult : struct
    {
        /// <summary>
        ///     The property expression
        /// </summary>
        private readonly Expression<Func<TParameter1, TParameter2, TResult>> propertyExpression;

        protected PropertyObserverBase(
            [NotNull] TParameter1 parameter1,
            [NotNull] TParameter2 parameter2,
            [NotNull] Expression<Func<TParameter1, TParameter2, TResult>> propertyExpression)
        {
            this.propertyExpression = propertyExpression ?? throw new ArgumentNullException(nameof(propertyExpression));
            this.Parameter1 = parameter1 ?? throw new ArgumentNullException(nameof(parameter1));
            this.Parameter2 = parameter2 ?? throw new ArgumentNullException(nameof(parameter2));
            this.ExpressionString = this.CreateChain(parameter1, parameter2);
        }

        /// <summary>
        ///     The expression
        /// </summary>
        public override string ExpressionString { get; }

        /// <summary>
        ///     Gets the parameter1.
        /// </summary>
        /// <value>
        ///     The parameter1.
        /// </value>
        [CanBeNull]
        public TParameter1 Parameter1 { get; }

        [CanBeNull]
        public TParameter2 Parameter2 { get; }

        /// <summary>
        /// Creates the chain.
        /// </summary>
        /// <param name="owner">The parameter1.</param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException">Operation not supported for the given expression type {expression.Type}. "
        ///                     + "Only MemberExpression and ConstantExpression are currently supported.</exception>
        protected string CreateChain(TParameter1 parameter1, TParameter2 parameter2)
        {
            var tree = ExpressionTree.GetTree(this.propertyExpression.Body);
            var expressionString = propertyExpression.ToString();

            CreateChain(parameter1, parameter2, tree);

            return expressionString;
        }

        private void CreateChain(TParameter1 parameter1, TParameter2 parameter2, ITree nodes)
        {
            foreach (var treeRoot in nodes.Roots)
            {
                switch (treeRoot)
                {
                    case ParameterNode parameterElement:
                        {
                            if (!(parameterElement.Next is PropertyNode propertyElement))
                            {
                                continue;
                            }
                            var parameterGetter = ExpressionGetter.CreateParameterGetter(
                                parameterElement, 
                                propertyExpression);
                            var root = new RootPropertyObserverNode(
                                propertyElement.PropertyInfo, 
                                this.OnAction, 
                                (INotifyPropertyChanged)parameterGetter(parameter1, parameter2));

                            Looptree(propertyElement, root);
                            RootNodes.Add(root);
                            break;
                        }
                    case ConstantNode constantElement when treeRoot.Next is FieldNode fieldElement:
                        {
                            if (!(fieldElement.Next is PropertyNode propertyElement))
                            {
                                continue;
                            }

                            var root = new RootPropertyObserverNode(
                                propertyElement.PropertyInfo,
                                this.OnAction,
                                (INotifyPropertyChanged)fieldElement.FieldInfo.GetValue(constantElement.Value));

                            Looptree(propertyElement, root);
                            RootNodes.Add(root);
                            break;
                        }
                    case ConstantNode constantElement:
                        {
                            if (!(treeRoot.Next is PropertyNode propertyElement))
                            {
                                continue;
                            }

                            var root = new RootPropertyObserverNode(
                                propertyElement.PropertyInfo,
                                this.OnAction,
                                (INotifyPropertyChanged)constantElement.Value);

                            Looptree(propertyElement, root);
                            RootNodes.Add(root);

                            break;
                        }
                    default:
                        throw new NotSupportedException();
                }
            }
        }
    }
}