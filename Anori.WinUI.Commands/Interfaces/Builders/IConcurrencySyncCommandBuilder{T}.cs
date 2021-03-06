﻿using System;
using System.Linq.Expressions;
using Anori.WinUI.Commands.Commands;
using JetBrains.Annotations;

namespace Anori.WinUI.Commands.Interfaces.Builders
{
    public interface IConcurrencySyncCommandBuilder<T>
    {
        [NotNull]
        ConcurrencyCanExecuteObserverCommand<T> Build();

        [NotNull]
        ConcurrencyCanExecuteObserverCommand<T> Build(
            [NotNull] Action<ConcurrencyCanExecuteObserverCommand<T>> setCommand);

        [NotNull]
        IConcurrencySyncCanExecuteBuilder<T> CanExecute([NotNull] Predicate<T> canExecute);

        [NotNull]
        IConcurrencySyncCanExecuteBuilder<T> CanExecute([NotNull] ICanExecuteSubject canExecute);

        [NotNull]
        IConcurrencySyncCanExecuteBuilder<T> ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute);

        [NotNull]
        IConcurrencySyncCanExecuteBuilder<T> ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute,
            bool fallback);

        [NotNull]
        IActivatableConcurrencySyncCommandBuilder<T> Activatable();

        [NotNull]
        IConcurrencySyncCommandBuilder<T> OnError([NotNull] Action<Exception> error);

        [NotNull]
        IConcurrencySyncCommandBuilder<T> OnCompleted([NotNull] Action completed);

        [NotNull]
        IConcurrencySyncCommandBuilder<T> OnCancel([NotNull] Action cancel);
    }
}