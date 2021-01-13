// -----------------------------------------------------------------------
// <copyright file="DirectAndRelayCommand.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anorisoft.WinUI.Commands
{
    using JetBrains.Annotations;

    using System;
    using System.Windows.Input;

    using Anorisoft.WinUI.Commands.Interfaces;

    /// <summary>
    /// </summary>
    /// <seealso cref="SyncCommandBase" />
    /// <seealso cref="IRaiseCanExecuteCommand" />
    /// <seealso cref="System.Windows.Input.ICommand" />
    public class DirectAndRelayCommand : SyncCommandBase, IRaiseCanExecuteCommand
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DirectAndRelayCommand" /> class.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        public DirectAndRelayCommand([NotNull] Action execute)
            : base(execute)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DirectAndRelayCommand" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public DirectAndRelayCommand([NotNull] Action execute, [NotNull] Func<bool> canExecute)
            : base(execute, canExecute)
        {
        }

        /// <summary>
        ///     Called when [can execute changed].
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            var handler = this.CanExecuteChangedInternal;
            handler?.Invoke(this, EventArgs.Empty);
        }

        public sealed override event EventHandler CanExecuteChanged
        {
            add => this.Subscribe(value);
            remove => this.Unsubscribe(value);
        }

        /// <summary>
        ///     Occurs when [can execute changed internal].
        /// </summary>
        private event EventHandler CanExecuteChangedInternal;

        /// <summary>
        ///     Unsubscribes the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        protected void Unsubscribe(EventHandler value)
        {
            if (!this.HasCanExecute)
            {
                return;
            }

            CommandManager.RequerySuggested -= value;
            this.CanExecuteChangedInternal -= value;
        }

        /// <summary>
        ///     Subscribes the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        protected void Subscribe(EventHandler value)
        {
            if (!this.HasCanExecute)
            {
                return;
            }

            CommandManager.RequerySuggested += value;
            this.CanExecuteChangedInternal += value;
        }
    }
}