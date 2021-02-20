using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Anorisoft.WinUI.Commands.CanExecuteObservers;
using Anorisoft.WinUI.Commands.Commands;
using Anorisoft.WinUI.Commands.Exceptions;
using Anorisoft.WinUI.Commands.Interfaces;
using Anorisoft.WinUI.Commands.Interfaces.Builders;
using JetBrains.Annotations;

namespace Anorisoft.WinUI.Commands.Builder
{
    public sealed class ConcurrencyAsyncCommandBuilder<T> :
        IConcurrencyAsyncCommandBuilder<T>,
        IConcurrencyAsyncCanExecuteBuilder<T>,
        IActivatableConcurrencyAsyncCommandBuilder<T>,
        IActivatableConcurrencyAsyncCanExecuteBuilder<T>
    {
        /// <summary>
        /// The execute
        /// </summary>
        private readonly Func<T, CancellationToken, Task> execute;

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
        /// Initializes a new instance of the <see cref="SyncCommandBuilder" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <exception cref="ArgumentNullException">execute</exception>
        public ConcurrencyAsyncCommandBuilder([NotNull] Func<T, CancellationToken, Task> execute) =>
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));

        /// <summary>
        /// Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns></returns>
        ActivatableConcurrencyAsyncCanExecuteObserverCommand<T> IActivatableConcurrencyAsyncCanExecuteBuilder<T>.Build(
            Action<ActivatableConcurrencyAsyncCanExecuteObserverCommand<T>> setCommand) => Build(setCommand);

        /// <summary>
        /// Observeses the property.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        IActivatableConcurrencyAsyncCanExecuteBuilder<T> IActivatableConcurrencyAsyncCanExecuteBuilder<T>.
            ObservesProperty<TType>(Expression<Func<TType>> expression) => ObservesProperty(expression);

        /// <summary>
        /// Observeses the command manager.
        /// </summary>
        /// <returns></returns>
        IActivatableConcurrencyAsyncCanExecuteBuilder<T> IActivatableConcurrencyAsyncCanExecuteBuilder<T>.
            ObservesCommandManager() => ObservesCommandManager();

        /// <summary>
        /// Automatics the activate.
        /// </summary>
        /// <returns></returns>
        IActivatableConcurrencyAsyncCanExecuteBuilder<T> IActivatableConcurrencyAsyncCanExecuteBuilder<T>.
            AutoActivate() => AutoActivate();

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        ActivatableConcurrencyAsyncCanExecuteObserverCommand<T> IActivatableConcurrencyAsyncCanExecuteBuilder<T>.Build() => BuildActivatable();

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        ActivatableConcurrencyAsyncCanExecuteObserverCommand<T> IActivatableConcurrencyAsyncCommandBuilder<T>.Build() => BuildActivatable();

        /// <summary>
        /// Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns></returns>
        ActivatableConcurrencyAsyncCanExecuteObserverCommand<T> IActivatableConcurrencyAsyncCommandBuilder<T>.Build(
            Action<ActivatableConcurrencyAsyncCanExecuteObserverCommand<T>> setCommand) => Build(setCommand);

        /// <summary>
        /// Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        IActivatableConcurrencyAsyncCanExecuteBuilder<T> IActivatableConcurrencyAsyncCommandBuilder<T>.
            CanExecute(Predicate<T> canExecute) => CanExecute(canExecute);

        /// <summary>
        /// Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        IActivatableConcurrencyAsyncCanExecuteBuilder<T> IActivatableConcurrencyAsyncCommandBuilder<T>.
            ObservesCanExecute(Expression<Func<bool>> canExecute) => ObservesCanExecute(canExecute);

        /// <summary>
        /// Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <param name="fallback">if set to <c>true</c> [fallback].</param>
        /// <returns></returns>
        IActivatableConcurrencyAsyncCanExecuteBuilder<T> IActivatableConcurrencyAsyncCommandBuilder<T>.
            ObservesCanExecute(Expression<Func<bool>> canExecute, bool fallback) =>
            ObservesCanExecute(canExecute, fallback);

        /// <summary>
        /// Automatics the activate.
        /// </summary>
        /// <returns></returns>
        IActivatableConcurrencyAsyncCanExecuteBuilder<T> IActivatableConcurrencyAsyncCommandBuilder<T>.AutoActivate() => AutoActivate();


        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        ConcurrencyAsyncCanExecuteObserverCommand<T> IConcurrencyAsyncCanExecuteBuilder<T>.Build() => Build();

        /// <summary>
        /// Observeses the property.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        IConcurrencyAsyncCanExecuteBuilder<T> IConcurrencyAsyncCanExecuteBuilder<T>.ObservesProperty<TType>(
            Expression<Func<TType>> expression) => ObservesProperty(expression);

        /// <summary>
        /// Observeses the command manager.
        /// </summary>
        /// <returns></returns>
        IConcurrencyAsyncCanExecuteBuilder<T> IConcurrencyAsyncCanExecuteBuilder<T>.ObservesCommandManager() =>
            ObservesCommandManager();

        /// <summary>
        /// Automatics the activate.
        /// </summary>
        /// <returns></returns>
        IConcurrencyAsyncCanExecuteBuilder<T> IConcurrencyAsyncCanExecuteBuilder<T>.AutoActivate() => AutoActivate();

        /// <summary>
        /// Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns></returns>
        ConcurrencyAsyncCanExecuteObserverCommand<T> IConcurrencyAsyncCanExecuteBuilder<T>.Build(
            Action<ConcurrencyAsyncCanExecuteObserverCommand<T>> setCommand) => Build(setCommand);

        /// <summary>
        /// Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns></returns>
        ConcurrencyAsyncCanExecuteObserverCommand<T> IConcurrencyAsyncCommandBuilder<T>.Build(
            Action<ConcurrencyAsyncCanExecuteObserverCommand<T>> setCommand) => Build(setCommand);

        /// <summary>
        /// Activatables this instance.
        /// </summary>
        /// <returns></returns>
        IActivatableConcurrencyAsyncCanExecuteBuilder<T> IConcurrencyAsyncCommandBuilder<T>.Activatable() =>
            Activatable();

        /// <summary>
        /// Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        IConcurrencyAsyncCanExecuteBuilder<T> IConcurrencyAsyncCommandBuilder<T>.ObservesCanExecute(
            Expression<Func<bool>> canExecute) => ObservesCanExecute(canExecute);

        /// <summary>
        /// Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <param name="fallback">if set to <c>true</c> [fallback].</param>
        /// <returns></returns>
        IConcurrencyAsyncCanExecuteBuilder<T> IConcurrencyAsyncCommandBuilder<T>.ObservesCanExecute(
            Expression<Func<bool>> canExecute, bool fallback) => ObservesCanExecute(canExecute, fallback);

        /// <summary>
        /// Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        IConcurrencyAsyncCanExecuteBuilder<T> IConcurrencyAsyncCommandBuilder<T>.CanExecute(Predicate<T> canExecute) =>
            CanExecute(canExecute);

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        ConcurrencyAsyncCanExecuteObserverCommand<T> IConcurrencyAsyncCommandBuilder<T>.Build() => Build();

        /// <summary>
        /// Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        /// <exception cref="CommandBuilderException">
        /// </exception>
        [NotNull]
        public ConcurrencyAsyncCommandBuilder<T> CanExecute([NotNull] Predicate<T> canExecute)
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
        /// Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">setCommand</exception>
        [NotNull]
        private ActivatableConcurrencyAsyncCanExecuteObserverCommand<T> Build(
            [NotNull] Action<ActivatableConcurrencyAsyncCanExecuteObserverCommand<T>> setCommand)
        {
            if (setCommand == null) throw new ArgumentNullException(nameof(setCommand));
            var command = BuildActivatable();
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
        private ConcurrencyAsyncCanExecuteObserverCommand<T> Build(
            [NotNull] Action<ConcurrencyAsyncCanExecuteObserverCommand<T>> setCommand)
        {
            if (setCommand == null) throw new ArgumentNullException(nameof(setCommand));
            var command = Build();
            setCommand(command);
            return command;
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NoCanExecuteException"></exception>
        [NotNull]
        private ActivatableConcurrencyAsyncCanExecuteObserverCommand<T> BuildActivatable()
        {
            if (observes.Any())
            {
                if (canExecuteFunction != null)
                {
                    return new ActivatableConcurrencyAsyncCanExecuteObserverCommand<T>(execute, isAutoActiate,
                        canExecuteFunction,
                        observes.ToArray());
                }

                if (canExecuteExpression != null)
                {
                    return new ActivatableConcurrencyAsyncCanExecuteObserverCommand<T>(execute, isAutoActiate,
                        canExecuteExpression,
                        observes.ToArray());
                }

                throw new NoCanExecuteException();
            }

            if (canExecuteFunction != null)
            {
                return new ActivatableConcurrencyAsyncCanExecuteObserverCommand<T>(execute, isAutoActiate,
                    canExecuteFunction);
            }

            if (canExecuteExpression != null)
            {
                return new ActivatableConcurrencyAsyncCanExecuteObserverCommand<T>(execute, isAutoActiate,
                    canExecuteExpression);
            }

            return new ActivatableConcurrencyAsyncCanExecuteObserverCommand<T>(execute, isAutoActiate);
        }


        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NoCanExecuteException"></exception>
        [NotNull]
        private ConcurrencyAsyncCanExecuteObserverCommand<T> Build()
        {
            if (observes.Any())
            {
                if (canExecuteFunction != null)
                {
                    return new ConcurrencyAsyncCanExecuteObserverCommand<T>(execute,
                        canExecuteFunction,
                        observes.ToArray());
                }

                if (canExecuteExpression != null)
                {
                    return new ConcurrencyAsyncCanExecuteObserverCommand<T>(execute,
                        canExecuteExpression,
                        observes.ToArray());
                }

                throw new NoCanExecuteException();
            }

            if (canExecuteFunction != null)
            {
                return new ConcurrencyAsyncCanExecuteObserverCommand<T>(execute,
                    canExecuteFunction);
            }

            if (canExecuteExpression != null)
            {
                return new ConcurrencyAsyncCanExecuteObserverCommand<T>(execute,
                    canExecuteExpression);
            }

            return new ConcurrencyAsyncCanExecuteObserverCommand<T>(execute);
        }

        /// <summary>
        /// Observeses the property.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        [NotNull]
        private ConcurrencyAsyncCommandBuilder<T> ObservesProperty<TType>([NotNull] Expression<Func<TType>> expression)
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
        private ConcurrencyAsyncCommandBuilder<T> ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute)
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
        private ConcurrencyAsyncCommandBuilder<T> ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute,
            bool fallback)
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
        private ConcurrencyAsyncCommandBuilder<T> ObservesCommandManager()
        {
            if (observes.Contains(CommandManagerObserver.Observer))
            {
                throw new CommandBuilderException(Resources.ExceptionStrings.CanExecuteFunctionAlreadyDefined);
            }

            this.observes.Add(CommandManagerObserver.Observer);
            return this;
        }

        /// <summary>
        /// Activatables this instance.
        /// </summary>
        /// <returns></returns>
        [NotNull]
        private ConcurrencyAsyncCommandBuilder<T> Activatable() => this;

        /// <summary>
        /// Automatics the activate.
        /// </summary>
        /// <returns></returns>
        [NotNull]
        private ConcurrencyAsyncCommandBuilder<T> AutoActivate()
        {
            isAutoActiate = true;
            return this;
        }
    }
}