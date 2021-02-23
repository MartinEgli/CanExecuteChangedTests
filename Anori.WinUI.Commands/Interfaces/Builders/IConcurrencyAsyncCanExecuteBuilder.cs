using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Anori.WinUI.Commands.Commands;
using JetBrains.Annotations;

namespace Anori.WinUI.Commands.Interfaces.Builders
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

        [NotNull] IConcurrencyAsyncCanExecuteBuilder OnError([NotNull] Func<Exception, Task> error);

        [NotNull] IConcurrencyAsyncCanExecuteBuilder OnCompleted([NotNull] Func<Task> completed);

        [NotNull] IConcurrencyAsyncCanExecuteBuilder OnCancel([NotNull] Func<Task> cancel);

    }
}