using System;
using System.Linq.Expressions;
using Anorisoft.WinUI.Commands.Commands;
using JetBrains.Annotations;

namespace Anorisoft.WinUI.Commands.Interfaces.Builders
{
    public interface IConcurrencyAsyncCommandBuilder<T>
    {
        [NotNull]
        ConcurrencyAsyncCanExecuteObserverCommand<T> Build();

        [NotNull]
        ConcurrencyAsyncCanExecuteObserverCommand<T> Build([NotNull] Action<ConcurrencyAsyncCanExecuteObserverCommand<T>> setCommand);

        [NotNull]
        IConcurrencyAsyncCanExecuteBuilder<T> CanExecute([NotNull] Predicate<T> canExecute);

        [NotNull]
        IConcurrencyAsyncCanExecuteBuilder<T> CanExecute([NotNull] ICanExecuteSubject canExecute);


        [NotNull]
        IConcurrencyAsyncCanExecuteBuilder<T> ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute);

        [NotNull]
        IConcurrencyAsyncCanExecuteBuilder<T> ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute, bool fallback);

        [NotNull]
        IActivatableConcurrencyAsyncCommandBuilder<T> Activatable();
    }
}