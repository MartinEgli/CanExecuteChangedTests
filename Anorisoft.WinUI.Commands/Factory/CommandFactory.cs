// -----------------------------------------------------------------------
// <copyright file="CommandFactory.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace Anorisoft.WinUI.Commands
{
    using System.Collections.Generic;

    using Anorisoft.WinUI.Commands.Interfaces;

    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IAsyncCanExecuteBuilder
    {
        IAsyncCommand Build();

        IAsyncCanExecuteBuilder ObservesProperty<TType>(Expression<Func<TType>> canExecute);
    }

    public interface IAsyncCanExecuteBuilder<T>
    {
        ISyncCommand Build();

        IAsyncCanExecuteBuilder<T> ObservesProperty<TType>(Expression<Func<TType>> canExecute);
    }

    public interface IAsyncCommandBuilder
    {
        IAsyncCommand Build();

        IAsyncCanExecuteBuilder CanExecute(Func<bool> canExecute);

        IAsyncCanExecuteBuilder ObservesCanExecute(Expression<Func<bool>> canExecute);

        IAsyncCommandBuilder ObservesCommandManager();
    }

    public interface IAsyncCommandBuilder<T>
    {
        IAsyncCommand<T> Build();

        IAsyncCanExecuteBuilder<T> CanExecute(Func<T, bool> canExecute);

        IAsyncCanExecuteBuilder<T> ObservesCanExecute(Expression<Func<bool>> canExecute);

        IAsyncCommandBuilder<T> ObservesCommandManager();
    }

    public interface ICommandFactoryCreator
    {
    }

    public interface ISyncCanExecuteBuilder
    {
        ISyncCommand Build();

        ISyncCanExecuteBuilder ObservesProperty<TType>(Expression<Func<TType>> canExecute);
    }

    public interface ISyncCanExecuteBuilder<T>
    {
        ISyncCommand<T> Build();

        IAsyncCanExecuteBuilder<T> ObservesProperty<TType>(Expression<Func<TType>> canExecute);
    }

    public interface ISyncCommand : ICommand
    {
    }

    public interface ISyncCommand<T> : ICommand<T>
    {
    }

    public interface ISyncCommandBuilder
    {
        ISyncCommand Build();

        ISyncCanExecuteBuilder CanExecute(Func<bool> canExecute);

        ISyncCanExecuteBuilder ObservesCanExecute(Expression<Func<bool>> canExecute);

        ISyncCommandBuilder ObservesCommandManager();
    }

    public interface ISyncCommandBuilder<T>
    {
        ISyncCommand<T> Build();

        ISyncCanExecuteBuilder<T> CanExecute(Func<T, bool> canExecute);

        ISyncCanExecuteBuilder<T> ObservesCanExecute(Expression<Func<bool>> canExecute);

        ISyncCommandBuilder<T> ObservesCommandManager();
    }

    public class CommandFactory : ICommandFactory
    {
        public ISyncCommandBuilder Command(Action execute)
        {
            return new SyncCommandBuilder(execute);
        }

        public ISyncCommandBuilder<T> Command<T>(Action<T> execute)
        {
            throw new NotImplementedException();
        }

        public IAsyncCommandBuilder Command(Func<Task> execute)
        {
            throw new NotImplementedException();
        }

        public IAsyncCommandBuilder<T> Command<T>(Func<T, Task> execute)
        {
            throw new NotImplementedException();
        }

        public ISyncCanExecuteBuilder Command(Action execute, Func<bool> canExecute)
        {
            throw new NotImplementedException();
        }

        public ISyncCanExecuteBuilder<T> Command<T>(Action<T> execute, Func<T, bool> canExecute)
        {
            throw new NotImplementedException();
        }

        public IAsyncCanExecuteBuilder Command(Func<Task> execute, Func<bool> canExecute)
        {
            throw new NotImplementedException();
        }

        public IAsyncCanExecuteBuilder<T> Command<T>(Func<T, Task> execute, Func<T, bool> canExecute)
        {
            throw new NotImplementedException();
        }
    }

    public class SyncCommandBuilder : ISyncCommandBuilder, ISyncCanExecuteBuilder
    {
        private Func<bool> canExecute;
        private Action execute;
        private Expression<Func<bool>> canExecuteExpression;
        private bool isObservesCommandManager = false;

        private List<LambdaExpression> observes = new List<LambdaExpression>();

        public SyncCommandBuilder(Action execute)
        {
            this.execute = execute;
        }

        public ISyncCommand Build()
        {
            throw new NotImplementedException();
        }

        public ISyncCanExecuteBuilder ObservesProperty<TType>(Expression<Func<TType>> observes)
        {
            this.observes.Add(observes);
            return this;
        }

        public ISyncCanExecuteBuilder CanExecute(Func<bool> canExecute)
        {
            this.canExecute = canExecute;
            return this;
        }

        public ISyncCanExecuteBuilder ObservesCanExecute(Expression<Func<bool>> canExecute)
        {
            this.canExecuteExpression = canExecute;
            return this;
        }

        public ISyncCommandBuilder ObservesCommandManager()
        {
            this.isObservesCommandManager = true;
            return this;
        }
    }
}