using System;
using System.Linq.Expressions;
using Anorisoft.WinUI.Commands.Interfaces;

namespace Anorisoft.WinUI.Commands.Factory
{
    public interface IAsyncCommandBuilder<T>
    {
        IAsyncCommand<T> Build();

        IAsyncCanExecuteBuilder<T> CanExecute(Func<T, bool> canExecute);

        IAsyncCanExecuteBuilder<T> ObservesCanExecute(Expression<Func<bool>> canExecute);

        IAsyncCommandBuilder<T> ObservesCommandManager();
    }

    public interface IAsyncCommandBuilder
    {
        IAsyncCommand Build();

        IAsyncCanExecuteBuilder CanExecute(Func<bool> canExecute);

        IAsyncCanExecuteBuilder ObservesCanExecute(Expression<Func<bool>> canExecute);

        IAsyncCommandBuilder ObservesCommandManager();
    }
}