using System;
using System.Linq.Expressions;
using JetBrains.Annotations;

namespace Anorisoft.WinUI.Commands.Interfaces
{
    public interface IAsyncCanExecuteBuilder
    {
        [NotNull]
        IAsyncCommand Build();

        [NotNull]
        IAsyncCommand Build([NotNull] Action<IAsyncCommand> setCommand);
        
        [NotNull]
        IAsyncCanExecuteBuilder ObservesProperty<TType>([NotNull] Expression<Func<TType>> canExecute);

        [NotNull]
        IAsyncCanExecuteBuilder ObservesCommandManager();

        [NotNull]
        IActivatableAsyncCanExecuteBuilder Activatable();
    }
}