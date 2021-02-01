﻿// -----------------------------------------------------------------------
// <copyright file="IParameter.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anorisoft.WinUI.Common.Parameters
{
    /// <summary>
    /// </summary>
    /// <seealso cref="Anorisoft.WinUI.Common.Parameters.IReadOnlyParameter{T}" />
    /// <seealso cref="Anorisoft.WinUI.Common.Parameters.IParameter" />
    public interface IParameter : IReadOnlyParameter
    {
        /// <summary>
        ///     Gets or sets the value.
        /// </summary>
        /// <value>
        ///     The value.
        /// </value>
        new object Value { get; set; }
    }
}