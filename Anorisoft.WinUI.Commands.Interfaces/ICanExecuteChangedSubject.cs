// -----------------------------------------------------------------------
// <copyright file="ICanExecuteChangedSubject.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anorisoft.WinUI.Commands.Interfaces
{
    using System;

    public interface ICanExecuteSubject : ICanExecuteChangedSubjectBase
    {
        Func<bool> CanExecute { get; }
    }

    public interface ICanExecuteChangedSubject : ICanExecuteChangedSubjectBase
    {
    }

    /// <summary>
    ///     Interface Can Execute Updater
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public interface ICanExecuteChangedSubjectBase : IDisposable
    {
        /// <summary>
        ///     Adds the specified observer.
        /// </summary>
        /// <param name="observer">The observer.</param>
        void Add(ICanExecuteChangedObserver observer);

        /// <summary>
        ///     Removes the specified observer.
        /// </summary>
        /// <param name="observer">The observer.</param>
        void Remove(ICanExecuteChangedObserver observer);
    }
}