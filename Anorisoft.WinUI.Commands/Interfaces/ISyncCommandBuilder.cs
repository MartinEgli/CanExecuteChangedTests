using JetBrains.Annotations;
using System;
using System.Linq.Expressions;

namespace Anorisoft.WinUI.Commands.Interfaces
{
    public interface ISyncCommandBuilder
    {
        [NotNull]
        ISyncCommand Build();

        [NotNull]
        ISyncCommand Build([NotNull] Action<ISyncCommand> setCommand);

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