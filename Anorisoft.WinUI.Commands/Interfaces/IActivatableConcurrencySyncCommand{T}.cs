using Anorisoft.WinUI.Common;

namespace Anorisoft.WinUI.Commands.Interfaces
{
    public interface IActivatableConcurrencySyncCommand<in T> : IConcurrencySyncCommand<T>, IActivatable<IActivatableConcurrencySyncCommand<T>>
    {
    }
}