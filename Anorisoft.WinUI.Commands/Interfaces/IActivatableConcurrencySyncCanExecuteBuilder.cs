using System;
using System.Linq.Expressions;
using JetBrains.Annotations;

namespace Anorisoft.WinUI.Commands.Interfaces
{
    public interface IActivatableConcurrencySyncCanExecuteBuilder
    {
        [NotNull]
        IActivatableConcurrencySyncCommand Build();

        [NotNull]
        IActivatableConcurrencySyncCommand Build([NotNull] Action<IActivatableConcurrencySyncCommand> setCommand);

        [NotNull]
        IActivatableConcurrencySyncCanExecuteBuilder ObservesProperty<TType>([NotNull] Expression<Func<TType>> expression);

        [NotNull]
        IActivatableConcurrencySyncCanExecuteBuilder ObservesCommandManager();

        [NotNull]
        IActivatableConcurrencySyncCanExecuteBuilder AutoActivate();
    }

    public interface IActivatableConcurrencyAsyncCanExecuteBuilder
    {
        [NotNull]
        IActivatableConcurrencyAsyncCommand Build();

        [NotNull]
        IActivatableConcurrencyAsyncCommand Build([NotNull] Action<IActivatableConcurrencyAsyncCommand> setCommand);

        [NotNull]
        IActivatableConcurrencyAsyncCanExecuteBuilder ObservesProperty<TType>([NotNull] Expression<Func<TType>> expression);

        [NotNull]
        IActivatableConcurrencyAsyncCanExecuteBuilder ObservesCommandManager();

        [NotNull]
        IActivatableConcurrencyAsyncCanExecuteBuilder AutoActivate();
    }

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