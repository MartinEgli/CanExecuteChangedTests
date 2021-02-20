using JetBrains.Annotations;
using System;
using System.Linq.Expressions;
using Anorisoft.WinUI.Commands.Commands;

namespace Anorisoft.WinUI.Commands.Interfaces
{
    public interface ISyncCommandBuilder
    {
        [NotNull]
        CanExecuteObserverCommand Build();

        [NotNull]
        CanExecuteObserverCommand Build([NotNull] Action<CanExecuteObserverCommand> setCommand);

        [NotNull]
        ISyncCanExecuteBuilder CanExecute([NotNull] Func<bool> canExecute);

        [NotNull]
        ISyncCanExecuteBuilder ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute);

        [NotNull]
        ISyncCanExecuteBuilder ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute, bool fallback);

        [NotNull]
        IActivatableSyncCanExecuteBuilder Activatable();
    }
}