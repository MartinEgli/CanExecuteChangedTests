using System;
using System.Linq.Expressions;

namespace Anorisoft.WinUI.Commands.Factory
{
    public interface ISyncCommandBuilder : ISyncCanExecuteBuilder
    {

        ISyncCanExecuteBuilder CanExecute(Func<bool> canExecute);

        ISyncCanExecuteBuilder ObservesCanExecute(Expression<Func<bool>> canExecute);

        ISyncCanExecuteBuilder ObservesCanExecute(Expression<Func<bool>> canExecute, bool fallback);

    }

    public interface ISyncCommandBuilder<T> : ISyncCanExecuteBuilder<T>
    {
        ISyncCanExecuteBuilder<T> CanExecute(Func<T, bool> canExecute);

        ISyncCanExecuteBuilder<T> ObservesCanExecute(Expression<Func<bool>> canExecute);

        ISyncCanExecuteBuilder<T> ObservesCanExecute(Expression<Func<bool>> canExecute, bool fallback);
    }
}