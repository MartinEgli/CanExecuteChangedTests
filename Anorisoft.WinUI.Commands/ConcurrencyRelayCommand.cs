// -----------------------------------------------------------------------
// <copyright file="ConcurrencyRelayCommand.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anorisoft.WinUI.Commands
{
    using JetBrains.Annotations;

    using System;
    using System.Threading;
    using System.Windows.Input;

    public class ConcurrencyRelayCommand : ConcurrencyCommandBase
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ConcurrencyRelayCommand" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="completed">The completed.</param>
        /// <param name="error">The error.</param>
        /// <param name="cancel">The cancel.</param>
        public ConcurrencyRelayCommand(
            [NotNull] Action<CancellationToken> execute,
            [CanBeNull] Action completed = null,
            [CanBeNull] Action<Exception> error = null,
            [CanBeNull] Action cancel = null)
            : base(execute, completed, error, cancel)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ConcurrencyRelayCommand" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecute">The can execute.</param>
        /// <param name="completed">The completed.</param>
        /// <param name="error">The error.</param>
        /// <param name="cancel">The cancel.</param>
        public ConcurrencyRelayCommand(
            [NotNull] Action<CancellationToken> execute,
            [NotNull] Func<bool> canExecute,
            [CanBeNull] Action completed = null,
            [CanBeNull] Action<Exception> error = null,
            [CanBeNull] Action cancel = null)
            : base(execute, canExecute, completed, error, cancel)
        {
        }

        /// <summary>
        ///     Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public sealed override event EventHandler CanExecuteChanged
        {
            add => this.Subscribe(value);
            remove => this.Unsubscribe(value);
        }

        /// <summary>
        ///     Raises the can execute command.
        /// </summary>
        public override void RaiseCanExecuteCommand()
        {
            if (this.Dispatcher.CheckAccess())
            {
                CommandManager.InvalidateRequerySuggested();
            }
            else
            {
                this.Dispatcher.Invoke(CommandManager.InvalidateRequerySuggested);
            }
        }

        /// <summary>
        ///     Subscribes the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        protected void Subscribe(EventHandler value)
        {
            if (this.HasCanExecute)
            {
                CommandManager.RequerySuggested += value;
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
                CommandManager.RequerySuggested -= value;
            }
        }
    }
}