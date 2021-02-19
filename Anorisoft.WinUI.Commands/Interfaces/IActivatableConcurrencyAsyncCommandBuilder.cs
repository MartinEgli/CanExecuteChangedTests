using System;
using System.Linq.Expressions;
using JetBrains.Annotations;

namespace Anorisoft.WinUI.Commands.Interfaces
{
    public interface IActivatableConcurrencyAsyncCommandBuilder
    {
        [NotNull]
        IActivatableConcurrencyAsyncCommand Build();

        [NotNull]
        IActivatableConcurrencyAsyncCommand Build([NotNull] Action<IActivatableConcurrencyAsyncCommand> setCommand);

        [NotNull]
        IActivatableConcurrencyAsyncCanExecuteBuilder CanExecute([NotNull] Func<bool> canExecute);

        [NotNull]
        IActivatableConcurrencyAsyncCanExecuteBuilder ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute);

        [NotNull]
        IActivatableConcurrencyAsyncCanExecuteBuilder ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute, bool fallback);

        [NotNull]
        IActivatableConcurrencyAsyncCanExecuteBuilder Activatable();

    }
}