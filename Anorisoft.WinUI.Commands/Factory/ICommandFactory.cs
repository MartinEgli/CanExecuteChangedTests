// -----------------------------------------------------------------------
// <copyright file="ICommandFactory.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Anorisoft.WinUI.Commands.Factory
{
    public interface ICommandFactory
    {
        ISyncCommandBuilder Command(Action execute);

        ISyncCommandBuilder<T> Command<T>(Action<T> execute);

        IAsyncCommandBuilder Command(Func<Task> execute);

        IAsyncCommandBuilder<T> Command<T>(Func<T, Task> execute);

        ISyncCanExecuteBuilder Command(Action execute, Func<bool> canExecute);

        ISyncCanExecuteBuilder<T> Command<T>(Action<T> execute, Predicate<T> canExecute);

        IAsyncCanExecuteBuilder Command(Func<Task> execute, Func<bool> canExecute);

        IAsyncCanExecuteBuilder<T> Command<T>(Func<T, Task> execute, Predicate<T> canExecute);

        ISyncCommandBuilder Command(Action<CancellationToken> execute);

        ISyncCommandBuilder<T> Command<T>(Action<T, CancellationToken> execute);

        IAsyncCanExecuteBuilder Command(Func<CancellationToken, Task> execute);

        IAsyncCanExecuteBuilder<T> Command<T>(Func<T, CancellationToken, Task> execute);

        ISyncCanExecuteBuilder Command(Action<CancellationToken> execute, Func<bool> canExecute);

        ISyncCanExecuteBuilder<T> Command<T>(Action<T, CancellationToken> execute, Predicate<T> canExecute);

        IAsyncCanExecuteBuilder Command(Func<CancellationToken, Task> execute, Func<bool> canExecute);

        IAsyncCanExecuteBuilder<T> Command<T>(Func<T, CancellationToken, Task> execute, Predicate<T> canExecute);
    }
}