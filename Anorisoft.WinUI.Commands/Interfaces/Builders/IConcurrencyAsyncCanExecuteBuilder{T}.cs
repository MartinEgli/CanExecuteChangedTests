﻿using System;
using System.Linq.Expressions;
using Anorisoft.WinUI.Commands.Commands;
using JetBrains.Annotations;

namespace Anorisoft.WinUI.Commands.Interfaces.Builders
{
    public interface IConcurrencyAsyncCanExecuteBuilder<T>
    {
        [NotNull]
        ConcurrencyAsyncCanExecuteObserverCommand<T> Build();

        [NotNull]
        ConcurrencyAsyncCanExecuteObserverCommand<T> Build([NotNull] Action<ConcurrencyAsyncCanExecuteObserverCommand<T>> setCommand);

        [NotNull]
        IConcurrencyAsyncCanExecuteBuilder<T> ObservesProperty<TType>([NotNull] Expression<Func<TType>> expression);

        [NotNull]
        IConcurrencyAsyncCanExecuteBuilder<T> ObservesCommandManager();

        [NotNull]
        IConcurrencyAsyncCanExecuteBuilder<T> AutoActivate();
    }
}