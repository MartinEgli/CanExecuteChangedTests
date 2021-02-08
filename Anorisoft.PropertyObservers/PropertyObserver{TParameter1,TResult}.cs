using System;
using System.ComponentModel;
using System.Linq.Expressions;
using JetBrains.Annotations;

namespace Anorisoft.PropertyObservers
{
    public sealed class PropertyObserver<TParameter1, TResult> : PropertyObserverBase<TParameter1, TResult> where TParameter1 : INotifyPropertyChanged
        where TResult : struct
    {
        [NotNull] public Action Action { get; }

        internal PropertyObserver(
            [NotNull] TParameter1 parameter1,
            [NotNull] Expression<Func<TParameter1, TResult>> propertyExpression,
            [NotNull] Action action) : base(parameter1, propertyExpression)
        {
            Action = action ?? throw new ArgumentNullException(nameof(action));
        }

        protected override void OnAction() => Action();
    }
}