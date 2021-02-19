﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Anorisoft.WinUI.Commands.CanExecuteObservers;
using Anorisoft.WinUI.Commands.Commands;
using Anorisoft.WinUI.Commands.Exeptions;
using Anorisoft.WinUI.Commands.Interfaces;
using JetBrains.Annotations;

namespace Anorisoft.WinUI.Commands.Builder
{
    public class SyncCommandBuilder<T> :
        ISyncCommandBuilder<T>,
        ISyncCanExecuteBuilder<T>,
        IActivatableSyncCommandBuilder<T>,
        IActivatableSyncCanExecuteBuilder<T>
    {
        /// <summary>
        /// The execute
        /// </summary>
        [NotNull] private readonly Action<T> execute;

        /// <summary>
        /// The observes
        /// </summary>
        private readonly List<ICanExecuteChangedSubject> observes = new List<ICanExecuteChangedSubject>();

        /// <summary>
        /// The can execute expression
        /// </summary>
        [CanBeNull] private CanExecuteObserver canExecuteExpression;

        /// <summary>
        /// The can execute function
        /// </summary>
        [CanBeNull] private Predicate<T> canExecuteFunction;

        /// <summary>
        /// The is automatic actiate
        /// </summary>
        private bool isAutoActiate = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="SyncCommandBuilder"/> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <exception cref="SyncCommandBuilder">execute</exception>
        public SyncCommandBuilder([NotNull] Action<T> execute) =>
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));

        /// <summary>
        /// Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns></returns>
        IActivatableSyncCommand<T> IActivatableSyncCanExecuteBuilder<T>.Build(
            Action<IActivatableSyncCommand<T>> setCommand) => Build(setCommand);

        /// <summary>
        /// Observeses the property.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        IActivatableSyncCanExecuteBuilder<T> IActivatableSyncCanExecuteBuilder<T>.ObservesProperty<TType>(
            Expression<Func<TType>> expression) => ObservesProperty(expression);

        /// <summary>
        /// Observeses the command manager.
        /// </summary>
        /// <returns></returns>
        IActivatableSyncCanExecuteBuilder<T> IActivatableSyncCanExecuteBuilder<T>.ObservesCommandManager() =>
            ObservesCommandManager();

        /// <summary>
        /// Automatics the activate.
        /// </summary>
        /// <returns></returns>
        IActivatableSyncCanExecuteBuilder<T> IActivatableSyncCanExecuteBuilder<T>.AutoActivate() => AutoActivate();

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        IActivatableSyncCommand<T> IActivatableSyncCanExecuteBuilder<T>.Build() => Build();

        /// <summary>
        /// Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns></returns>
        IActivatableSyncCommand<T> IActivatableSyncCommandBuilder<T>.Build(
            Action<IActivatableSyncCommand<T>> setCommand) => Build(setCommand);

        /// <summary>
        /// Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        IActivatableSyncCanExecuteBuilder<T> IActivatableSyncCommandBuilder<T>.CanExecute(Predicate<T> canExecute) =>
            CanExecute(canExecute);

        /// <summary>
        /// Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        IActivatableSyncCanExecuteBuilder<T> IActivatableSyncCommandBuilder<T>.ObservesCanExecute(
            Expression<Func<bool>> canExecute) => ObservesCanExecute(canExecute);

        /// <summary>
        /// Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <param name="fallback">if set to <c>true</c> [fallback].</param>
        /// <returns></returns>
        IActivatableSyncCanExecuteBuilder<T> IActivatableSyncCommandBuilder<T>.ObservesCanExecute(
            Expression<Func<bool>> canExecute, bool fallback) => ObservesCanExecute(canExecute, fallback);

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        IActivatableSyncCommand<T> IActivatableSyncCommandBuilder<T>.Build() => Build();

        /// <summary>
        /// Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns></returns>
        IActivatableSyncCommand<T> ISyncCanExecuteBuilder<T>.Build(Action<IActivatableSyncCommand<T>> setCommand) =>
            Build(setCommand);

        /// <summary>
        /// Activatables this instance.
        /// </summary>
        /// <returns></returns>
        IActivatableSyncCanExecuteBuilder<T> ISyncCanExecuteBuilder<T>.Activatable() => Activatable();

        /// <summary>
        /// Observeses the property.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        ISyncCanExecuteBuilder<T> ISyncCanExecuteBuilder<T>.
            ObservesProperty<TType>(Expression<Func<TType>> canExecute) => ObservesProperty(canExecute);

        /// <summary>
        /// Observeses the command manager.
        /// </summary>
        /// <returns></returns>
        ISyncCanExecuteBuilder<T> ISyncCanExecuteBuilder<T>.ObservesCommandManager() => ObservesCommandManager();

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        IActivatableSyncCommand<T> ISyncCanExecuteBuilder<T>.Build() => Build();

        /// <summary>
        /// Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns></returns>
        ISyncCommand<T> ISyncCommandBuilder<T>.Build(Action<ISyncCommand<T>> setCommand) => Build(setCommand);

        /// <summary>
        /// Activatables this instance.
        /// </summary>
        /// <returns></returns>
        IActivatableSyncCanExecuteBuilder<T> ISyncCommandBuilder<T>.Activatable() => Activatable();

        /// <summary>
        /// Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        ISyncCanExecuteBuilder<T> ISyncCommandBuilder<T>.ObservesCanExecute(Expression<Func<bool>> canExecute) =>
            ObservesCanExecute(canExecute);

        /// <summary>
        /// Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <param name="fallback">if set to <c>true</c> [fallback].</param>
        /// <returns></returns>
        ISyncCanExecuteBuilder<T> ISyncCommandBuilder<T>.ObservesCanExecute(Expression<Func<bool>> canExecute,
            bool fallback) => ObservesCanExecute(canExecute, fallback);

        /// <summary>
        /// Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        ISyncCanExecuteBuilder<T> ISyncCommandBuilder<T>.CanExecute(Predicate<T> canExecute) => CanExecute(canExecute);

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        ISyncCommand<T> ISyncCommandBuilder<T>.Build() => Build();

        /// <summary>
        /// Activatables this instance.
        /// </summary>
        /// <returns></returns>
        private SyncCommandBuilder<T> Activatable() => this;

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NoCanExecuteException"></exception>
        [NotNull]
        private ActivatableCanExecuteObserverCommand<T> Build()
        {
            if (observes.Any())
            {
                if (canExecuteFunction != null)
                {
                    return new ActivatableCanExecuteObserverCommand<T>(execute, isAutoActiate, canExecuteFunction,
                        observes.ToArray());
                }

                if (canExecuteExpression != null)
                {
                    return new ActivatableCanExecuteObserverCommand<T>(execute, isAutoActiate, canExecuteExpression,
                        observes.ToArray());
                }

                throw new NoCanExecuteException();
            }

            if (canExecuteFunction != null)
            {
                return new ActivatableCanExecuteObserverCommand<T>(execute, isAutoActiate, canExecuteFunction);
            }

            if (canExecuteExpression != null)
            {
                return new ActivatableCanExecuteObserverCommand<T>(execute, isAutoActiate, canExecuteExpression);
            }

            return new ActivatableCanExecuteObserverCommand<T>(execute, isAutoActiate);
        }

        /// <summary>
        /// Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">setCommand</exception>
        [NotNull]
        private ActivatableCanExecuteObserverCommand<T> Build([NotNull] Action<IActivatableSyncCommand<T>> setCommand)
        {
            if (setCommand == null) throw new ArgumentNullException(nameof(setCommand));
            var command = Build();
            setCommand(command);
            return command;
        }

        /// <summary>
        /// Observeses the property.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">expression</exception>
        [NotNull]
        private SyncCommandBuilder<T> ObservesProperty<TType>([NotNull] Expression<Func<TType>> expression)
        {
            if (expression == null) throw new ArgumentNullException(nameof(expression));
            this.observes.Add(new PropertyObserverFactory().ObservesProperty(expression));
            return this;
        }

        /// <summary>
        /// Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        /// <exception cref="CommandBuilderException">
        /// </exception>
        /// <exception cref="ArgumentNullException">canExecute</exception>
        [NotNull]
        public SyncCommandBuilder<T> CanExecute([NotNull] Predicate<T> canExecute)
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
        /// Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">canExecute</exception>
        /// <exception cref="CommandBuilderException">
        /// </exception>
        [NotNull]
        private SyncCommandBuilder<T> ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute)
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
        /// <exception cref="ArgumentNullException">canExecute</exception>
        /// <exception cref="CommandBuilderException">
        /// </exception>
        [NotNull]
        private SyncCommandBuilder<T> ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute, bool fallback)
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
        /// <exception cref="CommandBuilderException"></exception>
        [NotNull]
        private SyncCommandBuilder<T> ObservesCommandManager()
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
        private SyncCommandBuilder<T> AutoActivate()
        {
            isAutoActiate = true;
            return this;
        }
    }
}