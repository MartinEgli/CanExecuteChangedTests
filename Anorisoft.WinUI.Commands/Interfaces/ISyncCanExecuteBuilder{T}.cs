using System;
using System.Linq.Expressions;
using JetBrains.Annotations;

namespace Anorisoft.WinUI.Commands.Interfaces
{
    public interface ISyncCanExecuteBuilder<in T>
    {
        [NotNull]
        IActivatableSyncCommand<T> Build();

        [NotNull]
        IActivatableSyncCommand<T> Build([NotNull] Action<IActivatableSyncCommand<T>> setCommand);

        [NotNull]
        ISyncCanExecuteBuilder<T> ObservesProperty<TType>([NotNull] Expression<Func<TType>> canExecute);

        [NotNull]
        ISyncCanExecuteBuilder<T> ObservesCommandManager();

        [NotNull]
        ISyncCanExecuteBuilder<T> AutoActivate();
    }
}