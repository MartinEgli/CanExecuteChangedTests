using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Anori.WinUI.Commands.Commands;
using JetBrains.Annotations;

namespace Anori.WinUI.Commands.Interfaces.Builders
{
    public interface IActivatableConcurrencyAsyncCanExecuteBuilder
    {
        [NotNull]
        ActivatableConcurrencyAsyncCanExecuteObserverCommand Build();

        [NotNull]
        ActivatableConcurrencyAsyncCanExecuteObserverCommand Build(
            [NotNull] Action<ActivatableConcurrencyAsyncCanExecuteObserverCommand> setCommand);

        [NotNull]
        IActivatableConcurrencyAsyncCanExecuteBuilder ObservesProperty<TType>(
            [NotNull] Expression<Func<TType>> expression);

        [NotNull]
        IActivatableConcurrencyAsyncCanExecuteBuilder Observes([NotNull] ICanExecuteChangedSubject observer);

        [NotNull]
        IActivatableConcurrencyAsyncCanExecuteBuilder ObservesCommandManager();

        [NotNull]
        IActivatableConcurrencyAsyncCanExecuteBuilder AutoActivate();

        [NotNull]
        IActivatableConcurrencyAsyncCanExecuteBuilder OnError([NotNull] Func<Exception, Task> error);

        [NotNull]
        IActivatableConcurrencyAsyncCanExecuteBuilder OnCompleted([NotNull] Func<Task> completed);

        [NotNull]
        IActivatableConcurrencyAsyncCanExecuteBuilder OnCancel([NotNull] Func<Task> cancel);
    }
}