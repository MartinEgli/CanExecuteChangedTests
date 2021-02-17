using Anorisoft.WinUI.Common;

namespace Anorisoft.WinUI.Commands.Interfaces
{
    public interface IActivatableAsyncCommand<in T> : IAsyncCommand<T>, IActivatable
    {
    }
}