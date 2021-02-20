using System;
using System.Linq.Expressions;
using Anorisoft.WinUI.Commands.Commands;
using JetBrains.Annotations;

namespace Anorisoft.WinUI.Commands.Interfaces.Builders
{
    public interface IActivatableAsyncCommandBuilder
    {
        [NotNull]
        ActivatableAsyncCanExecuteObserverCommand Build();

        [NotNull]
        ActivatableAsyncCanExecuteObserverCommand Build([NotNull] Action<ActivatableAsyncCanExecuteObserverCommand> setCommand);

        [NotNull]
        IActivatableAsyncCanExecuteBuilder CanExecute([NotNull] Func<bool> canExecute);

        [NotNull]
        IActivatableAsyncCanExecuteBuilder CanExecute([NotNull] ICanExecuteSubject canExecute);


        [NotNull]
        IActivatableAsyncCanExecuteBuilder ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute);

        [NotNull]
        IActivatableAsyncCanExecuteBuilder ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute, bool fallback);

        [NotNull]
        IActivatableAsyncCanExecuteBuilder AutoActivate();

    }
}