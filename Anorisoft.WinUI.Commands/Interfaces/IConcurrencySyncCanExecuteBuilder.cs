using System;
using System.Linq.Expressions;
using JetBrains.Annotations;

namespace Anorisoft.WinUI.Commands.Interfaces
{
    public interface IConcurrencySyncCanExecuteBuilder
    {
        [NotNull]
        IActivatableSyncCommand Build();

        [NotNull]
        IActivatableSyncCommand Build([NotNull] Action<IActivatableSyncCommand> setCommand);

        [NotNull]
        IConcurrencySyncCanExecuteBuilder ObservesProperty<TType>([NotNull] Expression<Func<TType>> expression);

        [NotNull]
        IConcurrencySyncCanExecuteBuilder ObservesCommandManager();

        [NotNull]
        IConcurrencySyncCanExecuteBuilder AutoActivate();
    }
}