using System;
using System.Collections.Generic;
using System.ComponentModel;
using Anorisoft.ExpressionObservers.Nodes;

namespace Anorisoft.PropertyObservers
{
    public abstract class PropertyObserverBase : IDisposable
    {
        /// <summary>
        ///     The root observerNode
        /// </summary>
        internal IList<RootPropertyObserverNode> RootNodes { get; } = new List<RootPropertyObserverNode>();

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Unsubscribe();
        }

        /// <summary>
        ///     Subscribes this instance.
        /// </summary>
        public void Subscribe()
        {
            foreach (var rootPropertyObserverNode in RootNodes)
            {
                rootPropertyObserverNode.SubscribeListenerForOwner();
            }

            OnAction();
        }

        /// <summary>
        ///     Unsubscribes this instance.
        /// </summary>
        public void Unsubscribe()
        {
            foreach (var rootPropertyObserverNode in RootNodes)
            {
                rootPropertyObserverNode.UnsubscribeListener();
            }
        }

        /// <summary>
        ///     The action
        /// </summary>
        protected abstract void OnAction();

        protected void CreateChain(INotifyPropertyChanged parameter1, RootNodeCollection nodes)
        {
            foreach (var elementsRoot in nodes.Roots)
            {
                switch (elementsRoot)
                {
                    case ParameterNode parameterElement:
                        {
                            if (!(parameterElement.Next is PropertyNode propertyElement))
                            {
                                continue;
                            }

                            var root = new RootPropertyObserverNode(
                                propertyElement.PropertyInfo,
                                this.OnAction,
                                parameter1);
                            LoopElements(propertyElement, root);
                            RootNodes.Add(root);
                            break;
                        }
                    case ConstantNode constantElement when elementsRoot.Next is FieldNode fieldElement:
                        {
                            if (!(fieldElement.Next is PropertyNode propertyElement))
                            {
                                continue;
                            }

                            var root = new RootPropertyObserverNode(
                                propertyElement.PropertyInfo,
                                this.OnAction,
                                (INotifyPropertyChanged)fieldElement.FieldInfo.GetValue(constantElement.Value));

                            LoopElements(propertyElement, root);
                            RootNodes.Add(root);
                            break;
                        }
                    case ConstantNode constantElement:
                        {
                            if (!(elementsRoot.Next is PropertyNode propertyElement))
                            {
                                continue;
                            }

                            var root = new RootPropertyObserverNode(
                                propertyElement.PropertyInfo,
                                this.OnAction,
                                (INotifyPropertyChanged)constantElement.Value);

                            LoopElements(propertyElement, root);
                            RootNodes.Add(root);

                            break;
                        }
                    default:
                        throw new NotSupportedException();
                }
            }
        }

        protected void CreateChain(RootNodeCollection nodes)
        {
            foreach (var elementsRoot in nodes.Roots)
            {
                switch (elementsRoot)
                {
                    case ConstantNode constantElement when elementsRoot.Next is FieldNode fieldElement:
                        {
                            if (!(fieldElement.Next is PropertyNode propertyElement))
                            {
                                continue;
                            }

                            var root = new RootPropertyObserverNode(
                                propertyElement.PropertyInfo,
                                this.OnAction,
                                (INotifyPropertyChanged)fieldElement.FieldInfo.GetValue(constantElement.Value));

                            LoopElements(propertyElement, root);
                            RootNodes.Add(root);
                            break;
                        }
                    case ConstantNode constantElement:
                        {
                            if (!(elementsRoot.Next is PropertyNode propertyElement))
                            {
                                continue;
                            }

                            var root = new RootPropertyObserverNode(
                                propertyElement.PropertyInfo,
                                this.OnAction,
                                (INotifyPropertyChanged)constantElement.Value);

                            LoopElements(propertyElement, root);
                            RootNodes.Add(root);

                            break;
                        }
                    default:
                        throw new NotSupportedException();
                }
            }
        }

        internal void LoopElements(IExpressionNode expressionNode, PropertyObserverNode observerNode)
        {
            var previousNode = observerNode;
            while (expressionNode.Next != null && expressionNode.Next is PropertyNode property)
            {
                var currentNode = new PropertyObserverNode(property.PropertyInfo, this.OnAction);

                previousNode.Previous = currentNode;
                previousNode = currentNode;
                expressionNode = expressionNode.Next;
            }
        }
    }
}