// -----------------------------------------------------------------------
// <copyright file="PropertyObserver{TOwner,T}.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anorisoft.WinUI.Commands.CanExecuteObservers
{
    using System;
    using System.ComponentModel;
    using System.Linq.Expressions;

    using Anorisoft.WinUI.Commands.Interfaces;
    using Anorisoft.WinUI.Common;
    using Anorisoft.WinUI.Common.NotifyPropertyChangedObservers;

    using JetBrains.Annotations;

    public sealed class PropertyObserver<TOwner, T> : PropertyObserverBase<PropertyObserver<TOwner, T>>,
                                                      IPropertyObserver
        where TOwner : INotifyPropertyChanged
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="PropertyObserver{TOwner, T}" /> class.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="propertyExpression">The property expression.</param>
        public PropertyObserver(TOwner owner, Expression<Func<TOwner, T>> propertyExpression)
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
            this.Observer = PropertyObserver.Observes(owner, propertyExpression, () => this.Update.Raise(), false);
        }

        public TOwner Owner { get; }

        /// <summary>
        ///     Creates the specified owner.
        /// </summary>
        /// <typeparam name="TOwner">The type of the owner.</typeparam>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="owner">The owner.</param>
        /// <param name="propertyExpression">The property expression.</param>
        /// <returns></returns>
        public static PropertyObserver<TOwner, TType> Create<TType>(
            [NotNull] TOwner owner,
            [NotNull] Expression<Func<TOwner, TType>> propertyExpression)
        {
            var instance = new PropertyObserver<TOwner, TType>(owner, propertyExpression);
            instance.Subscribe();
            return instance;
        }

        /// <summary>
        ///     Occurs when [can execute changed].
        /// </summary>
        protected override event Action Update;
    }
}