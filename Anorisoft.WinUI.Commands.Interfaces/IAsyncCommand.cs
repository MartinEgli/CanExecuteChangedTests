﻿// -----------------------------------------------------------------------
// <copyright file="IAsyncCommand.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Anorisoft.WinUI.Common;
using JetBrains.Annotations;

namespace Anorisoft.WinUI.Commands.Interfaces
{
    using System.Threading.Tasks;
    using System.Windows.Input;

    public interface IAsyncCommand : System.Windows.Input.ICommand
    {
        /// <summary>
        /// Executes the asynchronous.
        /// </summary>
        /// <returns></returns>
        [NotNull]
        Task ExecuteAsync();

        /// <summary>
        /// Determines whether this instance can execute.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance can execute; otherwise, <c>false</c>.
        /// </returns>
        bool CanExecute();
    }
}