using System;
using System.Linq.Expressions;
using JetBrains.Annotations;

namespace Anorisoft.WinUI.Commands.Interfaces
{
    public interface IConcurrencyAsyncCanExecuteBuilder<in T>
    {
        [NotNull]
        IConcurrencyAsyncCommand<T> Build();

        [NotNull]
        IConcurrencyAsyncCommand<T> Build([NotNull] Action<IConcurrencyAsyncCommand<T>> setCommand);

        [NotNull]
        IConcurrencyAsyncCanExecuteBuilder<T> ObservesProperty<TType>([NotNull] Expression<Func<TType>> expression);

        [NotNull]
        IConcurrencyAsyncCanExecuteBuilder<T> ObservesCommandManager();

        [NotNull]
        IConcurrencyAsyncCanExecuteBuilder<T> AutoActivate();
    }
}