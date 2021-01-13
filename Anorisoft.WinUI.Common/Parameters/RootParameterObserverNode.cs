// -----------------------------------------------------------------------
// <copyright file="RootParameterObserverNode.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anorisoft.WinUI.Common.Parameters
{
    using System;
    using System.ComponentModel;
    using System.Reflection;

    using Anorisoft.WinUI.Common.NotifyPropertyChangedObservers;

    internal class RootParameterObserverNode : ParameterObserverNode
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RootPropertyObserverNode" /> class.
        /// </summary>
        /// <param name="propertyInfo">The property information.</param>
        /// <param name="action">The action.</param>
        /// <param name="owner">The owner.</param>
        public RootParameterObserverNode(PropertyInfo propertyInfo, Action action, object owner)
            : base(propertyInfo, action)
        {
            this.Owner = owner;
            if (owner is IReadOnlyParameter p)
            {
                this.Parameter = p;
            }
            else
            {
                if (propertyInfo.GetValue(owner) is IReadOnlyParameter v)
                {
                    this.Parameter = v;
                }
            }
        }

        /// <summary>
        ///     Gets the owner.
        /// </summary>
        /// <value>
        ///     The owner.
        /// </value>
        public object Owner { get; }

        public IReadOnlyParameter Parameter { get; }

        /// <summary>
        ///     Subscribes the listener for owner.
        /// </summary>
        public void SubscribeListenerForOwner()
        {
            this.SubscribeListenerFor(this.Parameter);
        }
    }
}