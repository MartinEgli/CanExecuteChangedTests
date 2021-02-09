// -----------------------------------------------------------------------
// <copyright file="PropertyObserver{TOwner,T}.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Anorisoft.PropertyObservers;

namespace Anorisoft.WinUI.Commands.CanExecuteObservers
{
    using System;
    using System.ComponentModel;
    using System.Linq.Expressions;

    using Anorisoft.WinUI.Commands.Interfaces;
    using Anorisoft.WinUI.Common;

    using JetBrains.Annotations;

    public sealed class PropertyObserver<TParameter,TResult> : PropertyObserverBase< TResult>,
                                                      IPropertyObserver
        where TParameter : INotifyPropertyChanged
    where TResult : struct
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="PropertyObserver{TOwner, T}" /> class.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="propertyExpression">The property expression.</param>
        public PropertyObserver(TParameter owner, Expression<Func<TParameter, TResult>> propertyExpression)
        {
            if (owner == null)
            {
                throw new ArgumentNullException(nameof(owner));
            }

            if (propertyExpression == null)
            {
                throw new ArgumentNullException(nameof(propertyExpression));
            }

            this.Owner = owner;
            this.Observer = PropertyObserver.Observes(owner, propertyExpression, () => this.Update.Raise());
            this.PropertyExpression = Observer.ExpressionString;
        }

        public TParameter Owner { get; }

        /// <summary>
        ///     Creates the specified owner.
        /// </summary>
        /// <typeparam name="TParameter">The type of the owner.</typeparam>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="owner">The owner.</param>
        /// <param name="propertyExpression">The property expression.</param>
        /// <returns></returns>
        public static PropertyObserver<TParameter, TResult> Create(
            [NotNull] TParameter owner,
            [NotNull] Expression<Func<TParameter, TResult>> propertyExpression)
        {
            var instance = new PropertyObserver<TParameter, TResult>(owner, propertyExpression);
            instance.Subscribe();
            return instance;
        }

        /// <summary>
        ///     Occurs when [can execute changed].
        /// </summary>
        protected override event Action Update;
    }
}