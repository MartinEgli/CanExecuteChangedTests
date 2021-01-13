// -----------------------------------------------------------------------
// <copyright file="ICommand.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anorisoft.WinUI.Commands.Interfaces
{
    using System.Windows.Input;

    public interface ICommand<in T> : System.Windows.Input.ICommand
    {
        bool CanExecute(T parameter);

        void Execute(T parameter);
    }

    public interface ICommand : System.Windows.Input.ICommand
    {
        bool CanExecute();

        void Execute();
    }
}