using System;
using System.Linq.Expressions;
using Anorisoft.WinUI.Commands.Commands;
using JetBrains.Annotations;

namespace Anorisoft.WinUI.Commands.Interfaces.Builders
{
    public interface IConcurrencyAsyncCanExecuteBuilder
    {
        [NotNull]
        ConcurrencyAsyncCanExecuteObserverCommand Build();

        [NotNull]
        ConcurrencyAsyncCanExecuteObserverCommand Build([NotNull] Action<ConcurrencyAsyncCanExecuteObserverCommand> setCommand);

        [NotNull]
        IConcurrencyAsyncCanExecuteBuilder ObservesProperty<TType>([NotNull] Expression<Func<TType>> expression);

        [NotNull]
        IConcurrencyAsyncCanExecuteBuilder Observes([NotNull] ICanExecuteChangedSubject observer);

        [NotNull]
        IConcurrencyAsyncCanExecuteBuilder ObservesCommandManager();

        [NotNull]
        IActivatableConcurrencyAsyncCanExecuteBuilder Activatable();

        [NotNull] IConcurrencyAsyncCanExecuteBuilder OnError([NotNull] Action<Exception> error);

        [NotNull] IConcurrencyAsyncCanExecuteBuilder OnCompleted([NotNull] Action completed);

        [NotNull] IConcurrencyAsyncCanExecuteBuilder OnCancel([NotNull] Action cancel);

    }
}