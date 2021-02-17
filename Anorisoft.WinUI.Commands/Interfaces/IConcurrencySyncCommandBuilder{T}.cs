using System;
using System.Linq.Expressions;
using Anorisoft.WinUI.Commands.Interfaces;
using JetBrains.Annotations;

namespace Anorisoft.WinUI.Commands.Factory
{
    public interface IConcurrencySyncCommandBuilder<T>
    {
        [NotNull]
        IConcurrencySyncCanExecuteBuilder<T> CanExecute([NotNull] Predicate<T> canExecute);

        [NotNull]
        IConcurrencySyncCanExecuteBuilder<T> ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute);

        [NotNull]
        IConcurrencySyncCanExecuteBuilder<T> ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute, bool fallback);
    }
}