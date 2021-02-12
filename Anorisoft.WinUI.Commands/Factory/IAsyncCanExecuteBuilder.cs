using System;
using System.Linq.Expressions;
using Anorisoft.WinUI.Commands.Interfaces;

namespace Anorisoft.WinUI.Commands.Factory
{
    public interface IAsyncCanExecuteBuilder<T>
    {
        ISyncCommand Build();

        IAsyncCanExecuteBuilder<T> ObservesProperty<TType>(Expression<Func<TType>> canExecute);
    }

    public interface IAsyncCanExecuteBuilder
    {
        IAsyncCommand Build();

        IAsyncCanExecuteBuilder ObservesProperty<TType>(Expression<Func<TType>> canExecute);
    }
}