// -----------------------------------------------------------------------
// <copyright file="CommandFactory.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Anorisoft.WinUI.Commands.Factory
{
    public class CommandFactory : ICommandFactory
    {
        public ISyncCommandBuilder Command(Action execute) => new SyncCommandBuilder(execute);

        public ISyncCommandBuilder<T> Command<T>(Action<T> execute)
        {
            return new SyncCommandBuilder<T>(execute);
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

        public ISyncCanExecuteBuilder<T> Command<T>(Action<T> execute, Predicate<T> canExecute)
        {
            throw new NotImplementedException();
        }

        public IAsyncCanExecuteBuilder Command(Func<Task> execute, Func<bool> canExecute)
        {
            throw new NotImplementedException();
        }

        public IAsyncCanExecuteBuilder<T> Command<T>(Func<T, Task> execute, Predicate<T> canExecute)
        {
            throw new NotImplementedException();
        }

        public ISyncCommandBuilder Command(Action<CancellationToken> execute)
        {
            throw new NotImplementedException();
        }

        public ISyncCommandBuilder<T> Command<T>(Action<T, CancellationToken> execute)
        {
            throw new NotImplementedException();
        }

        public IAsyncCanExecuteBuilder Command(Func<CancellationToken, Task> execute)
        {
            throw new NotImplementedException();
        }

        public IAsyncCanExecuteBuilder<T> Command<T>(Func<T, CancellationToken, Task> execute)
        {
            throw new NotImplementedException();
        }

        public ISyncCanExecuteBuilder Command(Action<CancellationToken> execute, Func<bool> canExecute)
        {
            throw new NotImplementedException();
        }

        public ISyncCanExecuteBuilder<T> Command<T>(Action<T, CancellationToken> execute, Predicate<T> canExecute)
        {
            throw new NotImplementedException();
        }

        public IAsyncCanExecuteBuilder Command(Func<CancellationToken, Task> execute, Func<bool> canExecute)
        {
            throw new NotImplementedException();
        }

        public IAsyncCanExecuteBuilder<T> Command<T>(Func<T, CancellationToken, Task> execute, Predicate<T> canExecute)
        {
            throw new NotImplementedException();
        }

        public IAsyncCanExecuteBuilder<T> Command<T>(Func<T, Task> execute, Func<T, bool> canExecute)
        {
            throw new NotImplementedException();
        }

        public static ICommandFactory Factory { get; } = new CommandFactory();
    }
}