using System;
using System.Linq.Expressions;
using Anorisoft.WinUI.Commands.Factory;
using JetBrains.Annotations;

namespace Anorisoft.WinUI.Commands.Interfaces
{
    public interface ISyncCommandBuilder : ISyncCanExecuteBuilder
    {

        [NotNull]
        ISyncCanExecuteBuilder CanExecute([NotNull] Func<bool> canExecute);

        [NotNull]
        ISyncCanExecuteBuilder ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute);

        [NotNull]
        ISyncCanExecuteBuilder ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute, bool fallback);

    }
}