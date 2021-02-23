using System;
using System.Linq.Expressions;
using Anori.WinUI.Commands.Commands;
using JetBrains.Annotations;

namespace Anori.WinUI.Commands.Interfaces.Builders
{
    public interface IActivatableAsyncCanExecuteBuilder
    {
        [NotNull]
        ActivatableAsyncCanExecuteObserverCommand Build();

        [NotNull]
        ActivatableAsyncCanExecuteObserverCommand Build([NotNull] Action<ActivatableAsyncCanExecuteObserverCommand> setCommand);

        [NotNull]
        IActivatableAsyncCanExecuteBuilder ObservesProperty<TType>([NotNull] Expression<Func<TType>> expression);

        [NotNull]
        IActivatableAsyncCanExecuteBuilder Observes([NotNull] ICanExecuteChangedSubject observer);

        [NotNull]
        IActivatableAsyncCanExecuteBuilder ObservesCommandManager();

        [NotNull]
        IActivatableAsyncCanExecuteBuilder AutoActivate();
    }
}