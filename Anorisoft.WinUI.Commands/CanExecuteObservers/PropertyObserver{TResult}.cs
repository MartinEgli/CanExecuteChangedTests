﻿// -----------------------------------------------------------------------
// <copyright file="PropertyObserver{T}.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Anorisoft.PropertyObservers;
using Anorisoft.WinUI.Commands.Interfaces;
using Anorisoft.WinUI.Common;

namespace Anorisoft.WinUI.Commands.CanExecuteObservers
{
    using System;
    using System.ComponentModel;
    using System.Linq.Expressions;

   

    using JetBrains.Annotations;

    public sealed class PropertyObserver<T> : PropertyObserverBase<T>, IPropertyObserver
    where T : struct
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="PropertyObserver{TOwner,T}" /> class.
        /// </summary>
        /// <param name="propertyExpression">The property expression.</param>
        private PropertyObserver([NotNull] Expression<Func<T>> propertyExpression)
        {
            if (propertyExpression == null)
            {
                throw new ArgumentNullException(nameof(propertyExpression));
            }

            this.Observer = PropertyObserver.Observes(propertyExpression, () => this.Update.Raise());
            this.PropertyExpression = Observer.ExpressionString;
        }

        /// <summary>
        ///     Gets the owner.
        /// </summary>
        /// <value>
        ///     The owner.
        /// </value>
  //      public INotifyPropertyChanged Owner => this.Observer.Owner;

        /// <summary>
        ///     Occurs when [can execute changed].
        /// </summary>
        protected override event Action Update;

        /// <summary>
        ///     Creates the specified property expression.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="propertyExpression">The property expression.</param>
        /// <returns></returns>
        [NotNull]
        public static PropertyObserver<TType> Create<TType>([NotNull] Expression<Func<TType>> propertyExpression) 
            where TType : struct
        {
            var instance = new PropertyObserver<TType>(propertyExpression);
            instance.Subscribe();
            return instance;
        }
    }
}