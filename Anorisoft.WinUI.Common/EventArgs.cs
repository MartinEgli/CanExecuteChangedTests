﻿// -----------------------------------------------------------------------
// <copyright file="EventArgs.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace CanExecuteChangedTests
{
    public class EventArgs<T> : EventArgs
    {
        private T Value { get; }

        public EventArgs(T value)
        {
            this.Value = value;
        }
    }
}