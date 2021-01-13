// -----------------------------------------------------------------------
// <copyright file="IReadOnlyParameter.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anorisoft.WinUI.Common.Parameters
{
    using System;

    using CanExecuteChangedTests;

    public interface IReadOnlyParameter<T> : IReadOnlyParameter
    {
        new T Value { get; }

        new event EventHandler<EventArgs<T>> ValueChanged;
    }

    public interface IReadOnlyParameter
    {
        object Value { get; }

        event EventHandler<EventArgs<object>> ValueChanged;
    }
}