// -----------------------------------------------------------------------
// <copyright file="PropertyObserver{TOwner,T}.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Anorisoft.PropertyObservers;
using Anorisoft.PropertyObservers.ReferenceObservers;

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
    /// <typeparam name="TParameter">The type of the parameter.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <seealso cref="Anorisoft.WinUI.Commands.CanExecuteObservers.PropertyObserverBase{TResult}" />
    /// <seealso cref="Anorisoft.WinUI.Commands.Interfaces.IPropertyObserver" />
    public sealed class PropertyReferenceObserver<TParameter, TResult> : PropertyObserverBase<TResult>,
                                                      IPropertyObserver
        where TParameter : INotifyPropertyChanged
    where TResult : class
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="PropertyValueObserver{TParameter,TResult}" /> class.
        /// </summary>
        /// <param name="parameter">The owner.</param>
        /// <param name="propertyExpression">The property expression.</param>
        public PropertyReferenceObserver(TParameter parameter, Expression<Func<TParameter, TResult>> propertyExpression)
        {
            if (parameter == null)
            {
                throw new ArgumentNullException(nameof(parameter));
            }

            if (propertyExpression == null)
            {
                throw new ArgumentNullException(nameof(propertyExpression));
            }

            this.Parameter = parameter;
            this.Observer = PropertyReferenceObserver.Observes(parameter, propertyExpression, () => this.Update.Raise());
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
        public TParameter Parameter { get; }

        /// <summary>
        ///     Creates the specified owner.
        /// </summary>
        /// <typeparam name="TParameter">The type of the owner.</typeparam>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="parameter">The owner.</param>
        /// <param name="propertyExpression">The property expression.</param>
        /// <returns></returns>
        public static PropertyReferenceObserver<TParameter, TResult> Create(
            [NotNull] TParameter parameter,
            [NotNull] Expression<Func<TParameter, TResult>> propertyExpression)
        {
            var instance = new PropertyReferenceObserver<TParameter, TResult>(parameter, propertyExpression);
            instance.Subscribe();
            return instance;
        }
    }
}