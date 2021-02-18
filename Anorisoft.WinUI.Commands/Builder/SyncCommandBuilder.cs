using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Anorisoft.WinUI.Commands.CanExecuteObservers;
using Anorisoft.WinUI.Commands.Commands;
using Anorisoft.WinUI.Commands.Exeptions;
using Anorisoft.WinUI.Commands.Factory;
using Anorisoft.WinUI.Commands.Interfaces;
using JetBrains.Annotations;

namespace Anorisoft.WinUI.Commands.Builder
{
    public class SyncCommandBuilder :
        ISyncCommandBuilder,
        ISyncCanExecuteBuilder,
        IActivatableSyncCommandBuilder,
        IActivatableSyncCanExecuteBuilder
    {
        /// <summary>
        /// The execute
        /// </summary>
        private readonly Action execute;

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
        private Func<bool> canExecuteFunction;

        /// <summary>
        /// The is automatic actiate
        /// </summary>
        private bool isAutoActivate;

        /// <summary>
        /// Initializes a new instance of the <see cref="SyncCommandBuilder"/> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <exception cref="ArgumentNullException">execute</exception>
        public SyncCommandBuilder([NotNull] Action execute) =>
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));

        IActivatableSyncCommand IActivatableSyncCanExecuteBuilder.Build(Action<IActivatableSyncCommand> setCommand)
            => Build(setCommand);

        IActivatableSyncCanExecuteBuilder IActivatableSyncCanExecuteBuilder.ObservesProperty<TType>(
            Expression<Func<TType>> expression)
            => ObservesProperty(expression);

        IActivatableSyncCanExecuteBuilder IActivatableSyncCanExecuteBuilder.ObservesCommandManager()
            => ObservesCommandManager();

        IActivatableSyncCanExecuteBuilder IActivatableSyncCanExecuteBuilder.AutoActivate()
            => AutoActivate();

        IActivatableSyncCommand IActivatableSyncCanExecuteBuilder.Build()
            => Build();

        IActivatableSyncCommand IActivatableSyncCommandBuilder.Build(Action<IActivatableSyncCommand> setCommand)
            => Build(setCommand);

        IActivatableSyncCanExecuteBuilder IActivatableSyncCommandBuilder.CanExecute(Func<bool> canExecute)
            => CanExecute(canExecute);

        IActivatableSyncCanExecuteBuilder IActivatableSyncCommandBuilder.ObservesCanExecute(
            Expression<Func<bool>> canExecute)
            => ObservesCanExecute(canExecute);

        IActivatableSyncCanExecuteBuilder IActivatableSyncCommandBuilder.ObservesCanExecute(
            Expression<Func<bool>> canExecute, bool fallback)
            => ObservesCanExecute(canExecute, fallback);

        IActivatableSyncCommand IActivatableSyncCommandBuilder.Build()
            => Build();

        ISyncCanExecuteBuilder ISyncCanExecuteBuilder.ObservesCommandManager()
            => ObservesCommandManager();

        ISyncCanExecuteBuilder ISyncCanExecuteBuilder.ObservesProperty<TType>(Expression<Func<TType>> expression)
            => ObservesProperty(expression);

        ISyncCommand ISyncCanExecuteBuilder.Build(Action<ISyncCommand> setCommand)
            => Build(setCommand);

        IActivatableSyncCanExecuteBuilder ISyncCanExecuteBuilder.Activatable()
            => Activatable();

        ISyncCommand ISyncCanExecuteBuilder.Build()
            => Build();

        ISyncCanExecuteBuilder ISyncCommandBuilder.CanExecute(Func<bool> canExecute)
            => CanExecute(canExecute);

        ISyncCanExecuteBuilder ISyncCommandBuilder.ObservesCanExecute(Expression<Func<bool>> canExecute)
            => ObservesCanExecute(canExecute);

        ISyncCanExecuteBuilder ISyncCommandBuilder.ObservesCanExecute(Expression<Func<bool>> canExecute, bool fallback)
            => ObservesCanExecute(canExecute, fallback);

        ISyncCommand ISyncCommandBuilder.Build(Action<ISyncCommand> setCommand)
            => Build(setCommand);

        IActivatableSyncCanExecuteBuilder ISyncCommandBuilder.Activatable()
            => Activatable();

        ISyncCommand ISyncCommandBuilder.Build() 
            => Build();

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NoCanExecuteException"></exception>
        [NotNull]
        private ActivatableCanExecuteObserverCommand Build()
        {
            if (observes.Any())
            {
                if (canExecuteFunction != null)
                {
                    return new ActivatableCanExecuteObserverCommand(execute, isAutoActivate, canExecuteFunction,
                        observes.ToArray());
                }

                if (canExecuteExpression != null)
                {
                    return new ActivatableCanExecuteObserverCommand(execute, isAutoActivate, canExecuteExpression,
                        observes.ToArray());
                }

                throw new NoCanExecuteException();
            }

            if (canExecuteFunction != null)
            {
                return new ActivatableCanExecuteObserverCommand(execute, isAutoActivate, canExecuteFunction);
            }

            if (canExecuteExpression != null)
            {
                return new ActivatableCanExecuteObserverCommand(execute, isAutoActivate, canExecuteExpression);
            }

            return new ActivatableCanExecuteObserverCommand(execute, isAutoActivate);
        }

        /// <summary>
        /// Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns></returns>
        private IActivatableSyncCommand Build([NotNull] Action<IActivatableSyncCommand> setCommand)
        {
            if (setCommand == null) throw new ArgumentNullException(nameof(setCommand));
            var command = Build();
            setCommand(command);
            return command;
        }

        /// <summary>
        /// Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns></returns>
        private ISyncCommand Build(Action<ISyncCommand> setCommand)
        {
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
        [NotNull]
        private SyncCommandBuilder ObservesProperty<TType>([NotNull] Expression<Func<TType>> expression)
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
        /// <exception cref="CommandFactoryException">
        /// </exception>
        /// <exception cref="ArgumentNullException">canExecute</exception>
        [NotNull]
        public SyncCommandBuilder CanExecute([NotNull] Func<bool> canExecute)
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
        /// Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">canExecute</exception>
        /// <exception cref="CommandFactoryException">
        /// </exception>
        [NotNull]
        private SyncCommandBuilder ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute)
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
        /// <exception cref="ArgumentNullException">canExecute</exception>
        /// <exception cref="CommandFactoryException">
        /// </exception>
        [NotNull]
        private SyncCommandBuilder ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute, bool fallback)
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
        /// <exception cref="CommandFactoryException"></exception>
        [NotNull]
        private SyncCommandBuilder ObservesCommandManager()
        {
            if (observes.Contains(CommandManagerObserver.Observer))
            {
                throw new CommandFactoryException(Resources.ExceptionStrings.CanExecuteFunctionAlreadyDefined);
            }

            this.observes.Add(CommandManagerObserver.Observer);
            return this;
        }

        /// <summary>
        /// Activatables this instance.
        /// </summary>
        /// <returns></returns>
        [NotNull]
        private SyncCommandBuilder Activatable() => this;

        /// <summary>
        /// Automatics the activate.
        /// </summary>
        /// <returns></returns>
        [NotNull]
        private SyncCommandBuilder AutoActivate()
        {
            isAutoActivate = true;
            return this;
        }
    }
}