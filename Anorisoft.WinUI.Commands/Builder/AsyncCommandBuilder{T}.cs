﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Anorisoft.WinUI.Commands.CanExecuteObservers;
using Anorisoft.WinUI.Commands.Commands;
using Anorisoft.WinUI.Commands.Exeptions;
using Anorisoft.WinUI.Commands.Interfaces;
using Anorisoft.WinUI.Commands.Interfaces.Builders;
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

        /// <summary>
        /// Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns></returns>
        ActivatableAsyncCanExecuteObserverCommand<T> IActivatableAsyncCanExecuteBuilder<T>.Build(
            Action<ActivatableAsyncCanExecuteObserverCommand<T>> setCommand) => Build(setCommand);

        /// <summary>
        /// Observeses the property.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        IActivatableAsyncCanExecuteBuilder<T> IActivatableAsyncCanExecuteBuilder<T>.ObservesProperty<TType>(
            Expression<Func<TType>> expression) => ObservesProperty(expression);

        /// <summary>
        /// Observeses the command manager.
        /// </summary>
        /// <returns></returns>
        IActivatableAsyncCanExecuteBuilder<T> IActivatableAsyncCanExecuteBuilder<T>.ObservesCommandManager() =>
            ObservesCommandManager();

        /// <summary>
        /// Automatics the activate.
        /// </summary>
        /// <returns></returns>
        IActivatableAsyncCanExecuteBuilder<T> IActivatableAsyncCanExecuteBuilder<T>.AutoActivate() => AutoActivate();

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        ActivatableAsyncCanExecuteObserverCommand<T> IActivatableAsyncCanExecuteBuilder<T>.Build() => Build();

        /// <summary>
        /// Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns></returns>
        ActivatableAsyncCanExecuteObserverCommand<T> IActivatableAsyncCommandBuilder<T>.Build(
            Action<ActivatableAsyncCanExecuteObserverCommand<T>> setCommand) => Build(setCommand);

        /// <summary>
        /// Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        IActivatableAsyncCanExecuteBuilder<T> IActivatableAsyncCommandBuilder<T>.CanExecute(Predicate<T> canExecute) =>
            CanExecute(canExecute);

        /// <summary>
        /// Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        IActivatableAsyncCanExecuteBuilder<T> IActivatableAsyncCommandBuilder<T>.ObservesCanExecute(
            Expression<Func<bool>> canExecute) => ObservesCanExecute(canExecute);

        /// <summary>
        /// Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <param name="fallback">if set to <c>true</c> [fallback].</param>
        /// <returns></returns>
        IActivatableAsyncCanExecuteBuilder<T> IActivatableAsyncCommandBuilder<T>.ObservesCanExecute(
            Expression<Func<bool>> canExecute, bool fallback) => ObservesCanExecute(canExecute, fallback);

        /// <summary>
        /// Automatics the activate.
        /// </summary>
        /// <returns></returns>
        IActivatableAsyncCanExecuteBuilder<T> IActivatableAsyncCommandBuilder<T>.AutoActivate() => AutoActivate();

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        ActivatableAsyncCanExecuteObserverCommand<T> IActivatableAsyncCommandBuilder<T>.Build() => Build();

        /// <summary>
        /// Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns></returns>
        IAsyncCommand<T> IAsyncCanExecuteBuilder<T>.Build(Action<IAsyncCommand<T>> setCommand) => Build(setCommand);

        /// <summary>
        /// Observeses the property.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        IAsyncCanExecuteBuilder<T> IAsyncCanExecuteBuilder<T>.ObservesProperty<TType>(
            Expression<Func<TType>> canExecute) => ObservesProperty(canExecute);

        /// <summary>
        /// Observeses the command manager.
        /// </summary>
        /// <returns></returns>
        IAsyncCanExecuteBuilder<T> IAsyncCanExecuteBuilder<T>.ObservesCommandManager() => ObservesCommandManager();

        /// <summary>
        /// Activatables this instance.
        /// </summary>
        /// <returns></returns>
        IActivatableAsyncCanExecuteBuilder<T> IAsyncCanExecuteBuilder<T>.Activatable() => Activatable();

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        IAsyncCommand<T> IAsyncCanExecuteBuilder<T>.Build() => Build();

        /// <summary>
        /// Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns></returns>
        IAsyncCommand<T> IAsyncCommandBuilder<T>.Build(Action<IAsyncCommand<T>> setCommand) => Build(setCommand);

        /// <summary>
        /// Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        IAsyncCanExecuteBuilder<T> IAsyncCommandBuilder<T>.CanExecute(Predicate<T> canExecute) =>
            CanExecute(canExecute);

        /// <summary>
        /// Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        IAsyncCanExecuteBuilder<T> IAsyncCommandBuilder<T>.ObservesCanExecute(Expression<Func<bool>> canExecute) =>
            ObservesCanExecute(canExecute);

        /// <summary>
        /// Observeses the command manager.
        /// </summary>
        /// <returns></returns>
        IAsyncCommandBuilder<T> IAsyncCommandBuilder<T>.ObservesCommandManager() => ObservesCommandManager();

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        IAsyncCommand<T> IAsyncCommandBuilder<T>.Build() => Build();

        /// <summary>
        /// Activatables this instance.
        /// </summary>
        /// <returns></returns>
        IActivatableAsyncCanExecuteBuilder<T> IAsyncCommandBuilder<T>.Activatable() => Activatable();

        /// <summary>
        /// Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        /// <exception cref="CommandBuilderException">
        /// </exception>
        [NotNull]
        public AsyncCommandBuilder<T> CanExecute([NotNull] Predicate<T> canExecute)
        {
            if (this.canExecuteFunction != null)
            {
                throw new CommandBuilderException(Resources.ExceptionStrings.CanExecuteFunctionAlreadyDefined);
            }

            if (this.canExecuteExpression != null)
            {
                throw new CommandBuilderException(Resources.ExceptionStrings.CanExecuteExpressionAlreadyDefined);
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
        private ActivatableAsyncCanExecuteObserverCommand<T> Build([NotNull] Action<ActivatableAsyncCanExecuteObserverCommand<T>> setCommand)
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
        /// <exception cref="CommandBuilderException">
        /// </exception>
        [NotNull]
        private AsyncCommandBuilder<T> ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute)
        {
            if (canExecute == null) throw new ArgumentNullException(nameof(canExecute));
            if (this.canExecuteExpression != null)
            {
                throw new CommandBuilderException(Resources.ExceptionStrings.CanExecuteExpressionAlreadyDefined);
            }

            if (this.canExecuteFunction != null)
            {
                throw new CommandBuilderException(Resources.ExceptionStrings.CanExecuteFunctionAlreadyDefined);
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
        /// <exception cref="CommandBuilderException">
        /// </exception>
        [NotNull]
        private AsyncCommandBuilder<T> ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute, bool fallback)
        {
            if (canExecute == null) throw new ArgumentNullException(nameof(canExecute));
            if (this.canExecuteExpression != null)
            {
                throw new CommandBuilderException(Resources.ExceptionStrings.CanExecuteExpressionAlreadyDefined);
            }

            if (this.canExecuteFunction != null)
            {
                throw new CommandBuilderException(Resources.ExceptionStrings.CanExecuteFunctionAlreadyDefined);
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
                throw new CommandBuilderException(Resources.ExceptionStrings.CanExecuteFunctionAlreadyDefined);
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