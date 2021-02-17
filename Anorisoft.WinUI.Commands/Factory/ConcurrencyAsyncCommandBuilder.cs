using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Anorisoft.WinUI.Commands.CanExecuteObservers;
using Anorisoft.WinUI.Commands.Exeptions;
using Anorisoft.WinUI.Commands.Interfaces;
using JetBrains.Annotations;

namespace Anorisoft.WinUI.Commands.Factory
{
    public class ConcurrencyAsyncCommandBuilder : IConcurrencyAsyncCommandBuilder, IConcurrencyAsyncCanExecuteBuilder
    {
        /// <summary>
        /// The execute
        /// </summary>
        private readonly Func<CancellationToken, Task> execute;

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
        private bool isAutoActiate = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="SyncCommandBuilder"/> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <exception cref="ArgumentNullException">execute</exception>
        public ConcurrencyAsyncCommandBuilder([NotNull] Func<CancellationToken, Task> execute) =>
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        [NotNull]
        public IConcurrencyAsyncCommand Build()
        {
            if (observes.Any())
            {
                if (canExecuteFunction != null)
                {
                    return new ActivatableConcurrencyAsyncCanExecuteObserverCommand(execute, isAutoActiate, canExecuteFunction,
                        observes.ToArray());
                }

                if (canExecuteExpression != null)
                {
                    return new ActivatableConcurrencyAsyncCanExecuteObserverCommand(execute, isAutoActiate, canExecuteExpression,
                        observes.ToArray());
                }

                throw new NoCanExecuteException();
            }

            if (canExecuteFunction != null)
            {
                return new ActivatableConcurrencyAsyncCanExecuteObserverCommand(execute, isAutoActiate, canExecuteFunction);
            }

            if (canExecuteExpression != null)
            {
                return new ActivatableConcurrencyAsyncCanExecuteObserverCommand(execute, isAutoActiate, canExecuteExpression);
            }

            return new ActivatableConcurrencyAsyncCanExecuteObserverCommand(execute, isAutoActiate);
        }

        /// <summary>
        /// Observeses the property.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        [NotNull]
        public IConcurrencyAsyncCanExecuteBuilder ObservesProperty<TType>([NotNull] Expression<Func<TType>> expression)
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
        [NotNull]
        public IConcurrencyAsyncCanExecuteBuilder CanExecute([NotNull] Func<bool> canExecute)
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
        /// <exception cref="CommandFactoryException">
        /// </exception>
        [NotNull]
        public IConcurrencyAsyncCanExecuteBuilder ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute)
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
        public IConcurrencyAsyncCanExecuteBuilder ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute, bool fallback)
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
        public IConcurrencyAsyncCanExecuteBuilder ObservesCommandManager()
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
        public IConcurrencyAsyncCanExecuteBuilder AutoActivate()
        {
            isAutoActiate = true;
            return this;
        }
    }
}