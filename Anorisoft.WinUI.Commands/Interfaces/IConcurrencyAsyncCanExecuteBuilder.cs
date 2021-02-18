using System;
using System.Linq.Expressions;
using JetBrains.Annotations;

namespace Anorisoft.WinUI.Commands.Interfaces
{
    public interface IConcurrencyAsyncCanExecuteBuilder
    {
        [NotNull]
        IConcurrencyAsyncCommand Build();

        [NotNull]
        IConcurrencyAsyncCommand Build([NotNull] Action<IConcurrencyAsyncCommand> setCommand);

        [NotNull]
        IConcurrencyAsyncCanExecuteBuilder ObservesProperty<TType>([NotNull] Expression<Func<TType>> expression);

        [NotNull]
        IConcurrencyAsyncCanExecuteBuilder ObservesCommandManager();

        [NotNull]
        IActivatableConcurrencyAsyncCanExecuteBuilder Activatable();
    }
}