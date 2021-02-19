using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Anorisoft.ExpressionObservers.Nodes;

namespace Anorisoft.PropertyObservers.Common
{
    public abstract class PropertyObserverBase : IDisposable, IEquatable<PropertyObserverBase>
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
        ///     The expression
        /// </summary>
        public abstract string ExpressionString { get; }

        /// <summary>
        /// Unsubscribes this instance.
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

        /// <summary>
        /// Creates the chain.
        /// </summary>
        /// <param name="parameter1">The parameter1.</param>
        /// <param name="nodes">The nodes.</param>
        /// <exception cref="NotSupportedException"></exception>
        protected void CreateChain(INotifyPropertyChanged parameter1, Tree nodes)
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

                            var root = new RootPropertyObserverNode(
                                propertyElement.PropertyInfo,
                                this.OnAction,
                                parameter1);
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

        /// <summary>
        /// Creates the chain.
        /// </summary>
        /// <param name="nodes">The nodes.</param>
        /// <exception cref="NotSupportedException"></exception>
        protected void CreateChain(Tree nodes)
        {
            foreach (var treeRoot in nodes.Roots)
            {
                switch (treeRoot)
                {
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
                        throw new NotSupportedException($"{treeRoot}");
                }
            }
        }

        /// <summary>
        /// Looptrees the specified expression node.
        /// </summary>
        /// <param name="expressionNode">The expression node.</param>
        /// <param name="observerNode">The observer node.</param>
        internal void Looptree(IExpressionNode expressionNode, PropertyObserverNode observerNode)
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

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other">other</paramref> parameter; otherwise, false.
        /// </returns>
        public bool Equals(PropertyObserverBase other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return RootNodes.SequenceEqual(other.RootNodes) && ExpressionString == other.ExpressionString;
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((PropertyObserverBase)obj);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return ((RootNodes != null ? RootNodes.GetHashCode() : 0) * 397) ^ (ExpressionString != null ? ExpressionString.GetHashCode() : 0);
            }
        }
    }
}