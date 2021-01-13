// -----------------------------------------------------------------------
// <copyright file="IDispatchableContext.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anorisoft.WinUI.Common
{
    using System.Threading;

    public interface IDispatchableContext
    {
        SynchronizationContext SynchronizationContext { get; }
    }
}