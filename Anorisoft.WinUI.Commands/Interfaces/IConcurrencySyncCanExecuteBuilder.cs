using System;
using System.Linq.Expressions;
using JetBrains.Annotations;

namespace Anorisoft.WinUI.Commands.Interfaces
{
    public interface IConcurrencySyncCanExecuteBuilder
    {
        [NotNull]
        IConcurrencySyncCommand Build();

        [NotNull]
        IConcurrencySyncCommand Build([NotNull] Action<IConcurrencySyncCommand> setCommand);

        [NotNull]
        IConcurrencySyncCanExecuteBuilder ObservesProperty<TType>([NotNull] Expression<Func<TType>> expression);

        [NotNull]
        IConcurrencySyncCanExecuteBuilder ObservesCommandManager();
        
        [NotNull]
        IActivatableConcurrencySyncCanExecuteBuilder Activatable();
    }
}