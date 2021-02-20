// -----------------------------------------------------------------------
// <copyright file="CommandManagerCommandFixture.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Anorisoft.WinUI.Commands.Builder;
using Anorisoft.WinUI.Commands.CanExecuteObservers;
using Anorisoft.WinUI.Commands.Commands;
using Anorisoft.WinUI.Commands.Interfaces;
using NUnit.Framework;
using System;
using System.Threading;
using System.Windows.Input;
using System.Windows.Threading;

namespace Anorisoft.WinUI.Commands.Tests
{
    /// <summary>
    ///     Summary description for ObservableCommandFixture
    /// </summary>
    [TestFixture]
    public class CommandManagerCommandFixture
    {
        [SetUp]
        public void Init()
        {
            SynchronizationContext.SetSynchronizationContext(new TestSynchronizationContext());
        }

        [Test]
        public void CanExecuteCallsPassedInCanExecuteDelegate()
        {
            var handlers = new DelegateHandlers();
            var command =
                new ActivatableCanExecuteObserverCommand(handlers.Execute, handlers.CanExecute,
                    new CommandManagerObserver()).Activate();

            handlers.CanExecuteReturnValue = true;
            var actual = command.CanExecute();

            Assert.AreEqual(handlers.CanExecuteReturnValue, actual);
        }

        [Test]
        public void CommandBuilder_CanExecuteCallsPassedInCanExecuteDelegate()
        {
            var handlers = new DelegateHandlers();
            var builder = new CommandBuilder();
            var command = builder
                .Command(handlers.Execute)
                .CanExecute(handlers.CanExecute)
                .ObservesCommandManager()
                .Build();

            handlers.CanExecuteReturnValue = true;
            var actual = command.CanExecute();

            Assert.AreEqual(handlers.CanExecuteReturnValue, actual);
        }

        [Test]
        public void CommandBuilder_Activatable_CanExecuteCallsPassedInCanExecuteDelegate_ReturnFalse()
        {
            var handlers = new DelegateHandlers();
            var builder = new CommandBuilder();
            var command = builder
                .Command(handlers.Execute)
                .CanExecute(handlers.CanExecute)
                .ObservesCommandManager()
                .Activatable()
                .Build();

            handlers.CanExecuteReturnValue = true;
            var actual = command.CanExecute();

            Assert.AreEqual(false, actual);
        }

        [Test]
        public void CanExecuteReturnsTrueWithoutCanExecuteDelegate()
        {
            var handlers = new DelegateHandlers();
            var command = new ActivatableCanExecuteObserverCommand(handlers.Execute, new CommandManagerObserver())
                .Activate();

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
            var command = new ActivatableCanExecuteObserverCommand(() => executed = true, new CommandManagerObserver())
                .Activate() as ICommand;

            command.Execute(null);
            Assert.True(executed);
        }

        [Test]
        public void CommandBuilder_SyncCommand_CanExecute_Activatable_NotActive_RetuenFalse()
        {
            var executed = false;
            var handlers = new DelegateHandlers();
            var builder = new CommandBuilder();
            var command = builder
                .Command(() => executed = true)
                .CanExecute(handlers.CanExecute)
                .Activatable()
                .Build();

            handlers.CanExecuteReturnValue = true;
            var actual = command.CanExecute();
            Assert.False(actual);

            ((ICommand)command).Execute(null);
            Assert.False(executed);
        }

        [Test]
        public void CommandBuilder_SyncCommand_Activatable_NotActive_Return0()
        {
            var handlers = new DelegateHandlers();
            var builder = new CommandBuilder();
            var command = builder
                .Command(() => handlers.Execute())
                .Activatable()
                .Build();

            handlers.CanExecuteReturnValue = true;
            var actual = command.CanExecute();
            Assert.False(actual);

            ((ICommand)command).Execute(null);
            Assert.AreEqual(0, handlers.ExecuteCount);
        }

