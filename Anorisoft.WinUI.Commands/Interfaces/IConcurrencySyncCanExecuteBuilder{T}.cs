using System;
using System.Linq.Expressions;
using JetBrains.Annotations;

namespace Anorisoft.WinUI.Commands.Interfaces
{
    public interface IConcurrencySyncCanExecuteBuilder<in T>
    {
        [NotNull]
        IActivatableSyncCommand<T> Build();

        [NotNull]
        IActivatableSyncCommand<T> Build([NotNull] Action<IActivatableSyncCommand<T>> setCommand);

        [NotNull]
        IConcurrencySyncCanExecuteBuilder<T> ObservesProperty<TType>([NotNull] Expression<Func<TType>> expression);

        [NotNull]
        IConcurrencySyncCanExecuteBuilder<T> ObservesCommandManager();

        [NotNull]
        IConcurrencySyncCanExecuteBuilder<T> AutoActivate();
    }
}