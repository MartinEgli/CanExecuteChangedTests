using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anorisoft.WinUI.Commands.Exeptions
{
   [Serializable]
    public class NoCanExecuteException : Exception
    {
        public NoCanExecuteException(string message) : base(message) { }
        public NoCanExecuteException() : base() { }
    }
}
