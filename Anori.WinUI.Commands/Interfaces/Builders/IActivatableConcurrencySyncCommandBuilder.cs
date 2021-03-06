﻿using System;
using System.Linq.Expressions;
using Anori.WinUI.Commands.Commands;
using JetBrains.Annotations;

namespace Anori.WinUI.Commands.Interfaces.Builders
{
    public interface IActivatableConcurrencySyncCommandBuilder
    {
        [NotNull]
        ActivatableConcurrencyCanExecuteObserverCommand Build();

        [NotNull]
        ActivatableConcurrencyCanExecuteObserverCommand Build(
            [NotNull] Action<ActivatableConcurrencyCanExecuteObserverCommand> setCommand);

        [NotNull]
        IActivatableConcurrencySyncCanExecuteBuilder CanExecute([NotNull] Func<bool> canExecute);

        [NotNull]
        IActivatableConcurrencySyncCanExecuteBuilder CanExecute([NotNull] ICanExecuteSubject canExecute);

        [NotNull]
        IActivatableConcurrencySyncCanExecuteBuilder ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute);

        [NotNull]
        IActivatableConcurrencySyncCanExecuteBuilder ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute,
            bool fallback);

        [NotNull]
        IActivatableConcurrencySyncCanExecuteBuilder AutoActivate();

        [NotNull]
        IActivatableConcurrencySyncCommandBuilder OnError([NotNull] Action<Exception> error);

        [NotNull]
        IActivatableConcurrencySyncCommandBuilder OnCompleted([NotNull] Action completed);

        [NotNull]
        IActivatableConcurrencySyncCommandBuilder OnCancel([NotNull] Action cancel);
    }
}