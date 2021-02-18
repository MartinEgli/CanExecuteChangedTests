// -----------------------------------------------------------------------
// <copyright file="CommandFactory.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Threading;
using System.Threading.Tasks;
using Anorisoft.WinUI.Commands.Factory;
using Anorisoft.WinUI.Commands.Interfaces;

namespace Anorisoft.WinUI.Commands.Builder
{
    public class CommandBuilder : ICommandBuilder
    {
        public static ICommandBuilder Builder { get; } = new CommandBuilder();

        public ISyncCommandBuilder Command(Action execute)
            => new SyncCommandBuilder(execute);

        public ISyncCanExecuteBuilder Command(Action execute, Func<bool> canExecute)
            => new SyncCommandBuilder(execute).CanExecute(canExecute);

        public ISyncCommandBuilder<T> Command<T>(Action<T> execute)
            => new SyncCommandBuilder<T>(execute);

        public ISyncCanExecuteBuilder<T> Command<T>(Action<T> execute, Predicate<T> canExecute)
            => new SyncCommandBuilder<T>(execute).CanExecute(canExecute);

        public IAsyncCommandBuilder Command(Func<Task> execute)
            => new AsyncCommandBuilder(execute);

        public IAsyncCanExecuteBuilder Command(Func<Task> execute, Func<bool> canExecute)
            => new AsyncCommandBuilder(execute).CanExecute(canExecute);

        public IAsyncCommandBuilder<T> Command<T>(Func<T, Task> execute)
            => new AsyncCommandBuilder<T>(execute);

        public IAsyncCanExecuteBuilder<T> Command<T>(Func<T, Task> execute, Predicate<T> canExecute)
            => new AsyncCommandBuilder<T>(execute).CanExecute(canExecute);

        public IConcurrencySyncCommandBuilder Command(Action<CancellationToken> execute)
            => new ConcurrencySyncCommandBuilder(execute);

        public IConcurrencySyncCommandBuilder<T> Command<T>(Action<T, CancellationToken> execute)
            => new ConcurrencySyncCommandBuilder<T>(execute);

        public IConcurrencyAsyncCommandBuilder Command(Func<CancellationToken, Task> execute)
            => new ConcurrencyAsyncCommandBuilder(execute);

        public IConcurrencyAsyncCommandBuilder<T> Command<T>(Func<T, CancellationToken, Task> execute)
            => new ConcurrencyAsyncCommandBuilder<T>(execute);

        public IConcurrencySyncCanExecuteBuilder Command(Action<CancellationToken> execute, Func<bool> canExecute)
            => new ConcurrencySyncCommandBuilder(execute).CanExecute(canExecute);

        public IConcurrencySyncCanExecuteBuilder<T> Command<T>(Action<T, CancellationToken> execute, Predicate<T> canExecute)
            => new ConcurrencySyncCommandBuilder<T>(execute).CanExecute(canExecute);

        public IConcurrencyAsyncCanExecuteBuilder Command(Func<CancellationToken, Task> execute, Func<bool> canExecute)
            => new ConcurrencyAsyncCommandBuilder(execute).CanExecute(canExecute);

        public IConcurrencyAsyncCanExecuteBuilder<T> Command<T>(Func<T, CancellationToken, Task> execute, Predicate<T> canExecute)
            => new ConcurrencyAsyncCommandBuilder<T>(execute).CanExecute(canExecute);
    }
}