// -----------------------------------------------------------------------
// <copyright file="CanExecuteObserver.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Linq.Expressions;
using Anorisoft.PropertyObservers;
using Anorisoft.WinUI.Common;
using JetBrains.Annotations;

namespace Anorisoft.WinUI.Commands.CanExecuteObservers
{
    public sealed class CanExecuteObserver : CanExecuteObserverBase
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CanExecuteObserver" /> class.
        /// </summary>
        /// <param name="canExecuteExpression">The can execute expression.</param>
        /// <exception cref="ArgumentNullException">canExecuteExpression</exception>
        public CanExecuteObserver([NotNull] Expression<Func<bool>> canExecuteExpression)
        {
            if (canExecuteExpression == null)
            {
                throw new ArgumentNullException(nameof(canExecuteExpression));
            }
            var observesAndGet = PropertyObserver.ObservesAndGet(canExecuteExpression, () => this.Update.Raise(), false);
            this.Observer = observesAndGet;
            this.CanExecute = observesAndGet.GetValue;
        }

        /// <summary>
        ///     Occurs when [can execute changed].
        /// </summary>
        protected override event Action Update;

        /// <summary>
        ///     Creates the specified can execute expression.
        /// </summary>
        /// <param name="canExecuteExpression">The can execute expression.</param>
        /// <returns></returns>
        public static CanExecuteObserver Create([NotNull] Expression<Func<bool>> canExecuteExpression)
        {
            var instance = new CanExecuteObserver(canExecuteExpression);
            instance.Subscribe();
            return instance;
        }
    }
}