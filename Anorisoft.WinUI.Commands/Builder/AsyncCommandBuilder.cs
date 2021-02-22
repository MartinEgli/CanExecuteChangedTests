using Anorisoft.WinUI.Commands.CanExecuteObservers;
using Anorisoft.WinUI.Commands.Commands;
using Anorisoft.WinUI.Commands.Exceptions;
using Anorisoft.WinUI.Commands.Interfaces;
using Anorisoft.WinUI.Commands.Interfaces.Builders;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Anorisoft.WinUI.Commands.Builder
{
    public sealed class AsyncCommandBuilder :
        IAsyncCommandBuilder,
        IAsyncCanExecuteBuilder,
        IActivatableAsyncCommandBuilder,
        IActivatableAsyncCanExecuteBuilder
    {
        /// <summary>
        /// The execute
        /// </summary>
        [NotNull] private readonly Func<Task> execute;

        /// <summary>
        /// The observes
        /// </summary>
        [NotNull]
        [ItemNotNull]
        private readonly List<ICanExecuteChangedSubject> observes = new List<ICanExecuteChangedSubject>();

        /// <summary>
        /// The can execute expression
        /// </summary>
        [CanBeNull] private ICanExecuteSubject canExecuteSubject;

        /// <summary>
        /// The can execute function
        /// </summary>
        [CanBeNull] private Func<bool> canExecuteFunction;

        /// <summary>
        /// The is automatic actiate
        /// </summary>
        private bool isAutoActivate = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="SyncCommandBuilder"/> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <exception cref="ArgumentNullException">execute</exception>
        public AsyncCommandBuilder([NotNull] Func<Task> execute) =>
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));

        /// <summary>
        /// Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns></returns>
        ActivatableAsyncCanExecuteObserverCommand IActivatableAsyncCanExecuteBuilder.
            Build(Action<ActivatableAsyncCanExecuteObserverCommand> setCommand) => Build(setCommand);

        /// <summary>
        /// Observeses the property.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        IActivatableAsyncCanExecuteBuilder IActivatableAsyncCanExecuteBuilder.ObservesProperty<TType>(
            Expression<Func<TType>> expression) => ObservesProperty(expression);

        /// <summary>
        /// Observeses the specified observer.
        /// </summary>
        /// <param name="observer">The observer.</param>
        /// <returns></returns>
        IActivatableAsyncCanExecuteBuilder IActivatableAsyncCanExecuteBuilder.Observes(ICanExecuteChangedSubject observer)
        {
            observes.Add(observer);
            return this;
        }

        /// <summary>
        /// Observeses the specified observer.
        /// </summary>
        /// <param name="observer">The observer.</param>
        /// <returns></returns>
        IAsyncCanExecuteBuilder IAsyncCanExecuteBuilder.Observes(ICanExecuteChangedSubject observer)
        {
            observes.Add(observer);
            return this;
        }

        /// <summary>
        /// Observeses the command manager.
        /// </summary>
        /// <returns></returns>
        IActivatableAsyncCanExecuteBuilder IActivatableAsyncCanExecuteBuilder.ObservesCommandManager() =>
            ObservesCommandManager();

        /// <summary>
        /// Automatics the activate.
        /// </summary>
        /// <returns></returns>
        IActivatableAsyncCanExecuteBuilder IActivatableAsyncCanExecuteBuilder.AutoActivate() => AutoActivate();

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        ActivatableAsyncCanExecuteObserverCommand IActivatableAsyncCanExecuteBuilder.Build() => BuildActivatable();

        /// <summary>
        /// Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns></returns>
        ActivatableAsyncCanExecuteObserverCommand IActivatableAsyncCommandBuilder.Build(
            Action<ActivatableAsyncCanExecuteObserverCommand> setCommand) =>
            Build(setCommand);

        /// <summary>
        /// Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        IActivatableAsyncCanExecuteBuilder IActivatableAsyncCommandBuilder.CanExecute(Func<bool> canExecute) =>
            CanExecute(canExecute);

        IActivatableAsyncCanExecuteBuilder IActivatableAsyncCommandBuilder.CanExecute(ICanExecuteSubject canExecute)
        {
            this.canExecuteSubject = canExecute;
            return this;
        }

        IAsyncCanExecuteBuilder IAsyncCommandBuilder.CanExecute(ICanExecuteSubject canExecute)
        {
            this.canExecuteSubject = canExecute;
            return this;
        }

        /// <summary>
        /// Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        IActivatableAsyncCanExecuteBuilder IActivatableAsyncCommandBuilder.ObservesCanExecute(
            Expression<Func<bool>> canExecute) => ObservesCanExecute(canExecute);

        /// <summary>
        /// Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <param name="fallback">if set to <c>true</c> [fallback].</param>
        /// <returns></returns>
        IActivatableAsyncCanExecuteBuilder IActivatableAsyncCommandBuilder.ObservesCanExecute(
            Expression<Func<bool>> canExecute, bool fallback) => ObservesCanExecute(canExecute, fallback);

        /// <summary>
        /// Automatics the activate.
        /// </summary>
        /// <returns></returns>
        IActivatableAsyncCanExecuteBuilder IActivatableAsyncCommandBuilder.AutoActivate() => AutoActivate();

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        ActivatableAsyncCanExecuteObserverCommand IActivatableAsyncCommandBuilder.Build() => BuildActivatable();

        /// <summary>
        /// Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns></returns>
        AsyncCanExecuteObserverCommand IAsyncCanExecuteBuilder.Build(Action<AsyncCanExecuteObserverCommand> setCommand) => Build(setCommand);

        /// <summary>
        /// Observeses the property.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        IAsyncCanExecuteBuilder IAsyncCanExecuteBuilder.ObservesProperty<TType>(Expression<Func<TType>> canExecute) =>
            ObservesProperty(canExecute);

        /// <summary>
        /// Activatables this instance.
        /// </summary>
        /// <returns></returns>
        IActivatableAsyncCanExecuteBuilder IAsyncCanExecuteBuilder.Activatable() => Activatable();

        /// <summary>
        /// Observeses the command manager.
        /// </summary>
        /// <returns></returns>
        IAsyncCanExecuteBuilder IAsyncCanExecuteBuilder.ObservesCommandManager() => ObservesCommandManager();

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        AsyncCanExecuteObserverCommand IAsyncCanExecuteBuilder.Build() => Build();

        /// <summary>
        /// Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns></returns>
        AsyncCanExecuteObserverCommand IAsyncCommandBuilder.Build(Action<AsyncCanExecuteObserverCommand> setCommand) => Build(setCommand);

        /// <summary>
        /// Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        IAsyncCanExecuteBuilder IAsyncCommandBuilder.CanExecute(Func<bool> canExecute) => CanExecute(canExecute);

        /// <summary>
        /// Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        IAsyncCanExecuteBuilder IAsyncCommandBuilder.ObservesCanExecute(Expression<Func<bool>> canExecute) =>
            ObservesCanExecute(canExecute);

        /// <summary>
        /// Observeses the command manager.
        /// </summary>
        /// <returns></returns>
        IAsyncCommandBuilder IAsyncCommandBuilder.ObservesCommandManager() => ObservesCommandManager();

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        AsyncCanExecuteObserverCommand IAsyncCommandBuilder.Build() => Build();

        /// <summary>
        /// Activatables this instance.
        /// </summary>
        /// <returns></returns>
        IActivatableAsyncCommandBuilder IAsyncCommandBuilder.Activatable() => Activatable();

        /// <summary>
        /// Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <param name="fallback">if set to <c>true</c> [fallback].</param>
        /// <returns></returns>
        /// <exception cref="CommandBuilderException">
        /// </exception>
        [NotNull]
        public AsyncCommandBuilder ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute, bool fallback)
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
        /// Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        /// <exception cref="CommandBuilderException">
        /// </exception>
        [NotNull]
        private AsyncCommandBuilder CanExecute([NotNull] Func<bool> canExecute)
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
        private AsyncCanExecuteObserverCommand Build([NotNull] Action<AsyncCanExecuteObserverCommand> setCommand)
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
        private ActivatableAsyncCanExecuteObserverCommand Build(
            [NotNull] Action<ActivatableAsyncCanExecuteObserverCommand> setCommand)
        {
            if (setCommand == null)
            {
                throw new ArgumentNullException(nameof(setCommand));
            }

            var command = BuildActivatable();
            setCommand(command);
            return command;
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        [NotNull]
        private ActivatableAsyncCanExecuteObserverCommand BuildActivatable()
        {
            if (observes.Any())
            {
                if (canExecuteFunction != null)
                {
                    return new ActivatableAsyncCanExecuteObserverCommand(execute, isAutoActivate, canExecuteFunction,
                        observes.ToArray());
                }

                if (canExecuteSubject != null)
                {
                    return new ActivatableAsyncCanExecuteObserverCommand(execute, isAutoActivate, canExecuteSubject,
                        observes.ToArray());
                }

                throw new NoCanExecuteException();
            }

            if (canExecuteFunction != null)
            {
                return new ActivatableAsyncCanExecuteObserverCommand(execute, isAutoActivate, canExecuteFunction);
            }

            if (canExecuteSubject != null)
            {
                return new ActivatableAsyncCanExecuteObserverCommand(execute, isAutoActivate, canExecuteSubject);
            }

            return new ActivatableAsyncCanExecuteObserverCommand(execute, isAutoActivate);
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        [NotNull]
        private AsyncCanExecuteObserverCommand Build()
        {
            if (observes.Any())
            {
                if (canExecuteFunction != null)
                {
                    return new AsyncCanExecuteObserverCommand(execute, canExecuteFunction,
                        observes.ToArray());
                }

                if (canExecuteSubject != null)
                {
                    return new AsyncCanExecuteObserverCommand(execute, canExecuteSubject,
                        observes.ToArray());
                }

                throw new NoCanExecuteException();
            }

            if (canExecuteFunction != null)
            {
                return new AsyncCanExecuteObserverCommand(execute, canExecuteFunction);
            }

            if (canExecuteSubject != null)
            {
                return new AsyncCanExecuteObserverCommand(execute, canExecuteSubject);
            }

            return new AsyncCanExecuteObserverCommand(execute);
        }

        /// <summary>
        /// Observeses the property.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        [NotNull]
        private AsyncCommandBuilder ObservesProperty<TType>([NotNull] Expression<Func<TType>> expression)
        {
            if (expression == null) throw new ArgumentNullException(nameof(expression));
            this.observes.Add(new PropertyObserverFactory().ObservesProperty(expression));
            return this;
        }

        /// <summary>
        /// Activatables this instance.
        /// </summary>
        /// <returns></returns>
        private AsyncCommandBuilder Activatable()
        {
            isAutoActivate = false;
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
        private AsyncCommandBuilder ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute)
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
        /// Automatics the activate.
        /// </summary>
        /// <returns></returns>
        [NotNull]
        private AsyncCommandBuilder AutoActivate()
        {
            isAutoActivate = true;
            return this;
        }

        /// <summary>
        /// Observeses the command manager.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="CommandBuilderException"></exception>
        [NotNull]
        private AsyncCommandBuilder ObservesCommandManager()
        {
            if (observes.Contains(CommandManagerObserver.Observer))
            {
                throw new CommandBuilderException(Resources.ExceptionStrings.CanExecuteFunctionAlreadyDefined);
            }

            this.observes.Add(CommandManagerObserver.Observer);
            return this;
        }
    }
}