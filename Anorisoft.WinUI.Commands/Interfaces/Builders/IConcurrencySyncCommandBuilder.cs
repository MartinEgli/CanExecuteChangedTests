using System;
using System.Linq.Expressions;
using Anorisoft.WinUI.Commands.Commands;
using JetBrains.Annotations;

namespace Anorisoft.WinUI.Commands.Interfaces.Builders
{
    public interface IConcurrencySyncCommandBuilder
    {
        [NotNull]
        ConcurrencyCanExecuteObserverCommand Build();

        [NotNull]
        ConcurrencyCanExecuteObserverCommand Build([NotNull] Action<ConcurrencyCanExecuteObserverCommand> setCommand);

        [NotNull]
        IConcurrencySyncCanExecuteBuilder CanExecute([NotNull] Func<bool> canExecute);

        [NotNull]
        IConcurrencySyncCanExecuteBuilder CanExecute([NotNull] ICanExecuteSubject canExecute);


        [NotNull]
        IConcurrencySyncCanExecuteBuilder ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute);

        [NotNull]
        IConcurrencySyncCanExecuteBuilder ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute, bool fallback);

        [NotNull]
        IActivatableConcurrencySyncCommandBuilder Activatable();


        [NotNull] IConcurrencySyncCanExecuteBuilder OnError([NotNull] Action<Exception> error);

        [NotNull] IConcurrencySyncCanExecuteBuilder OnCompleted([NotNull] Action completed);

        [NotNull] IConcurrencySyncCanExecuteBuilder OnCancel([NotNull] Action cancel);
    }
}