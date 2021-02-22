using System;
using System.Linq.Expressions;
using Anorisoft.WinUI.Commands.Commands;
using JetBrains.Annotations;

namespace Anorisoft.WinUI.Commands.Interfaces.Builders
{
    public interface IConcurrencySyncCanExecuteBuilder
    {
        [NotNull]
        ConcurrencyCanExecuteObserverCommand Build();

        [NotNull]
        ConcurrencyCanExecuteObserverCommand Build([NotNull] Action<ConcurrencyCanExecuteObserverCommand> setCommand);

        [NotNull]
        IActivatableConcurrencySyncCanExecuteBuilder Activatable();

        [NotNull]
        IConcurrencySyncCanExecuteBuilder ObservesProperty<TType>([NotNull] Expression<Func<TType>> expression);

        [NotNull]
        IConcurrencySyncCanExecuteBuilder Observes([NotNull] ICanExecuteChangedSubject observer);

        [NotNull]
        IConcurrencySyncCanExecuteBuilder ObservesCommandManager();

        [NotNull] IConcurrencySyncCanExecuteBuilder OnError([NotNull] Action<Exception> error);
        
        [NotNull] IConcurrencySyncCanExecuteBuilder OnCompleted([NotNull] Action completed);
        
        [NotNull] IConcurrencySyncCanExecuteBuilder OnCancel([NotNull] Action cancel);
    }
}