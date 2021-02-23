using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Anori.WinUI.Commands.Commands;
using JetBrains.Annotations;

namespace Anori.WinUI.Commands.Interfaces.Builders
{
    public interface IConcurrencyAsyncCanExecuteBuilder<T>
    {
        [NotNull]
        ConcurrencyAsyncCanExecuteObserverCommand<T> Build();

        [NotNull]
        ConcurrencyAsyncCanExecuteObserverCommand<T> Build(
            [NotNull] Action<ConcurrencyAsyncCanExecuteObserverCommand<T>> setCommand);

        [NotNull]
        IConcurrencyAsyncCanExecuteBuilder<T> ObservesProperty<TType>([NotNull] Expression<Func<TType>> expression);

        [NotNull]
        IConcurrencyAsyncCanExecuteBuilder<T> Observes([NotNull] ICanExecuteChangedSubject observer);

        [NotNull]
        IConcurrencyAsyncCanExecuteBuilder<T> ObservesCommandManager();

        [NotNull]
        IConcurrencyAsyncCanExecuteBuilder<T> AutoActivate();

        [NotNull]
        IConcurrencyAsyncCanExecuteBuilder<T> OnError([NotNull] Func<Exception, Task> error);

        [NotNull]
        IConcurrencyAsyncCanExecuteBuilder<T> OnCompleted([NotNull] Func<Task> completed);

        [NotNull]
        IConcurrencyAsyncCanExecuteBuilder<T> OnCancel([NotNull] Func<Task> cancel);
    }
}