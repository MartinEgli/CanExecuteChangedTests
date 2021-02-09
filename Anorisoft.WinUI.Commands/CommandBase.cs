// -----------------------------------------------------------------------
// <copyright file="CommandBase.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Threading;
using Anorisoft.WinUI.Common;

namespace Anorisoft.WinUI.Commands
{
    using JetBrains.Annotations;

    using System;
    using System.Diagnostics;
    using System.Windows.Input;

    public abstract class CommandBase : ICommand, IDispatchableContext
    {
        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public abstract event EventHandler CanExecuteChanged;

        /// <summary>
        /// Gets the synchronization context.
        /// </summary>
        /// <value>
        /// The synchronization context.
        /// </value>
        public SynchronizationContext SynchronizationContext { get; } = SynchronizationContext.Current;

        /// <summary>
        /// Gets or sets a value indicating whether this instance has can execute.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has can execute; otherwise, <c>false</c>.
        /// </value>
        protected abstract bool HasCanExecute { get; }

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
        bool ICommand.CanExecute([CanBeNull] object parameter) => this.CanExecute(parameter);

        /// <inheritdoc />
        /// <summary>
        ///     Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">
        ///     Data used by the command.  If the command does not require data to be passed, this object can
        ///     be set to <see langword="null" />.
        /// </param>
        void ICommand.Execute([CanBeNull] object parameter)
        {
            this.Execute(parameter);
        }

        /// <summary>
        ///     Determines whether this instance can execute the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        protected abstract bool CanExecute([CanBeNull] object parameter);

        /// <summary>
        ///     Handle the internal invocation of <see cref="ICommand.Execute(object)" />
        /// </summary>
        /// <param name="parameter">Command Parameter</param>
        protected abstract void Execute([CanBeNull] object parameter);
    }
}