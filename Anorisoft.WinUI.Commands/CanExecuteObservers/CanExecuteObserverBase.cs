// -----------------------------------------------------------------------
// <copyright file="CanExecuteObserverBase.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anorisoft.WinUI.Commands.CanExecuteObservers
{
    using System;

    using Anorisoft.WinUI.Commands.Interfaces;

    public abstract class CanExecuteObserverBase : PropertyObserverBase, ICanExecuteObserver
    {
        /// <summary>
        ///     Called when [can execute changed].
        /// </summary>
        public Func<bool> CanExecute { get; protected set; }
    }
}