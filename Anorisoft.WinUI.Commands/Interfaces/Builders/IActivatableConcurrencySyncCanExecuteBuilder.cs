using System;
using System.Linq.Expressions;
using Anorisoft.WinUI.Commands.Commands;
using JetBrains.Annotations;

namespace Anorisoft.WinUI.Commands.Interfaces.Builders
{
    public interface IActivatableConcurrencySyncCanExecuteBuilder
    {
        [NotNull]
        ActivatableConcurrencyCanExecuteObserverCommand Build();

        [NotNull]
        ActivatableConcurrencyCanExecuteObserverCommand Build([NotNull] Action<ActivatableConcurrencyCanExecuteObserverCommand> setCommand);

        [NotNull]
        IActivatableConcurrencySyncCanExecuteBuilder ObservesProperty<TType>([NotNull] Expression<Func<TType>> expression);

        [NotNull]
        IActivatableConcurrencySyncCanExecuteBuilder Observes([NotNull] ICanExecuteChangedSubject observer);


        [NotNull]
        IActivatableConcurrencySyncCanExecuteBuilder ObservesCommandManager();

        [NotNull]
        IActivatableConcurrencySyncCanExecuteBuilder AutoActivate();


        [NotNull] IActivatableConcurrencySyncCanExecuteBuilder OnError([NotNull] Action<Exception> error);

        [NotNull] IActivatableConcurrencySyncCanExecuteBuilder OnCompleted([NotNull] Action completed);

        [NotNull] IActivatableConcurrencySyncCanExecuteBuilder OnCancel([NotNull] Action cancel);
    }
}