using System;
using System.Linq.Expressions;
using Anorisoft.WinUI.Commands.Commands;
using JetBrains.Annotations;

namespace Anorisoft.WinUI.Commands.Interfaces.Builders
{
    public interface IActivatableConcurrencyAsyncCommandBuilder<T>
    {
        [NotNull]
        ActivatableConcurrencyAsyncCanExecuteObserverCommand<T> Build();

        [NotNull]
        ActivatableConcurrencyAsyncCanExecuteObserverCommand<T> Build([NotNull] Action<ActivatableConcurrencyAsyncCanExecuteObserverCommand<T>> setCommand);

        [NotNull]
        IActivatableConcurrencyAsyncCanExecuteBuilder<T> CanExecute([NotNull] Predicate<T> canExecute);

        [NotNull]
        IActivatableConcurrencyAsyncCanExecuteBuilder<T> CanExecute([NotNull] ICanExecuteSubject canExecute);


        [NotNull]
        IActivatableConcurrencyAsyncCanExecuteBuilder<T> ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute);

        [NotNull]
        IActivatableConcurrencyAsyncCanExecuteBuilder<T> ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute, bool fallback);

        [NotNull]
        IActivatableConcurrencyAsyncCanExecuteBuilder<T> AutoActivate();


        [NotNull] IActivatableConcurrencyAsyncCanExecuteBuilder<T> OnError([NotNull] Action<Exception> error);

        [NotNull] IActivatableConcurrencyAsyncCanExecuteBuilder<T> OnCompleted([NotNull] Action completed);

        [NotNull] IActivatableConcurrencyAsyncCanExecuteBuilder<T> OnCancel([NotNull] Action cancel);

    }
}