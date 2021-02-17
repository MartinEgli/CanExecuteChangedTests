using Anorisoft.WinUI.Common;

namespace Anorisoft.WinUI.Commands.Interfaces
{
    public interface IActivatableConcurrencyAsyncCommand<in T> : IConcurrencyAsyncCommand<T>, IActivatable
    {
    }
}