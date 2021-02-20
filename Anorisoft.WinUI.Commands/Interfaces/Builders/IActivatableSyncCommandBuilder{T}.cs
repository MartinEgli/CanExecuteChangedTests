using System;
using System.Linq.Expressions;
using Anorisoft.WinUI.Commands.Commands;
using JetBrains.Annotations;

namespace Anorisoft.WinUI.Commands.Interfaces.Builders
{
    public interface IActivatableSyncCommandBuilder<T>
    {
        [NotNull]
        ActivatableCanExecuteObserverCommand<T> Build();

        [NotNull]
        ActivatableCanExecuteObserverCommand<T> Build([NotNull] Action<ActivatableCanExecuteObserverCommand<T>> setCommand);

        [NotNull]
        IActivatableSyncCanExecuteBuilder<T> CanExecute([NotNull] Predicate<T> canExecute);

        [NotNull]
        IActivatableSyncCanExecuteBuilder<T> ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute);

        [NotNull]
        IActivatableSyncCanExecuteBuilder<T> ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute, bool fallback);


        [NotNull]
        IActivatableSyncCanExecuteBuilder<T> AutoActivate();
    }
}