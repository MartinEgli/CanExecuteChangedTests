using System;
using System.Linq.Expressions;
using Anorisoft.WinUI.Commands.Commands;
using JetBrains.Annotations;

namespace Anorisoft.WinUI.Commands.Interfaces.Builders
{
    public interface IActivatableConcurrencySyncCommandBuilder
    {
        [NotNull]
        ActivatableConcurrencyCanExecuteObserverCommand Build();

        [NotNull]
        ActivatableConcurrencyCanExecuteObserverCommand Build([NotNull] Action<ActivatableConcurrencyCanExecuteObserverCommand> setCommand);

        [NotNull]
        IActivatableConcurrencySyncCanExecuteBuilder CanExecute([NotNull] Func<bool> canExecute);

        [NotNull]
        IActivatableConcurrencySyncCanExecuteBuilder ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute);

        [NotNull]
        IActivatableConcurrencySyncCanExecuteBuilder ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute,
            bool fallback);

        [NotNull]
        IActivatableConcurrencySyncCanExecuteBuilder AutoActivate();
    }
}