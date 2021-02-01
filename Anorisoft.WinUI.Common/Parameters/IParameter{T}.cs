﻿// -----------------------------------------------------------------------
// <copyright file="IParameter{T}.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anorisoft.WinUI.Common.Parameters
{
    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="Anorisoft.WinUI.Common.Parameters.IReadOnlyParameter{T}" />
    /// <seealso cref="Anorisoft.WinUI.Common.Parameters.IParameter" />
    public interface IParameter<T> : IReadOnlyParameter<T>, IParameter
    {
        /// <summary>
        ///     Gets or sets the value.
        /// </summary>
        /// <value>
        ///     The value.
        /// </value>
        new T Value { get; set; }
    }
}