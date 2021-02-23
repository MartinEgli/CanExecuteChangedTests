using System;
using System.Linq.Expressions;
using Anori.WinUI.Commands.Commands;
using JetBrains.Annotations;

namespace Anori.WinUI.Commands.Interfaces.Builders
{
    public interface IActivatableConcurrencySyncCanExecuteBuilder<T>
    {
        [NotNull]
        ActivatableConcurrencyCanExecuteObserverCommand<T> Build();

        [NotNull]
        ActivatableConcurrencyCanExecuteObserverCommand<T> Build(
            [NotNull] Action<ActivatableConcurrencyCanExecuteObserverCommand<T>> setCommand);

        [NotNull]
        IActivatableConcurrencySyncCanExecuteBuilder<T> ObservesProperty<TType>(
            [NotNull] Expression<Func<TType>> expression);

        [NotNull]
        IActivatableConcurrencySyncCanExecuteBuilder<T> Observes([NotNull] ICanExecuteChangedSubject observer);

        [NotNull]
        IActivatableConcurrencySyncCanExecuteBuilder<T> ObservesCommandManager();

        [NotNull]
        IActivatableConcurrencySyncCanExecuteBuilder<T> AutoActivate();

        [NotNull]
        IActivatableConcurrencySyncCanExecuteBuilder<T> OnError([NotNull] Action<Exception> error);

        [NotNull]
        IActivatableConcurrencySyncCanExecuteBuilder<T> OnCompleted([NotNull] Action completed);

        [NotNull]
        IActivatableConcurrencySyncCanExecuteBuilder<T> OnCancel([NotNull] Action cancel);
    }
}