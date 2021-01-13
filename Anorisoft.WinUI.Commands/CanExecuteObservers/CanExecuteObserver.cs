// -----------------------------------------------------------------------
// <copyright file="CanExecuteObserver.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anorisoft.WinUI.Commands.CanExecuteObservers
{
    using System;
    using System.ComponentModel;
    using System.Linq.Expressions;

    using Anorisoft.WinUI.Common;
    using Anorisoft.WinUI.Common.NotifyPropertyChangedObservers;

    using JetBrains.Annotations;

    public sealed class CanExecuteObserver : CanExecuteObserverBase<CanExecuteObserver>
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

            this.Observer = PropertyObserver.Observes(canExecuteExpression, () => this.Update.Raise(), false);
            this.Owner = this.Observer.Owner ?? throw new NullReferenceException(nameof(this.Owner));
            this.CanExecute = canExecuteExpression.Compile();
        }

        /// <summary>
        ///     Gets the owner.
        /// </summary>
        /// <value>
        ///     The owner.
        /// </value>
        [NotNull]
        public INotifyPropertyChanged Owner { get; }

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