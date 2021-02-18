using System;
using System.Linq.Expressions;
using JetBrains.Annotations;

namespace Anorisoft.WinUI.Commands.Interfaces
{
    public interface IActivatableSyncCanExecuteBuilder<in T>
    {
        [NotNull]
        IActivatableSyncCommand<T> Build();

        [NotNull]
        IActivatableSyncCommand<T> Build([NotNull] Action<IActivatableSyncCommand<T>> setCommand);

        [NotNull]
        IActivatableSyncCanExecuteBuilder<T> ObservesProperty<TType>([NotNull] Expression<Func<TType>> expression);

        [NotNull]
        IActivatableSyncCanExecuteBuilder<T> ObservesCommandManager();

        [NotNull]
        IActivatableSyncCanExecuteBuilder<T> AutoActivate();
    }
}