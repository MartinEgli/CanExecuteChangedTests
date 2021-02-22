using System;
using System.Linq.Expressions;
using Anorisoft.WinUI.Commands.Commands;
using JetBrains.Annotations;

namespace Anorisoft.WinUI.Commands.Interfaces.Builders
{
    public interface IActivatableConcurrencyAsyncCanExecuteBuilder
    {
        [NotNull]
        ActivatableConcurrencyAsyncCanExecuteObserverCommand Build();

        [NotNull]
        ActivatableConcurrencyAsyncCanExecuteObserverCommand Build([NotNull] Action<ActivatableConcurrencyAsyncCanExecuteObserverCommand> setCommand);

        [NotNull]
        IActivatableConcurrencyAsyncCanExecuteBuilder ObservesProperty<TType>([NotNull] Expression<Func<TType>> expression);

        [NotNull]
        IActivatableConcurrencyAsyncCanExecuteBuilder Observes([NotNull] ICanExecuteChangedSubject observer);


        [NotNull]
        IActivatableConcurrencyAsyncCanExecuteBuilder ObservesCommandManager();

        [NotNull]
        IActivatableConcurrencyAsyncCanExecuteBuilder AutoActivate();


        [NotNull] IActivatableConcurrencyAsyncCanExecuteBuilder OnError([NotNull] Action<Exception> error);

        [NotNull] IActivatableConcurrencyAsyncCanExecuteBuilder OnCompleted([NotNull] Action completed);

        [NotNull] IActivatableConcurrencyAsyncCanExecuteBuilder OnCancel([NotNull] Action cancel);
    }
}