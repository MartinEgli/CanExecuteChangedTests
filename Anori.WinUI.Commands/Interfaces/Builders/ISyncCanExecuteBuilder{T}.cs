using System;
using System.Linq.Expressions;
using Anori.WinUI.Commands.Commands;
using JetBrains.Annotations;

namespace Anori.WinUI.Commands.Interfaces.Builders
{
    public interface ISyncCanExecuteBuilder<T>
    {
        [NotNull]
        CanExecuteObserverCommand<T> Build();

        [NotNull]
        CanExecuteObserverCommand<T> Build([NotNull] Action<CanExecuteObserverCommand<T>> setCommand);

        [NotNull]
        ISyncCanExecuteBuilder<T> ObservesProperty<TType>([NotNull] Expression<Func<TType>> canExecute);

        [NotNull]
        ISyncCanExecuteBuilder<T> Observes([NotNull] ICanExecuteChangedSubject observer);

        [NotNull]
        ISyncCanExecuteBuilder<T> ObservesCommandManager();

        [NotNull]
        IActivatableSyncCanExecuteBuilder<T> Activatable();
    }
}