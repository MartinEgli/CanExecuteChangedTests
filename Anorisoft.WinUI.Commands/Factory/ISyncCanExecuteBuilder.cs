using System;
using System.Linq.Expressions;

namespace Anorisoft.WinUI.Commands.Factory
{
    public interface ISyncCanExecuteBuilder<T>
    {
        ISyncCommand<T> Build();

        ISyncCanExecuteBuilder<T> ObservesProperty<TType>(Expression<Func<TType>> canExecute);

        ISyncCanExecuteBuilder<T> ObservesCommandManager();

        ISyncCanExecuteBuilder<T> AutoCativate();
    }

    public interface ISyncCanExecuteBuilder
    {
        ISyncCommand Build();

        ISyncCanExecuteBuilder ObservesProperty<TType>(Expression<Func<TType>> expression);

        ISyncCanExecuteBuilder ObservesCommandManager();

        ISyncCanExecuteBuilder AutoActivate();
    }
}