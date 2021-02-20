using System;
using System.Linq.Expressions;
using Anorisoft.WinUI.Commands.Commands;
using JetBrains.Annotations;

namespace Anorisoft.WinUI.Commands.Interfaces.Builders
{
    public interface IActivatableSyncCommandBuilder
    {
        [NotNull]
        ActivatableCanExecuteObserverCommand Build();

        [NotNull]
        ActivatableCanExecuteObserverCommand Build([NotNull] Action<ActivatableCanExecuteObserverCommand> setCommand);

        [NotNull]
        IActivatableSyncCanExecuteBuilder CanExecute([NotNull] Func<bool> canExecute);

        [NotNull]
        IActivatableSyncCanExecuteBuilder CanExecute([NotNull] ICanExecuteSubject canExecute);


        [NotNull]
        IActivatableSyncCanExecuteBuilder ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute);

        [NotNull]
        IActivatableSyncCanExecuteBuilder ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute, bool fallback);

        [NotNull]
        IActivatableSyncCanExecuteBuilder AutoActivate();
    }
}