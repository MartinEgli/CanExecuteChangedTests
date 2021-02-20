﻿using System;
using System.Linq.Expressions;
using Anorisoft.WinUI.Commands.Commands;
using JetBrains.Annotations;

namespace Anorisoft.WinUI.Commands.Interfaces.Builders
{
    public interface IConcurrencySyncCanExecuteBuilder
    {
        [NotNull]
        ConcurrencyCanExecuteObserverCommand Build();

        [NotNull]
        ConcurrencyCanExecuteObserverCommand Build([NotNull] Action<ConcurrencyCanExecuteObserverCommand> setCommand);

        [NotNull]
        IConcurrencySyncCanExecuteBuilder ObservesProperty<TType>([NotNull] Expression<Func<TType>> expression);

        [NotNull]
        IConcurrencySyncCanExecuteBuilder ObservesCommandManager();
        
        [NotNull]
        IActivatableConcurrencySyncCanExecuteBuilder Activatable();
    }
}