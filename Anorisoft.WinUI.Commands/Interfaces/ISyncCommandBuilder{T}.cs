using System;
using System.Linq.Expressions;
using Anorisoft.WinUI.Commands.Factory;
using JetBrains.Annotations;

namespace Anorisoft.WinUI.Commands.Interfaces
{
    public interface ISyncCommandBuilder<T> : ISyncCanExecuteBuilder<T>
    {
        [NotNull]
        ISyncCanExecuteBuilder<T> CanExecute([NotNull] Predicate<T> canExecute);

        [NotNull]
        ISyncCanExecuteBuilder<T> ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute);

        [NotNull]
        ISyncCanExecuteBuilder<T> ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute, bool fallback);
    }
}