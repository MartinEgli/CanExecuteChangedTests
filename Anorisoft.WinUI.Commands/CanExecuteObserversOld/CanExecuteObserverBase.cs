// -----------------------------------------------------------------------
// <copyright file="CanExecuteObserverBase.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using Anorisoft.WinUI.Commands.Interfaces;

namespace Anorisoft.WinUI.Commands.CanExecuteObserversOld
{
    public abstract class CanExecuteObserverBase<T> : PropertyObserverBase<T>, ICanExecuteObserver
        where T : CanExecuteObserverBase<T>
    {
        /// <summary>
        ///     Called when [can execute changed].
        /// </summary>
        public Func<bool> CanExecute { get; protected set; }
    }
}