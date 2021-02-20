using System;
using System.Linq.Expressions;
using Anorisoft.WinUI.Commands.Commands;
using JetBrains.Annotations;

namespace Anorisoft.WinUI.Commands.Interfaces.Builders
{
    public interface IConcurrencySyncCommandBuilder<T>
    {
        [NotNull]
        ConcurrencyCanExecuteObserverCommand<T> Build();

        [NotNull]
        ConcurrencyCanExecuteObserverCommand<T> Build([NotNull] Action<ConcurrencyCanExecuteObserverCommand<T>> setCommand);

        [NotNull]
        IConcurrencySyncCanExecuteBuilder<T> CanExecute([NotNull] Predicate<T> canExecute);

        [NotNull]
        IConcurrencySyncCanExecuteBuilder<T> ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute);

        [NotNull]
        IConcurrencySyncCanExecuteBuilder<T> ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute,
            bool fallback);

        [NotNull]
        IActivatableConcurrencySyncCanExecuteBuilder<T> Activatable();
    }
}