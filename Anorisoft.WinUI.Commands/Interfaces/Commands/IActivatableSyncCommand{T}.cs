using Anorisoft.WinUI.Common;

namespace Anorisoft.WinUI.Commands.Interfaces.Commands
{
    public interface IActivatableSyncCommand<in T> : ISyncCommand<T>, IActivatable<IActivatableSyncCommand<T>>
    {
    }
}