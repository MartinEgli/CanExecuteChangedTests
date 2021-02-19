using System;

namespace Anorisoft.WinUI.Commands.Interfaces
{
    public interface ICanExecute
    {
        /// <summary>
        /// Gets the can execute.
        /// </summary>
        /// <value>
        /// The can execute.
        /// </value>
        Func<bool> CanExecute { get; }
    }
}