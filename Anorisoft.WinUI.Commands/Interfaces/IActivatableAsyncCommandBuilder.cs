using System;
using System.Linq.Expressions;
using JetBrains.Annotations;

namespace Anorisoft.WinUI.Commands.Interfaces
{
    public interface IActivatableAsyncCommandBuilder
    {
        [NotNull]
        IActivatableAsyncCommand Build();

        [NotNull]
        IActivatableAsyncCommand Build([NotNull] Action<IActivatableAsyncCommand> setCommand);

        [NotNull]
        IActivatableAsyncCanExecuteBuilder CanExecute([NotNull] Func<bool> canExecute);

        [NotNull]
        IActivatableAsyncCanExecuteBuilder ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute);

        [NotNull]
        IActivatableAsyncCanExecuteBuilder ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute, bool fallback);

    }
}