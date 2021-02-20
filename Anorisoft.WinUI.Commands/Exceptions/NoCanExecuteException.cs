using System;

namespace Anorisoft.WinUI.Commands.Exceptions
{
   [Serializable]
    public class NoCanExecuteException : Exception
    {
        public NoCanExecuteException(string message) : base(message) { }
        public NoCanExecuteException() : base() { }
    }
}
