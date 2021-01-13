// -----------------------------------------------------------------------
// <copyright file="ICommandFactory.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anorisoft.WinUI.Commands
{
    using System;
    using System.Threading.Tasks;

    public interface ICommandFactory
    {
        ISyncCommandBuilder Command(Action execute);

        ISyncCommandBuilder<T> Command<T>(Action<T> execute);

        IAsyncCommandBuilder Command(Func<Task> execute);

        IAsyncCommandBuilder<T> Command<T>(Func<T, Task> execute);

        ISyncCanExecuteBuilder Command(Action execute, Func<bool> canExecute);

        ISyncCanExecuteBuilder<T> Command<T>(Action<T> execute, Func<T, bool> canExecute);

        IAsyncCanExecuteBuilder Command(Func<Task> execute, Func<bool> canExecute);

        IAsyncCanExecuteBuilder<T> Command<T>(Func<T, Task> execute, Func<T, bool> canExecute);
    }
}