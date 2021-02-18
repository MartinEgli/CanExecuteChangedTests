using System;
using System.Linq.Expressions;
using JetBrains.Annotations;

namespace Anorisoft.WinUI.Commands.Interfaces
{
    public interface IConcurrencySyncCanExecuteBuilder<in T>
    {
        [NotNull]
        IConcurrencySyncCommand<T> Build();

        [NotNull]
        IConcurrencySyncCommand<T> Build([NotNull] Action<IConcurrencySyncCommand<T>> setCommand);

        [NotNull]
        IConcurrencySyncCanExecuteBuilder<T> ObservesProperty<TType>([NotNull] Expression<Func<TType>> expression);

        [NotNull]
        IConcurrencySyncCanExecuteBuilder<T> ObservesCommandManager();

        [NotNull]
        IActivatableConcurrencySyncCanExecuteBuilder<T> Activateable();
    }
}