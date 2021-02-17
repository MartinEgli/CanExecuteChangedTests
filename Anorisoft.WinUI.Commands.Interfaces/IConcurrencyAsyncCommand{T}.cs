using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Anorisoft.WinUI.Commands.Interfaces
{
    public interface IConcurrencyAsyncCommand<in T> : System.Windows.Input.ICommand
    {
        /// <summary>
        /// Executes the asynchronous.
        /// </summary>
        /// <returns></returns>
        [NotNull]
        Task ExecuteAsync([CanBeNull] T parameter, CancellationToken token);

        /// <summary>
        /// Determines whether this instance can execute.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance can execute; otherwise, <c>false</c>.
        /// </returns>
        bool CanExecute([CanBeNull] T parameter);
    }
}