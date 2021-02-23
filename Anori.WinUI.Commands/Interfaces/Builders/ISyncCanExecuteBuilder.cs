using System;
using System.Linq.Expressions;
using Anori.WinUI.Commands.Commands;
using JetBrains.Annotations;

namespace Anori.WinUI.Commands.Interfaces.Builders
{
    public interface ISyncCanExecuteBuilder
    {
        [NotNull]
        CanExecuteObserverCommand Build();

        [NotNull]
        CanExecuteObserverCommand Build([NotNull] Action<CanExecuteObserverCommand> setCommand);

        [NotNull]
        ISyncCanExecuteBuilder ObservesProperty<TType>([NotNull] Expression<Func<TType>> expression);

        [NotNull]
        ISyncCanExecuteBuilder Observes([NotNull] ICanExecuteChangedSubject observer);

        [NotNull]
        ISyncCanExecuteBuilder ObservesCommandManager();

        [NotNull]
        IActivatableSyncCanExecuteBuilder Activatable();
    }
}