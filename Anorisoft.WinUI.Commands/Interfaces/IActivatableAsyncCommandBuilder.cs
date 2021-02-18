using System;
using System.Linq.Expressions;
using JetBrains.Annotations;

namespace Anorisoft.WinUI.Commands.Interfaces
{
    public interface IActivatableAsyncCommandBuilder
    {
        [NotNull]
        IActivatableAsyncCommand Build();

        [NotNull]
        IActivatableAsyncCommand Build([NotNull] Action<IActivatableAsyncCommand> setCommand);

        [NotNull]
        IActivatableAsyncCanExecuteBuilder CanExecute([NotNull] Func<bool> canExecute);

        [NotNull]
        IActivatableAsyncCanExecuteBuilder ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute);

        [NotNull]
        IActivatableAsyncCanExecuteBuilder ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute, bool fallback);

    }

    public interface IActivatableAsyncCommandBuilder<T>
    {
        [NotNull]
        IActivatableAsyncCommand<T> Build();

        [NotNull]
        IActivatableAsyncCommand<T> Build([NotNull] Action<IActivatableAsyncCommand<T>> setCommand);

        [NotNull]
        IActivatableAsyncCanExecuteBuilder<T> CanExecute([NotNull] Predicate<T> canExecute);

        [NotNull]
        IActivatableAsyncCanExecuteBuilder<T> ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute);

        [NotNull]
        IActivatableAsyncCanExecuteBuilder<T> ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute, bool fallback);

    }
}