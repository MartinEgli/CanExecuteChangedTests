// -----------------------------------------------------------------------
// <copyright file="CommandManagerCommandFixture.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Anorisoft.WinUI.Commands.Commands;

namespace Anorisoft.WinUI.Commands.Tests
{
    using Anorisoft.WinUI.Commands.Interfaces;

    using NUnit.Framework;

    using System;
    using System.Windows.Input;
    using System.Windows.Threading;

    using Anorisoft.WinUI.Commands.CanExecuteObservers;

    using ICommand = System.Windows.Input.ICommand;

    /// <summary>
    ///     Summary description for ObservableCommandFixture
    /// </summary>
    [TestFixture]
    public class CommandManagerCommandFixture
    {
        [Test]
        public void CanExecuteCallsPassedInCanExecuteDelegate()
        {
            var handlers = new DelegateHandlers();
            var command = new ActivatableCanExecuteObserverCommand(handlers.Execute, handlers.CanExecute, new CommandManagerObserver());

            handlers.CanExecuteReturnValue = true;
            var actual = command.CanExecute();

            Assert.AreEqual(handlers.CanExecuteReturnValue, actual);
        }

        [Test]
        public void CanExecuteReturnsTrueWithoutCanExecuteDelegate()
        {
            var handlers = new DelegateHandlers();
            var command = new ActivatableCanExecuteObserverCommand(handlers.Execute, new CommandManagerObserver());

            var condition = command.CanExecute();

            Assert.True(condition);
        }

        [Test]
        public void CanRemoveCanExecuteChangedHandler()
        {
            var command = new ActivatableCanExecuteObserverCommand(() => { }, new CommandManagerObserver());
            var canExecuteChangedRaised = false;

            void Handler(object s, EventArgs e) => canExecuteChangedRaised = true;

            command.CanExecuteChanged += Handler;
            command.CanExecuteChanged -= Handler;

            CommandManager.InvalidateRequerySuggested();
            Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Background, new Action(() => { }));

            Assert.False(canExecuteChangedRaised);
        }

        [Test]
        public void ExecuteCallsPassedInExecuteDelegateNoCanExecute()
        {
            var executed = false;
            var command = new ActivatableCanExecuteObserverCommand(() => executed = true, new CommandManagerObserver()) as ICommand;

            command.Execute(null);
            Assert.True(executed);
        }

        [Test]
        public void ExecuteCallsPassedInExecuteDelegateNoCanExecute2()
        {
            var executed = false;
            var command = new ActivatableCanExecuteObserverCommand(() => executed = true, new CommandManagerObserver());

            command.Execute();
            Assert.True(executed);
        }

        [Test]
        public void ExecuteCallsCanExecuteTrue()
        {
            var executed = false;
            var command =
                new ActivatableCanExecuteObserverCommand(() => executed = true, () => true, new CommandManagerObserver()) as ICommand;

            command.Execute(null);
            Assert.True(executed);
        }

        [Test]
        public void ExecuteNoParameterCallsCanExecuteTrue()
        {
            var executed = false;
            var command = new ActivatableCanExecuteObserverCommand(() => executed = true, () => true, new CommandManagerObserver());

            command.Execute();
            Assert.True(executed);
        }

        [Test]
        public void ExecuteCallsCanExecuteFalse()
        {
            var executed = false;
            var command =
                new ActivatableCanExecuteObserverCommand(() => executed = true, () => false, new CommandManagerObserver()) as ICommand;

            command.Execute(null);
            Assert.False(executed);
        }

        [Test]
        public void ExecuteNoParameterCallsCanExecuteFalse()
        {
            var executed = false;
            var command = new ActivatableCanExecuteObserverCommand(() => executed = true, () => false, new CommandManagerObserver());

            command.Execute();
            Assert.False(executed);
        }

        [Test]
        public void ExecuteCallsWithExceptionPassedInExecuteDelegate()
        {
            var command = new ActivatableCanExecuteObserverCommand(
                              () => throw new Exception("Test Exception"),
                              new CommandManagerObserver()) as ICommand;

            Assert.Throws<Exception>(() => { command.Execute(null); });
        }

        [Test]
        public void ExecuteCallsNoParameterWithExceptionPassedInExecuteDelegate()
        {
            var command = new ActivatableCanExecuteObserverCommand(
                () => throw new Exception("Test Exception"),
                new CommandManagerObserver());

            Assert.Throws<Exception>(() => { command.Execute(); });
        }

