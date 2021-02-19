using System;
using System.Collections.Generic;
using Anorisoft.WinUI.Commands.Interfaces;
using Anorisoft.WinUI.Commands.Resources;

namespace Anorisoft.WinUI.Commands.Commands
{
    public static class ObservableCollectionExtensions
    {

        public static void AddIfNotContains(this IList<ICanExecuteChangedSubjectBase> observers, IEnumerable<ICanExecuteChangedSubject> newItems)
        {
            foreach (var propertyObserver in newItems)
            {
                if (propertyObserver == null)
                {
                    throw new ArgumentNullException(nameof(observers), "Observable item.");
                }

                if (observers.Contains(propertyObserver))
                {
                    throw new ArgumentException(
                        string.Format(ExceptionStrings.ObserverIsAlreadyBeingObserved, propertyObserver),
                        nameof(propertyObserver));
                }

                observers.Add(propertyObserver);
            }
        }
    }
}