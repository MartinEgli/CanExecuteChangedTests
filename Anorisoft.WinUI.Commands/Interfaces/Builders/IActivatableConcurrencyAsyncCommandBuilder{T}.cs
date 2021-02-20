using System;
using System.Linq.Expressions;
using Anorisoft.WinUI.Commands.Commands;
using JetBrains.Annotations;

namespace Anorisoft.WinUI.Commands.Interfaces.Builders
{
    public interface IActivatableConcurrencyAsyncCommandBuilder<T>
    {
        [NotNull]
        ActivatableConcurrencyAsyncCanExecuteObserverCommand<T> Build();

        [NotNull]
        ActivatableConcurrencyAsyncCanExecuteObserverCommand<T> Build([NotNull] Action<ActivatableConcurrencyAsyncCanExecuteObserverCommand<T>> setCommand);

        [NotNull]
        IActivatableConcurrencyAsyncCanExecuteBuilder<T> CanExecute([NotNull] Predicate<T> canExecute);

        [NotNull]
        IActivatableConcurrencyAsyncCanExecuteBuilder<T> ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute);

        [NotNull]
        IActivatableConcurrencyAsyncCanExecuteBuilder<T> ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute, bool fallback);

        [NotNull]
        IActivatableConcurrencyAsyncCanExecuteBuilder<T> AutoActivate();

    }
}