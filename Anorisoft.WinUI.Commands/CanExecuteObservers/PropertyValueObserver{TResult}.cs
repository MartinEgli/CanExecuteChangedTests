// -----------------------------------------------------------------------
// <copyright file="PropertyObserver{T}.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Anorisoft.PropertyObservers;
using Anorisoft.WinUI.Commands.Interfaces;
using Anorisoft.WinUI.Common;

namespace Anorisoft.WinUI.Commands.CanExecuteObservers
{
    using JetBrains.Annotations;
    using System;
    using System.Linq.Expressions;

    public sealed class PropertyValueObserver<TResult> : PropertyObserverBase<TResult>, IPropertyObserver
    where TResult : struct
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyValueObserver{TParameter}"/> class.
        /// </summary>
        /// <param name="propertyExpression">The property expression.</param>
        /// <exception cref="ArgumentNullException">propertyExpression</exception>
        private PropertyValueObserver([NotNull] Expression<Func<TResult>> propertyExpression)
        {
            if (propertyExpression == null)
            {
                throw new ArgumentNullException(nameof(propertyExpression));
            }

            this.Observer = PropertyValueObserver.Observes(propertyExpression, () => this.Update.Raise());
            this.PropertyExpression = Observer.ExpressionString;
        }

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
        public static PropertyValueObserver<TType> Create<TType>([NotNull] Expression<Func<TType>> propertyExpression)
            where TType : struct
        {
            if (propertyExpression == null) throw new ArgumentNullException(nameof(propertyExpression));
            var instance = new PropertyValueObserver<TType>(propertyExpression);
            instance.Subscribe();
            return instance;
        }
    }
}