        [Test]
        public void CommandBuilder_SyncCommand_CanExecute_Activatable_AutoActivate_Return1()
        {
            var handlers = new DelegateHandlers();
            var builder = new CommandBuilder();
            var command = builder
                .Command(() => handlers.Execute())
                .CanExecute(handlers.CanExecute)
                .Activatable()
                .AutoActivate()
                .Build();

            handlers.CanExecuteReturnValue = true;
            var actual = command.CanExecute();
            Assert.True(actual);

            ((ICommand)command).Execute(null);
            Assert.AreEqual(1, handlers.ExecuteCount);
        }

        [Test]
        public void CommandBuilder_SyncCommand_Activatable_AutoActivate_Return1()
        {
            var handlers = new DelegateHandlers();
            var builder = new CommandBuilder();
            var command = builder
                .Command(() => handlers.Execute())
                .Activatable()
                .AutoActivate()
                .Build();

            handlers.CanExecuteReturnValue = true;
            var actual = command.CanExecute();
            Assert.True(actual);

            ((ICommand)command).Execute(null);
            Assert.AreEqual(1, handlers.ExecuteCount);
        }

        [Test]
        public void CommandBuilder_SyncCommandOfT_Activatable_AutoActivate_Return1()
        {
            var handlers = new DelegateHandlers();
            var builder = new CommandBuilder();
            var command = builder
                .Command((int i) => handlers.Execute())
                .Activatable()
                .AutoActivate()
                .Build();

            handlers.CanExecuteReturnValue = true;
            var actual = command.CanExecute(0);
            Assert.True(actual);

            command.Execute(1);
            Assert.AreEqual(1, handlers.ExecuteCount);
        }

        [Test]
        public void CommandBuilder_AsyncCommandOfT_Activatable_AutoActivate_Return1()
        {
            var handlers = new DelegateHandlers();
            var builder = new CommandBuilder();
            var command = builder
                .Command(async (int i) => await handlers.ExecuteAsync())
                .Activatable()
                .AutoActivate()
                .Build();

            handlers.CanExecuteReturnValue = true;
            var actual = command.CanExecute(0);
            Assert.True(actual);

            ((ICommand)command).Execute(1);
            Assert.AreEqual(1, handlers.ExecuteCount);
        }

        [Test]
        public void CommandBuilder_ConcurrencySyncCommand_Activatable_AutoActivate_Return1()
        {
            var handlers = new DelegateHandlers();
            using var waitHandle = new EventWaitHandle(false, EventResetMode.AutoReset);
            var builder = new CommandBuilder();
            var command = builder
                .Command((tocken) =>
                {
                    handlers.Execute();
                    waitHandle.Set();
                })
                .Activatable()
                .AutoActivate()
                .Build();

            handlers.CanExecuteReturnValue = true;
            var actual = command.CanExecute();
            Assert.True(actual);

            ((ICommand)command).Execute(null);
            waitHandle.WaitOne();
            Assert.AreEqual(1, handlers.ExecuteCount);
        }

        [Test]
        public void CommandBuilder_ConcurrencySyncCommandOfT_Activatable_AutoActivate_Return1()
        {
            var handlers = new DelegateHandlers();
            using var waitHandle = new EventWaitHandle(false, EventResetMode.AutoReset);
            var builder = new CommandBuilder();
            var command = builder
                .Command((int i, CancellationToken tocken) =>
                {
                    handlers.Execute();
                    waitHandle.Set();
                })
                .Activatable()
                .AutoActivate()
                .Build();

            handlers.CanExecuteReturnValue = true;
            var actual = command.CanExecute(0);
            Assert.True(actual);

            command.Execute(1);
            waitHandle.WaitOne();
            Assert.AreEqual(1, handlers.ExecuteCount);
        }

        [Test]
        public void CommandBuilder_ConcurrencyAsyncCommand_Activatable_AutoActivate_Return1()
        {
            var handlers = new DelegateHandlers();
            using var waitHandle = new EventWaitHandle(false, EventResetMode.AutoReset);
            var builder = new CommandBuilder();
            var command = builder
                .Command(async (tocken) =>
                {
                    await handlers.ExecuteAsync();
                    waitHandle.Set();
                })
                .Activatable()
                .AutoActivate()
                .Build();

            handlers.CanExecuteReturnValue = true;
            var actual = command.CanExecute();
            Assert.True(actual);

            ((ICommand)command).Execute(null);
            waitHandle.WaitOne();
            Assert.AreEqual(1, handlers.ExecuteCount);
        }

