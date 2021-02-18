using System;
using System.Linq.Expressions;
using JetBrains.Annotations;

namespace Anorisoft.WinUI.Commands.Interfaces
{
    public interface IConcurrencySyncCommandBuilder<T>
    {
        [NotNull]
        IConcurrencySyncCommand<T> Build();

        [NotNull]
        IConcurrencySyncCommand<T> Build([NotNull] Action<IConcurrencySyncCommand<T>> setCommand);

        [NotNull]
        IConcurrencySyncCanExecuteBuilder<T> CanExecute([NotNull] Predicate<T> canExecute);

        [NotNull]
        IConcurrencySyncCanExecuteBuilder<T> ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute);

        [NotNull]
        IConcurrencySyncCanExecuteBuilder<T> ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute, bool fallback);

        [NotNull]
        IActivatableConcurrencySyncCanExecuteBuilder<T> Activateable();
    }
}