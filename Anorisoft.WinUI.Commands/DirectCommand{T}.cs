// -----------------------------------------------------------------------
// <copyright file="DirectCommand{T}.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anorisoft.WinUI.Commands
{
    using Anorisoft.WinUI.Common;

    using JetBrains.Annotations;

    using System;

    using Anorisoft.WinUI.Commands.Interfaces;

    /// <summary>
    /// </summary>
    /// <seealso cref="System.Windows.Input.ICommand" />
    public class DirectCommand<T> : SyncCommandBase<T>, IRaiseCanExecuteCommand
    {
        /// <summary>
        ///     Occurs when [can execute changed internal].
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <inheritdoc />
        public DirectCommand([NotNull] Action<T> execute)
            : base(execute)
        {
        }

        public sealed override event EventHandler CanExecuteChanged
        {
            add => this.Subscribe(value);
            remove => this.Unsubscribe(value);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DirectCommand{T}" /> class.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public DirectCommand([NotNull] Action<T> execute, [NotNull] Predicate<T> canExecute)
            : base(execute, canExecute)
        {
        }

        /// <summary>
        ///     Called when [can execute changed].
        /// </summary>
        public void RaiseCanExecuteChanged() => this.CanExecuteChangedInternal.RaiseEmpty(this);

        /// <summary>
        ///     Occurs when [can execute changed internal].
        /// </summary>
#pragma warning disable S3264 // Events should be invoked

        private event EventHandler CanExecuteChangedInternal;

#pragma warning restore S3264 // Events should be invoked

        /// <summary>
        ///     Subscribes the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        protected void Subscribe(EventHandler value)
        {
            if (this.HasCanExecute)
            {
                this.CanExecuteChangedInternal += value;
            }
        }

        /// <summary>
        ///     Unsubscribes the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        protected void Unsubscribe(EventHandler value)
        {
            if (this.HasCanExecute)
            {
                this.CanExecuteChangedInternal -= value;
            }
        }
    }
}