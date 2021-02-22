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
        private ICanExecuteSubject canExecuteSubject;

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
        /// Observeses the specified observer.
        /// </summary>
        /// <param name="observer">The observer.</param>
        /// <returns></returns>
        IActivatableConcurrencyAsyncCanExecuteBuilder<T> IActivatableConcurrencyAsyncCanExecuteBuilder<T>.Observes(
            ICanExecuteChangedSubject observer)
        {
            observes.Add(observer);
            return this;
        }

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

        IActivatableConcurrencyAsyncCanExecuteBuilder<T> IActivatableConcurrencyAsyncCanExecuteBuilder<T>.OnError(Action<Exception> error)
        {
            return OnError(error);
        }

        IActivatableConcurrencyAsyncCanExecuteBuilder<T> IActivatableConcurrencyAsyncCanExecuteBuilder<T>.OnCompleted(Action completed)
        {
            return OnCompleted(completed);
        }

        IActivatableConcurrencyAsyncCanExecuteBuilder<T> IActivatableConcurrencyAsyncCanExecuteBuilder<T>.OnCancel(Action cancel)
        {
            return OnCancel(cancel);
        }

        IActivatableConcurrencyAsyncCanExecuteBuilder<T> IActivatableConcurrencyAsyncCommandBuilder<T>.OnError(Action<Exception> error)
        {
            return OnError(error);
        }

        IActivatableConcurrencyAsyncCanExecuteBuilder<T> IActivatableConcurrencyAsyncCommandBuilder<T>.OnCompleted(Action completed)
        {
            return OnCompleted(completed);
        }

        IActivatableConcurrencyAsyncCanExecuteBuilder<T> IActivatableConcurrencyAsyncCommandBuilder<T>.OnCancel(Action cancel)
        {
            return OnCancel(cancel);
        }

        IConcurrencyAsyncCanExecuteBuilder<T> IConcurrencyAsyncCanExecuteBuilder<T>.OnError(Action<Exception> error)
        {
            return OnError(error);
        }

        IConcurrencyAsyncCanExecuteBuilder<T> IConcurrencyAsyncCanExecuteBuilder<T>.OnCompleted(Action completed)
        {
            return OnCompleted(completed);
        }

        IConcurrencyAsyncCanExecuteBuilder<T> IConcurrencyAsyncCanExecuteBuilder<T>.OnCancel(Action cancel)
        {
            return OnCancel(cancel);
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        ActivatableConcurrencyAsyncCanExecuteObserverCommand<T> IActivatableConcurrencyAsyncCanExecuteBuilder<T>.
            Build() => BuildActivatable();

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        ActivatableConcurrencyAsyncCanExecuteObserverCommand<T> IActivatableConcurrencyAsyncCommandBuilder<T>.Build() =>
            BuildActivatable();

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
        /// Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        IActivatableConcurrencyAsyncCanExecuteBuilder<T> IActivatableConcurrencyAsyncCommandBuilder<T>.CanExecute(ICanExecuteSubject canExecute)
        {
            this.canExecuteSubject = canExecute;
            return this;
        }

        /// <summary>
        /// Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        IConcurrencyAsyncCanExecuteBuilder<T> IConcurrencyAsyncCommandBuilder<T>.CanExecute(ICanExecuteSubject canExecute)
        {
            this.canExecuteSubject = canExecute;
            return this;
        }

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
        IActivatableConcurrencyAsyncCanExecuteBuilder<T> IActivatableConcurrencyAsyncCommandBuilder<T>.AutoActivate() =>
            AutoActivate();

        /// <summary>
        /// Observeses the specified observer.
        /// </summary>
        /// <param name="observer">The observer.</param>
        /// <returns></returns>
        IConcurrencyAsyncCanExecuteBuilder<T> IConcurrencyAsyncCanExecuteBuilder<T>.Observes(
            ICanExecuteChangedSubject observer)
        {
            observes.Add(observer);
            return this;
        }

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
        IActivatableConcurrencyAsyncCommandBuilder<T> IConcurrencyAsyncCommandBuilder<T>.Activatable() =>
            Activatable();

        IConcurrencyAsyncCanExecuteBuilder<T> IConcurrencyAsyncCommandBuilder<T>.OnError(Action<Exception> error)
        {
            return OnError(error);
        }

        IConcurrencyAsyncCanExecuteBuilder<T> IConcurrencyAsyncCommandBuilder<T>.OnCompleted(Action completed)
        {
            return OnCompleted(completed);
        }

        IConcurrencyAsyncCanExecuteBuilder<T> IConcurrencyAsyncCommandBuilder<T>.OnCancel(Action cancel)
        {
            return OnCancel(cancel);
        }

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

            if (this.canExecuteSubject != null)
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
                        canExecuteFunction, completedAction, errorAction, cancelAction,
                        observes.ToArray());
                }

                if (canExecuteSubject != null)
                {
                    return new ActivatableConcurrencyAsyncCanExecuteObserverCommand<T>(execute, isAutoActiate,
                        canExecuteSubject, completedAction, errorAction, cancelAction,
                        observes.ToArray());
                }

                throw new NoCanExecuteException();
            }

            if (canExecuteFunction != null)
            {
                return new ActivatableConcurrencyAsyncCanExecuteObserverCommand<T>(execute, isAutoActiate,
                    canExecuteFunction, completedAction, errorAction, cancelAction);
            }

            if (canExecuteSubject != null)
            {
                return new ActivatableConcurrencyAsyncCanExecuteObserverCommand<T>(execute, isAutoActiate,
                    canExecuteSubject, completedAction, errorAction, cancelAction);
            }

            return new ActivatableConcurrencyAsyncCanExecuteObserverCommand<T>(execute, isAutoActiate, completedAction, errorAction, cancelAction);
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
                        canExecuteFunction, completedAction, errorAction, cancelAction,
                        observes.ToArray());
                }

                if (canExecuteSubject != null)
                {
                    return new ConcurrencyAsyncCanExecuteObserverCommand<T>(execute,
                        canExecuteSubject, completedAction, errorAction, cancelAction,
                        observes.ToArray());
                }

                throw new NoCanExecuteException();
            }

            if (canExecuteFunction != null)
            {
                return new ConcurrencyAsyncCanExecuteObserverCommand<T>(execute,
                    canExecuteFunction, completedAction, errorAction, cancelAction);
            }

            if (canExecuteSubject != null)
            {
                return new ConcurrencyAsyncCanExecuteObserverCommand<T>(execute,
                    canExecuteSubject, completedAction, errorAction, cancelAction);
            }

            return new ConcurrencyAsyncCanExecuteObserverCommand<T>(execute, completedAction, errorAction, cancelAction);
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
            if (this.canExecuteSubject != null)
            {
                throw new CommandBuilderException(Resources.ExceptionStrings.CanExecuteExpressionAlreadyDefined);
            }

            if (this.canExecuteFunction != null)
            {
                throw new CommandBuilderException(Resources.ExceptionStrings.CanExecuteFunctionAlreadyDefined);
            }

            this.canExecuteSubject = CanExecuteObserver.Create(canExecute);
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
            if (this.canExecuteSubject != null)
            {
                throw new CommandBuilderException(Resources.ExceptionStrings.CanExecuteExpressionAlreadyDefined);
            }

            if (this.canExecuteFunction != null)
            {
                throw new CommandBuilderException(Resources.ExceptionStrings.CanExecuteFunctionAlreadyDefined);
            }

            this.canExecuteSubject = CanExecuteObserver.Create(canExecute, fallback);
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

        private ConcurrencyAsyncCommandBuilder<T> OnError(Action<Exception> error)
        {
            errorAction = error;
            return this;
        }

        private ConcurrencyAsyncCommandBuilder<T> OnCompleted(Action completed)
        {
            completedAction = completed;
            return this;
        }

        private ConcurrencyAsyncCommandBuilder<T> OnCancel(Action cancel)
        {
            cancelAction = cancel;
            return this;
        }

        /// <summary>
        /// The error action
        /// </summary>
        private Action<Exception> errorAction;
        /// <summary>
        /// The completed action
        /// </summary>
        private Action completedAction;
        /// <summary>
        /// The cancel action
        /// </summary>
        private Action cancelAction;
    }
}