// -----------------------------------------------------------------------
// <copyright file="PropertyObserver.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Linq.Expressions;
using Anorisoft.PropertyObservers.ValueObservers;
using JetBrains.Annotations;

namespace Anorisoft.PropertyObservers.ReferenceObservers
{
    public sealed class PropertyReferenceObserver<TParameter1, TParameter2, TResult> : 
            PropertyReferenceObserverBase<TParameter1, TParameter2, TResult>
        where TParameter1 : INotifyPropertyChanged
        where TParameter2 : INotifyPropertyChanged
        where TResult : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyValueObserver{TParameter1,TParameter2,TResult}"/> class.
        /// </summary>
        /// <param name="parameter2">The parameter1.</param>
        /// <param name="propertyExpression">The parameter2.</param>
        /// <param name="action">The property expression.</param>
        /// <param name="action">The action.</param>
        /// <exception cref="PropertyValueObserver{TParameter1,TParameter2,TResult}">action</exception>
        internal PropertyReferenceObserver(
            [NotNull] TParameter1 parameter1,
            [NotNull] TParameter2 parameter2,
            [NotNull] Expression<Func<TParameter1, TParameter2, TResult>> propertyExpression,
            [NotNull] Action action) : base(parameter1, parameter2, propertyExpression) =>
            Action = action ?? throw new ArgumentNullException(nameof(action));

        /// <summary>
        /// Gets the action.
        /// </summary>
        /// <value>
        /// The action.
        /// </value>
        [NotNull] public Action Action { get; }

        /// <summary>
        /// The action
        /// </summary>
        protected override void OnAction() => Action();
    }
}