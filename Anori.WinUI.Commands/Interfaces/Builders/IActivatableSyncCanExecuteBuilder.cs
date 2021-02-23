using System;
using System.Linq.Expressions;
using Anori.WinUI.Commands.Commands;
using JetBrains.Annotations;

namespace Anori.WinUI.Commands.Interfaces.Builders
{
    public interface IActivatableSyncCanExecuteBuilder
    {
        [NotNull]
        ActivatableCanExecuteObserverCommand Build();

        [NotNull]
        ActivatableCanExecuteObserverCommand Build([NotNull] Action<ActivatableCanExecuteObserverCommand> setCommand);

        [NotNull]
        IActivatableSyncCanExecuteBuilder ObservesProperty<TType>([NotNull] Expression<Func<TType>> expression);

        [NotNull]
        IActivatableSyncCanExecuteBuilder Observes([NotNull] ICanExecuteChangedSubject observer);

        [NotNull]
        IActivatableSyncCanExecuteBuilder ObservesCommandManager();

        [NotNull]
        IActivatableSyncCanExecuteBuilder AutoActivate();
    }
}