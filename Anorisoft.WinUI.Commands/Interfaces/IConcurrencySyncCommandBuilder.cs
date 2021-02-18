using System;
using System.Linq.Expressions;
using JetBrains.Annotations;

namespace Anorisoft.WinUI.Commands.Interfaces
{
    public interface IConcurrencySyncCommandBuilder 
    {
        [NotNull]
        IConcurrencySyncCommand Build();

        [NotNull]
        IConcurrencySyncCommand Build([NotNull] Action<IConcurrencySyncCommand> setCommand);

        [NotNull]
        IConcurrencySyncCanExecuteBuilder CanExecute([NotNull] Func<bool> canExecute);

        [NotNull]
        IConcurrencySyncCanExecuteBuilder ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute);

        [NotNull]
        IConcurrencySyncCanExecuteBuilder ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute, bool fallback);

        [NotNull]
        IActivatableConcurrencySyncCanExecuteBuilder Activatable();
    }
}