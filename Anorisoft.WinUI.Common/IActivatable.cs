// -----------------------------------------------------------------------
// <copyright file="IActivatable.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anorisoft.WinUI.Common
{
    /// <summary>
    /// </summary>
    public interface IActivatable : IActivated

    {
        /// <summary>
        ///     Activates this instance.
        /// </summary>
        void Activate();

        /// <summary>
        ///     Deactivates this instance.
        /// </summary>
        void Deactivate();
    }
}