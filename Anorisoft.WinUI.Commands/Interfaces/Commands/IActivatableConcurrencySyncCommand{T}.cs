using Anorisoft.WinUI.Common;

namespace Anorisoft.WinUI.Commands.Interfaces.Commands
{
    public interface IActivatableConcurrencySyncCommand<in T> : IConcurrencySyncCommand<T>, IActivatable<IActivatableConcurrencySyncCommand<T>>
    {
    }
}