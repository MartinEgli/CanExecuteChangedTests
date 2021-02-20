using Anorisoft.WinUI.Common;

namespace Anorisoft.WinUI.Commands.Interfaces.Commands
{
    public interface IActivatableConcurrencyAsyncCommand<in T> : IConcurrencyAsyncCommand<T>, IActivatable
    {
    }
}