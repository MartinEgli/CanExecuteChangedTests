using System;
using System.Linq.Expressions;
using Anorisoft.WinUI.Commands.Commands;
using JetBrains.Annotations;

namespace Anorisoft.WinUI.Commands.Interfaces.Builders
{
    public interface IActivatableAsyncCommandBuilder<T>
    {
        [NotNull]
        ActivatableAsyncCanExecuteObserverCommand<T> Build();

        [NotNull]
        ActivatableAsyncCanExecuteObserverCommand<T> Build([NotNull] Action<ActivatableAsyncCanExecuteObserverCommand<T>> setCommand);

        [NotNull]
        IActivatableAsyncCanExecuteBuilder<T> CanExecute([NotNull] Predicate<T> canExecute);

        [NotNull]
        IActivatableAsyncCanExecuteBuilder<T> ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute);

        [NotNull]
        IActivatableAsyncCanExecuteBuilder<T> ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute, bool fallback);


        [NotNull]
        IActivatableAsyncCanExecuteBuilder<T> AutoActivate();

    }
}