using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Anori.WinUI.Commands.Interfaces
{
    public interface IConcurrencyAsyncCommand : System.Windows.Input.ICommand
    {
        /// <summary>
        /// Executes the asynchronous.
        /// </summary>
        /// <returns></returns>
        [NotNull]
        Task ExecuteAsync(CancellationToken token);

        /// <summary>
        /// Determines whether this instance can execute.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance can execute; otherwise, <c>false</c>.
        /// </returns>
        bool CanExecute();
    }
}