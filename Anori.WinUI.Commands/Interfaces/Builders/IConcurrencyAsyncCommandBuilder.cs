using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Anori.WinUI.Commands.Commands;
using JetBrains.Annotations;

namespace Anori.WinUI.Commands.Interfaces.Builders
{
    public interface IConcurrencyAsyncCommandBuilder
    {
        [NotNull]
        ConcurrencyAsyncCanExecuteObserverCommand Build();

        [NotNull]
        ConcurrencyAsyncCanExecuteObserverCommand Build(
            [NotNull] Action<ConcurrencyAsyncCanExecuteObserverCommand> setCommand);

        [NotNull]
        IConcurrencyAsyncCanExecuteBuilder CanExecute([NotNull] Func<bool> canExecute);

        [NotNull]
        IConcurrencyAsyncCanExecuteBuilder CanExecute([NotNull] ICanExecuteSubject canExecute);

        [NotNull]
        IConcurrencyAsyncCanExecuteBuilder ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute);

        [NotNull]
        IConcurrencyAsyncCanExecuteBuilder ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute,
            bool fallback);

        [NotNull]
        IActivatableConcurrencyAsyncCommandBuilder Activatable();

        [NotNull]
        IConcurrencyAsyncCommandBuilder OnError([NotNull] Func<Exception, Task> error);

        [NotNull]
        IConcurrencyAsyncCommandBuilder OnCompleted([NotNull] Func<Task> completed);

        [NotNull]
        IConcurrencyAsyncCommandBuilder OnCancel([NotNull] Func<Task> cancel);
    }
}