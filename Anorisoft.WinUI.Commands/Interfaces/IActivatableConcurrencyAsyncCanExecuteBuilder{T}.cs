using System;
using System.Linq.Expressions;
using JetBrains.Annotations;

namespace Anorisoft.WinUI.Commands.Interfaces
{
    public interface IActivatableConcurrencyAsyncCanExecuteBuilder<in T>
    {
        [NotNull]
        IActivatableConcurrencyAsyncCommand<T> Build();

        [NotNull]
        IActivatableConcurrencyAsyncCommand<T> Build([NotNull] Action<IActivatableConcurrencyAsyncCommand<T>> setCommand);

        [NotNull]
        IActivatableConcurrencyAsyncCanExecuteBuilder<T> ObservesProperty<TType>([NotNull] Expression<Func<TType>> expression);

        [NotNull]
        IActivatableConcurrencyAsyncCanExecuteBuilder<T> ObservesCommandManager();

        [NotNull]
        IActivatableConcurrencyAsyncCanExecuteBuilder<T> AutoActivate();
    }
}