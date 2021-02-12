// -----------------------------------------------------------------------
// <copyright file="ActivatableCanExecuteObserverCommand.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using Anorisoft.WinUI.Commands.Factory;
using Anorisoft.WinUI.Commands.Interfaces;
using Anorisoft.WinUI.Common;
using CanExecuteChangedTests;
using JetBrains.Annotations;

namespace Anorisoft.WinUI.Commands
{
    public class ActivatableCanExecuteObserverCommand : SyncCommandBase, ISyncCommand,
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
        ///     Initializes a new instance of the <see cref="ActivatableCanExecuteObserverCommand" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="autoActivate">if set to <c>true</c> [automatic activate].</param>
        /// <param name="observer">The observer.</param>
        /// <param name="observers">The observers.</param>
        /// <exception cref="ArgumentNullException">
        ///     observer
        ///     or
        ///     observer
        /// </exception>
        public ActivatableCanExecuteObserverCommand(
            [NotNull] Action execute,
            bool autoActivate,
            [NotNull] [ItemNotNull] params ICanExecuteChangedSubject[] observers)
            : base(execute)
        {
           
            if (observers == null)
            {
                throw new ArgumentNullException(nameof(observers));
            }

            this.observers.AddRange(observers);
            if (autoActivate)
            {
                this.Activate();
            }
        }

        public ActivatableCanExecuteObserverCommand(
            [NotNull] Action execute,
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
            this.observers.AddRange(observers);
            if (autoActivate)
            {
                this.Activate();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivatableCanExecuteObserverCommand" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="observer">The observer.</param>
        /// <param name="observers">The observers.</param>
        public ActivatableCanExecuteObserverCommand(
            [NotNull] Action execute,
            [NotNull] [ItemNotNull] params ICanExecuteChangedSubject[] observers)
            : this(execute, false, observers)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivatableCanExecuteObserverCommand"/> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecuteSubject">The can execute subject.</param>
        /// <param name="observers">The observers.</param>
        public ActivatableCanExecuteObserverCommand(
            [NotNull] Action execute,
            [NotNull] ICanExecuteSubject canExecuteSubject,
            [NotNull] [ItemNotNull] params ICanExecuteChangedSubject[] observers)
            : this(execute, false, canExecuteSubject, observers)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivatableCanExecuteObserverCommand"/> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecuteSubject">The can execute subject.</param>
        public ActivatableCanExecuteObserverCommand(
            [NotNull] Action execute,
            [NotNull] ICanExecuteSubject canExecuteSubject)
            : this(execute, false, canExecuteSubject)
        {
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
            [NotNull] Action execute,
            [NotNull] Func<bool> canExecute,
            [NotNull] [ItemNotNull] params ICanExecuteChangedSubject[] observers)
            : this(execute,true, canExecute)
        {
            
        }


        public ActivatableCanExecuteObserverCommand(
            [NotNull] Action execute,
            bool autoActivate,
            [NotNull] Func<bool> canExecute,
            [NotNull][ItemNotNull] params ICanExecuteChangedSubject[] observers)
            : base(execute, canExecute)
        {
            if (observers == null)
            {
                throw new ArgumentNullException(nameof(observers));
            }

            this.observers.AddRange(observers);
            if (autoActivate)
            {
                this.Activate();
            }
        }

        /// <summary>
        ///     Notifies that the value for <see cref="P:Anorisoft.WinUI.Common.IActivated.IsActive" /> property has changed.
        /// </summary>
        public event EventHandler<EventArgs<bool>> IsActiveChanged;

        /// <summary>
        ///     Gets or sets a value indicating whether the object is active.
        /// </summary>
        /// <value>
        ///     <see langword="true" /> if the object is active; otherwise <see langword="false" />.
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
        ///     Activates this instance.
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
        ///     Deactivates this instance.
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