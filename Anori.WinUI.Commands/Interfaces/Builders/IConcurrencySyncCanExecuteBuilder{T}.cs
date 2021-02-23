using Anori.WinUI.Commands.Commands;
using JetBrains.Annotations;
using System;
using System.Linq.Expressions;

namespace Anori.WinUI.Commands.Interfaces.Builders
{
    public interface IConcurrencySyncCanExecuteBuilder<T>
    {
        [NotNull]
        ConcurrencyCanExecuteObserverCommand<T> Build();

        [NotNull]
        ConcurrencyCanExecuteObserverCommand<T> Build([NotNull] Action<ConcurrencyCanExecuteObserverCommand<T>> setCommand);

        [NotNull]
        IConcurrencySyncCanExecuteBuilder<T> ObservesProperty<TType>([NotNull] Expression<Func<TType>> expression);

        [NotNull]
        IConcurrencySyncCanExecuteBuilder<T> Observes([NotNull] ICanExecuteChangedSubject observer);

        [NotNull]
        IConcurrencySyncCanExecuteBuilder<T> ObservesCommandManager();

        [NotNull]
        IActivatableConcurrencySyncCanExecuteBuilder<T> Activatable();

        [NotNull] IConcurrencySyncCanExecuteBuilder<T> OnError([NotNull] Action<Exception> error);

        [NotNull] IConcurrencySyncCanExecuteBuilder<T> OnCompleted([NotNull] Action completed);

        [NotNull] IConcurrencySyncCanExecuteBuilder<T> OnCancel([NotNull] Action cancel);
    }
}