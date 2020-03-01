// -----------------------------------------------------------------------
// <copyright file="RelayCommandT.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace CanExecuteChangedTests
{
    using System;
    using System.Diagnostics;
    using System.Windows.Input;

    using JetBrains.Annotations;

    /// <inheritdoc />
    /// <summary>
    ///     A Command whose sole purpose is to relay its functionality to other objects by invoking delegates.
    ///     The default return value for the CanExecute method is 'true'.
    /// </summary>
    /// <typeparam name="T">Parameter type</typeparam>
    /// <seealso cref="T:System.Windows.Input.ICommand" />
    public class RelayCommandT<T> : ICommand
    {
        /// <summary>
        ///     The can execute
        /// </summary>
        private readonly Predicate<T> canExecute;

        /// <summary>
        ///     The execute
        /// </summary>
        private readonly Action<T> execute;

        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Anori.WPF.Commands.RelayCommand`1" /> class and the Command can
        ///     always be executed.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        public RelayCommandT(Action<T> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="RelayCommandT&lt;T&gt;" /> class.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public RelayCommandT([NotNull] Action<T> execute, [CanBeNull] Predicate<T> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException(nameof(execute));
            }

            this.execute = execute;
            this.canExecute = canExecute;
        }

        #region ICommand Members

        /// <inheritdoc />
        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (this.canExecute != null)
                {
                    CommandManager.RequerySuggested += value;
                }
            }

            remove
            {
                if (this.canExecute != null)
                {
                    CommandManager.RequerySuggested -= value;
                }
            }
        }

        /// <inheritdoc />
        /// <summary>
        ///     Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">
        ///     Data used by the command.  If the command does not require data to be passed, this object can
        ///     be set to <see langword="null" />.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if this command can be executed; otherwise, <see langword="false" />.
        /// </returns>
        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return this.canExecute == null || this.canExecute((T)parameter);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">
        ///     Data used by the command.  If the command does not require data to be passed, this object can
        ///     be set to <see langword="null" />.
        /// </param>
        public void Execute(object parameter)
        {
            this.execute((T)parameter);
        }

        #endregion ICommand Members
    }
}