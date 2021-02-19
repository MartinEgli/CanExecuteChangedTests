using System;
using System.Linq.Expressions;
using JetBrains.Annotations;

namespace Anorisoft.WinUI.Commands.Interfaces
{
    public interface IActivatableConcurrencyAsyncCanExecuteBuilder
    {
        [NotNull]
        IActivatableConcurrencyAsyncCommand Build();

        [NotNull]
        IActivatableConcurrencyAsyncCommand Build([NotNull] Action<IActivatableConcurrencyAsyncCommand> setCommand);

        [NotNull]
        IActivatableConcurrencyAsyncCanExecuteBuilder ObservesProperty<TType>([NotNull] Expression<Func<TType>> expression);

        [NotNull]
        IActivatableConcurrencyAsyncCanExecuteBuilder ObservesCommandManager();

        [NotNull]
        IActivatableConcurrencyAsyncCanExecuteBuilder AutoActivate();
    }
}