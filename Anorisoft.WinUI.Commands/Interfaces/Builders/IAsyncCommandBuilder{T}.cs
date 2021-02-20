using System;
using System.Linq.Expressions;
using Anorisoft.WinUI.Commands.Commands;
using JetBrains.Annotations;

namespace Anorisoft.WinUI.Commands.Interfaces.Builders
{
    public interface IAsyncCommandBuilder<T>
    {
        [NotNull]
        AsyncCanExecuteObserverCommand<T> Build();

        /// <summary>
        /// Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns></returns>
        [NotNull]
        AsyncCanExecuteObserverCommand<T> Build([NotNull] Action<AsyncCanExecuteObserverCommand<T>> setCommand);

        /// <summary>
        /// Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        [NotNull]
        IAsyncCanExecuteBuilder<T> CanExecute([NotNull] Predicate<T> canExecute);

        /// <summary>
        /// Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        [NotNull]
        IAsyncCanExecuteBuilder<T> ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute);

        /// <summary>
        /// Observeses the command manager.
        /// </summary>
        /// <returns></returns>
        [NotNull]
        IAsyncCommandBuilder<T> ObservesCommandManager();

        /// <summary>
        /// Activatables this instance.
        /// </summary>
        /// <returns></returns>
        [NotNull]
        IActivatableAsyncCanExecuteBuilder<T> Activatable();
    }
}