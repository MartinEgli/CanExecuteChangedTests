// -----------------------------------------------------------------------
// <copyright file="PropertyObserverNode.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Reflection;

namespace Anorisoft.PropertyObservers
{
    /// <summary>
    ///     Represents each node of nested properties expression and takes care of
    ///     subscribing/unsubscribing INotifyPropertyChanged.PropertyChanged listeners on it.
    /// </summary>
    internal class PropertyObserverNode

    {
        /// <summary>
        /// The action
        /// </summary>
        private readonly Action action;

        /// <summary>
        /// The notify property changed
        /// </summary>
        private INotifyPropertyChanged notifyPropertyChanged;

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyObserverNode"/> class.
        /// </summary>
        /// <param name="propertyInfo">The property information.</param>
        /// <param name="action">The action.</param>
        /// <exception cref="ArgumentNullException">propertyInfo</exception>
        public PropertyObserverNode(PropertyInfo propertyInfo, Action action)
        {
            PropertyInfo = propertyInfo ?? throw new ArgumentNullException(nameof(propertyInfo));
            this.action = () =>
            {
                action?.Invoke();
                if (Previous == null)
                {
                    return;
                }

                Previous.UnsubscribeListener();
                GenerateNextNode();
            };
        }

        /// <summary>
        /// Gets or sets the next.
        /// </summary>
        /// <value>
        /// The next.
        /// </value>
        public PropertyObserverNode Previous { get; set; }

        /// <summary>
        /// Gets the property information.
        /// </summary>
        /// <value>
        /// The property information.
        /// </value>
        public PropertyInfo PropertyInfo { get; }

        /// <summary>
        /// Subscribes the listener for.
        /// </summary>
        /// <param name="propertyChanged">The property changed.</param>
        public void SubscribeListenerFor(INotifyPropertyChanged propertyChanged)
        {
            notifyPropertyChanged = propertyChanged;
            notifyPropertyChanged.PropertyChanged += OnPropertyChanged;

            if (Previous != null)
            {
                GenerateNextNode();
            }
        }

        /// <summary>
        /// Unsubscribes the listener.
        /// </summary>
        public void UnsubscribeListener()
        {
            if (notifyPropertyChanged != null)
            {
                notifyPropertyChanged.PropertyChanged -= OnPropertyChanged;
            }

            Previous?.UnsubscribeListener();
        }

        /// <summary>
        /// Generates the next node.
        /// </summary>
        /// <exception cref="InvalidOperationException">Trying to subscribe PropertyChanged listener in object that "
        ///                     + $"owns '{this.Previous.PropertyInfo.Name}' property, but the object does not implements INotifyPropertyChanged.</exception>
        private void GenerateNextNode()
        {
            var nextProperty = PropertyInfo.GetValue(notifyPropertyChanged);
            if (nextProperty == null)
            {
                return;
            }

            if (!(nextProperty is INotifyPropertyChanged propertyChanged))
            {
                throw new InvalidOperationException(
                    "Trying to subscribe PropertyChanged listener in object that "
                    + $"owns '{Previous.PropertyInfo.Name}' property, but the object does not implements INotifyPropertyChanged.");
            }

            Previous.SubscribeListenerFor(propertyChanged);
        }

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e?.PropertyName == PropertyInfo.Name || string.IsNullOrEmpty(e?.PropertyName))
            {
                action?.Invoke();
            }
        }
    }
}