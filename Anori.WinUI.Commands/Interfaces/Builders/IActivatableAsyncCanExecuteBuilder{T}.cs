using System;
using System.Linq.Expressions;
using Anori.WinUI.Commands.Commands;
using JetBrains.Annotations;

namespace Anori.WinUI.Commands.Interfaces.Builders
{
    public interface IActivatableAsyncCanExecuteBuilder<T>
    {
        [NotNull]
        ActivatableAsyncCanExecuteObserverCommand<T> Build();

        [NotNull]
        ActivatableAsyncCanExecuteObserverCommand<T> Build([NotNull] Action<ActivatableAsyncCanExecuteObserverCommand<T>> setCommand);

        [NotNull]
        IActivatableAsyncCanExecuteBuilder<T> ObservesProperty<TType>([NotNull] Expression<Func<TType>> expression);

        [NotNull]
        IActivatableAsyncCanExecuteBuilder<T> Observes([NotNull] ICanExecuteChangedSubject observer);


        [NotNull]
        IActivatableAsyncCanExecuteBuilder<T> ObservesCommandManager();

        [NotNull]
        IActivatableAsyncCanExecuteBuilder<T> AutoActivate();
    }
}