using System;
using System.Linq.Expressions;
using Anorisoft.WinUI.Commands.Commands;
using JetBrains.Annotations;

namespace Anorisoft.WinUI.Commands.Interfaces
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
        IActivatableSyncCanExecuteBuilder ObservesCommandManager();

        [NotNull]
        IActivatableSyncCanExecuteBuilder AutoActivate();
    }
}