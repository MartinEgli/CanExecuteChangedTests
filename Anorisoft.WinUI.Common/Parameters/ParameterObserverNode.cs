// -----------------------------------------------------------------------
// <copyright file="ParameterObserverNode.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anorisoft.WinUI.Common.Parameters
{
    using CanExecuteChangedTests;

    using System;
    using System.Reflection;

    /// <summary>
    ///     Represents each node of nested properties expression and takes care of
    ///     subscribing/unsubscribing INotifyPropertyChanged.PropertyChanged listeners on it.
    /// </summary>
    internal class ParameterObserverNode

    {
        /// <summary>
        ///     The action
        /// </summary>
        private readonly Action action;

        /// <summary>
        ///     The notify property changed
        /// </summary>
        private IReadOnlyParameter notifyPropertyChanged;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ParameterObserverNode" /> class.
        /// </summary>
        /// <param name="propertyInfo">The property information.</param>
        /// <param name="action">The action.</param>
        /// <exception cref="ArgumentNullException">propertyInfo</exception>
        public ParameterObserverNode(PropertyInfo propertyInfo, Action action)
        {
            this.PropertyInfo = propertyInfo ?? throw new ArgumentNullException(nameof(propertyInfo));
            this.action = () =>
                {
                    action?.Invoke();
                    if (this.Next == null)
                    {
                        return;
                    }

                    this.Next.UnsubscribeListener();
                    this.GenerateNextNode();
                };
        }

        /// <summary>
        ///     Gets or sets the next.
        /// </summary>
        /// <value>
        ///     The next.
        /// </value>
        public ParameterObserverNode Next { get; set; }

        /// <summary>
        ///     Gets the property information.
        /// </summary>
        /// <value>
        ///     The property information.
        /// </value>
        public PropertyInfo PropertyInfo { get; }

        /// <summary>
        ///     Subscribes the listener for.
        /// </summary>
        /// <param name="propertyChanged">The property changed.</param>
        public void SubscribeListenerFor(IReadOnlyParameter propertyChanged)
        {
            this.notifyPropertyChanged = propertyChanged;
            this.notifyPropertyChanged.ValueChanged += this.OnPropertyChanged;

            if (this.Next != null)
            {
                this.GenerateNextNode();
            }
        }

        /// <summary>
        ///     Unsubscribes the listener.
        /// </summary>
        public void UnsubscribeListener()
        {
            if (this.notifyPropertyChanged != null)
            {
                this.notifyPropertyChanged.ValueChanged -= this.OnPropertyChanged;
            }

            this.Next?.UnsubscribeListener();
        }

        /// <summary>
        ///     Generates the next node.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        ///     Trying to subscribe PropertyChanged listener in object that "
        ///     + $"owns '{this.Next.PropertyInfo.Name}' property, but the object does not implements INotifyPropertyChanged.
        /// </exception>
        private void GenerateNextNode()
        {
            var nextProperty = this.PropertyInfo.GetValue(this.notifyPropertyChanged.Value);
            if (nextProperty == null)
            {
                return;
            }

            if (!(nextProperty is IReadOnlyParameter propertyChanged))
            {
                throw new InvalidOperationException(
                    "Trying to subscribe PropertyChanged listener in object that "
                    + $"owns '{this.Next.PropertyInfo.Name}' property, but the object does not implements INotifyPropertyChanged.");
            }

            this.Next.SubscribeListenerFor(propertyChanged);
        }

        /// <summary>
        ///     Called when [property changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs{System.Object}" /> instance containing the event data.</param>
        private void OnPropertyChanged(object sender, EventArgs<object> e)
        {
            this.action?.Invoke();
        }
    }
}