using System;
using System.Linq.Expressions;
using JetBrains.Annotations;

namespace Anorisoft.WinUI.Commands.Interfaces
{
    public interface IActivatableSyncCommandBuilder
    {
        [NotNull]
        IActivatableSyncCommand Build();

        [NotNull]
        IActivatableSyncCommand Build([NotNull] Action<IActivatableSyncCommand> setCommand);

        [NotNull]
        IActivatableSyncCanExecuteBuilder CanExecute([NotNull] Func<bool> canExecute);

        [NotNull]
        IActivatableSyncCanExecuteBuilder ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute);

        [NotNull]
        IActivatableSyncCanExecuteBuilder ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute, bool fallback);

    }

    public interface IActivatableConcurrencySyncCommandBuilder<T>
    {
        [NotNull]
        IActivatableConcurrencySyncCommand<T> Build();

        [NotNull]
        IActivatableConcurrencySyncCommand<T> Build([NotNull] Action<IActivatableConcurrencySyncCommand<T>> setCommand);

        [NotNull]
        IActivatableConcurrencySyncCanExecuteBuilder<T> CanExecute([NotNull] Predicate<T> canExecute);

        [NotNull]
        IActivatableConcurrencySyncCanExecuteBuilder<T> ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute);

        [NotNull]
        IActivatableConcurrencySyncCanExecuteBuilder<T> ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute, bool fallback);
    }
}