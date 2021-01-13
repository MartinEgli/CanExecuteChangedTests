// -----------------------------------------------------------------------
// <copyright file="PropertyObserverFactory.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anorisoft.WinUI.Commands.CanExecuteObservers
{
    using System;
    using System.ComponentModel;
    using System.Linq.Expressions;

    public class PropertyObserverFactory
    {
        /// <summary>
        ///     Observeses the property.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="propertyExpression">The property expression.</param>
        /// <returns></returns>
        public PropertyObserver<TType> ObservesProperty<TType>(Expression<Func<TType>> propertyExpression)
        {
            return PropertyObserver<TType>.Create(propertyExpression);
        }

        /// <summary>
        ///     Observeses the property.
        /// </summary>
        /// <typeparam name="TOwner">The type of the owner.</typeparam>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="owner">The owner.</param>
        /// <param name="propertyExpression">The property expression.</param>
        /// <returns></returns>
        public PropertyObserver<TOwner, TType> ObservesProperty<TOwner, TType>(
            TOwner owner,
            Expression<Func<TOwner, TType>> propertyExpression)
            where TOwner : INotifyPropertyChanged
        {
            return PropertyObserver<TOwner, TType>.Create(owner, propertyExpression);
        }

        /// <summary>
        ///     Observeses the can execute.
        /// </summary>
        /// <param name="canExecuteExpression">The can execute expression.</param>
        /// <returns></returns>
        public CanExecuteObserver ObservesCanExecute(Expression<Func<bool>> canExecuteExpression)
        {
            return CanExecuteObserver.Create(canExecuteExpression);
        }

        /// <summary>
        ///     Observeses the can execute.
        /// </summary>
        /// <typeparam name="TOwner">The type of the owner.</typeparam>
        /// <param name="owner">The owner.</param>
        /// <param name="canExecuteExpression">The can execute expression.</param>
        /// <returns></returns>
        public CanExecuteObserver<TOwner> ObservesCanExecute<TOwner>(
            TOwner owner,
            Expression<Func<TOwner, bool>> canExecuteExpression)
            where TOwner : INotifyPropertyChanged
        {
            return CanExecuteObserver<TOwner>.Create(owner, canExecuteExpression);
        }
    }
}