using System;
using System.Linq.Expressions;
using Anorisoft.ExpressionObservers;
using JetBrains.Annotations;

namespace Anorisoft.PropertyObservers
{
    public sealed class PropertyGetterObserver<TResult> : PropertyObserverBase<TResult>
        where TResult : struct
    {
        [NotNull] private readonly Action<TResult?> action;
        [NotNull] private readonly Func<TResult?> getter;

        internal PropertyGetterObserver(
            [NotNull] Expression<Func<TResult>> propertyExpression,
            [NotNull] Action<TResult?> action) : base(propertyExpression)
        {
            this.action = action ?? throw new ArgumentNullException(nameof(action));
            this.getter = ExpressionGetter.CreateValueGetter(propertyExpression);
        }

        protected override void OnAction() => action(getter());
    }
}