        [Test]
        public void CommandBuilder_ConcurrencyAsyncCommandOfT_Activatable_AutoActivate_Return1()
        {
            var handlers = new DelegateHandlers();
            using var waitHandle = new EventWaitHandle(false, EventResetMode.AutoReset);
            var builder = new CommandBuilder();
            var command = builder
                .Command(async (int i, CancellationToken tocken) =>
                {
                    await handlers.ExecuteAsync();
                    waitHandle.Set();
                })
                .Activatable()
                .AutoActivate()
                .Build();

            handlers.CanExecuteReturnValue = true;
            var actual = command.CanExecute(0);
            Assert.True(actual);

            command.Execute(1);
            waitHandle.WaitOne();
            Assert.AreEqual(1, handlers.ExecuteCount);
        }

        [Test]
        public void CommandBuilder_SyncCommand_CanExecute_Return1()
        {
            var handlers = new DelegateHandlers();
            var builder = new CommandBuilder();
            var command = builder
                .Command(() => handlers.Execute())
                .CanExecute(handlers.CanExecute)
                .Build();

            handlers.CanExecuteReturnValue = true;
            var actual = command.CanExecute();
            Assert.True(actual);

            ((ICommand)command).Execute(null);
            Assert.AreEqual(1, handlers.ExecuteCount);
        }

        [Test]
        public void CommandBuilder_SyncCommand_ObservesCanExecute_Return1()
        {
            var handlers = new DelegateHandlers();
            var builder = new CommandBuilder();
            var command = builder
                .Command(() => handlers.Execute())
                .ObservesCanExecute(() => handlers.CanExecute())
                .Build();

            handlers.CanExecuteReturnValue = true;
            var actual = command.CanExecute();
            Assert.True(actual);

            ((ICommand)command).Execute(null);
            Assert.AreEqual(1, handlers.ExecuteCount);
        }

        [Test]
        public void CommandBuilder_SyncCommand_ObservesCanExecute_Return0()
        {
            var handlers = new DelegateHandlers();
            var builder = new CommandBuilder();
            var command = builder
                .Command(() => handlers.Execute())
                .ObservesCanExecute(() => false || 1 > handlers.ExecuteCount)
                .Build();

            handlers.CanExecuteReturnValue = true;
            var actual = command.CanExecute();
            Assert.False(actual);

            ((ICommand)command).Execute(null);
            Assert.AreEqual(0, handlers.ExecuteCount);
        }

        [Test]
        public void CommandBuilder_SyncCommandOfT_CanExecute_Return1()
        {
            var handlers = new DelegateHandlers();
            var builder = new CommandBuilder();
            var command = builder
                .Command((int i) => handlers.Execute())
                .CanExecute(i => handlers.CanExecute())
                .Build();

            handlers.CanExecuteReturnValue = true;
            var actual = command.CanExecute(0);
            Assert.True(actual);

            command.Execute(1);
            Assert.AreEqual(1, handlers.ExecuteCount);
        }

        [Test]
        public void CommandBuilder_AsyncCommand_CanExecute_Return1()
        {
            var handlers = new DelegateHandlers();
            using var waitHandle = new EventWaitHandle(false, EventResetMode.AutoReset);
            var builder = new CommandBuilder();
            var command = builder
                .Command(async () =>
                {
                    await handlers.ExecuteAsync();
                    waitHandle.Set();
                })
                .CanExecute(handlers.CanExecute)
                .Build();

            handlers.CanExecuteReturnValue = true;
            var actual = command.CanExecute();
            Assert.True(actual);

            ((ICommand)command).Execute(null);
            waitHandle.WaitOne();
            Assert.AreEqual(1, handlers.ExecuteCount);
        }

        [Test]
        public void CommandBuilder_ConurrencySyncCommand_CanExecute_ReturnTrue()
        {
            var handlers = new DelegateHandlers();
            using var waitHandle = new EventWaitHandle(false, EventResetMode.AutoReset);
            var builder = new CommandBuilder();
            var command = builder
                .Command((token) =>
                {
                    handlers.Execute();
                    waitHandle.Set();
                })
                .CanExecute(handlers.CanExecute)
                .Build();

            handlers.CanExecuteReturnValue = true;
            var actual = command.CanExecute();
            Assert.True(actual);

            ((ICommand)command).Execute(null);
            waitHandle.WaitOne();
            Assert.AreEqual(1, handlers.ExecuteCount);
        }

