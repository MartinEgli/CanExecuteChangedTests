// -----------------------------------------------------------------------
// <copyright file="DelegateHandlers.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Threading.Tasks;

namespace Anorisoft.WinUI.Commands.Tests
{
    using System;

    public class DelegateHandlers
    {
        public bool CanExecuteReturnValue = true;

        public bool CanExecute()
        {
            return this.CanExecuteReturnValue;
        }

        public void Execute()
        {
            ExecuteCount++;
        }
        public async Task ExecuteAsync()
        {
            await Task.Yield();
            ExecuteCount++;
        }
        public int ExecuteCount { get; private set; }
    }
}