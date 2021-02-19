using System;
using System.Linq.Expressions;
using JetBrains.Annotations;

namespace Anorisoft.WinUI.Commands.Interfaces
{
    public interface IActivatableConcurrencySyncCommandBuilder
    {
        [NotNull]
        IActivatableConcurrencySyncCommand Build();

        [NotNull]
        IActivatableConcurrencySyncCommand Build([NotNull] Action<IActivatableConcurrencySyncCommand> setCommand);

        [NotNull]
        IActivatableConcurrencySyncCanExecuteBuilder CanExecute([NotNull] Func<bool> canExecute);

        [NotNull]
        IActivatableConcurrencySyncCanExecuteBuilder ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute);

        [NotNull]
        IActivatableConcurrencySyncCanExecuteBuilder ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute,
            bool fallback);

        [NotNull]
        IActivatableConcurrencySyncCanExecuteBuilder Activatable();
    }
}