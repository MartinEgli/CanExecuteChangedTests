using System;
using System.Linq.Expressions;
using JetBrains.Annotations;

namespace Anorisoft.WinUI.Commands.Interfaces
{
    public interface IActivatableConcurrencySyncCanExecuteBuilder
    {
        [NotNull]
        IActivatableConcurrencySyncCommand Build();

        [NotNull]
        IActivatableConcurrencySyncCommand Build([NotNull] Action<IActivatableConcurrencySyncCommand> setCommand);

        [NotNull]
        IActivatableConcurrencySyncCanExecuteBuilder ObservesProperty<TType>([NotNull] Expression<Func<TType>> expression);

        [NotNull]
        IActivatableConcurrencySyncCanExecuteBuilder ObservesCommandManager();

        [NotNull]
        IActivatableConcurrencySyncCanExecuteBuilder AutoActivate();
    }
}