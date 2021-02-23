using System;
using System.Linq.Expressions;
using Anori.WinUI.Commands.Commands;
using JetBrains.Annotations;

namespace Anori.WinUI.Commands.Interfaces.Builders
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
        ISyncCanExecuteBuilder CanExecute([NotNull] ICanExecuteSubject canExecute);

        [NotNull]
        ISyncCanExecuteBuilder ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute);

        [NotNull]
        ISyncCanExecuteBuilder ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute, bool fallback);

        [NotNull]
        IActivatableSyncCommandBuilder Activatable();
    }
}