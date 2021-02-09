// -----------------------------------------------------------------------
// <copyright file="Command.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Threading;
using Anorisoft.WinUI.Common;

namespace Anorisoft.WinUI.Commands
{
    using JetBrains.Annotations;

    using System;
    using System.Windows.Input;

    /// <summary>
    ///     A Command whose sole purpose is to relay its functionality to other objects by invoking delegates.
    ///     The default return value for the CanExecute method is 'true'.
    /// </summary>
    /// <typeparam name="T">Parameter type</typeparam>
    /// <seealso cref="T:System.Windows.Input.ICommand" />
    public abstract class SyncCommandBase<T> : CommandBase, Interfaces.ICommand<T>, IDispatchableContext
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance has can execute.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has can execute; otherwise, <c>false</c>.
        /// </value>
        protected override bool HasCanExecute => this.canExecute != null;

        /// <summary>
        ///     The can execute
        /// </summary>
        [CanBeNull]
        private readonly Predicate<T> canExecute;

        /// <summary>
        ///     The execute
        /// </summary>
        [NotNull]
        private readonly Action<T> execute;

        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Anorisoft.WPF.Commands.Command`1" /> class and the Command can
        ///     always be executed.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        protected SyncCommandBase([NotNull] Action<T> execute) =>
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));

        /// <summary>
        /// Initializes a new instance of the <see cref="SyncCommandBase{T}"/> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecute">The can execute.</param>
        /// <exception cref="ArgumentNullException">canExecute</exception>
        protected SyncCommandBase([NotNull] Action<T> execute, [NotNull] Predicate<T> canExecute)
            : this(execute) =>
            this.canExecute = canExecute ?? throw new ArgumentNullException(nameof(canExecute));

        /// <summary>
        /// Determines whether this instance can execute the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>
        ///   <c>true</c> if this instance can execute the specified parameter; otherwise, <c>false</c>.
        /// </returns>
        public bool CanExecute([CanBeNull] T parameter)
        {
            return this.canExecute == null || this.canExecute(parameter);
        }

        /// <summary>
        /// Executes the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public void Execute([CanBeNull] T parameter)
        {
            if (this.CanExecute(parameter))
            {
                this.execute(parameter);
            }
        }

        /// <summary>
        ///     Determines whether this instance can execute the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        protected sealed override bool CanExecute(object parameter) => this.CanExecute((T)parameter);

        /// <summary>
        ///     Handle the internal invocation of <see cref="ICommand.Execute(object)" />
        /// </summary>
        /// <param name="parameter">Command Parameter</param>
        protected sealed override void Execute(object parameter) => this.Execute((T)parameter);


    }
}