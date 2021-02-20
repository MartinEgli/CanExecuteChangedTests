using System;
using System.Linq.Expressions;
using Anorisoft.WinUI.Commands.Commands;
using JetBrains.Annotations;

namespace Anorisoft.WinUI.Commands.Interfaces.Builders
{
    public interface IAsyncCommandBuilder
    {
        [NotNull]
        AsyncCanExecuteObserverCommand Build();

        [NotNull]
        AsyncCanExecuteObserverCommand Build([NotNull] Action<AsyncCanExecuteObserverCommand> setCommand);

        [NotNull]
        IAsyncCanExecuteBuilder CanExecute([NotNull] Func<bool> canExecute);

        [NotNull]
        IAsyncCanExecuteBuilder CanExecute([NotNull] ICanExecuteSubject canExecute);


        [NotNull]
        IAsyncCanExecuteBuilder ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute);

        [NotNull]
        IAsyncCommandBuilder ObservesCommandManager();

        [NotNull]
        IActivatableAsyncCommandBuilder Activatable();
    }
}