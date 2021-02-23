using System;
using System.Linq.Expressions;
using Anori.WinUI.Commands.Commands;
using JetBrains.Annotations;

namespace Anori.WinUI.Commands.Interfaces.Builders
{
    public interface IActivatableConcurrencySyncCommandBuilder<T>
    {
        [NotNull]
        ActivatableConcurrencyCanExecuteObserverCommand<T> Build();

        [NotNull]
        ActivatableConcurrencyCanExecuteObserverCommand<T> Build(
            [NotNull] Action<ActivatableConcurrencyCanExecuteObserverCommand<T>> setCommand);

        [NotNull]
        IActivatableConcurrencySyncCanExecuteBuilder<T> CanExecute([NotNull] Predicate<T> canExecute);

        [NotNull]
        IActivatableConcurrencySyncCanExecuteBuilder<T> CanExecute([NotNull] ICanExecuteSubject canExecute);

        [NotNull]
        IActivatableConcurrencySyncCanExecuteBuilder<T> ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute);

        [NotNull]
        IActivatableConcurrencySyncCanExecuteBuilder<T> ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute,
            bool fallback);

        [NotNull]
        IActivatableConcurrencySyncCanExecuteBuilder<T> AutoActivate();

        [NotNull]
        IActivatableConcurrencySyncCommandBuilder<T> OnError([NotNull] Action<Exception> error);

        [NotNull]
        IActivatableConcurrencySyncCommandBuilder<T> OnCompleted([NotNull] Action completed);

        [NotNull]
        IActivatableConcurrencySyncCommandBuilder<T> OnCancel([NotNull] Action cancel);
    }
}