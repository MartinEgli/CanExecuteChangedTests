// -----------------------------------------------------------------------
// <copyright file="PropertyObserver.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Linq.Expressions;
using JetBrains.Annotations;

namespace Anorisoft.PropertyObservers
{
    public sealed class PropertyObserver<TResult> : PropertyObserverBase<TResult> 
        where TResult : struct
    {
        [NotNull] public Action Action { get; }

        internal PropertyObserver(
            [NotNull] Expression<Func< TResult>> propertyExpression,
            [NotNull] Action action) : base(propertyExpression)
        {
            Action = action ?? throw new ArgumentNullException(nameof(action));
        }

        protected override void OnAction() => Action();
    }
}