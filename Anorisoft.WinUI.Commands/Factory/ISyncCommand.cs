using Anorisoft.WinUI.Commands.Interfaces;
using Anorisoft.WinUI.Common;

namespace Anorisoft.WinUI.Commands.Factory
{
    public interface ISyncCommand<in T> : ICommand<T>, IActivatable
    {
    }

    public interface ISyncCommand : ICommand, IActivatable
    {
    }
}