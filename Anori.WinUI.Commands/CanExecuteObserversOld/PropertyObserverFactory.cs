// -----------------------------------------------------------------------
// <copyright file="PropertyObserverFactory.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace Anori.WinUI.Commands.CanExecuteObserversOld
{
    public class PropertyObserverFactory
    {
        /// <summary>
        ///     Observeses the property.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="propertyExpression">The property expression.</param>
        /// <returns></returns>
        public CanExecuteObserversOld.PropertyObserver<TType> ObservesProperty<TType>(Expression<Func<TType>> propertyExpression)
        {
            return CanExecuteObserversOld.PropertyObserver<TType>.Create(propertyExpression);
        }

        /// <summary>
        ///     Observeses the property.
        /// </summary>
        /// <typeparam name="TOwner">The type of the owner.</typeparam>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="owner">The owner.</param>
        /// <param name="propertyExpression">The property expression.</param>
        /// <returns></returns>
        public CanExecuteObserversOld.PropertyObserver<TOwner, TType> ObservesProperty<TOwner, TType>(
            TOwner owner,
            Expression<Func<TOwner, TType>> propertyExpression)
            where TOwner : INotifyPropertyChanged
        {
            return CanExecuteObserversOld.PropertyObserver<TOwner, TType>.Create(owner, propertyExpression);
        }

        /// <summary>
        ///     Observeses the can execute.
        /// </summary>
        /// <param name="canExecuteExpression">The can execute expression.</param>
        /// <returns></returns>
        public CanExecuteObserversOld.CanExecuteObserver ObservesCanExecute(Expression<Func<bool>> canExecuteExpression)
        {
            return CanExecuteObserversOld.CanExecuteObserver.Create(canExecuteExpression);
        }

        /// <summary>
        ///     Observeses the can execute.
        /// </summary>
        /// <typeparam name="TParameter">The type of the owner.</typeparam>
        /// <param name="parameter">The owner.</param>
        /// <param name="canExecuteExpression">The can execute expression.</param>
        /// <returns></returns>
        public CanExecuteObserversOld.CanExecuteObserver<TParameter> ObservesCanExecute<TParameter>(
            TParameter parameter,
            Expression<Func<TParameter, bool>> canExecuteExpression)
            where TParameter : INotifyPropertyChanged
        {
            return CanExecuteObserversOld.CanExecuteObserver<TParameter>.Create(parameter, canExecuteExpression);
        }
    }
}