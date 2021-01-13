// -----------------------------------------------------------------------
// <copyright file="IAsyncCommand.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anorisoft.WinUI.Commands.Interfaces
{
    using System.Threading.Tasks;
    using System.Windows.Input;

    public interface IAsyncCommand : System.Windows.Input.ICommand
    {
        Task ExecuteAsync();

        bool CanExecute();
    }
}