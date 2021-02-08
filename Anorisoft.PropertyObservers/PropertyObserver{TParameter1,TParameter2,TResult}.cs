// -----------------------------------------------------------------------
// <copyright file="PropertyObserver.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Linq.Expressions;
using JetBrains.Annotations;

namespace Anorisoft.PropertyObservers
{
    public sealed class
        PropertyObserver<TParameter1, TParameter2, TResult> : PropertyObserverBase<TParameter1, TParameter2, TResult>
        where TParameter1 : INotifyPropertyChanged
        where TParameter2 : INotifyPropertyChanged
        where TResult : struct
    {
        internal PropertyObserver(
            [NotNull] TParameter1 parameter1,
            [NotNull] TParameter2 parameter2,
            [NotNull] Expression<Func<TParameter1, TParameter2, TResult>> propertyExpression,
            [NotNull] Action action) : base(parameter1, parameter2, propertyExpression)
        {
            Action = action ?? throw new ArgumentNullException(nameof(action));
        }

        [NotNull] public Action Action { get; }

        protected override void OnAction() => Action();
    }
}