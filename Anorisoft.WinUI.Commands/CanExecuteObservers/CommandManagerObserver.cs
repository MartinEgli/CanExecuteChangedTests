// -----------------------------------------------------------------------
// <copyright file="CommandManagerObserver.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anorisoft.WinUI.Commands.CanExecuteObservers
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Input;

    using Anorisoft.WinUI.Commands.Interfaces;

    using JetBrains.Annotations;

    public class CommandManagerObserver : ICanExecuteChangedSubject
    {
        /// <summary>
        ///     The dictionary
        /// </summary>
        private readonly IDictionary<ICanExecuteChangedObserver, EventHandler> dictionary =
            new Dictionary<ICanExecuteChangedObserver, EventHandler>();

        /// <summary>
        ///     Adds the specified observer.
        /// </summary>
        /// <param name="observer">The observer.</param>
        public void Add([NotNull] ICanExecuteChangedObserver observer)
        {
            if (observer == null)
            {
                throw new ArgumentNullException(nameof(observer));
            }

            if (this.dictionary.TryGetValue(observer, out _))
            {
                return;
            }

            void Handler(object sender, EventArgs args) => observer.RaisePropertyChanged();
            this.dictionary.Add(observer, Handler);
            CommandManager.RequerySuggested += Handler;
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Removes the specified observer.
        /// </summary>
        /// <param name="observer">The observer.</param>
        public void Remove([NotNull] ICanExecuteChangedObserver observer)
        {
            if (observer == null)
            {
                throw new ArgumentNullException(nameof(observer));
            }

            if (!this.dictionary.TryGetValue(observer, out var handler))
            {
                return;
            }

            CommandManager.RequerySuggested -= handler;
            this.dictionary.Remove(observer);
            observer.RaisePropertyChanged();
        }

        /// <summary>
        ///     Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        ///     <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only
        ///     unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            foreach (var handler in this.dictionary.Values)
            {
                CommandManager.RequerySuggested -= handler;
            }

            foreach (var observable in this.dictionary.Keys)
            {
                observable.RaisePropertyChanged();
            }

            this.dictionary.Clear();
        }
    }
}