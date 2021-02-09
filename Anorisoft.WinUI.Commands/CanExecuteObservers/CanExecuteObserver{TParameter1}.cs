// -----------------------------------------------------------------------
// <copyright file="CanExecuteObserver{TOwner}.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Anorisoft.PropertyObservers;

namespace Anorisoft.WinUI.Commands.CanExecuteObservers
{
    using System;
    using System.ComponentModel;
    using System.Linq.Expressions;
    using Anorisoft.WinUI.Common;
    using JetBrains.Annotations;

    public sealed class CanExecuteObserver<TParameter> : CanExecuteObserverBase
        where TParameter : INotifyPropertyChanged

    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CanExecuteObserver{TOwner}" /> class.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="canExecuteExpression">The can execute expression.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public CanExecuteObserver([NotNull] TParameter owner, [NotNull] Expression<Func<TParameter, bool>> canExecuteExpression)
        {
            if (owner == null)
            {
                throw new ArgumentNullException(nameof(owner));
            }

            if (canExecuteExpression == null)
            {
                throw new ArgumentNullException(nameof(canExecuteExpression));
            }

            this.Owner = owner;
            this.Observer = PropertyObserver.Observes<TParameter, bool>(owner, canExecuteExpression, () => this.Update.Raise());
            this.CanExecute = () => canExecuteExpression.Compile()(owner);
        }

        /// <summary>
        ///     Gets the owner.
        /// </summary>
        /// <value>
        ///     The owner.
        /// </value>
        public TParameter Owner { get; }

        /// <summary>
        ///     Occurs when [can execute changed].
        /// </summary>
        protected override event Action Update;

        /// <summary>
        ///     Creates the specified owner.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="canExecuteExpression">The can execute expression.</param>
        /// <returns></returns>
        public static CanExecuteObserver<TParameter> Create(
            TParameter owner,
            Expression<Func<TParameter, bool>> canExecuteExpression)
        {
            var instance = new CanExecuteObserver<TParameter>(owner, canExecuteExpression);
            instance.Subscribe();
            return instance;
        }
    }
}