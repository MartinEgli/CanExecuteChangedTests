using System;
using System.Linq.Expressions;
using Anori.WinUI.Commands.Commands;
using JetBrains.Annotations;

namespace Anori.WinUI.Commands.Interfaces.Builders
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
        IAsyncCanExecuteBuilder<T> Observes([NotNull] ICanExecuteChangedSubject observer);

        [NotNull]
        IAsyncCanExecuteBuilder<T> ObservesCommandManager();

        [NotNull]
        IActivatableAsyncCanExecuteBuilder<T> Activatable();
    }
}