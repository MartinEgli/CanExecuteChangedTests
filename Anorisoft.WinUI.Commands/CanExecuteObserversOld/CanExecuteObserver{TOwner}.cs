// -----------------------------------------------------------------------
// <copyright file="CanExecuteObserver{TOwner}.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Linq.Expressions;
using Anorisoft.WinUI.Common;
using Anorisoft.WinUI.Common.NotifyPropertyChangedObservers;
using JetBrains.Annotations;

namespace Anorisoft.WinUI.Commands.CanExecuteObserversOld
{
    public sealed class CanExecuteObserver<TOwner> : CanExecuteObserverBase<CanExecuteObserver<TOwner>>
        where TOwner : INotifyPropertyChanged

    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CanExecuteObserver{TOwner}" /> class.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="canExecuteExpression">The can execute expression.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public CanExecuteObserver([NotNull] TOwner owner, [NotNull] Expression<Func<TOwner, bool>> canExecuteExpression)
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
            this.Observer = PropertyObserver.Observes(owner, canExecuteExpression, () => this.Update.Raise(), false);
            this.CanExecute = () => canExecuteExpression.Compile()(owner);
        }

        /// <summary>
        ///     Gets the owner.
        /// </summary>
        /// <value>
        ///     The owner.
        /// </value>
        public TOwner Owner { get; }

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
        public static CanExecuteObserver<TOwner> Create(
            TOwner owner,
            Expression<Func<TOwner, bool>> canExecuteExpression)
        {
            var instance = new CanExecuteObserver<TOwner>(owner, canExecuteExpression);
            instance.Subscribe();
            return instance;
        }
    }
}