        [Test]
        public void ExecuteCallsWithExceptionCanExecuteTrue()
        {
            var command = new ActivatableCanExecuteObserverCommand(
                              () => throw new Exception("Test Exception"),
                              () => true,
                              new CommandManagerObserver()) as ICommand;

            Assert.Throws<Exception>(() => { command.Execute(null); });
        }

        [Test]
        public void ExecuteCallsNoParameterWithExceptionCanExecuteTrue()
        {
            var command = new ActivatableCanExecuteObserverCommand(
                () => throw new Exception("Test Exception"),
                () => true,
                new CommandManagerObserver());

            Assert.Throws<Exception>(() => { command.Execute(); });
        }

        [Test]
        public void ExecuteCallsWithExceptionCanExecuteFalse()
        {
            var command = new ActivatableCanExecuteObserverCommand(
                              () => throw new Exception("Test Exception"),
                              () => false,
                              new CommandManagerObserver()) as ICommand;
            command.Execute(null);
            Assert.IsTrue(true);
        }

        [Test]
        public void ExecuteCallsNoParameterWithExceptionCanExecuteFalse()
        {
            var command = new ActivatableCanExecuteObserverCommand(
                () => throw new Exception("Test Exception"),
                () => false,
                new CommandManagerObserver());
            command.Execute();
            Assert.IsTrue(true);
        }

        [Test]
        public void GenericObservableCommandNotObservingPropertiesShouldNotRaiseOnEmptyPropertyName()
        {
            var canExecuteChangedRaised = false;
            var commandTestObject = new CommandTestObject();
            var command = new ActivatableCanExecuteObserverCommand(() => { }, new CommandManagerObserver());

            command.CanExecuteChanged += delegate { canExecuteChangedRaised = true; };

            commandTestObject.RaisePropertyChanged(null);

            Assert.False(canExecuteChangedRaised);
        }

        [Test]
        public void RaiseCanExecuteChangedNoRaiseCanExecuteChanged()
        {
            var handlers = new DelegateHandlers();
            var command = new ActivatableCanExecuteObserverCommand(handlers.Execute, () => true, new CommandManagerObserver());
            var canExecuteChangedRaised = false;
            command.CanExecuteChanged += delegate { canExecuteChangedRaised = true; };

            Assert.False(canExecuteChangedRaised);
        }

        [Test]
        public void RaiseCanExecuteChangedRaisesCanExecuteChanged()
        {
            var canExecuteChangedRaised = false;

            var handlers = new DelegateHandlers();
            var command = new ActivatableCanExecuteObserverCommand(handlers.Execute, () => false, new CommandManagerObserver());

            command.CanExecuteChanged += delegate { canExecuteChangedRaised = true; };

            CommandManager.InvalidateRequerySuggested();
            Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Background, new Action(() => { }));

            Assert.True(canExecuteChangedRaised);
        }

        [Test]
        public void ShouldThrowIfExecuteMethodDelegateNull()
        {
            Assert.Throws<ArgumentNullException>(
                () =>
                    {
                        var command = new ActivatableCanExecuteObserverCommand(null, new CommandManagerObserver());
                    });
        }

        [Test]
        public void ShouldThrowIfExecuteMethodDelegateNullAndObservableCommandNull()
        {
            Assert.Throws<ArgumentNullException>(
                () =>
                    {
                        var command = new ActivatableCanExecuteObserverCommand(null, (ICanExecuteSubject)null);
                    });
        }

        [Test]
        public void ShouldThrowIfObservableCommandNull()
        {
            Assert.Throws<ArgumentNullException>(
                () =>
                    {
                        var command = new ActivatableCanExecuteObserverCommand(() => { }, (ICanExecuteSubject)null);
                    });
        }

        [Test]
        public void ShouldThrowIfSecondObservableCommandNull()
        {
            Assert.Throws<ArgumentNullException>(
                () =>
                    {
                        var command = new ActivatableCanExecuteObserverCommand(() => { }, new CommandManagerObserver(), null);
                    });
        }

        [Test]
        public void WhenConstructedWithGenericTypeOfObject_InitializesValues()
        {
            // Prepare

            // Act
            var actual = new ActivatableCanExecuteObserverCommand(() => { }, new CommandManagerObserver());

            // verify
            Assert.NotNull(actual);
        }
    }
}