        [Test]
        public void CommandBuilder_ConurrencySyncCommandOfT_CanExecute_ReturnTrue()
        {
            var handlers = new DelegateHandlers();
            var waitHandle = new EventWaitHandle(false, EventResetMode.AutoReset);
            var builder = new CommandBuilder();
            var command = builder
                .Command((int i, CancellationToken token) =>
                {
                    handlers.Execute();
                    waitHandle.Set();
                })
                .CanExecute(i => handlers.CanExecute())
                .Build();

            handlers.CanExecuteReturnValue = true;
            var actual = command.CanExecute(0);
            Assert.True(actual);

            command.Execute(1);
            waitHandle.WaitOne();
            waitHandle.Dispose();
            Assert.AreEqual(1, handlers.ExecuteCount);
        }

        [Test]
        public void CommandBuilder_ConurrencyAsyncCommand_CanExecute_ReturnTrue()
        {
            var handlers = new DelegateHandlers();
            var waitHandle = new EventWaitHandle(false, EventResetMode.AutoReset);
            var builder = new CommandBuilder();
            var command = builder
                .Command(async (token) =>
                {
                    await handlers.ExecuteAsync();
                    waitHandle.Set();
                })
                .CanExecute(handlers.CanExecute)
                .Build();

            handlers.CanExecuteReturnValue = true;
            var actual = command.CanExecute();
            Assert.True(actual);

            ((ICommand)command).Execute(null);
            waitHandle.WaitOne();
            waitHandle.Dispose();
            Assert.AreEqual(1, handlers.ExecuteCount);
        }

        [Test]
        public void CommandBuilder_ConurrencyAsyncCommandOfT_CanExecute_ReturnTrue()
        {
            var handlers = new DelegateHandlers();
            var waitHandle = new EventWaitHandle(false, EventResetMode.AutoReset);
            var builder = new CommandBuilder();
            var command = builder
                .Command(async (int i, CancellationToken token) =>
                {
                    await handlers.ExecuteAsync();
                    waitHandle.Set();
                })
                .CanExecute(t => handlers.CanExecute())
                .Build();

            handlers.CanExecuteReturnValue = true;
            var actual = command.CanExecute(0);
            Assert.True(actual);

            command.Execute(1);
            waitHandle.WaitOne();
            waitHandle.Dispose();
            Assert.AreEqual(1, handlers.ExecuteCount);
        }

        [Test]
        public void CommandBuilder_SyncCommand_Return1()
        {
            var handlers = new DelegateHandlers();
            var builder = new CommandBuilder();
            var command = builder
                .Command(() => handlers.Execute())
                .Build();

            handlers.CanExecuteReturnValue = true;
            var actual = command.CanExecute();
            Assert.True(actual);

            ((ICommand)command).Execute(null);
            Assert.AreEqual(1, handlers.ExecuteCount);
        }

        [Test]
        public void CommandBuilder_SyncCommand_CanExecute_Activatable_Activate_RetuenTrue()
        {
            var executed = false;
            var handlers = new DelegateHandlers();
            var builder = new CommandBuilder();
            var command = builder
                .Command(() => executed = true)
                .CanExecute(handlers.CanExecute)
                .Activatable()
                .Build()
                .Activate();

            handlers.CanExecuteReturnValue = true;
            var actual = command.CanExecute();
            Assert.True(actual);

            command.Execute(null);
            Assert.True(executed);
        }

        [Test]
        public void CommandBuilder_SyncCommand_Activatable_Activate_RetuenTrue()
        {
            var executed = false;
            var handlers = new DelegateHandlers();
            var builder = new CommandBuilder();
            var command = builder
                .Command(() => executed = true)
                .Activatable()
                .Build()
                .Activate();

            handlers.CanExecuteReturnValue = true;
            var actual = command.CanExecute();
            Assert.True(actual);

            command.Execute(null);
            Assert.True(executed);
        }

