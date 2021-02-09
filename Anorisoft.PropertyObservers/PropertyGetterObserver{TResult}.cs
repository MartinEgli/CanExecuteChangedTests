using System;
using System.Linq.Expressions;
using Anorisoft.ExpressionObservers;
using JetBrains.Annotations;

namespace Anorisoft.PropertyObservers
{
    public sealed class PropertyGetterObserver<TResult> : PropertyObserverBase
        where TResult : struct
    {
        [NotNull] private readonly Action<TResult?> action;
        [NotNull] private readonly Func<TResult?> getter;

        internal PropertyGetterObserver(
            [NotNull] Expression<Func<TResult>> propertyExpression,
            [NotNull] Action<TResult?> action) 
        {
            this.action = action ?? throw new ArgumentNullException(nameof(action));
            propertyExpression = propertyExpression ?? throw new ArgumentNullException(nameof(propertyExpression));
            var tree = ExpressionTree.GetTree(propertyExpression.Body);
            ExpressionString = propertyExpression.ToString();

            base.CreateChain(tree);
            this.getter = ExpressionGetter.CreateValueGetter<TResult>(propertyExpression.Parameters, tree);
        }

        protected override void OnAction() => action(getter());


        public override string ExpressionString { get; }

        public TResult? GetValue() => getter();
    }


    public sealed class PropertyObserverAndGetter<TResult> : PropertyObserverBase
        where TResult : struct
    {
        [NotNull] private readonly Action action;
        [NotNull] private readonly Func<TResult?> getter;

        internal PropertyObserverAndGetter(
            [NotNull] Expression<Func<TResult>> propertyExpression,
            [NotNull] Action action)
        {
            this.action = action ?? throw new ArgumentNullException(nameof(action));
            propertyExpression = propertyExpression ?? throw new ArgumentNullException(nameof(propertyExpression));
            var tree = ExpressionTree.GetTree(propertyExpression.Body);
            ExpressionString = propertyExpression.ToString();

            base.CreateChain(tree);
            this.getter = ExpressionGetter.CreateValueGetter<TResult>(propertyExpression.Parameters, tree);
        }

        protected override void OnAction() => action();


        public override string ExpressionString { get; }

        public TResult? GetValue() => getter();
    }

    public sealed class PropertyObserverWithGetterAndFallback<TResult> : PropertyObserverBase
        where TResult : struct
    {
        [NotNull] private readonly Action action;
        [NotNull] private readonly Func<TResult> getter;

        internal PropertyObserverWithGetterAndFallback(
            [NotNull] Expression<Func<TResult>> propertyExpression,
            [NotNull] Action action, TResult fallback)
        {
            this.action = action ?? throw new ArgumentNullException(nameof(action));
            propertyExpression = propertyExpression ?? throw new ArgumentNullException(nameof(propertyExpression));
            var tree = ExpressionTree.GetTree(propertyExpression.Body);
            ExpressionString = propertyExpression.ToString();

            base.CreateChain(tree);
            this.getter = ExpressionGetter.CreateValueGetter<TResult>(propertyExpression.Parameters, tree, fallback);
        }

        protected override void OnAction() => action();


        public override string ExpressionString { get; }

        public TResult GetValue() => getter();
    }
}