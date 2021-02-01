// -----------------------------------------------------------------------
// <copyright file="IParameterObserverNode.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anorisoft.WinUI.Common.Parameters
{
    internal interface IParameterObserverNode
    {
        public void UnsubscribeListener();

        public void SubscribeListenerFor(IReadOnlyParameter parameter);
    }
}