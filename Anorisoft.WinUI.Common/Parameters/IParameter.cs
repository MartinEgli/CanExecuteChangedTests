// -----------------------------------------------------------------------
// <copyright file="IParameter.cs" company="Anori Soft">
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
        new T Value { get; set; }
    }

    /// <summary>
    /// </summary>
    /// <seealso cref="Anorisoft.WinUI.Common.Parameters.IReadOnlyParameter{T}" />
    /// <seealso cref="Anorisoft.WinUI.Common.Parameters.IParameter" />
    public interface IParameter : IReadOnlyParameter
    {
        new object Value { get; set; }
    }
}