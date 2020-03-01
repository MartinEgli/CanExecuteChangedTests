﻿// -----------------------------------------------------------------------
// <copyright file="DirectCommand.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Windows.Input;

namespace CanExecuteChangedTests
{
    /// <summary>
    /// </summary>
    /// <seealso cref="System.Windows.Input.ICommand" />
    public class DirectCommand : ICommand
    {
        /// <summary>
        ///     The can execute
        /// </summary>
        private Func<bool> canExecute;

        /// <summary>
        ///     The execute
        /// </summary>
        private Action execute;

        /// <summary>
        ///     Initializes a new instance of the <see cref="DirectAndRelayCommand" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        public DirectCommand(Action execute)
            : this(execute, DefaultCanExecute)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DirectAndRelayCommand" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecute">The can execute.</param>
        /// <exception cref="ArgumentNullException">
        ///     execute
        ///     or
        ///     canExecute
        /// </exception>
        public DirectCommand(Action execute, Func<bool> canExecute)
        {
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this.canExecute = canExecute ?? throw new ArgumentNullException(nameof(canExecute));
        }

        /// <summary>
        ///     Occurs when [can execute changed internal].
        /// </summary>
        private event EventHandler CanExecuteChangedInternal;

        /// <summary>
        ///     Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add => this.CanExecuteChangedInternal += value;

            remove => this.CanExecuteChangedInternal -= value;
        }

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
        public bool CanExecute(object parameter) => this.canExecute != null && this.canExecute();

        /// <summary>
        ///     Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">
        ///     Data used by the command.  If the command does not require data to be passed, this object can
        ///     be set to <see langword="null" />.
        /// </param>
        public void Execute(object parameter) => this.execute();

        /// <summary>
        ///     Called when [can execute changed].
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            var handler = this.CanExecuteChangedInternal;
            handler?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        ///     Destroys this instance.
        /// </summary>
        public void Destroy()
        {
            this.canExecute = () => false;
            this.execute = () => { };
        }

        /// <summary>
        ///     Defaults the can execute.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        private static bool DefaultCanExecute() => true;
    }
}