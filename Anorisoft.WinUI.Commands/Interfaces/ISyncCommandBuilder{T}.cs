using System;
using System.Linq.Expressions;
using Anorisoft.WinUI.Commands.Factory;
using JetBrains.Annotations;

namespace Anorisoft.WinUI.Commands.Interfaces
{
    public interface ISyncCommandBuilder<T> 
    {
        [NotNull]
        ISyncCommand<T> Build();

        [NotNull]
        ISyncCommand<T> Build([NotNull] Action<ISyncCommand<T>> setCommand);

        [NotNull]
        ISyncCanExecuteBuilder<T> CanExecute([NotNull] Predicate<T> canExecute);

        [NotNull]
        ISyncCanExecuteBuilder<T> ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute);

        [NotNull]
        ISyncCanExecuteBuilder<T> ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute, bool fallback);

        [NotNull]
        IActivatableSyncCanExecuteBuilder<T> Activatable();
    }
}