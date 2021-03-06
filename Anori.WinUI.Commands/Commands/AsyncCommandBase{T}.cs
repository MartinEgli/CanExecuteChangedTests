﻿// -----------------------------------------------------------------------
// <copyright file="AsyncCommandBase{T}.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Anori.WinUI.Commands.Interfaces;
using Anori.WinUI.Common;
using CanExecuteChangedTests;
using JetBrains.Annotations;

namespace Anori.WinUI.Commands.Commands
{
    public interface IExecutable
    {
        bool IsExecuting { get; }
    }

    /// <summary>
    ///     AsyncCommandBase class.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="Anori.WinUI.Commands.CommandBase" />
    /// <seealso cref="Anori.WinUI.Commands.Interfaces.IAsyncCommand{T}" />
    public abstract class AsyncCommandBase<T> : 
        CommandBase, 
        IAsyncCommand<T>, 
        IExecutable, 
        INotifyPropertyChanged
    {
        /// <summary>
        ///     The can execute
        /// </summary>
        [CanBeNull] private readonly Predicate<T> canExecute;

        /// <summary>
        ///     The error handler
        /// </summary>
        [CanBeNull] private readonly Action<Exception> error;

        /// <summary>
        ///     The execute
        /// </summary>
        [NotNull] private readonly Func<T, Task> execute;

        /// <summary>
        ///     The is executing
        /// </summary>
        private bool isExecuting;

        /// <summary>
        ///     Initializes a new instance of the <see cref="AsyncCommandBase{T}" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecute">The can execute.</param>
        /// <param name="errorHandler">The error handler.</param>
        protected AsyncCommandBase(
            [NotNull] Func<T, Task> execute,
            [NotNull] Predicate<T> canExecute,
            [NotNull] Action<Exception> error)
            : this(execute, canExecute) =>
            this.error = error ?? throw new ArgumentNullException(nameof(error));

        /// <summary>
        ///     Initializes a new instance of the <see cref="AsyncRelayCommand{T}" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecute">The can execute.</param>
        /// <exception cref="ArgumentNullException">canExecute</exception>
        protected AsyncCommandBase(
            [NotNull] Func<T, Task> execute, 
            [NotNull] Predicate<T> canExecute)
            : this(execute) =>
            this.canExecute = canExecute ?? throw new ArgumentNullException(nameof(canExecute));

        /// <summary>
        /// Initializes a new instance of the <see cref="AsyncCommandBase{T}"/> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecuteSubject">The can execute subject.</param>
        /// <exception cref="ArgumentNullException">canExecuteSubject</exception>
        protected AsyncCommandBase(
            [NotNull] Func<T, Task> execute,
            [NotNull] ICanExecuteSubject canExecuteSubject)
            : this(execute)
        {
            if (canExecuteSubject == null)
            {
                throw new ArgumentNullException(nameof(canExecuteSubject));
            }

            this.canExecute = (t) => canExecuteSubject.CanExecute();
        }
        
        /// <summary>
        ///     Initializes a new instance of the <see cref="AsyncRelayCommand{T}" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="error">The error.</param>
        /// <exception cref="ArgumentNullException">errorHandler</exception>
        protected AsyncCommandBase(
            [NotNull] Func<T, Task> execute, 
            [NotNull] Action<Exception> error)
            : this(execute) =>
            this.error = error ?? throw new ArgumentNullException(nameof(error));

        /// <summary>
        ///     Initializes a new instance of the <see cref="AsyncRelayCommand{T}" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <exception cref="ArgumentNullException">execute</exception>
        protected AsyncCommandBase(
            [NotNull] Func<T, Task> execute) =>
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));

        /// <summary>
        ///     Gets or sets a value indicating whether this instance has can execute.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance has can execute; otherwise, <c>false</c>.
        /// </value>
        protected override bool HasCanExecute => this.canExecute == null;

        /// <summary>
        ///     Determines whether this instance can execute the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>
        ///     <c>true</c> if this instance can execute the specified parameter; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool CanExecute([CanBeNull] T parameter) => !this.isExecuting && (this.canExecute == null || this.canExecute(parameter));

        /// <summary>
        ///     Executes the asynchronous.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public async Task ExecuteAsync(T parameter)
        {
        //    if (this.CanExecute(parameter))
            {
                try
                {
                    this.IsExecuting = true;

                    await this.execute(parameter).ConfigureAwait(true);
                }
                finally
                {
                    this.IsExecuting = false;
                }
            }
        }

        /// <summary>
        ///     The is executing
        /// </summary>
        public bool IsExecuting
        {
            get => this.isExecuting;
            set
            {
                if (this.isExecuting == value)
                {
                    return;
                }

                this.isExecuting = value;
                this.RaisePropertyChanged();
                this.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        ///     Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Raises the can execute changed.
        /// </summary>
        public abstract void RaiseCanExecuteChanged();

        /// <summary>
        ///     Determines whether this instance can execute the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        protected sealed override bool CanExecute(object parameter) => this.CanExecute((T)parameter);

        /// <summary>
        ///     Handle the internal invocation of <see cref="ISyncCommand.Execute(object)" />
        /// </summary>
        /// <param name="parameter">Command Parameter</param>
        protected sealed override void Execute(object parameter) =>
            this.ExecuteAsync((T)parameter).FireAndForgetSafeAsync(this.error);

        /// <summary>
        ///     Called when [property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        [NotifyPropertyChangedInvocator]
        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null) =>
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}