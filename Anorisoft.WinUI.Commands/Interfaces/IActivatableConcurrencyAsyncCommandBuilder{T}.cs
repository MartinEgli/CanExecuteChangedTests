using System;
using System.Linq.Expressions;
using JetBrains.Annotations;

namespace Anorisoft.WinUI.Commands.Interfaces
{
    public interface IActivatableConcurrencyAsyncCommandBuilder<T>
    {
        [NotNull]
        IActivatableConcurrencyAsyncCommand<T> Build();

        [NotNull]
        IActivatableConcurrencyAsyncCommand<T> Build([NotNull] Action<IActivatableConcurrencyAsyncCommand<T>> setCommand);

        [NotNull]
        IActivatableConcurrencyAsyncCanExecuteBuilder<T> CanExecute([NotNull] Predicate<T> canExecute);

        [NotNull]
        IActivatableConcurrencyAsyncCanExecuteBuilder<T> ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute);

        [NotNull]
        IActivatableConcurrencyAsyncCanExecuteBuilder<T> ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute, bool fallback);

        [NotNull]
        IActivatableConcurrencyAsyncCanExecuteBuilder<T> Activatable();

    }
}