using System;
using System.Linq.Expressions;
using JetBrains.Annotations;

namespace Anorisoft.WinUI.Commands.Interfaces
{
    public interface IActivatableConcurrencySyncCanExecuteBuilder<in T>
    {
        [NotNull]
        IActivatableConcurrencySyncCommand<T> Build();

        [NotNull]
        IActivatableConcurrencySyncCommand<T> Build([NotNull] Action<IActivatableConcurrencySyncCommand<T>> setCommand);

        [NotNull]
        IActivatableConcurrencySyncCanExecuteBuilder<T> ObservesProperty<TType>([NotNull] Expression<Func<TType>> expression);

        [NotNull]
        IActivatableConcurrencySyncCanExecuteBuilder<T> ObservesCommandManager();

        [NotNull]
        IActivatableConcurrencySyncCanExecuteBuilder<T> AutoActivate();
    }
}