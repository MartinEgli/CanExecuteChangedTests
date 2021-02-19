using System;
using System.Linq.Expressions;
using JetBrains.Annotations;

namespace Anorisoft.WinUI.Commands.Interfaces
{
    public interface ISyncCanExecuteBuilder
    {
        [NotNull]
        ISyncCommand Build();

        [NotNull]
        ISyncCommand Build([NotNull] Action<ISyncCommand> setCommand);

        [NotNull]
        ISyncCanExecuteBuilder ObservesProperty<TType>([NotNull] Expression<Func<TType>> expression);

        [NotNull]
        ISyncCanExecuteBuilder ObservesCommandManager();

        [NotNull]
        IActivatableSyncCanExecuteBuilder Activatable();
    }
}