using System;
using System.Linq.Expressions;
using Anorisoft.WinUI.Commands.Commands;
using JetBrains.Annotations;

namespace Anorisoft.WinUI.Commands.Interfaces.Builders
{
    public interface IAsyncCanExecuteBuilder<T>
    {
        [NotNull]
        AsyncCanExecuteObserverCommand<T> Build();

        [NotNull]
        AsyncCanExecuteObserverCommand<T> Build([NotNull] Action<AsyncCanExecuteObserverCommand<T>> setCommand);

        [NotNull]
        IAsyncCanExecuteBuilder<T> ObservesProperty<TType>([NotNull] Expression<Func<TType>> canExecute);

        [NotNull]
        IAsyncCanExecuteBuilder<T> ObservesCommandManager();

        [NotNull]
        IActivatableAsyncCanExecuteBuilder<T> Activatable();
    }
}