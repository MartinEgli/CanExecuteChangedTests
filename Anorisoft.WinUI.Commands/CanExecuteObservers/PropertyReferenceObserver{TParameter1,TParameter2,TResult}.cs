// -----------------------------------------------------------------------
// <copyright file="PropertyObserver{TOwner,T}.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Anorisoft.PropertyObservers;

namespace Anorisoft.WinUI.Commands.CanExecuteObservers
{
    using Anorisoft.WinUI.Commands.Interfaces;
    using Anorisoft.WinUI.Common;
    using JetBrains.Annotations;
    using System;
    using System.ComponentModel;
    using System.Linq.Expressions;

    /// <summary>
    /// PropertyObserver
    /// </summary>
    /// <typeparam name="TParameter1">The type of the parameter.</typeparam>
    /// <typeparam name="TParameter2">The type of the parameter2.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <seealso cref="Anorisoft.WinUI.Commands.CanExecuteObservers.PropertyObserverBase{TResult}" />
    /// <seealso cref="Anorisoft.WinUI.Commands.Interfaces.IPropertyObserver" />
    public sealed class PropertyReferenceObserver<TParameter1, TParameter2, TResult> : PropertyObserverBase<TResult>,
                                                      IPropertyObserver
        where TParameter1 : INotifyPropertyChanged
        where TParameter2 : INotifyPropertyChanged
        where TResult : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyReferenceObserver{TParameter1, TParameter2, TResult}"/> class.
        /// </summary>
        /// <param name="parameter1">The parameter.</param>
        /// <param name="propertyExpression">The property expression.</param>
        /// <exception cref="ArgumentNullException">
        /// parameter
        /// or
        /// propertyExpression
        /// </exception>
        public PropertyReferenceObserver([NotNull] TParameter1 parameter1, [NotNull] TParameter2 parameter2, [NotNull] Expression<Func<TParameter1, TParameter2, TResult>> propertyExpression)
        {
            if (propertyExpression == null)
            {
                throw new ArgumentNullException(nameof(propertyExpression));
            }

            this.Parameter1 = parameter1 ?? throw new ArgumentNullException(nameof(parameter1));
            Parameter2 = parameter2 ?? throw new ArgumentNullException(nameof(parameter2));
            this.Observer = PropertyReferenceObserver.Observes(parameter1, parameter2, propertyExpression, () => this.Update.Raise());
            this.PropertyExpression = Observer.ExpressionString;
        }

        /// <summary>
        ///     Occurs when [can execute changed].
        /// </summary>
        protected override event Action Update;

        /// <summary>
        /// Gets the parameter.
        /// </summary>
        /// <value>
        /// The parameter.
        /// </value>
        [NotNull] public TParameter1 Parameter1 { get; }

        /// <summary>
        /// Gets the parameter2.
        /// </summary>
        /// <value>
        /// The parameter2.
        /// </value>
        [NotNull] public TParameter2 Parameter2 { get; }

        /// <summary>
        ///     Creates the specified owner.
        /// </summary>
        /// <typeparam name="TParameter1">The type of the owner.</typeparam>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="parameter">The owner.</param>
        /// <param name="propertyExpression">The property expression.</param>
        /// <returns></returns>
        public static PropertyReferenceObserver<TParameter1, TParameter2, TResult> Create(
            [NotNull] TParameter1 parameter1,
            [NotNull] TParameter2 parameter2,
            [NotNull] Expression<Func<TParameter1, TParameter2, TResult>> propertyExpression)
        {
            var instance = new PropertyReferenceObserver<TParameter1, TParameter2, TResult>(parameter1, parameter2, propertyExpression);
            instance.Subscribe();
            return instance;
        }
    }
}