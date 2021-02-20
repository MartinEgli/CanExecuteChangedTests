﻿using System;
using System.Linq.Expressions;
using Anorisoft.WinUI.Commands.Commands;
using JetBrains.Annotations;

namespace Anorisoft.WinUI.Commands.Interfaces.Builders
{
    public interface IActivatableConcurrencySyncCommandBuilder<T>
    {
        [NotNull]
        ActivatableConcurrencyCanExecuteObserverCommand<T> Build();

        [NotNull]
        ActivatableConcurrencyCanExecuteObserverCommand<T> Build([NotNull] Action<ActivatableConcurrencyCanExecuteObserverCommand<T>> setCommand);

        [NotNull]
        IActivatableConcurrencySyncCanExecuteBuilder<T> CanExecute([NotNull] Predicate<T> canExecute);

        [NotNull]
        IActivatableConcurrencySyncCanExecuteBuilder<T> ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute);

        [NotNull]
        IActivatableConcurrencySyncCanExecuteBuilder<T> ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute, bool fallback);
        [NotNull]
        IActivatableConcurrencySyncCanExecuteBuilder<T> AutoActivate();
    }
}