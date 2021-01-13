// -----------------------------------------------------------------------
// <copyright file="IDispatchable.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anorisoft.WinUI.Common
{
    using System.Windows.Threading;

    public interface IDispatchable
    {
        Dispatcher Dispatcher { get; }
    }
}