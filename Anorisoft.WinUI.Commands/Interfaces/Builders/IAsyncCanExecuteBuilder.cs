using System;
using System.Linq.Expressions;
using Anorisoft.WinUI.Commands.Commands;
using JetBrains.Annotations;

namespace Anorisoft.WinUI.Commands.Interfaces.Builders
{
    public interface IAsyncCanExecuteBuilder
    {
        [NotNull]
        AsyncCanExecuteObserverCommand Build();

        [NotNull]
        AsyncCanExecuteObserverCommand Build([NotNull] Action<AsyncCanExecuteObserverCommand> setCommand);
        
        [NotNull]
        IAsyncCanExecuteBuilder ObservesProperty<TType>([NotNull] Expression<Func<TType>> canExecute);

        [NotNull]
        IAsyncCanExecuteBuilder Observes([NotNull] ICanExecuteChangedSubject observer);


        [NotNull]
        IAsyncCanExecuteBuilder ObservesCommandManager();

        [NotNull]
        IActivatableAsyncCanExecuteBuilder Activatable();
    }
}