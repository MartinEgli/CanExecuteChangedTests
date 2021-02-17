using Anorisoft.WinUI.Common;

namespace Anorisoft.WinUI.Commands.Interfaces
{
    public interface IActivatableSyncCommand<in T> : ISyncCommand<T>, IActivatable
    {
    }
}