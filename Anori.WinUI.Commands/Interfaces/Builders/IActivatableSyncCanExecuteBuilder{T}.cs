using System;
using System.Linq.Expressions;
using Anori.WinUI.Commands.Commands;
using JetBrains.Annotations;

namespace Anori.WinUI.Commands.Interfaces.Builders
{
    public interface IActivatableSyncCanExecuteBuilder<T>
    {
        [NotNull]
        ActivatableCanExecuteObserverCommand<T> Build();

        [NotNull]
        ActivatableCanExecuteObserverCommand<T> Build([NotNull] Action<ActivatableCanExecuteObserverCommand<T>> setCommand);

        [NotNull]
        IActivatableSyncCanExecuteBuilder<T> ObservesProperty<TType>([NotNull] Expression<Func<TType>> expression);

        [NotNull]
        IActivatableSyncCanExecuteBuilder<T> Observes([NotNull] ICanExecuteChangedSubject observer);


        [NotNull]
        IActivatableSyncCanExecuteBuilder<T> ObservesCommandManager();

        [NotNull]
        IActivatableSyncCanExecuteBuilder<T> AutoActivate();
    }
}