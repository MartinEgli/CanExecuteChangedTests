using Anorisoft.WinUI.Common;

namespace Anorisoft.WinUI.Commands.Interfaces.Commands
{
    public interface IActivatableAsyncCommand<in T> : IAsyncCommand<T>, IActivatable<IActivatableAsyncCommand<T>>
    {
    }
}