        [Test]
        public void CommandBuilder_ExecuteCallsPassedInExecuteDelegateNoCanExecute()
        {
            var executed = false;
            var handlers = new DelegateHandlers();
            var builder = new CommandBuilder();
            var command = builder
                .Command(() => executed = true)
                .CanExecute(handlers.CanExecute)
                .ObservesCommandManager()
                .Activatable()
                .Build();

            handlers.CanExecuteReturnValue = true;
            var actual = command.CanExecute();
            Assert.False(actual);
            ((ICommand)command).Execute(null);

            Assert.False(executed);
        }

        [Test]
        public void ExecuteCallsPassedInExecuteDelegateNoCanExecute2()
        {
            var executed = false;
            var command = new ActivatableCanExecuteObserverCommand(() => executed = true, new CommandManagerObserver())
                .Activate();

            command.Execute();
            Assert.True(executed);
        }

        [Test]
        public void ExecuteCallsCanExecuteTrue()
        {
            var executed = false;
            var command =
                new ActivatableCanExecuteObserverCommand(() => executed = true, () => true,
                    new CommandManagerObserver()).Activate() as ICommand;

            command.Execute(null);
            Assert.True(executed);
        }

        [Test]
        public void ExecuteNoParameterCallsCanExecuteTrue()
        {
            var executed = false;
            var command =
                new ActivatableCanExecuteObserverCommand(() => executed = true, () => true,
                    new CommandManagerObserver()).Activate();

            command.Execute();
            Assert.True(executed);
        }

        [Test]
        public void ExecuteCallsCanExecuteFalse()
        {
            var executed = false;
            var command =
                new ActivatableCanExecuteObserverCommand(() => executed = true, () => false,
                    new CommandManagerObserver()) as ICommand;

            command.Execute(null);
            Assert.False(executed);
        }

        [Test]
        public void ExecuteNoParameterCallsCanExecuteFalse()
        {
            var executed = false;
            var command =
                new ActivatableCanExecuteObserverCommand(() => executed = true, () => false,
                    new CommandManagerObserver());

            command.Execute();
            Assert.False(executed);
        }

        [Test]
        public void ExecuteCallsWithExceptionPassedInExecuteDelegate()
        {
            var command = new ActivatableCanExecuteObserverCommand(
                () => throw new Exception("Test Exception"),
                new CommandManagerObserver()).Activate() as ICommand;

            Assert.Throws<Exception>(() => { command.Execute(null); });
        }

        [Test]
        public void ExecuteCallsNoParameterWithExceptionPassedInExecuteDelegate()
        {
            var command = new ActivatableCanExecuteObserverCommand(
                () => throw new Exception("Test Exception"),
                new CommandManagerObserver()).Activate();

            Assert.Throws<Exception>(() => { command.Execute(); });
        }

        [Test]
        public void ExecuteCallsWithExceptionCanExecuteTrue()
        {
            var command = new ActivatableCanExecuteObserverCommand(
                () => throw new Exception("Test Exception"),
                () => true,
                new CommandManagerObserver()).Activate() as ICommand;

            Assert.Throws<Exception>(() => { command.Execute(null); });
        }

        [Test]
        public void ExecuteCallsNoParameterWithExceptionCanExecuteTrue()
        {
            var command = new ActivatableCanExecuteObserverCommand(
                () => throw new Exception("Test Exception"),
                () => true,
                new CommandManagerObserver()).Activate();

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
            var command =
                new ActivatableCanExecuteObserverCommand(handlers.Execute, () => true, new CommandManagerObserver());
            var canExecuteChangedRaised = false;
            command.CanExecuteChanged += delegate { canExecuteChangedRaised = true; };

            Assert.False(canExecuteChangedRaised);
        }

        [Test]
        public void RaiseCanExecuteChangedRaisesCanExecuteChanged()
        {
            var canExecuteChangedRaised = false;

            var handlers = new DelegateHandlers();
            var command =
                new ActivatableCanExecuteObserverCommand(handlers.Execute, true, () => false,
                    new CommandManagerObserver());

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
                    var command =
                        new ActivatableCanExecuteObserverCommand(() => { }, true, new CommandManagerObserver(), null);
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