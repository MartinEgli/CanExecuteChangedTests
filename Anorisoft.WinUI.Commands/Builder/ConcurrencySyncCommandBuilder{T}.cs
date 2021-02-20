using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using Anorisoft.WinUI.Commands.CanExecuteObservers;
using Anorisoft.WinUI.Commands.Commands;
using Anorisoft.WinUI.Commands.Exceptions;
using Anorisoft.WinUI.Commands.Interfaces;
using Anorisoft.WinUI.Commands.Interfaces.Builders;
using JetBrains.Annotations;

namespace Anorisoft.WinUI.Commands.Builder
{
    public sealed class ConcurrencySyncCommandBuilder<T> :
        IConcurrencySyncCommandBuilder<T>,
        IConcurrencySyncCanExecuteBuilder<T>,
        IActivatableConcurrencySyncCommandBuilder<T>,
        IActivatableConcurrencySyncCanExecuteBuilder<T>
    {
        /// <summary>
        /// The execute
        /// </summary>
        private readonly Action<T, CancellationToken> execute;

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
        private bool isAutoActivate = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="SyncCommandBuilder" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <exception cref="ArgumentNullException">execute</exception>
        public ConcurrencySyncCommandBuilder([NotNull] Action<T, CancellationToken> execute) =>
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));

        /// <summary>
        /// Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns></returns>
        ActivatableConcurrencyCanExecuteObserverCommand<T> IActivatableConcurrencySyncCanExecuteBuilder<T>.Build(
            Action<ActivatableConcurrencyCanExecuteObserverCommand<T>> setCommand) => Build(setCommand);

        /// <summary>
        /// Observeses the property.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        IActivatableConcurrencySyncCanExecuteBuilder<T> IActivatableConcurrencySyncCanExecuteBuilder<T>.
            ObservesProperty<TType>(Expression<Func<TType>> expression) => ObservesProperty(expression);

        /// <summary>
        /// Observeses the specified observer.
        /// </summary>
        /// <param name="observer">The observer.</param>
        /// <returns></returns>
        IActivatableConcurrencySyncCanExecuteBuilder<T> IActivatableConcurrencySyncCanExecuteBuilder<T>.Observes(
            ICanExecuteChangedSubject observer)
        {
            observes.Add(observer);
            return this;
        }

        /// <summary>
        /// Observeses the command manager.
        /// </summary>
        /// <returns></returns>
        IActivatableConcurrencySyncCanExecuteBuilder<T> IActivatableConcurrencySyncCanExecuteBuilder<T>.
            ObservesCommandManager() => ObservesCommandManager();

        /// <summary>
        /// Automatics the activate.
        /// </summary>
        /// <returns></returns>
        IActivatableConcurrencySyncCanExecuteBuilder<T> IActivatableConcurrencySyncCanExecuteBuilder<T>.
            AutoActivate() => AutoActivate();

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        ActivatableConcurrencyCanExecuteObserverCommand<T> IActivatableConcurrencySyncCanExecuteBuilder<T>.Build() =>
            BuildActivatable();

        /// <summary>
        /// Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns></returns>
        ActivatableConcurrencyCanExecuteObserverCommand<T> IActivatableConcurrencySyncCommandBuilder<T>.Build(
            Action<ActivatableConcurrencyCanExecuteObserverCommand<T>> setCommand) => Build(setCommand);

        /// <summary>
        /// Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        IActivatableConcurrencySyncCanExecuteBuilder<T> IActivatableConcurrencySyncCommandBuilder<T>.
            CanExecute(Predicate<T> canExecute) => CanExecute(canExecute);

        IActivatableConcurrencySyncCanExecuteBuilder<T> IActivatableConcurrencySyncCommandBuilder<T>.CanExecute(ICanExecuteSubject canExecute)
        {
            this.canExecuteSubject = canExecute;
            return this;
        }

        IConcurrencySyncCanExecuteBuilder<T> IConcurrencySyncCommandBuilder<T>.CanExecute(ICanExecuteSubject canExecute)
        {
            this.canExecuteSubject = canExecute;
            return this;
        }

        /// <summary>
        /// Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        IActivatableConcurrencySyncCanExecuteBuilder<T> IActivatableConcurrencySyncCommandBuilder<T>.
            ObservesCanExecute(Expression<Func<bool>> canExecute) => ObservesCanExecute(canExecute);

        /// <summary>
        /// Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <param name="fallback">if set to <c>true</c> [fallback].</param>
        /// <returns></returns>
        IActivatableConcurrencySyncCanExecuteBuilder<T> IActivatableConcurrencySyncCommandBuilder<T>.ObservesCanExecute(
            Expression<Func<bool>> canExecute, bool fallback) => ObservesCanExecute(canExecute, fallback);

        /// <summary>
        /// Automatics the activate.
        /// </summary>
        /// <returns></returns>
        IActivatableConcurrencySyncCanExecuteBuilder<T> IActivatableConcurrencySyncCommandBuilder<T>.AutoActivate() =>
            AutoActivate();

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        ActivatableConcurrencyCanExecuteObserverCommand<T> IActivatableConcurrencySyncCommandBuilder<T>.Build() =>
            BuildActivatable();

        /// <summary>
        /// Observeses the specified observer.
        /// </summary>
        /// <param name="observer">The observer.</param>
        /// <returns></returns>
        IConcurrencySyncCanExecuteBuilder<T> IConcurrencySyncCanExecuteBuilder<T>.Observes(
            ICanExecuteChangedSubject observer)
        {
            observes.Add(observer);
            return this;
        }

        /// <summary>
        /// Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns></returns>
        ConcurrencyCanExecuteObserverCommand<T> IConcurrencySyncCanExecuteBuilder<T>.Build(
            Action<ConcurrencyCanExecuteObserverCommand<T>> setCommand) => Build(setCommand);

        /// <summary>
        /// Activatables this instance.
        /// </summary>
        /// <returns></returns>
        IActivatableConcurrencySyncCanExecuteBuilder<T> IConcurrencySyncCanExecuteBuilder<T>.Activatable() =>
            Activatable();

        /// <summary>
        /// Observeses the property.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        IConcurrencySyncCanExecuteBuilder<T> IConcurrencySyncCanExecuteBuilder<T>.ObservesProperty<TType>(
            Expression<Func<TType>> expression) => ObservesProperty(expression);

        /// <summary>
        /// Observeses the command manager.
        /// </summary>
        /// <returns></returns>
        IConcurrencySyncCanExecuteBuilder<T> IConcurrencySyncCanExecuteBuilder<T>.ObservesCommandManager() =>
            ObservesCommandManager();

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        ConcurrencyCanExecuteObserverCommand<T> IConcurrencySyncCanExecuteBuilder<T>.Build() => Build();

        /// <summary>
        /// Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns></returns>
        ConcurrencyCanExecuteObserverCommand<T> IConcurrencySyncCommandBuilder<T>.Build(
            Action<ConcurrencyCanExecuteObserverCommand<T>> setCommand) => Build(setCommand);

        /// <summary>
        /// Activateables this instance.
        /// </summary>
        /// <returns></returns>
        IActivatableConcurrencySyncCommandBuilder<T> IConcurrencySyncCommandBuilder<T>.Activatable() =>
            Activatable();

        /// <summary>
        /// Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        IConcurrencySyncCanExecuteBuilder<T> IConcurrencySyncCommandBuilder<T>.CanExecute(Predicate<T> canExecute) =>
            CanExecute(canExecute);

        /// <summary>
        /// Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        IConcurrencySyncCanExecuteBuilder<T> IConcurrencySyncCommandBuilder<T>.ObservesCanExecute(
            Expression<Func<bool>> canExecute) => ObservesCanExecute(canExecute);

        /// <summary>
        /// Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <param name="fallback">if set to <c>true</c> [fallback].</param>
        /// <returns></returns>
        IConcurrencySyncCanExecuteBuilder<T> IConcurrencySyncCommandBuilder<T>.ObservesCanExecute(
            Expression<Func<bool>> canExecute, bool fallback) => ObservesCanExecute(canExecute, fallback);

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        ConcurrencyCanExecuteObserverCommand<T> IConcurrencySyncCommandBuilder<T>.Build() => Build();

        /// <summary>
        /// Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        /// <exception cref="CommandBuilderException">
        /// </exception>
        [NotNull]
        public ConcurrencySyncCommandBuilder<T> CanExecute([NotNull] Predicate<T> canExecute)
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
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NoCanExecuteException"></exception>
        [NotNull]
        private ActivatableConcurrencyCanExecuteObserverCommand<T> BuildActivatable()
        {
            if (observes.Any())
            {
                if (canExecuteFunction != null)
                {
                    return new ActivatableConcurrencyCanExecuteObserverCommand<T>(execute, isAutoActivate,
                        canExecuteFunction,
                        observes.ToArray());
                }

                if (canExecuteSubject != null)
                {
                    return new ActivatableConcurrencyCanExecuteObserverCommand<T>(execute, isAutoActivate,
                        canExecuteSubject,
                        observes.ToArray());
                }

                throw new NoCanExecuteException();
            }

            if (canExecuteFunction != null)
            {
                return new ActivatableConcurrencyCanExecuteObserverCommand<T>(execute, isAutoActivate,
                    canExecuteFunction);
            }

            if (canExecuteSubject != null)
            {
                return new ActivatableConcurrencyCanExecuteObserverCommand<T>(execute, isAutoActivate,
                    canExecuteSubject);
            }

            return new ActivatableConcurrencyCanExecuteObserverCommand<T>(execute, isAutoActivate);
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NoCanExecuteException"></exception>
        [NotNull]
        private ConcurrencyCanExecuteObserverCommand<T> Build()
        {
            if (observes.Any())
            {
                if (canExecuteFunction != null)
                {
                    return new ConcurrencyCanExecuteObserverCommand<T>(execute,
                        canExecuteFunction,
                        observes.ToArray());
                }

                if (canExecuteSubject != null)
                {
                    return new ConcurrencyCanExecuteObserverCommand<T>(execute,
                        canExecuteSubject,
                        observes.ToArray());
                }

                throw new NoCanExecuteException();
            }

            if (canExecuteFunction != null)
            {
                return new ConcurrencyCanExecuteObserverCommand<T>(execute,
                    canExecuteFunction);
            }

            if (canExecuteSubject != null)
            {
                return new ConcurrencyCanExecuteObserverCommand<T>(execute,
                    canExecuteSubject);
            }

            return new ConcurrencyCanExecuteObserverCommand<T>(execute);
        }

        /// <summary>
        /// Observeses the property.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        [NotNull]
        private ConcurrencySyncCommandBuilder<T> ObservesProperty<TType>([NotNull] Expression<Func<TType>> expression)
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
        private ConcurrencySyncCommandBuilder<T> ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute)
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
        private ConcurrencySyncCommandBuilder<T> ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute,
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
        private ConcurrencySyncCommandBuilder<T> ObservesCommandManager()
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
        private ConcurrencySyncCommandBuilder<T> AutoActivate()
        {
            isAutoActivate = true;
            return this;
        }

        /// <summary>
        /// Activatables this instance.
        /// </summary>
        /// <returns></returns>
        [NotNull]
        private ConcurrencySyncCommandBuilder<T> Activatable()
        {
            isAutoActivate = false;
            return this;
        }

        /// <summary>
        /// Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">setCommand</exception>
        private ActivatableConcurrencyCanExecuteObserverCommand<T> Build(
            [NotNull] Action<ActivatableConcurrencyCanExecuteObserverCommand<T>> setCommand)
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
        private ConcurrencyCanExecuteObserverCommand<T> Build(
            [NotNull] Action<ConcurrencyCanExecuteObserverCommand<T>> setCommand)
        {
            if (setCommand == null) throw new ArgumentNullException(nameof(setCommand));
            var command = Build();
            setCommand(command);
            return command;
        }
    }
}