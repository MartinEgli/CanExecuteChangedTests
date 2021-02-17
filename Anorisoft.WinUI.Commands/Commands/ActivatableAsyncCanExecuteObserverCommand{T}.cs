﻿// -----------------------------------------------------------------------
// <copyright file="ActivatableCanExecuteObserverCommand.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Anorisoft.WinUI.Commands.Interfaces;
using Anorisoft.WinUI.Commands.Resources;
using Anorisoft.WinUI.Common;
using CanExecuteChangedTests;
using JetBrains.Annotations;

namespace Anorisoft.WinUI.Commands
{
    public class ActivatableAsyncCanExecuteObserverCommand<T> :
        AsyncCommandBase<T>,
        ICanExecuteChangedObserver,
        IDisposable,
        IActivatable
    {
        /// <summary>
        ///     The observers
        /// </summary>
        private readonly List<ICanExecuteChangedSubjectBase> observers = new List<ICanExecuteChangedSubjectBase>();

        /// <summary>
        ///     The is active
        /// </summary>
        private bool isActive;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivatableCanExecuteObserverCommand" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="autoActivate">if set to <c>true</c> [automatic activate].</param>
        /// <param name="observers">The observers.</param>
        /// <exception cref="ArgumentNullException">observer
        /// or
        /// observer</exception>
        public ActivatableAsyncCanExecuteObserverCommand(
            [NotNull] Func<T, Task> execute,
            bool autoActivate,
            [NotNull] [ItemNotNull] params ICanExecuteChangedSubject[] observers)
            : base(execute)
        {
            if (observers == null)
            {
                throw new ArgumentNullException(nameof(observers));
            }

            AddIfNotContains(observers);

            if (autoActivate)
            {
                this.Activate();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivatableAsyncCanExecuteObserverCommand"/> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="error">The error.</param>
        public ActivatableAsyncCanExecuteObserverCommand(
            [NotNull] Func<T, Task> execute,
            [NotNull] Action<Exception> error)
            : this(execute, false, error)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivatableAsyncCanExecuteObserverCommand" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="autoActivate">if set to <c>true</c> [automatic activate].</param>
        /// <param name="error">The error.</param>
        public ActivatableAsyncCanExecuteObserverCommand(
            [NotNull] Func<T, Task> execute,
            bool autoActivate,
            [NotNull] Action<Exception> error)
            : base(execute, error)
        {
            if (autoActivate)
            {
                this.Activate();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivatableCanExecuteObserverCommand" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="autoActivate">if set to <c>true</c> [automatic activate].</param>
        /// <param name="canExecuteSubject">The can execute subject.</param>
        /// <param name="observers">The observers.</param>
        /// <exception cref="ArgumentNullException">canExecuteSubject
        /// or
        /// observers</exception>
        public ActivatableAsyncCanExecuteObserverCommand(
            [NotNull] Func<T, Task> execute,
            bool autoActivate,
            [NotNull] ICanExecuteSubject canExecuteSubject,
            [NotNull] [ItemNotNull] params ICanExecuteChangedSubject[] observers)
            : base(execute, canExecuteSubject)
        {
            if (canExecuteSubject == null)
            {
                throw new ArgumentNullException(nameof(canExecuteSubject));
            }

            if (observers == null)
            {
                throw new ArgumentNullException(nameof(observers));
            }

            this.observers.Add(canExecuteSubject);

            AddIfNotContains(observers);

            if (autoActivate)
            {
                this.Activate();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivatableCanExecuteObserverCommand" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="observers">The observers.</param>
        public ActivatableAsyncCanExecuteObserverCommand(
            [NotNull] Func<T, Task> execute,
            [NotNull] [ItemNotNull] params ICanExecuteChangedSubject[] observers)
            : this(execute, false, observers)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivatableCanExecuteObserverCommand" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecuteSubject">The can execute subject.</param>
        /// <param name="observers">The observers.</param>
        public ActivatableAsyncCanExecuteObserverCommand(
            [NotNull] Func<T, Task> execute,
            [NotNull] ICanExecuteSubject canExecuteSubject,
            [NotNull] [ItemNotNull] params ICanExecuteChangedSubject[] observers)
            : this(execute, false, canExecuteSubject, observers)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivatableCanExecuteObserverCommand" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecuteSubject">The can execute subject.</param>
        public ActivatableAsyncCanExecuteObserverCommand(
            [NotNull] Func<T, Task> execute,
            [NotNull] ICanExecuteSubject canExecuteSubject)
            : this(execute, false, canExecuteSubject)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivatableCanExecuteObserverCommand" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecute">The can execute.</param>
        /// <param name="observers">The observers.</param>
        /// <exception cref="ArgumentNullException">observers
        /// or
        /// observers</exception>
        public ActivatableAsyncCanExecuteObserverCommand(
            [NotNull] Func<T, Task> execute,
            [NotNull] Predicate<T> canExecute,
            [NotNull] [ItemNotNull] params ICanExecuteChangedSubject[] observers)
            : this(execute, false, canExecute, observers)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivatableCanExecuteObserverCommand" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="autoActivate">if set to <c>true</c> [automatic activate].</param>
        /// <param name="canExecute">The can execute.</param>
        /// <param name="observers">The observers.</param>
        /// <exception cref="ArgumentNullException">observers</exception>
        public ActivatableAsyncCanExecuteObserverCommand(
            [NotNull] Func<T, Task> execute,
            bool autoActivate,
            [NotNull] Predicate<T> canExecute,
            [NotNull] [ItemNotNull] params ICanExecuteChangedSubject[] observers)
            : base(execute, canExecute)
        {
            if (observers == null)
            {
                throw new ArgumentNullException(nameof(observers));
            }

            AddIfNotContains(observers);

            if (autoActivate)
            {
                this.Activate();
            }
        }

        /// <summary>
        /// Notifies that the value for <see cref="P:Anorisoft.WinUI.Common.IActivated.IsActive" /> property has changed.
        /// </summary>
        public event EventHandler<EventArgs<bool>> IsActiveChanged;

        /// <summary>
        /// Gets or sets a value indicating whether the object is active.
        /// </summary>
        /// <value>
        ///   <see langword="true" /> if the object is active; otherwise <see langword="false" />.
        /// </value>
        public bool IsActive
        {
            get => this.isActive;
            private set
            {
                if (this.isActive == value)
                {
                    return;
                }

                this.isActive = value;
                this.IsActiveChanged.Raise(this, value);
                this.CanExecuteChanged.RaiseEmpty(this);
            }
        }

        /// <summary>
        /// Activates this instance.
        /// </summary>
        public void Activate()
        {
            if (this.IsActive)
            {
                return;
            }

            this.Subscribe();
            this.IsActive = true;
        }

        /// <summary>
        /// Deactivates this instance.
        /// </summary>
        public void Deactivate()
        {
            if (!this.IsActive)
            {
                return;
            }

            this.Unsubscribe();
            this.IsActive = false;
        }

        /// <summary>
        /// Called when [can execute changed].
        /// </summary>
        public void RaisePropertyChanged() => this.CanExecuteChanged.RaiseEmpty(this);

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public override event EventHandler CanExecuteChanged;

        /// <summary>
        /// Raises the can execute changed.
        /// </summary>
        public override void RaiseCanExecuteChanged() => this.CanExecuteChanged.RaiseEmpty(this);

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

        /// <summary>
        /// Adds if not contains.
        /// </summary>
        /// <param name="observers">The observers.</param>
        /// <exception cref="ArgumentException">propertyObserver</exception>
        private void AddIfNotContains(IEnumerable<ICanExecuteChangedSubject> observers)
        {
            foreach (var propertyObserver in observers)
            {
                if (this.observers.Contains(propertyObserver))
                {
                    throw new ArgumentException(
                        string.Format(ExceptionStrings.ObserverIsAlreadyBeingObserved, propertyObserver),
                        nameof(propertyObserver));
                }

                this.observers.Add(propertyObserver);
            }
        }
    }
}