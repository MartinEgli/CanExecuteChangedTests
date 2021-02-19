using System;
using System.Linq.Expressions;
using JetBrains.Annotations;

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
}