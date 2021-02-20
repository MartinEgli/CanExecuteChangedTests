﻿using System;
using System.Linq.Expressions;
using Anorisoft.WinUI.Commands.Interfaces.Builders;
using JetBrains.Annotations;

namespace Anorisoft.WinUI.Commands.Interfaces
{
    public interface IConcurrencyAsyncCommandBuilder<T>
    {
        [NotNull]
        IConcurrencyAsyncCommand<T> Build();

        [NotNull]
        IConcurrencyAsyncCommand<T> Build([NotNull] Action<IConcurrencyAsyncCommand<T>> setCommand);

        [NotNull]
        IConcurrencyAsyncCanExecuteBuilder<T> CanExecute([NotNull] Predicate<T> canExecute);

        [NotNull]
        IConcurrencyAsyncCanExecuteBuilder<T> ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute);

        [NotNull]
        IConcurrencyAsyncCanExecuteBuilder<T> ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute, bool fallback);

        [NotNull]
        IActivatableConcurrencyAsyncCanExecuteBuilder<T> Activatable();
    }
}