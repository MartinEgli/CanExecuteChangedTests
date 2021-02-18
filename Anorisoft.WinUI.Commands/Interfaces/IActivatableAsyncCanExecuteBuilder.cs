using JetBrains.Annotations;
using System;
using System.Linq.Expressions;

namespace Anorisoft.WinUI.Commands.Interfaces
{
    public interface IActivatableAsyncCanExecuteBuilder
    {
        [NotNull]
        IActivatableAsyncCommand Build();

        [NotNull]
        IActivatableAsyncCommand Build([NotNull] Action<IActivatableAsyncCommand> setCommand);

        [NotNull]
        IActivatableAsyncCanExecuteBuilder ObservesProperty<TType>([NotNull] Expression<Func<TType>> expression);

        [NotNull]
        IActivatableAsyncCanExecuteBuilder ObservesCommandManager();

        [NotNull]
        IActivatableAsyncCanExecuteBuilder AutoActivate();
    }

    public interface IActivatableAsyncCanExecuteBuilder<in T>
    {
        [NotNull]
        IActivatableAsyncCommand<T> Build();

        [NotNull]
        IActivatableAsyncCommand<T> Build([NotNull] Action<IActivatableAsyncCommand<T>> setCommand);

        [NotNull]
        IActivatableAsyncCanExecuteBuilder<T> ObservesProperty<TType>([NotNull] Expression<Func<TType>> expression);

        [NotNull]
        IActivatableAsyncCanExecuteBuilder<T> ObservesCommandManager();

        [NotNull]
        IActivatableAsyncCanExecuteBuilder<T> AutoActivate();
    }
}