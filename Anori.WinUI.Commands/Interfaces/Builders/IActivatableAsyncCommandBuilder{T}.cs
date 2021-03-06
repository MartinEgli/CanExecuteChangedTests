﻿using System;
using System.Linq.Expressions;
using Anori.WinUI.Commands.Commands;
using JetBrains.Annotations;

namespace Anori.WinUI.Commands.Interfaces.Builders
{
    public interface IActivatableAsyncCommandBuilder<T>
    {
        [NotNull]
        ActivatableAsyncCanExecuteObserverCommand<T> Build();

        [NotNull]
        ActivatableAsyncCanExecuteObserverCommand<T> Build([NotNull] Action<ActivatableAsyncCanExecuteObserverCommand<T>> setCommand);

        [NotNull]
        IActivatableAsyncCanExecuteBuilder<T> CanExecute([NotNull] Predicate<T> canExecute);

        [NotNull]
        IActivatableAsyncCanExecuteBuilder<T> CanExecute([NotNull] ICanExecuteSubject canExecute);
        
        [NotNull]
        IActivatableAsyncCanExecuteBuilder<T> ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute);

        [NotNull]
        IActivatableAsyncCanExecuteBuilder<T> ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute, bool fallback);
        
        [NotNull]
        IActivatableAsyncCanExecuteBuilder<T> AutoActivate();

    }
}