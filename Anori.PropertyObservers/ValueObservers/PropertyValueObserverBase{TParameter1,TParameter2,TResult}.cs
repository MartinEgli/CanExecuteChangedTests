using System;
using System.ComponentModel;
using System.Linq.Expressions;
using Anori.ExpressionObservers;
using Anori.ExpressionObservers.Nodes;
using Anori.PropertyObservers.Common;
using JetBrains.Annotations;

namespace Anori.PropertyObservers.ValueObservers
{
    public abstract class PropertyValueObserverBase<TParameter1, TParameter2, TResult> : PropertyObserverBase
        where TParameter1 : INotifyPropertyChanged
        where TParameter2 : INotifyPropertyChanged
        where TResult : struct
    {
        /// <summary>
        ///     The property expression
        /// </summary>
        private readonly Expression<Func<TParameter1, TParameter2, TResult>> propertyExpression;

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyObserverBase{TParameter1, TParameter2, TResult}"/> class.
        /// </summary>
        /// <param name="parameter1">The parameter1.</param>
        /// <param name="parameter2">The parameter2.</param>
        /// <param name="propertyExpression">The property expression.</param>
        /// <exception cref="ArgumentNullException">
        /// propertyExpression
        /// or
        /// parameter1
        /// or
        /// parameter2
        /// </exception>
        protected PropertyValueObserverBase(
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

        /// <summary>
        /// Gets the parameter2.
        /// </summary>
        /// <value>
        /// The parameter2.
        /// </value>
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

        /// <summary>
        /// Creates the chain.
        /// </summary>
        /// <param name="parameter1">The parameter1.</param>
        /// <param name="parameter2">The parameter2.</param>
        /// <param name="nodes">The nodes.</param>
        /// <exception cref="NotSupportedException"></exception>
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


    public abstract class PropertyObserverBase<TParameter1, TParameter2, TResult> : PropertyObserverBase
      where TParameter1 : INotifyPropertyChanged
      where TParameter2 : INotifyPropertyChanged
    {
        /// <summary>
        ///     The property expression
        /// </summary>
        private readonly Expression<Func<TParameter1, TParameter2, TResult>> propertyExpression;

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyValueObserverBase{TParameter1, TParameter2, TResult}"/> class.
        /// </summary>
        /// <param name="parameter1">The parameter1.</param>
        /// <param name="parameter2">The parameter2.</param>
        /// <param name="propertyExpression">The property expression.</param>
        /// <exception cref="ArgumentNullException">
        /// propertyExpression
        /// or
        /// parameter1
        /// or
        /// parameter2
        /// </exception>
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

        /// <summary>
        /// Gets the parameter2.
        /// </summary>
        /// <value>
        /// The parameter2.
        /// </value>
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

        /// <summary>
        /// Creates the chain.
        /// </summary>
        /// <param name="parameter1">The parameter1.</param>
        /// <param name="parameter2">The parameter2.</param>
        /// <param name="nodes">The nodes.</param>
        /// <exception cref="NotSupportedException"></exception>
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