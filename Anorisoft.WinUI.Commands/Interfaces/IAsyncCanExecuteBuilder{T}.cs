using System;
using System.Linq.Expressions;
using Anorisoft.WinUI.Commands.Factory;
using JetBrains.Annotations;

namespace Anorisoft.WinUI.Commands.Interfaces
{
    public interface IAsyncCanExecuteBuilder<in T>
    {
        [NotNull]
        IAsyncCommand<T> Build();

        [NotNull]
        IAsyncCommand<T> Build([NotNull] Action<IAsyncCommand<T>> setCommand);

        [NotNull]
        IAsyncCanExecuteBuilder<T> ObservesProperty<TType>([NotNull] Expression<Func<TType>> canExecute);

        [NotNull]
        IAsyncCanExecuteBuilder<T> ObservesCommandManager();

        [NotNull]
        IConcurrencyAsyncCanExecuteBuilder AutoActivate();
    }
}