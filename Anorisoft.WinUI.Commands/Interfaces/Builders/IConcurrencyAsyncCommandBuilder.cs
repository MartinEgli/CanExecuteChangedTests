using System;
using System.Linq.Expressions;
using Anorisoft.WinUI.Commands.Commands;
using JetBrains.Annotations;

namespace Anorisoft.WinUI.Commands.Interfaces.Builders
{
    public interface IConcurrencyAsyncCommandBuilder
    {
        [NotNull]
        ConcurrencyAsyncCanExecuteObserverCommand Build();

        [NotNull]
        ConcurrencyAsyncCanExecuteObserverCommand Build([NotNull] Action<ConcurrencyAsyncCanExecuteObserverCommand> setCommand);

        [NotNull]
        IConcurrencyAsyncCanExecuteBuilder CanExecute([NotNull] Func<bool> canExecute);

        [NotNull]
        IConcurrencyAsyncCanExecuteBuilder ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute);

        [NotNull]
        IConcurrencyAsyncCanExecuteBuilder ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute, bool fallback);

        [NotNull]
        IActivatableConcurrencyAsyncCanExecuteBuilder Activatable();
    }
}