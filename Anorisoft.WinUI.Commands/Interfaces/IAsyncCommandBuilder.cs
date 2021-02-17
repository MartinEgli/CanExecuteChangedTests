using JetBrains.Annotations;
using System;
using System.Linq.Expressions;

namespace Anorisoft.WinUI.Commands.Interfaces
{
    public interface IAsyncCommandBuilder
    {
        [NotNull]
        IAsyncCommand Build();

        [NotNull]
        IAsyncCommand Build([NotNull] Action<IAsyncCommand> setCommand);

        [NotNull]
        IAsyncCanExecuteBuilder CanExecute([NotNull] Func<bool> canExecute);

        [NotNull]
        IAsyncCanExecuteBuilder ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute);

        [NotNull]
        IAsyncCommandBuilder ObservesCommandManager();
    }
}