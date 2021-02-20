using System;

namespace Anorisoft.WinUI.Commands.Exceptions
{
    public class CommandBuilderException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandBuilderException"/> class.
        /// </summary>
        /// <param name="messgae">The messgae.</param>
        public CommandBuilderException(string messgae) : base(messgae){}
    }
}