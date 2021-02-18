﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Anorisoft.WinUI.Commands.CanExecuteObservers;
using Anorisoft.WinUI.Commands.Commands;
using Anorisoft.WinUI.Commands.Exeptions;
using Anorisoft.WinUI.Commands.Factory;
using Anorisoft.WinUI.Commands.Interfaces;
using JetBrains.Annotations;

namespace Anorisoft.WinUI.Commands.Builder
{
    public class AsyncCommandBuilder<T> :
        IAsyncCommandBuilder<T>,
        IAsyncCanExecuteBuilder<T>,
        IActivatableAsyncCommandBuilder<T>,
        IActivatableAsyncCanExecuteBuilder<T>
    {
        /// <summary>
        /// The execute
        /// </summary>
        private readonly Func<T, Task> execute;

        /// <summary>
        /// The observes
        /// </summary>
        private readonly List<ICanExecuteChangedSubject> observes = new List<ICanExecuteChangedSubject>();

        /// <summary>
        /// The can execute expression
        /// </summary>
        private CanExecuteObserver canExecuteExpression;

        /// <summary>
        /// The can execute function
        /// </summary>
        private Predicate<T> canExecuteFunction;

        /// <summary>
        /// The is automatic actiate
        /// </summary>
        private bool isAutoActiate;

        /// <summary>
        /// Initializes a new instance of the <see cref="SyncCommandBuilder"/> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <exception cref="ArgumentNullException">execute</exception>
        public AsyncCommandBuilder([NotNull] Func<T, Task> execute) =>
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));

        IActivatableAsyncCommand<T> IActivatableAsyncCanExecuteBuilder<T>.Build(
            Action<IActivatableAsyncCommand<T>> setCommand) => Build(setCommand);

        IActivatableAsyncCanExecuteBuilder<T> IActivatableAsyncCanExecuteBuilder<T>.ObservesProperty<TType>(
            Expression<Func<TType>> expression) => ObservesProperty(expression);

        IActivatableAsyncCanExecuteBuilder<T> IActivatableAsyncCanExecuteBuilder<T>.ObservesCommandManager() =>
            ObservesCommandManager();

        IActivatableAsyncCanExecuteBuilder<T> IActivatableAsyncCanExecuteBuilder<T>.AutoActivate() => AutoActivate();

        IActivatableAsyncCommand<T> IActivatableAsyncCanExecuteBuilder<T>.Build() => Build();

        IActivatableAsyncCommand<T> IActivatableAsyncCommandBuilder<T>.Build(
            Action<IActivatableAsyncCommand<T>> setCommand) => Build(setCommand);

        IActivatableAsyncCanExecuteBuilder<T> IActivatableAsyncCommandBuilder<T>.CanExecute(Predicate<T> canExecute) =>
            CanExecute(canExecute);

        IActivatableAsyncCanExecuteBuilder<T> IActivatableAsyncCommandBuilder<T>.ObservesCanExecute(
            Expression<Func<bool>> canExecute) => ObservesCanExecute(canExecute);

        IActivatableAsyncCanExecuteBuilder<T> IActivatableAsyncCommandBuilder<T>.ObservesCanExecute(
            Expression<Func<bool>> canExecute, bool fallback) => ObservesCanExecute(canExecute, fallback);

        IActivatableAsyncCommand<T> IActivatableAsyncCommandBuilder<T>.Build() => Build();

        IAsyncCommand<T> IAsyncCanExecuteBuilder<T>.Build(Action<IAsyncCommand<T>> setCommand) => Build(setCommand);

        IAsyncCanExecuteBuilder<T> IAsyncCanExecuteBuilder<T>.ObservesProperty<TType>(
            Expression<Func<TType>> canExecute) => ObservesProperty(canExecute);

        IAsyncCanExecuteBuilder<T> IAsyncCanExecuteBuilder<T>.ObservesCommandManager() => ObservesCommandManager();

        IActivatableAsyncCanExecuteBuilder<T> IAsyncCanExecuteBuilder<T>.Activatable() => Activatable();

        IAsyncCommand<T> IAsyncCanExecuteBuilder<T>.Build() => Build();

        IAsyncCommand<T> IAsyncCommandBuilder<T>.Build(Action<IAsyncCommand<T>> setCommand) => Build(setCommand);

        IAsyncCanExecuteBuilder<T> IAsyncCommandBuilder<T>.CanExecute(Predicate<T> canExecute) =>
            CanExecute(canExecute);

        IAsyncCanExecuteBuilder<T> IAsyncCommandBuilder<T>.ObservesCanExecute(Expression<Func<bool>> canExecute) =>
            ObservesCanExecute(canExecute);

        IAsyncCommandBuilder<T> IAsyncCommandBuilder<T>.ObservesCommandManager() => ObservesCommandManager();

        IAsyncCommand<T> IAsyncCommandBuilder<T>.Build() => Build();

        /// <summary>
        /// Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        /// <exception cref="CommandFactoryException">
        /// </exception>
        [NotNull]
        public AsyncCommandBuilder<T> CanExecute([NotNull] Predicate<T> canExecute)
        {
            if (this.canExecuteFunction != null)
            {
                throw new CommandFactoryException(Resources.ExceptionStrings.CanExecuteFunctionAlreadyDefined);
            }

            if (this.canExecuteExpression != null)
            {
                throw new CommandFactoryException(Resources.ExceptionStrings.CanExecuteExpressionAlreadyDefined);
            }

            this.canExecuteFunction = canExecute ?? throw new ArgumentNullException(nameof(canExecute));
            return this;
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        [NotNull]
        private ActivatableAsyncCanExecuteObserverCommand<T> Build()
        {
            if (observes.Any())
            {
                if (canExecuteFunction != null)
                {
                    return new ActivatableAsyncCanExecuteObserverCommand<T>(execute, isAutoActiate, canExecuteFunction,
                        observes.ToArray());
                }

                if (canExecuteExpression != null)
                {
                    return new ActivatableAsyncCanExecuteObserverCommand<T>(execute, isAutoActiate,
                        canExecuteExpression,
                        observes.ToArray());
                }

                throw new NoCanExecuteException();
            }

            if (canExecuteFunction != null)
            {
                return new ActivatableAsyncCanExecuteObserverCommand<T>(execute, isAutoActiate, canExecuteFunction);
            }

            if (canExecuteExpression != null)
            {
                return new ActivatableAsyncCanExecuteObserverCommand<T>(execute, isAutoActiate, canExecuteExpression);
            }

            return new ActivatableAsyncCanExecuteObserverCommand<T>(execute, isAutoActiate);
        }

        /// <summary>
        /// Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">setCommand</exception>
        [NotNull]
        private IAsyncCommand<T> Build([NotNull] Action<IAsyncCommand<T>> setCommand)
        {
            if (setCommand == null)
            {
                throw new ArgumentNullException(nameof(setCommand));
            }

            var command = Build();
            setCommand(command);
            return command;
        }

        /// <summary>
        /// Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">setCommand</exception>
        [NotNull]
        private IActivatableAsyncCommand<T> Build([NotNull] Action<IActivatableAsyncCommand<T>> setCommand)
        {
            if (setCommand == null)
            {
                throw new ArgumentNullException(nameof(setCommand));
            }

            var command = Build();
            setCommand(command);
            return command;
        }

        /// <summary>
        /// Activatables this instance.
        /// </summary>
        /// <returns></returns>
        private IActivatableAsyncCanExecuteBuilder<T> Activatable() => this;

        /// <summary>
        /// Observeses the property.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        [NotNull]
        private AsyncCommandBuilder<T> ObservesProperty<TType>([NotNull] Expression<Func<TType>> expression)
        {
            if (expression == null) throw new ArgumentNullException(nameof(expression));
            this.observes.Add(new PropertyObserverFactory().ObservesProperty(expression));
            return this;
        }

        /// <summary>
        /// Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        /// <exception cref="CommandFactoryException">
        /// </exception>
        [NotNull]
        private AsyncCommandBuilder<T> ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute)
        {
            if (canExecute == null) throw new ArgumentNullException(nameof(canExecute));
            if (this.canExecuteExpression != null)
            {
                throw new CommandFactoryException(Resources.ExceptionStrings.CanExecuteExpressionAlreadyDefined);
            }

            if (this.canExecuteFunction != null)
            {
                throw new CommandFactoryException(Resources.ExceptionStrings.CanExecuteFunctionAlreadyDefined);
            }

            this.canExecuteExpression = CanExecuteObserver.Create(canExecute);
            return this;
        }

        /// <summary>
        /// Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <param name="fallback">if set to <c>true</c> [fallback].</param>
        /// <returns></returns>
        /// <exception cref="CommandFactoryException">
        /// </exception>
        [NotNull]
        private AsyncCommandBuilder<T> ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute, bool fallback)
        {
            if (canExecute == null) throw new ArgumentNullException(nameof(canExecute));
            if (this.canExecuteExpression != null)
            {
                throw new CommandFactoryException(Resources.ExceptionStrings.CanExecuteExpressionAlreadyDefined);
            }

            if (this.canExecuteFunction != null)
            {
                throw new CommandFactoryException(Resources.ExceptionStrings.CanExecuteFunctionAlreadyDefined);
            }

            this.canExecuteExpression = CanExecuteObserver.Create(canExecute, fallback);
            return this;
        }

        /// <summary>
        /// Observeses the command manager.
        /// </summary>
        /// <returns></returns>
        [NotNull]
        private AsyncCommandBuilder<T> ObservesCommandManager()
        {
            if (observes.Contains(CommandManagerObserver.Observer))
            {
                throw new CommandFactoryException(Resources.ExceptionStrings.CanExecuteFunctionAlreadyDefined);
            }

            this.observes.Add(CommandManagerObserver.Observer);
            return this;
        }

        /// <summary>
        /// Automatics the activate.
        /// </summary>
        /// <returns></returns>
        [NotNull]
        private AsyncCommandBuilder<T> AutoActivate()
        {
            isAutoActiate = true;
            return this;
        }
    }
}