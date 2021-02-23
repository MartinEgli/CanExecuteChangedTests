using System;
using System.Linq.Expressions;
using Anori.WinUI.Commands.Commands;
using JetBrains.Annotations;

namespace Anori.WinUI.Commands.Interfaces.Builders
{
    public interface ISyncCommandBuilder<T>
    {
        [NotNull]
        CanExecuteObserverCommand<T> Build();

        [NotNull]
        CanExecuteObserverCommand<T> Build([NotNull] Action<CanExecuteObserverCommand<T>> setCommand);

        [NotNull]
        ISyncCanExecuteBuilder<T> CanExecute([NotNull] Predicate<T> canExecute);

        [NotNull]
        ISyncCanExecuteBuilder<T> CanExecute([NotNull] ICanExecuteSubject canExecute);

        [NotNull]
        ISyncCanExecuteBuilder<T> ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute);

        [NotNull]
        ISyncCanExecuteBuilder<T> ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute, bool fallback);

        [NotNull]
        IActivatableSyncCommandBuilder<T> Activatable();
    }
}