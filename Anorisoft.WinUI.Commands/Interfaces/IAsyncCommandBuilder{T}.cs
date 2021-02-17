using System;
using System.Linq.Expressions;
using JetBrains.Annotations;

namespace Anorisoft.WinUI.Commands.Interfaces
{
    public interface IAsyncCommandBuilder<T>
    {
        [NotNull]
        IAsyncCommand<T> Build();

        [NotNull]
        IAsyncCommand<T> Build([NotNull] Action<IAsyncCommand<T>> setCommand);

        [NotNull]
        IAsyncCanExecuteBuilder<T> CanExecute([NotNull] Predicate<T> canExecute);

        [NotNull]
        IAsyncCanExecuteBuilder<T> ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute);

        [NotNull]
        IAsyncCommandBuilder<T> ObservesCommandManager();
    }
}