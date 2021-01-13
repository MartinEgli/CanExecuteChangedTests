// -----------------------------------------------------------------------
// <copyright file="ActivatablePropertyObserverCommand{T}.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anorisoft.WinUI.Commands
{
    using Anorisoft.WinUI.Commands.Interfaces;
    using Anorisoft.WinUI.Common;

    using JetBrains.Annotations;

    using System;
    using System.Collections.Generic;

    public class ActivatableCanExecuteObserverCommand<T> : SyncCommandBase<T>, ICanExecuteChangedObserver, IDisposable
    {
        /// <summary>
        ///     The observers
        /// </summary>
        private readonly List<ICanExecuteChangedSubjectBase> observers = new List<ICanExecuteChangedSubjectBase>();

        /// <summary>
        ///     Initializes a new instance of the <see cref="ActivatableCanExecuteObserverCommand" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="observer">The observer.</param>
        /// <param name="observers">The observers.</param>
        /// <exception cref="ArgumentNullException">
        ///     observer
        ///     or
        ///     observer
        /// </exception>
        /// <exception cref="ArgumentException">propertyObserver</exception>
        public ActivatableCanExecuteObserverCommand(
            [NotNull] Action<T> execute,
            [NotNull] ICanExecuteChangedSubject observer,
            [NotNull][ItemNotNull] params ICanExecuteChangedSubject[] observers)
            : base(execute)
        {
            if (observer == null)
            {
                throw new ArgumentNullException(nameof(observers));
            }

            if (observers == null)
            {
                throw new ArgumentNullException(nameof(observers));
            }

            this.observers.Add(observer);
            foreach (var propertyObserver in observers)
            {
                if (this.observers.Contains(propertyObserver))
                {
                    throw new ArgumentException($"{propertyObserver} is already being observed.", nameof(propertyObserver));
                }
                this.observers.Add(propertyObserver);
            }

            this.Subscribe();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ActivatableCanExecuteObserverCommand" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecuteObserver">The can execute observer.</param>
        /// <param name="observers">The observers.</param>
        /// <exception cref="ArgumentNullException">
        ///     observers
        ///     or
        ///     observers
        /// </exception>
        public ActivatableCanExecuteObserverCommand(
            [NotNull] Action<T> execute,
            [NotNull] ICanExecuteSubject canExecuteObserver,
            [NotNull][ItemNotNull] params ICanExecuteChangedSubject[] observers)
            : base(execute, p => canExecuteObserver.CanExecute())
        {
            if (canExecuteObserver == null)
            {
                throw new ArgumentNullException(nameof(observers));
            }

            if (observers == null)
            {
                throw new ArgumentNullException(nameof(observers));
            }

            this.observers.Add(canExecuteObserver);
            this.observers.AddRange(observers);
            this.Subscribe();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ActivatableCanExecuteObserverCommand" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecute">The can execute.</param>
        /// <param name="observer">The observer.</param>
        /// <param name="observers">The observers.</param>
        /// <exception cref="ArgumentNullException">
        ///     observers
        ///     or
        ///     observers
        /// </exception>
        public ActivatableCanExecuteObserverCommand(
            [NotNull] Action<T> execute,
            [NotNull] Predicate<T> canExecute,
            [NotNull] ICanExecuteChangedSubject observer,
            [NotNull][ItemNotNull] params ICanExecuteChangedSubject[] observers)
            : base(execute, canExecute)
        {
            if (observer == null)
            {
                throw new ArgumentNullException(nameof(observers));
            }

            if (observers == null)
            {
                throw new ArgumentNullException(nameof(observers));
            }

            this.observers.Add(observer);
            this.observers.AddRange(observers);
            this.Subscribe();
        }

        /// <summary>
        ///     Called when [can execute changed].
        /// </summary>
        public void RaisePropertyChanged() => this.CanExecuteChanged.RaiseEmpty(this);

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public override event EventHandler CanExecuteChanged;

        /// <summary>
        ///     Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        ///     <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only
        ///     unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            this.Unsubscribe();
        }

        /// <summary>
        ///     Subscribes this instance.
        /// </summary>
        protected void Subscribe() => this.observers.ForEach(observer => observer.Add(this));

        /// <summary>
        ///     Unsubscribes this instance.
        /// </summary>
        protected void Unsubscribe() => this.observers.ForEach(observer => observer.Remove(this));
    }
}