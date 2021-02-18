using System;
using System.Linq.Expressions;
using JetBrains.Annotations;

namespace Anorisoft.WinUI.Commands.Interfaces
{
    public interface IConcurrencyAsyncCommandBuilder
    {
        [NotNull]
        IConcurrencyAsyncCommand Build();

        [NotNull]
        IConcurrencyAsyncCommand Build([NotNull] Action<IConcurrencyAsyncCommand> setCommand);

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